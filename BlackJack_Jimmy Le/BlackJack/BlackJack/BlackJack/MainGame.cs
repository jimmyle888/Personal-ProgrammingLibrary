//Author: Jimmy Le
//File Name: MainGame.cs
//Project Name: PASS 3
//Creation Date: Dec 2. 2016
//Modified Date: Dec 13. 2016
//Description: A card game called blackjack, where the users cards much reach a sum as close to 21 as possible without going over

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BlackJack
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Maintain all the possible game states
        const int PREGAME = 0;
        const int BETTING = 1;
        const int DEAL_CARDS = 2;
        const int PLAYER_TURN = 3;
        const int DEALER_TURN = 4;
        const int POST_GAME = 5;
        const int END_GAME = 6;

        //Store both the current and previous mouse states for user input
        MouseState mouse;
        MouseState prevMouse;

        //Store the spritefont for writing text
        SpriteFont font;

        //Store the timer for the DEAL_CARDS gamestate
        int dealTimer = 0;

        //Store the timer for the delay in the dealer turn
        int dealerTurnTimer = 0;

        //Store the location of the title
        Vector2 titleLoc = new Vector2(50, 50);
        
        //Store the header for the betting gamestate
        Vector2 betHeaderLoc = new Vector2(50, 100);

        //Store where the bet amount will be shown
        Vector2 betAmountLoc = new Vector2(450, 175);
        Vector2 betAmountLoc2 = new Vector2(260, 450);

        //Store where the wallet amount will be shown
        Vector2 walletAmountLoc = new Vector2(10, 450);

        //Store where the alert is for when the player goes over the wallet amount
        Vector2 overWalletAmountLoc = new Vector2(450, 200);

        //Store the location for the text for the buttons
        Vector2 betBtnTxtLoc = new Vector2(170, 250);  //Blue bet button on the main menu
        Vector2 quitBtnTxtLoc = new Vector2(550, 250); //Red quit button on the main menu
        
        Vector2 betBtnTxtLoc2 = new Vector2(525, 325); //Blue bet button in the betting screen

        Vector2 hitBtnTxtLoc = new Vector2(460, 420);  //Hit button in the main game
        Vector2 standBtnTxtLoc = new Vector2(580, 420); //Stand button in the main game
        Vector2 doubleDownBtnTxtLoc = new Vector2(700, 400); //Double down button in the main game

        //Store where the end game text will be
        Vector2 quitGameTextLoc = new Vector2(50, 200);

        //Store where the sum of the player's cards will be
        Vector2 playerHandTotalLoc = new Vector2(75, 250);

        //Store where the sum of the dealer's cards will be
        Vector2 dealerHandTotalLoc = new Vector2(75, 50);

        //Store where the result screen will be
        Vector2 gameResultLoc = new Vector2(400, 100);

        //Store where the reset game prompt will be
        Vector2 resetgamePromptLoc = new Vector2(400, 300);

        //The random object allowing for random number generation during Shuffle
        Random rng = new Random();

        //Stores the background image
        Texture2D bgImg;
        Rectangle bgBounds;

        //Stores the blue bet button
        Texture2D blueBtnImg;
        Rectangle blueBtnRec;  //For the main menu
        Rectangle blueBtnRec2; //For the bet screen

        //Stores the red quit button
        Texture2D redBtnImg;
        Rectangle redBtnRec;

        //Store the grey rectangle for various buttons in the main game
        Texture2D greyBtnImg;
        Rectangle greyBtnRec;   //HIT button
        Rectangle greyBtnRec2;  //STAND button
        Rectangle greyBtnRec3;  //DOUBLE DOWN button

        //Store all the card related images
        Texture2D deckImg;
        Texture2D faceDownImg;

        //Store the numpad image and rectangle
        Texture2D numpadImg;
        Rectangle numpadRec;

        //Store where each card in the deck is to be located initially
        Rectangle deckLoc;

        //Stores if the cards have been dealt
        bool areCardsDealt = false;

        //Stores if the player has hit
        bool playerHasHit = false;

        //Store the amount of money in the player's wallet
        int wallet = 200;

        //Store the bet inputted by the player
        string bet = "";

        //Store the quit game timer
        int quitGameTimer = 0;

        //Store all possible collecions of Cards in the game
        Card[] deck = new Card[52];
        Card[] pHand = new Card[12];
        Card[] dHand = new Card[12];

        //Store a number inidcating where the current top of the deck is, increases as cards are given out
        int topOfDeck = 0;

        //Store the number of cards in each player's hands
        int numPCards = 0;
        int numDCards = 0;

        //Store the dimensions of a card for alignment purposes
        int cardWidth = 0;
        int cardHeight = 0;

        //Store the current state of the game, initailly in PREGAME
        int gameState = PREGAME;

        //Store the dimensions of the screen, modify these if game resolution is changed
        float screenWidth = 800;
        float screenHeight = 480;

        //Store the sum of the player and dealer's hands
        int playerTotal = 0;
        int dealerTotal = 0;

        //Store if the player or dealer has busted (sum of cards in hand is over 21)
        bool playerBust = false;
        bool dealerBust = false;

        //Store endgame results of the game
        bool bothHaveBlackJack = false;  //Both have blackjack
        bool dealerBlackJack = false;    //Dealer has blackjack, and the player has <= 21
        bool playerBlackJack = false;    //Player has blackjack, and the dealer has <= 21 
        bool dealerTotalWin = false;     //Dealer's total is more than the player total
        bool playerTotalWin = false;     //Player total is larger than the dealer
        bool equalTotal = false;         //Both have the same total

        //Store if the game has decided the result of the game
        bool resultsDecided = false;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            //Make the mouse visible
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Load the font
            font = Content.Load<SpriteFont>("fonts/outputFont");
            
            //Load the background image
            bgImg = Content.Load<Texture2D>("images/backgrounds/BlackjackBackground");

            //Set the background image dimensions to fit the entire screen
            bgBounds = new Rectangle(0, 0, (int)screenWidth, (int)screenHeight);

            //Load the blue bet button image
            blueBtnImg = Content.Load<Texture2D>("images/sprites/blueButton");

            //Set the bounding rec for the bet button
            blueBtnRec = new Rectangle(20, 150, 300, 200);   //Main menu blue button
            blueBtnRec2 = new Rectangle(450, 250, 150, 150); //Betting screen blue button

            //Load the red quit button image
            redBtnImg = Content.Load<Texture2D>("images/sprites/redButton2");

            //Set the bounding rec for red quit button
            redBtnRec = new Rectangle(400, 150, 300, 200);

            //Load all card images
            deckImg = Content.Load<Texture2D>("images/sprites/CardFaces");
            faceDownImg = Content.Load<Texture2D>("images/sprites/CardBack");

            //Load the numpad image
            numpadImg = Content.Load<Texture2D>("images/sprites/numpad");

            //Set the bounding rec for the numpad
            numpadRec = new Rectangle(100, 150, numpadImg.Width, numpadImg.Height);

            //Load the grey rectangle image
            greyBtnImg = Content.Load<Texture2D>("images/sprites/greyRectangle");

            //Set the boudning rec for the grey rectangle buttons
            greyBtnRec = new Rectangle(450, 370, 100, 100);      //HIT button
            greyBtnRec2 = new Rectangle(570, 370, 100, 100);     //STAND button
            greyBtnRec3 = new Rectangle(690, 370, 100, 100);     //DOUBLE DOWN button

            //Calculate and store the dimensions of a card
            cardWidth = deckImg.Width / Card.CARDS_IN_SUIT;
            cardHeight = deckImg.Height / Card.NUM_SUITS;

            //NOTE: You can move this if you would like
            deckLoc = new Rectangle((int)screenWidth - 150, 60, cardWidth, cardHeight);

            //Create the initial deck
            CreateDeck();

            //Shuffle the deck before going into PREGAME, this will need to be done each time
            //PREGAME is entered
            ShuffleDeck(1000);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || quitGameTimer > 300)
            {
                this.Exit();
            }

            // TODO: Add your update logic here          
            //Store the input peripherals
            prevMouse = mouse;
            mouse = Mouse.GetState();

            //Store the player and dealer totals
            playerTotal = GetHandTotal(pHand, numPCards);
            dealerTotal = GetHandTotal(dHand, numDCards);

            //Update the current gameState logic only
            switch (gameState)
            {
                case PREGAME:                   
                    
                    //If the red quit button is pressed, quit the game
                    if (InBox(redBtnRec) == true && NewMouseClick() == true)
                    {
                        gameState = END_GAME;
                    }

                    //If the blue bet button is pressed, enter the BETTING gamestate
                    if (InBox(blueBtnRec) == true && NewMouseClick() == true && wallet > 0)
                    {
                        gameState = BETTING;
                    }
                    break;
                case BETTING:                

                	//Retrieve numpad inputs
                	NumPadInput();

                	//If the player presses the blue confirm bet button, enter the DEAL_CARDS gamestate
                	if (InBox(blueBtnRec2) && CompareBet(bet) != true && bet != "" && ZeroBet(bet) != true && NewMouseClick() == true)
                	{
                		gameState = DEAL_CARDS;
                	}
                    break;
                case DEAL_CARDS:
                    
                    //Deal the cards to the player
                	DealCards();

             		//After all the cards have been dealt (which takes about 240 frames) change gamestate to player turn
             		if (dealTimer > 250)
             		{
             			gameState = PLAYER_TURN;
             		}                
                    break;
                case PLAYER_TURN:
					
					//If the initial cards that are dealt have a sum of 21, immediatly change the gamestate to the dealer turn
                    if (playerTotal == 21 && numPCards == 2)
					{
						gameState = DEALER_TURN;
					}

					//Check if the player clicks the hit button, and then HIT the player if they did
					if (InBox(greyBtnRec) == true && NewMouseClick() == true)
					{
						//Set player has hit to true so they can't double down
                        playerHasHit = true;
                        
                        //Hit the player
						Hit(pHand, numPCards);				
					}
					else if(InBox(greyBtnRec2) == true && NewMouseClick() == true)
					{
						//If the player stands, automatically change gamestate to DEALER's turn
						gameState = DEALER_TURN;
					}
					else if(InBox(greyBtnRec3) == true && NewMouseClick() == true && IsDoubleDownPossible(bet) == true)
					{
						//If the player clicks and has enough to double down, then double down (double bet, then hit, then change gamestate)                     
                        //Hit the player
                        Hit(pHand, numPCards);

                        //Double the player's bet
                        DoubleDown();

                        //Immediatly recalculate the player total
                        playerTotal = GetHandTotal(pHand, numPCards);

                        //If the player total goes over 21, then they have busted
                        if(playerTotal > 21)
                        {
                        	//Set playerBust to true to indicate they have busted
                            playerBust = true;
                        	
                            //Change the gamestate to Post game
                            gameState = POST_GAME;
                        }
                        else
                        {
                        	//If the player doesn't bust, go to the dealer gamestate
                            gameState = DEALER_TURN;
						}
					}

					//Check if the player goes over 21 and has busted. If they do, go straight to the Post game
                    if (playerTotal > 21)
                    {
                        playerBust = true;
                        gameState = POST_GAME;
                    }
                    break;
                case DEALER_TURN:
                    
                	//Change the dealer's face down card to face up
                	dHand[0].isFaceUp = true;

                	//Increment the dealer turn timer 
                	dealerTurnTimer += 1;

                	//If the dealer has less than a sum of 17 in his hand, and 80 frames (a bit more than 1 second) have passed it will opt to hit
                	if (dealerTotal < 17 && dealerTurnTimer > 80)
                	{
                		//Hit the dealer
                		Hit(dHand, numDCards);

                        //Recheck the dealer hand total
                        dealerTotal = GetHandTotal(dHand, numDCards);            	
                	}
                    else if(dealerTotal >= 17)
                    {
                        //If the dealer has 17 or above, stand and go to post game to compare results
                        gameState = POST_GAME;
                    }

                    if (dealerTotal > 21)
                	{
                		//If the dealer gets over 21, then he has busted and set gamestate to post game to determine results
                        dealerBust = true;
                        gameState = POST_GAME;
                	}
                    break;
                case POST_GAME:

                	//Check the results of the game if the results have not been decided yet
                    if (resultsDecided != true)
                	{
                		//Determine the results
                        DetermineResults();

                		//Since the results have been decided already, set to true so it doesn't recalculate
                        resultsDecided = true;
                	}

                	//If the playerclicks in the results screen, reset the game and go back to pre game
                    if (NewMouseClick() == true)
                	{
                		ResetGame();
                	}	
                    break;
                case END_GAME:
                	
                	//Start the quit game timer (if the quit timer reaches over 300, quit the game)
                	quitGameTimer++;
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Draw the background
            spriteBatch.Draw(bgImg, bgBounds, Color.White);

            //Draw the current gameState graphics only
            switch (gameState)
            {
                case PREGAME:

                    //Draw the title
                    spriteBatch.DrawString(font, "Trevor Payne's Lanes of Blackjack", titleLoc, Color.Blue);
                    
                    //Draw the blue bet button
                    spriteBatch.Draw(blueBtnImg, blueBtnRec, Color.White);  
          
                    //Draw the red quit button
                    spriteBatch.Draw(redBtnImg, redBtnRec, Color.White);

                    //Draw the wallet amount
                    spriteBatch.DrawString(font, "Wallet Amount: $" + wallet, walletAmountLoc, Color.Blue);

                    //Draw the text for the blue bet button
                    spriteBatch.DrawString(font, "BET", betBtnTxtLoc, Color.White);

                    //Draw the text for the red quit button
                    spriteBatch.DrawString(font, "QUIT", quitBtnTxtLoc, Color.White);
                    break;
                case BETTING:
                    
                    //Draw the header
                    spriteBatch.DrawString(font, "Type in your bets using the number keys", betHeaderLoc, Color.Blue);

                    //Draw the numpad
                    spriteBatch.Draw(numpadImg, numpadRec, Color.White);

                    //Draw the bet amount
                    spriteBatch.DrawString(font, "Bet Amount: $" + bet, betAmountLoc, Color.Blue);

                    //Alert the player they have gone over the wallet amount
                    OverWalletAmount(bet);

                    //Tell the player the amount of money they have in their wallet
                    spriteBatch.DrawString(font, "Wallet Amount: $" + wallet, walletAmountLoc, Color.Blue);

                    //Draw the BET button in the betting screen
                    spriteBatch.Draw(blueBtnImg, blueBtnRec2, Color.White);

                    //Draw the text for the blue bet button
                    spriteBatch.DrawString(font, "BET", betBtnTxtLoc2, Color.White);
                    break;
                case DEAL_CARDS:
                    
                    //Draw the user interface elements
                    DrawUIElements();

					//Draw the cards
          			DrawCards();
                	
                	//Draw the deck
                	DrawDeck();
                    break;
                case PLAYER_TURN:
                    
                	//Draw the UI elements
                	DrawUIElements();

                	//Draw the cards
                	DrawCards();

                	//Draw the deck
                	DrawDeck();

                	//Draw the grey buttons
                	spriteBatch.Draw(greyBtnImg, greyBtnRec, Color.White);  //HIT button
                	spriteBatch.Draw(greyBtnImg, greyBtnRec2, Color.White); //STAND button
                	   
                    //If double down is possible, then display the double down button and the text over it
                    if (IsDoubleDownPossible(bet) == true)
                    {
                       spriteBatch.Draw(greyBtnImg, greyBtnRec3, Color.White);
                       spriteBatch.DrawString(font, "DOUBLE \n DOWN", doubleDownBtnTxtLoc, Color.Black);
                    }

                	//Draw the text for the grey buttons
                	spriteBatch.DrawString(font, "HIT", hitBtnTxtLoc, Color.Black);
                	spriteBatch.DrawString(font, "STAND", standBtnTxtLoc, Color.Black);
                    break;
                case DEALER_TURN:
                    
                    //Draw the UI elements
                	DrawUIElements();

                	//Draw the cards
                	DrawCards();

                	//Draw the deck
                	DrawDeck();
                    break;
                case POST_GAME:
                    //Draw the UI elements
                    DrawUIElements();

                    //Draw the cards
                    DrawCards();

                    //Output to the user the game results
                    DrawResults();

                    //Prompt the user to click anywhere in the POST GAME screen to go back to the main menu
                    spriteBatch.DrawString(font, "Click Anywhere to go \nback to the main menu", resetgamePromptLoc, Color.White);
                    break;
                case END_GAME:
                    
                	//Output to the user the end game text and how much money they have in their wallet
                	spriteBatch.DrawString(font, "Thanks for playing!! You are leaving the game with $" + wallet, quitGameTextLoc, Color.Blue);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Creates each of the 52 cards in a standard deck of cards and adds them to the deck array
        /// </summary>
        private void CreateDeck()
        {
            //count tracks the number of cards created
            int count = 0;

            //For every suit, create each card from Ace to King
            for (int i = 0; i < Card.NUM_SUITS; i++)
            {
                for (int j = 0; j < Card.CARDS_IN_SUIT; j++)
                {
                    //Create and add the new card to the deck array
                    deck[count] = new Card(deckImg, faceDownImg, deckLoc, i, j);
                    count++;
                }
            }
        }

        //Pre:The numble of card shuffles
        //Post:Swaps locations in deck array according to the number of shuffles
        //Desc:Simulates card shuffling in a deck by switching the locations of the cards by the amount of shuffles
        private void ShuffleDeck(int numShuffles)
        {
            //TODO: loop numShuffles times and generate 2 random numbers from 0 and deck.Length.  Swap the elements
            //      in deck at those elements.
            //      This may also be a good place to reset any individual round data, e.g. bet, numDCards, numPCards, etc.

        	//Placeholder to store card for card switching
        	int placeHolder = 0;

            //Shuffle the deck by the amount of shuffles
            for (int i = 0; i < numShuffles; i++)
            {
         		//Store 2 random locations in the deck so they can be swapped
         		int randomDeckLoc = rng.Next(0, deck.Length - 1);
            	int randomDeckLoc2 = rng.Next(0, deck.Length - 1);
          	
            	//Swap values in the deck
            	deck[placeHolder] = deck[randomDeckLoc];
            	deck[randomDeckLoc] = deck[randomDeckLoc2];
            	deck[randomDeckLoc2] = deck[placeHolder];
            }

        }

        //Pre:The array of which the cards are being counted, the number of cards in hand
        //Post:The sum of all the cards in the hand
        //Desc:Calculates the sum of the cards in the player's / dealer's hand
        private int GetHandTotal(Card[] hand, int numCardsInHand)
        {
          
            //TODO: Calculate the total value of the Cards in the hand array
            //Store how many aces there are in the hand 
            int aceCount = 0;
            
            //Store the sum of the cards
           	int total = 0;
            
            //Store if there is 1 or more aces
            bool moreThanOnceAce = false;

            //Store the amount of non special cards (cards that are not considered aces or face cards)
            int nonSpecialCards = 0;

            //Run through the hand and check for aces
            for (int i = 0; i < numCardsInHand; i++)
            {
                if (hand[i].symbol == Card.ACE && hand[i].isFaceUp)
                {
                    //Increment the ace count by one if an ace is found
                    aceCount++;

                    //Set more than once ace to true if an ace is found
                    moreThanOnceAce = true;
                }

                //For the first ace in the deck, add 11 to the sum
                if (hand[i].symbol == Card.ACE && aceCount == 1 && hand[i].isFaceUp == true)
                {
                    total += 11;
                }
                //For the rest of the aces, add 1 to the sum
                else if (hand[i].symbol == Card.ACE && aceCount > 1 && hand[i].isFaceUp == true)
                {
                    total += 1;
                }
            }

            //Run through the hand and check for face cards (as all face cards are valued at 10)
            for (int n = 0; n < numCardsInHand; n++)
            {
                if (hand[n].symbol == Card.JACK || hand[n].symbol == Card.QUEEN || hand[n].symbol == Card.KING)
                {
                    //If a face card is found, and it is face up, increment the score by 10
                    if (hand[n].isFaceUp == true)
                    {
                        total += 10;
                    }
                }
            }

            //Run through the hand and find the sum of the non special cards (not aces or face cards)
            for (int g = 0; g < numCardsInHand; g++)
            {
                if (hand[g].isFaceUp == true && hand[g].symbol != Card.ACE && hand[g].symbol != Card.JACK && hand[g].symbol != Card.QUEEN && hand[g].symbol != Card.KING)
                {
                    //Add up the sum of the each card's symbol value (1 less than its actual value)
                    total += hand[g].symbol;

                    //Increment counter which tracks the amount of non special cards
                    nonSpecialCards += 1;
                }
            }

            //Add the total to the amount of non special cards (symbol values are 1 less than the 
            //cards actual value, so you must add 1 to for every non special card to make up for it
            total += nonSpecialCards;

            //If there is an ace in the deck, and its still over 21, subtract the total by 10 to make the first ace a value of 1
            if (moreThanOnceAce == true && total > 21)
            {
                total -= 10;
            }

            //Return the total sum of the cards
            return total;
        }

        //Pre:None
        //Post:Sets the location and values of the initial cards that are dealt
        //Desc: Deals the first 4 cards when the gameplay starts up
        private void DealCards()
        {
        	//Check if the gamestate is in deal cards and if they have been dealt already
            if (gameState == DEAL_CARDS && areCardsDealt == false)
            {
                //Set top of deck to zero
                topOfDeck = 0;

                //Assign the first card of the deck to the player
                pHand[0] = deck[topOfDeck];
                pHand[0].isFaceUp = true;   //Set the card so its face up
                pHand[0].dest = new Rectangle(75, 300, 91, 128);  //Change the location of the card 

                //Increment the top of the deck value as well as the amount of cards the player has
                topOfDeck++;
                numPCards++;

                //Assign the 2nd card of the deck to the dealer
                dHand[0] = deck[topOfDeck];
                dHand[0].isFaceUp = false;  //Set the card so its face down
                dHand[0].dest = new Rectangle(75, 100, 91, 128);  //Change the location of the card

                //Increment the top of the deck value as well as the amount of cards the dealer has
                topOfDeck++;
                numDCards++;

                //Assign the 3rd card of the deck to the player
                pHand[1] = deck[topOfDeck];
                pHand[1].isFaceUp = true;  //Set the card so its face up
                pHand[1].dest = new Rectangle(95, 300, 91, 128);  //Change the location of the card so that its a bit to the left over from the other player card

                //Again, increment top of the deck as well as the amount of cards the player has 
                topOfDeck++;
                numPCards++;

                //Assign the fourth card of the deck to the player
                dHand[1] = deck[topOfDeck];
                dHand[1].isFaceUp = true;  //Set the card so its face up
                dHand[1].dest = new Rectangle(95, 100, 91, 128);  //Change the location of the card so that its a bit to the left of the dealer's first card

                //Again, increment the top of deck as well as the amount of dealer cards
                topOfDeck++;
                numDCards++;

                //Set to true so that the subprogram doesn't iterate again
                areCardsDealt = true;

            }
        }

        //Pre:None
        //Post:Draws cards
        //Desc:When in the deal cards gamestate, draw the cards with a delay, and in the rest of the main game, draw all the cards in the 
        //player's hands that are not null
        private void DrawCards()
        {
        	//Increment deal timer
        	dealTimer++;
        	
        	//If 60 frames (1 sec) has passed, draw the first player card
        	if (dealTimer > 60)
        	{
        		pHand[0].Draw(spriteBatch);
        	}
            if (dealTimer > 120)
            {
                //If 120 frames (2 secs) has passed, draw the first dealer card
                dHand[0].Draw(spriteBatch);
            }
            if (dealTimer > 180)
            {
                //If 180 frames (3 secs) has passed, draw the 2nd player card
                pHand[1].Draw(spriteBatch);
            }
            if (dealTimer > 240)
            {
                //If 240 frames (4 secs) has passed, draw the 2nd dealer card
                dHand[1].Draw(spriteBatch);
            }

        	//If the game is in the dealer or player turn state, draw the cards in their hands
        	if (gameState == PLAYER_TURN || gameState == DEALER_TURN || gameState == POST_GAME)
        	{
        		//Run through the player hand array, and for all cards that are not null, draw them
        		for (int i = 0; i < numPCards; i++)
        		{
        			if (pHand[i] != null)
        			{
        				//Draw the cards in the player hand
        				pHand[i].Draw(spriteBatch);
        			}
        		}

        		//Run through the dealer hand array, and for all cards that are not null, draw
        		for (int g = 0; g < numDCards; g++)
        		{
        			if (dHand[g] != null)
        			{
        				//Draw the dealer hand cards
        				dHand[g].Draw(spriteBatch);
        			}
        		}
        	}
        }	

        //Pre:None
        //Post:Draws deck
        //Desc:Draws the face down image of the cards multiple times according to the amount of cards in deck
        private void DrawDeck()
        {
        	//Draw the cards according to the length of the deck
        	for (int i = 0; i < deck.Length - topOfDeck; i++)
        	{
        		//Draw the cards slightly shifted over according to where it is in the loop, to give the appearence of multiple cards in deck
        		spriteBatch.Draw(faceDownImg, deckLoc, Color.White);
        		deckLoc.X = 650 - i;
        	}
        }
        
        //Pre:None
        //Post:Sets result to true if a mouse click is detected
        //Desc:Detects of a new mouse click has occured and returns a bool depending on what you 
        private bool NewMouseClick()
        {
            //Store the result
            Boolean result = false;

            //Detects whether a new mouse button click occured, and if there is a new mouse click, set result to true
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
            {
                result = true;
            }

            //Return the result
            return result;
        }

        //Pre:The rectangle that its checking
        //Post:Detects if the mouse is in the box
        //Desc:Detects if the player is in the box, and returns a boolean result if so
        private bool InBox(Rectangle box)
        {
            //Store the result
            Boolean result = false;

            //TODO: Detect whether mouse.X and mouse.Y both are inside the box parameter
            //If user mouse is inside the box, set result to true
            if (mouse.X > box.X && mouse.X < box.X + box.Width && mouse.Y > box.Y && mouse.Y < box.Y + box.Width)
            {
                result = true;
            }

            //Return result
            return result;
        }

        //Pre:None
        //Post:Detects a mouse click on the numpad and changes the player bet
        //Desc:Detect a click on the numpad in the betting gamestate,  
        private void NumPadInput()
        {
        	//If a number key is clicked on, add in the number they clicked on to the string
            if (mouse.X > numpadRec.X && mouse.X < numpadRec.X + 106 && mouse.Y > numpadRec.Y && mouse.Y < numpadRec.Y + 54 && NewMouseClick() == true && CompareBet(bet) != true)
            {
                //Add 1 to the bet
                bet += "1";
            }
            else if (mouse.X > numpadRec.X + 106 && mouse.X < numpadRec.X + 212 && mouse.Y > numpadRec.Y && mouse.Y < numpadRec.Y + 54 && NewMouseClick() == true && CompareBet(bet) != true)
            {
            	//Add 2
                bet += "2";
            }
            else if(mouse.X > numpadRec.X + 212 && mouse.X < numpadRec.X + numpadRec.Width && mouse.Y > numpadRec.Y && mouse.Y < numpadRec.Y + 54 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 3
                bet += "3";
        	}
        	else if(mouse.X > numpadRec.X && mouse.X < numpadRec.X + 106 && mouse.Y > numpadRec.Y + 54 && mouse.Y < numpadRec.Y + 108 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 4
                bet += "4";
        	}
        	else if(mouse.X > numpadRec.X + 106 && mouse.X < numpadRec.X + 212 && mouse.Y > numpadRec.Y + 54 && mouse.Y < numpadRec.Y + 108 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 5
                bet += "5";
        	}
        	else if(mouse.X > numpadRec.X + 212 && mouse.X < numpadRec.X + numpadRec.Width && mouse.Y > numpadRec.Y + 54 && mouse.Y < numpadRec.Y + 108 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 6
                bet += "6";
        	}
        	else if(mouse.X > numpadRec.X && mouse.X < numpadRec.X + 106 && mouse.Y > numpadRec.Y + 108 && mouse.Y < numpadRec.Y + 162 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 7
                bet += "7";
        	}
        	else if (mouse.X > numpadRec.X + 106 && mouse.X < numpadRec.X + 212 && mouse.Y > numpadRec.Y + 108 && mouse.Y < numpadRec.Y + 162 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 8
                bet += "8";
        	}
        	else if(mouse.X > numpadRec.X + 212 && mouse.X < numpadRec.X + numpadRec.Width && mouse.Y > numpadRec.Y + 108 && mouse.Y < numpadRec.Y + 162 && NewMouseClick() == true && CompareBet(bet) != true)
        	{
        		//Add 9
                bet += "9";
        	}
        	else if (mouse.X > numpadRec.X + 106 && mouse.X < numpadRec.X + 212 && mouse.Y > numpadRec.Y + 162 && mouse.Y < numpadRec.Y + numpadRec.Height && NewMouseClick() == true && CompareBet(bet) != true && bet.Length != 0)
        	{
        		//Add 0 to the bet if it is not the first digit of the bet
                bet += "0";
        	}
        	else if (mouse.X > numpadRec.X + 212 && mouse.X < numpadRec.X + numpadRec.Width && mouse.Y > numpadRec.Y + 162 && mouse.Y < numpadRec.Y + numpadRec.Height && NewMouseClick() == true && bet != "")
        	{
        		//Take the last digit of the bet out if possible (if the bet string is not blank)
                bet = bet.Substring(0, bet.Length - 1);
        	}

        }

        //Pre:The bet amount
        //Post:Return true or false depending if the bet is larger or smaller than the wallet amount respectivly
        //Desc:Detects if the player's bet is larger than the wallet amount
        private bool CompareBet(string pBet)
        {
        	//Store the result
            bool result = false;

        	//Compare the bet to the wallet, and if its larger, set result to true
            if (pBet != "" && ConvertBet(pBet) > wallet)
        	{
        		result = true;
        	}

            //Return result
        	return result;
        }

        //Pre:The bet amount
        //Post:Text that pops up when the player is over the wallet amount
        //Desc: Alerts the user if they have gone over the wallet amount
        private void OverWalletAmount(string pBet)
        {
        	//If the bet is over the amount of wallet they have, output to the user that they are over the wallet amount
            if (CompareBet(pBet) == true)
        	{
        		spriteBatch.DrawString(font, "OVER WALLET AMOUNT!!", overWalletAmountLoc, Color.Red);
        	}
        }

        //Pre:The bet amount
        //Post:Detects if the user has inputted no bet 
        //Desc:Detects if the user has inputted no bet, or 0 as just their bet
        private bool ZeroBet(string pBet)
        {
        	//Store the result
            bool result = false;

        	//Check if no bet has been inputted, if so, set result to true
            if (pBet != "" && ConvertBet(pBet) == 0)
        	{
        		result = true;
        	}

        	//Return the result
            return result;
        }

        //Pre:None
        //Post:Outputs to the user the User interface elements
        //Desc:Draws the key User inteface elements such as the wallet amount and the bet amount
        private void DrawUIElements()
        {
  			//If the gamestate is not in post game, draw these user interface elements
            if (gameState != POST_GAME)
            {
                //Tell the player the amount of money they have in their wallet
                spriteBatch.DrawString(font, "Wallet Amount: $" + wallet, walletAmountLoc, Color.Blue);
            
                //Draw the amount of money the player has bet
                spriteBatch.DrawString(font, "Bet Amount: $" + bet, betAmountLoc2, Color.Blue);
            } 
            
            //If the gamestate is in the player turn, the dealer turn, or the post game, output to the user the player and dealer total
            if (gameState == PLAYER_TURN || gameState == DEALER_TURN || gameState == POST_GAME)
            {
            	//Draw the player hand total
            	spriteBatch.DrawString(font, "Your Total: " + playerTotal, playerHandTotalLoc, Color.White);
	
            	//Draw the dealer hand total
            	spriteBatch.DrawString(font, "Dealer Total: " + dealerTotal, dealerHandTotalLoc, Color.White);
            }
        }

        //Pre:The hand array, and the number of cards in the hand
        //Post:Assigns a card to the dealer/player depending on the gamestate and the parameters, and incrememnts the number of cards in hand and top of deck
        //Desc:Hits the player or dealer by adding a card to their hand array, and then inrementing their counters
        private void Hit(Card[] hand, int numCardsInHand)
        {
        	//Assigns the at the top of the deck to the next empty spot in the player hand
            hand[numCardsInHand] = deck[topOfDeck];

        	//If the gamestate is player turn, modify the card perameters so they correspond with player card location 
            if (gameState == PLAYER_TURN)
        	{
        		//Change the player card location
                hand[numCardsInHand].dest = new Rectangle (75 + (numCardsInHand * 20), 300, cardWidth, cardHeight);

        		//Have the card be set as flipped up
                hand[numCardsInHand].isFaceUp = true;

        		//Increment the number of player cards
                numPCards++;
        	}
        	else if (gameState == DEALER_TURN)
        	{
        		//If dealer turn is the gamestate, modify the the card bounding rec to correspond with the dealer card locations 
                hand[numCardsInHand].dest = new Rectangle (75 + (numCardsInHand * 20), 100, cardWidth, cardHeight);

        		//Set the card to be face up
                hand[numCardsInHand].isFaceUp = true;

        		//Increment the number of dealer cards
                numDCards++;

                //Reset the dealer turn timer to zero
                dealerTurnTimer = 0;
        	}

        	//Increment the top of deck
            topOfDeck++;
        }

        //Pre:The bet amount
        //Post:Returns whethere double down is possible
        //Desc:Checks to see if double of the bet is larger than the wallet, and if the player has hit already, and returns whethere the player can double down or not
        private bool IsDoubleDownPossible(string pBet)
        {
            //Store the result
            bool result = false;

            //Check to see if the bet doubled is smaller than the wallet, and if the player has hit already
            if ((ConvertBet(pBet) * 2) <= wallet && playerHasHit == false)
            {
                //Set to true of the conditions are met
                result = true;
            }

            //Return the result
            return result;
        }

        //Pre:None
        //Post:Doubles Bet
        //Desc:Doubles the player bet when they decide to double down
        private void DoubleDown()
        {
           //Converts bet to string after doubling the bet
           bet = Convert.ToString((ConvertBet(bet) * 2));
        }

        //Pre:The bet amount
        //Post:Returns the bet as an int
        //Desc:Converts the bet(a string) into an int, and returns it
        private int ConvertBet(string pBet)
        {
        	//Store the converted version of the bet
        	int convertedBet = 0;

        	//Convert the bet into a string
        	convertedBet = Convert.ToInt32(pBet);

        	//Return the converted bet (as an int)
        	return convertedBet;
        }

        //Pre:None
        //Post:Determines the result and sets an endgame condition to true, as well as modify the wallet amount based on the result
        //Desc:Determine the result of the game, and changes the wallet to match the result
        private void DetermineResults()
        {
            if (playerBust == true)
            {
                //If the player busts, the bet amount is substracted from the the wallet amount
                wallet = wallet - ConvertBet(bet);
            }
            else if (dealerBust == true)
            {
                //If the dealer busts, the player gets their bet added on to their total wallet amount
                wallet = wallet + ConvertBet(bet);
            }
            else if (playerTotal == 21 && dealerTotal == 21)
            {
                //If the player and the dealer both have blackjack, then result is a push (bets are returned) 
                bothHaveBlackJack = true;	//Set end game scenario bothHaveBlackJack to true
            }
            else if (dealerTotal == 21 && playerTotal <= 21 && numDCards == 2)
            {
                //If the dealer has blackjack, and the player has 21 or less sum as their hand, then dealer wins
                dealerBlackJack = true;	   //Set end game scenario dealerBlackJack to true
                wallet = wallet - ConvertBet(bet);	//Subtract bet from wallet
            }
            else if (playerTotal == 21 && dealerTotal <= 21 && numPCards == 2)
            {
                //If the player has blackjack, and the dealer has 21 or less sum in their hand, then player wins
                playerBlackJack = true;  //Set end game scenario playerBlackJack to true
                wallet = (int)(wallet + ConvertBet(bet) * 1.5f);  //The player wins 1.5 times of their bet
            }
            else if (dealerTotal > playerTotal)
            {
                //If the dealer's hand total is larger than the player's hand total, then dealer wins
                dealerTotalWin = true;  //Set end game scenario dealerTotalWin to true
                wallet = wallet - ConvertBet(bet);	//The player has their bet amount subtracted from their wallet
            }
            else if (playerTotal > dealerTotal)
            {
                //If the player's hand total is larger than that of the dealer, then the player wins
                playerTotalWin = true;  //Set end game scenario playerTotalWin to true
                wallet = wallet + ConvertBet(bet);  //The player's bet is added onto their total wallet amount
            }
            else if (dealerTotal == playerTotal)
            {
                //If the dealer and player have the hand sum, then result is a push. No money is lost nor gained
                equalTotal = true;  //The end game scenario equalTotal is set to true
            }
        }

        //Pre:None
        //Post:Draws the result to the player
        //Desc:Depending on the end game scenario determined, output the results to the player
        private void DrawResults()
        {
        	//Check end game scenarios and output based on their results
        	if (playerBust == true)
        	{
        		//Output if the player busts
        		spriteBatch.DrawString(font, "YOU LOSE BY BUST \nWallet: (-) $" + bet + "\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Red);
        	}
        	else if (dealerBust == true)
        	{
        		//The dealer busts
        		spriteBatch.DrawString(font, "YOU WIN BY DEALER BUST \nWallet: (+) $" + bet + "\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Blue);
        	}
        	else if(bothHaveBlackJack == true)
        	{
        		//Both have blackjack
        		spriteBatch.DrawString(font, "THE HANDS ARE A DRAW\nWallet:Bet has been Returned\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Black);
        	}
        	else if(dealerBlackJack == true)
        	{
        		//Dealer wins by blackjack
        		spriteBatch.DrawString(font, "YOU LOSE, DEALER HAS BLACKJACK \nWallet: (-) $" + bet + "\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Red);
        	}
        	else if(playerBlackJack == true)
        	{
        		//Player wins by blackjack
        		spriteBatch.DrawString(font, "YOU WIN BY BLACKJACK \nWallet: (+) $" + (int)(ConvertBet(bet) * 1.5f) + "\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Blue);
        	}
        	else if(dealerTotalWin == true)
        	{
        		//Dealer wins by having a larger sum in their hand than the player
        		spriteBatch.DrawString(font, "YOU LOSE BY POINTS \nWallet: (-) $" + bet + "\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Red);
        	}
        	else if(playerTotalWin == true)
        	{
        		//Player wins by having a larger sum in their hand than the dealer
        		spriteBatch.DrawString(font, "YOU WIN BY POINTS \nWallet: (+) $" + bet + "\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Blue);
        	}
        	else if (equalTotal == true)
        	{
        		//Both dealer and player's totals are the same
        		spriteBatch.DrawString(font, "THE HANDS ARE A DRAW \nWallet:Bet has been returned\nYour new Wallet Balence is $" + wallet, gameResultLoc, Color.Black);
        	}
        }

        //Pre:None
        //Post:Resets all game variables (except for wallet amount) and brings the gamestate to the pregame
        //Desc:Resets the game back to the starting pregame scren, and resets all game variables (other than wallet) and shuffles deck
        //so the player starts fresh should they decide to play again
        private void ResetGame()
        {

        	//Reset the timer for the DEAL_CARDS gamestate
        	dealTimer = 0;

        	//Reset the timer for the delay in the dealer turn
        	dealerTurnTimer = 0;
        	
        	//Reset if the cards have been dealt
 			areCardsDealt = false;
	
        	//Reset the bet inputted by the player
        	bet = "";
	
        	//Reset all possible collecions of Cards in the game
        	Card[] deck = new Card[52];
        	Card[] pHand = new Card[12];
        	Card[] dHand = new Card[12];
	
        	//Reset the number inidcating where the current top of the deck is, increases as cards are given out
        	topOfDeck = 0;
	
        	//Reset the number of cards in each player's hands
        	numPCards = 0;
        	numDCards = 0;

            //Reset if the player has hit
            playerHasHit = false;
	
        	//Reset the sum of the player and dealer's hands
        	playerTotal = 0;
        	dealerTotal = 0;
	
        	//Reset if the player or dealer has busted (sum of cards in hand is over 21)
        	playerBust = false;
        	dealerBust = false;
	
        	//Reset results of the game
        	bothHaveBlackJack = false;  //Both have blackjack
        	dealerBlackJack = false;    //Dealer has blackjack, and the player has <= 21
        	playerBlackJack = false;    //Player has blackjack, and the dealer has <= 21 
        	dealerTotalWin = false;     //Dealer's total is more than the player total
        	playerTotalWin = false;     //Player total is larger than the dealer
        	equalTotal = false;         //Both have the same total
	
        	//Reset if the game has decided the result of the game
        	resultsDecided = false;

        	//Reshuffle the deck
            ShuffleDeck(1000);

        	//Change the gamestate back the pregame
            gameState = PREGAME;
        }
    }
}
