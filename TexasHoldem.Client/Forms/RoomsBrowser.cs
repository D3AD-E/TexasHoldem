﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using TexasHoldem.Client.Utils;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Client.Forms
{
    public partial class RoomsBrowserForm : Form
    {
        private string _username;

        private double _money;

        private readonly Core.Network.Client _client;

        private System.Timers.Timer _timer;

        private List<Room> _rooms;

        private bool _awaitMessage;

        public RoomsBrowserForm(string username, double money)
        {
            InitializeComponent();
            _awaitMessage = false;
            _timer = new System.Timers.Timer();
            _timer.Interval = 30000;
            _timer.Enabled = true;
            _timer.Start();
            _timer.Elapsed += _timer_Elapsed;

            _rooms = new List<Room>();
            _username = username;
            _money = money;
            _client = Core.Network.Client.Instance;

            LoggedInNameLabel.Text += _username;
            AvailableMoneyLabel.Text += _money.ToString();

            Color backColor = Color.FromArgb(242, 242, 242);
            colorListViewHeader(ref RoomsListView, backColor, Color.Black);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_awaitMessage)
            {
                ExeptionHandler.HandleExeption(new Exception("Server did not respond"), true);
            }
            _awaitMessage = true;
            _client.RequestHeartbeat((senderClient, res) =>
            {
                _awaitMessage = false;
            });
        }

        private void JoinRoom(int index)
        {
            if (CheckMoneyAmount())
            {
                Guid roomId = _rooms[index].Id;

                _client.RequestJoinRoom(roomId, (senderClient, res) =>
                {
                    if (res.HasError)
                    {
                        ExeptionHandler.HandleExeption(res.Exception);
                    }
                    else
                    {
                        Thread mThread = new Thread(delegate ()
                        {
                            var gf = new GameForm(_username, _money, res);
                            gf.ShowDialog();
                        });

                        mThread.SetApartmentState(ApartmentState.STA);

                        mThread.Start();
                    }
                });
            }
        }

        private void RoomsListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = RoomsListView.Columns[e.ColumnIndex].Width;
        }

        public static void colorListViewHeader(ref ListView list, Color backColor, Color foreColor)
        {
            list.OwnerDraw = true;
            list.DrawColumnHeader +=
                new DrawListViewColumnHeaderEventHandler
                (
                    (sender, e) => headerDraw(sender, e, backColor, foreColor)
                );
            list.DrawItem += new DrawListViewItemEventHandler(bodyDraw);
        }

        private static void headerDraw(object sender, DrawListViewColumnHeaderEventArgs e, Color backColor, Color foreColor)
        {
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            using SolidBrush foreBrush = new SolidBrush(foreColor);
            e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds);
        }

        private static void bodyDraw(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }

        private void JoinRoomFButton_Click(object sender, EventArgs e)
        {
            if (RoomsListView.SelectedIndices.Count == 0)
                return;

            JoinRoom(RoomsListView.SelectedIndices[0]);
        }

        private void RoomsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (RoomsListView.SelectedIndices.Count == 0)
                return;

            JoinRoom(RoomsListView.SelectedIndices[0]);
        }

        private void RoomsBrowserForm_Load(object sender, EventArgs e)
        {
            GetRooms();
        }

        private void GetRooms()
        {
            _client.RequestShowRooms((senderClient, res) =>
            {
                if (res.HasError)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    _rooms.Clear();
                    InvokeUI(() =>
                    {
                        RoomsListView.Items.Clear();
                    });
                    foreach (var room in res.Rooms)
                    {
                        _rooms.Add(room);

                        string[] row = { room.Name, room.SBBet + " / " + room.BBBet, room.CurrentPlayersAmount + "/" + room.MaxPlayersAmount };
                        var listViewItem = new ListViewItem(row);
                        InvokeUI(() =>
                        {
                            RoomsListView.Items.Add(listViewItem);
                        });
                    }
                }
            });
        }

        private void CreateRoomFButton_Click(object sender, EventArgs e)
        {
            if (CheckMoneyAmount())
            {
                var userInputForm = new CreateRoomForm();
                var res = userInputForm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    string roomName = userInputForm.RoomName;
                    int maxPlayers = userInputForm.MaxPlayers;

                    _client.RequestCreateRoom(roomName, maxPlayers, (senderClient, res) =>
                    {
                        if (res.HasError)
                        {
                            ExeptionHandler.HandleExeption(res.Exception);
                        }
                        else
                        {
                            Thread mThread = new Thread(delegate ()
                            {
                                var gf = new GameForm(_username, _money, res);
                                gf.ShowDialog();
                            });

                            mThread.SetApartmentState(ApartmentState.STA);

                            mThread.Start();
                        }
                    });
                }
            }
        }

        private bool CheckMoneyAmount()
        {
            if (_money == 0)
            {
                var messageBox = new FlatMessageBox("Error", "Insufficient funds to join a room");
                messageBox.ShowDialog();
                return false;
            }
            return true;
        }

        private void AddMoneyFButton_Click(object sender, EventArgs e)
        {
            var addMoneyForm = new AddMoneyForm();
            var res = addMoneyForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                _client.RequestAddMoneyPhraseConfirm(addMoneyForm.TypedMessage, (senderClient, res) =>
                {
                    if (res.HasError)
                    {
                        ExeptionHandler.HandleExeption(res.Exception);
                    }
                    else
                    {
                        _money += 1000;
                        var messagebox = new FlatMessageBox("Success", "1000 coins have been successfully added to your account", Color.Green, Color.Black);
                        messagebox.ShowDialog();
                        InvokeUI(() => AvailableMoneyLabel.Text = $"Balance = {_money}");
                    }
                });
            }
        }

        private void RefreshFButton_Click(object sender, EventArgs e)
        {
            GetRooms();
        }

        private void RoomsBrowserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}