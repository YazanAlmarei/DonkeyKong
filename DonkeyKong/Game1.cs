using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DonkeyKong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D monkeyTex;
        Monkey monkey;

        Texture2D queenTex;
        Queen queen;

        Texture2D mainMenu;
        Texture2D winPic;
        Texture2D losePic;

        Player player;
        static Tile[,] tiles;

        SpriteFont textFont;
        int lives = 3;
        float timer = 1f;

        Texture2D enemyTex;
        Enemy enemy;
        List<Enemy> enemies = new List<Enemy>();

        MouseState mouseState;
        KeyboardState currentKeyboardState;
        KeyboardState oldKeyboardState;
        GameState gameState;

        Random random;

        public enum GameState { Menu = 0, Game = 1, PostGameWin = 2, PostGameLose = 3 }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1040;
            graphics.PreferredBackBufferHeight = 640;

        }

        public static bool GetTileAtPosition(Vector2 vec)
        {
            return tiles[(int)vec.X / 40, (int)vec.Y / 40].bridgee;
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
            Texture2D wallTex = Content.Load<Texture2D>("empty2");
            Texture2D wallTex2 = Content.Load<Texture2D>("bridge2");
            random = new Random();

            monkeyTex = Content.Load<Texture2D>("DonkeyKong");
            monkey = new Monkey(monkeyTex, new Vector2(400, 100));

            queenTex = Content.Load<Texture2D>("pauline");
            queen = new Queen(queenTex, new Vector2(500, 15));

            player = new Player(playerTex, new Vector2(1000, 600));

            textFont = Content.Load<SpriteFont>("File");
            mainMenu = Content.Load<Texture2D>("start");
            winPic = Content.Load<Texture2D>("win");
            losePic = Content.Load<Texture2D>("loose");

            enemyTex = Content.Load<Texture2D>("enemy2");

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
                        tiles[i, j] = new Tile(bridgeTileTex, new Vector2(bridgeTileTex.Width * i, bridgeTileTex.Height * j), true);
                    }

                    if (strings[j][i] == 'W')
                    {
                        tiles[i, j] = new Tile(wallTex, new Vector2(wallTex.Width * i, wallTex.Height * j), true);
                    }

                    if (strings[j][i] == 'w')
                    {
                        tiles[i, j] = new Tile(wallTex2, new Vector2(wallTex2.Width * i, wallTex2.Height * j), true);
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
                    }

                    else if (strings[j][i] == 'F')
                    {
                        tiles[i, j] = new Tile(emptyTex, new Vector2(emptyTex.Width * i, emptyTex.Height * j), false);
                        enemy = new Enemy(enemyTex, new Vector2(emptyTex.Width * i, emptyTex.Height * j), 1f);
                        enemies.Add(enemy);
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
                    monkey.Update();
                    queen.Update();


                    if (Player.playerRec.Intersects(monkey.monkeyRect))
                    {
                        gameState = GameState.PostGameLose;

                    }

                    if (Player.playerRec.Intersects(queen.queenRect))
                    {
                        gameState = GameState.PostGameWin;
                    }

                    Player.playerRec = new Rectangle((int)player.position.X, (int)player.position.Y, player.texture.Width, player.texture.Height);
                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Update(gameTime);
                        if (enemy.CollideEnemy(Player.playerRec) && timer < 0)
                        {
                            lives--;
                            timer = 1f;
                            
                        }
                    }

                    if (lives == 0)
                    {
                        gameState = GameState.PostGameLose;
                    }
                    break;

                case GameState.PostGameWin:
                    break;

                case GameState.PostGameLose:
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

                    foreach(Enemy enemy in enemies)
                    {
                        enemy.Draw(spriteBatch);
                    }

                    spriteBatch.DrawString(textFont, "Lives = " + lives, new Vector2(0, 25), Color.Red);
                    monkey.Draw(spriteBatch);
                    queen.Draw(spriteBatch);
                    player.Draw(spriteBatch);

                    break;

                case GameState.PostGameWin:

                    spriteBatch.Draw(winPic, new Vector2(50, 80), Color.White);
                    spriteBatch.DrawString(textFont, "You Won!", new Vector2(450, 100), Color.Green);
                    break;

                case GameState.PostGameLose:

                    spriteBatch.Draw(losePic, new Vector2(35, -20), Color.White);
                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}