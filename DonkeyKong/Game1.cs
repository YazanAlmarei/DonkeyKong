using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;

namespace DonkeyKong
{
    public class Game1 : Game
    {      
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D theMonkey;
        Texture2D queen;
        Texture2D mainMenu;
        Texture2D winPic;
        Texture2D losePic;

        Player player;
        static Tile[,] tiles;

        SpriteFont textFont;
        int lives = 3;

        MouseState mouseState;
        KeyboardState currentKeyboardState;
        KeyboardState oldKeyboardState;
        GameState gameState;

        Random random;

        public enum GameState { Menu = 0, Game = 1, PostGame = 2 }

        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 640;

        }

        public static bool GetTileAtPosition(Vector2 vec)
        {
            return tiles[(int)vec.X / 50, (int)vec.Y / 50].bridgee;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D bridgeTileTex = Content.Load<Texture2D>("bridge");
            Texture2D bridgeLadderTex = Content.Load<Texture2D>("bridgeLadder");
            Texture2D emptyTex = Content.Load<Texture2D>("empty");
            Texture2D ladderTex = Content.Load<Texture2D>("Ladder");
            Texture2D playerTex = Content.Load<Texture2D>("SuperMarioFront");

            theMonkey = Content.Load<Texture2D>("DonkeyKong");
            queen = Content.Load<Texture2D>("pauline");
            textFont = Content.Load<SpriteFont>("File");
            mainMenu = Content.Load<Texture2D>("start");
            winPic = Content.Load<Texture2D>("win");
            losePic = Content.Load<Texture2D>("loose");


            //var texture = Content.Load<Texture2D>("SuperMarioFront");
            
           

            List<string> strings = new List<string>();
            StreamReader sr = new StreamReader("mapTXT.txt");
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }

            sr.Close();

            tiles = new Tile[strings[0].Length, strings.Count];

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {

                    if (strings[j][i] == 'B')
                    {
                        tiles[i, j] = new Tile(bridgeTileTex, new Vector2(bridgeTileTex.Width * i, bridgeTileTex.Height * j), false);
                    }

                    else if (strings[j][i] == 'C')
                    {
                        tiles[i, j] = new Tile(bridgeLadderTex, new Vector2(bridgeLadderTex.Width * i, bridgeLadderTex.Height * j), false);

                    }

                    else if (strings[j][i] == 'E')
                    {
                        tiles[i, j] = new Tile(emptyTex, new Vector2(emptyTex.Width * i, emptyTex.Height * j), false);
                    }

                    else if (strings[j][i] == 'L')
                    {
                        tiles[i, j] = new Tile(ladderTex, new Vector2(ladderTex.Width * i, ladderTex.Height * j), false);

                        player = new Player(playerTex, new Vector2(40, 600));
                    } 
                    
                }
                

            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            currentKeyboardState = Keyboard.GetState();
            random = new Random();

            switch (gameState)
            {

                case GameState.Menu:

                    if (currentKeyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.Game;
                    }

                    break;

                case GameState.Game:
                    player.Update(gameTime);

                    //if mario touches pauline >> gameState = GameState.PostGame

                    if (lives == 0 /*|| mario touches kong*/)
                    {
                        gameState = GameState.PostGame;
                    }


                    break;

                case GameState.PostGame:
                    if (currentKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
                    {
                        gameState = GameState.Menu;
                    }
                    break;

            }

                    base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch (gameState)
            {

                case GameState.Menu:
                    spriteBatch.Draw(mainMenu, Vector2.Zero, Color.White);
                    spriteBatch.DrawString(textFont, "You must avoid Donkey Kong and save Pauline! Press Enter to play!", new Vector2(190, 430), Color.Red);

                    break;

                case GameState.Game:
                    for (int i = 0; i < tiles.GetLength(0); i++)
                    {
                        for (int j = 0; j < tiles.GetLength(1); j++)
                        {
                            tiles[i, j].Draw(spriteBatch);
                        }
                    }
                    
                    
                    spriteBatch.DrawString(textFont, "Lives = " + lives, Vector2.Zero, Color.Red);
                    spriteBatch.Draw(theMonkey, new Vector2(360, 100), Color.White);
                    spriteBatch.Draw(queen, new Vector2(450, 15), Color.White);
                    player.Draw(spriteBatch);

                    break;

                case GameState.PostGame:

                    spriteBatch.Draw(winPic, new Vector2(50, 80), Color.White);
                    spriteBatch.DrawString(textFont, "You Won! Press Space to start over", new Vector2(350, 100), Color.Green);
                    
                    if(lives == 0 /*|| mario touches kong*/)
                    {
                        spriteBatch.Draw(losePic, Vector2.Zero, Color.White);
                    }
                    
                    break;


            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}