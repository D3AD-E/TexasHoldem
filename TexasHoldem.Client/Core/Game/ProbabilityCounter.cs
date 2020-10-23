using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Client.Core.Game
{
    class ProbabilityCounter
    {
        private List<Player>  _players;

        private List<Card> _board;

        private Card[] _deck;

        private const int DECK_SIZE = 52;

        public ProbabilityCounter(List<Player> players, List<Card> Board)
        {
            _players = players;
            _board = Board;
            _deck = new Card[DECK_SIZE];
            var suits = Enum.GetValues(typeof(CardProperty.Suit)).Cast<CardProperty.Suit>();
            var cardValues = Enum.GetValues(typeof(CardProperty.Value)).Cast<CardProperty.Value>();
            int currCard = 0;
            foreach (var suit in suits)
                foreach (var cValue in cardValues)
                {
                    var card = new Card(suit, cValue);
                    _deck[currCard] = card;
                    if (Board.Contains(card) || DoPlayersHaveCard(card))
                        _deck[currCard].IsDrawn = true;
                    currCard++;
                }
        }

        private bool DoPlayersHaveCard(Card card)
        {
            foreach(var player in _players)
            {
                if (player.Hand[0] == card || player.Hand[1] == card)
                    return true;
            }
            return false;
        }

        public Dictionary<int,double> GetProbability(out double split)
        {
            var toRet = new Dictionary<int, double>();

            var sortedPlayers = _players.OrderByDescending(x => x.Holding).ToList();
            var bestPlayer = sortedPlayers[0];

            int bestPlayersAmount = 1;

            for(int i = 1; i<sortedPlayers.Count(); i++)
            {
                if (bestPlayer.Holding.CompareTo(sortedPlayers[i].Holding)==0)
                {
                    bestPlayersAmount++;
                }
                else
                    break;
            }

            //assume no split
            int splitHits = 0;
            double accumulativeChance = 0;
            var bestHolding = bestPlayer.Holding;
            for (int i = bestPlayersAmount; i < sortedPlayers.Count(); i++)
            {
                int winHits = 0;
                foreach (var card in _deck)
                {
                    if (card.IsDrawn)
                        continue;
                    var Cards = sortedPlayers[i].Hand.ToList();
                    Cards.AddRange(_board);

                    var holding = new Holding(Cards);

                    int compareresult = holding.CompareTo(bestHolding);
                    if (compareresult == 0)
                        splitHits++;
                    else if (compareresult < 0)
                        winHits++;
                }

                double chance = (double)winHits / (double)DECK_SIZE;
                accumulativeChance += chance;
                toRet.Add(sortedPlayers[i].Place, chance);
            }
            split = (double)splitHits / (double)DECK_SIZE;

            for(int i = 0; i< bestPlayersAmount; i++)
            {
                toRet.Add(sortedPlayers[i].Place, (1 - accumulativeChance - split)/(double)bestPlayersAmount);
            }
            

            return toRet;
        }
    }
}
