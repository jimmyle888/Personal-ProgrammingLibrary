using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackJack
{
    class Card
    {
        //Suits are associated with numeric values for later use in image setup
        public const int DIAMONDS = 0;
        public const int HEARTS = 1;
        public const int CLUBS = 2;
        public const int SPADES = 3;

        //Values are associated with numberic ordered values for later use in image setup
        public const int ACE = 0;
        public const int TWO = 1;
        public const int THREE = 2;
        public const int FOUR = 3;
        public const int FIVE = 4;
        public const int SIX = 5;
        public const int SEVEN = 6;
        public const int EIGHT = 7;
        public const int NINE = 8;
        public const int TEN = 9;
        public const int JACK = 10;
        public const int QUEEN = 11;
        public const int KING = 12;

        //Basic Card data including its visuals, values and status
        public Texture2D img;
        public Texture2D faceDownImg;
        public Rectangle dest;
        public int suit;
        public int symbol;
        public bool isFaceUp = false;

        //Maintain the number of suits and card per suit for later use in image setup
        public const int NUM_SUITS = 4;
        public const int CARDS_IN_SUIT = 13;

        //Calculated data to display the correct card from the entire card set image
        private Rectangle src;
        private int cardWidth;
        private int cardHeight;

        public Card(Texture2D img, Texture2D faceDownImg, Rectangle dest, int suit, int value)
        {
            //Required data for card imagery
            this.img = img;
            this.faceDownImg = faceDownImg;
            this.dest = dest;
            this.suit = suit;
            this.symbol = value;

            //Calculated data based on images
            this.cardWidth = img.Width / CARDS_IN_SUIT;
            this.cardHeight = img.Height / NUM_SUITS;
            this.src = new Rectangle(value * cardWidth, suit * cardHeight, cardWidth, cardHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isFaceUp == true)
            {
                spriteBatch.Draw(img, dest, src, Color.White);
            }
            else
            {
                spriteBatch.Draw(faceDownImg, dest, Color.White);
            }
        }
    }
}
