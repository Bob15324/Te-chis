#region #include <bits/stdc++.h>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
#endregion


namespace Tetris.Code
{
   

   
    public class Main : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static SpriteFont Ubuntu32;
        public static int Width = 700;
        public static int Height = 880;
        public static int Points = 0;

        public static bool GameOver = false;

        public Board board;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public static Texture2D Create_Rec(int width, int height, Color color)
        {
            const int scar = 0;
            Color BLACK = Color.Black;
            Texture2D Rec = new Texture2D(Main._graphics.GraphicsDevice, width, height);

            Color[] Data = new Color[width * height];
            int pixel = 0;
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++ )
                {
                    if(i <= scar || i >= height - scar)
                    {
                        Data[pixel++] = BLACK;
                        continue;
                    }
                    if(j <= scar  || j >= width - scar )
                    {
                        Data[pixel++] = BLACK;
                        continue;
                    }

                    Data[pixel++] = color;
                }
            }
            Rec.SetData(Data);
            return Rec;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.ApplyChanges();

            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            board = new Board();
            Ubuntu32 = Content.Load<SpriteFont>("Ubuntu32");

            
        }

        protected override void Update(GameTime gameTime)
        {
            if(GameOver)
            {
                Exit();
            }
            board.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            board.Draw();
            _spriteBatch.Begin();
            _spriteBatch.DrawString(Ubuntu32 , "Points :" + Points , new Vector2(510 , 200) , Color.White);
            _spriteBatch.DrawString(Ubuntu32, "Next : ", new Vector2(520, 400), Color.Pink);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }



    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
}
