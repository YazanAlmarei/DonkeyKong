using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace DonkeyKong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;



        Tile[,] tiles;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 663;

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
                    }
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].Draw(spriteBatch);
                }
            }


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}