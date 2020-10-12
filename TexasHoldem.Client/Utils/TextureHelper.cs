using System.Drawing;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Client.Utils
{
    public static class TextureHelper
    {
        private const int CARD_SIZE_HEIGHT = 131;
        private const int CARD_SIZE_WIDTH = 85;
        private const int GAP_SIZE_HEIGHT = 11;
        private const int GAP_SIZE_WIDTH = 12;

        private const string FAIL_PATH = @"../../Pics/CardBack3.png";

        public static Image CardToImage(Card card, string path)
        {
            if(card == null)
            {
                return Image.FromFile(FAIL_PATH);
            }

            int offsetHeight = GAP_SIZE_HEIGHT;
            int offsetWidth = GAP_SIZE_WIDTH;

            if (card.Value == CardProperty.Value.Ace)
            {
                offsetWidth = 9;
                offsetHeight += (int)card.Suit * (GAP_SIZE_HEIGHT + CARD_SIZE_HEIGHT);
            }
            else
            {
                offsetHeight += (int)card.Suit * (GAP_SIZE_HEIGHT + CARD_SIZE_HEIGHT);
                offsetWidth += ((int)card.Value + 1) * (GAP_SIZE_WIDTH + CARD_SIZE_WIDTH) + ((int)card.Value + 1) / 2;
            }
            Image image;
            image = Image.FromFile(path);

            return CropImage(image, new Rectangle(offsetWidth, offsetHeight, CARD_SIZE_WIDTH, CARD_SIZE_HEIGHT));
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }
    }
}