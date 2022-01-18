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
    
    public class Board
    {
        public static Vector2[,] Postitions ;
        public static Block[,] Block_pos;
        public int Width = 400;
        public static int block_size =  40;
        public int Height = block_size * 20;
        Texture2D White_Block = Main.Create_Rec(block_size, block_size, Color.White);
        Shape current_Shape = null;
        Shape next_Shape = null;

        Color[] _color = new Color[5];
        
        
        
        public Board()
        {
            Random rand = new Random();



            _color[0] = new Color(126, 170, 107);
            _color[1] = new Color(216, 170, 107);
            _color[2] = new Color(216, 140, 195);
            _color[3] = new Color(234, 140, 74);
            _color[4] = new Color(38, 140, 207);
            Postitions = new Vector2[21,11];
            Block_pos = new Block[22 , 11];
            int x = block_size;
            int y = Main.Height - Height - block_size;
            for(int i = 1; i <= 20; i++)
            {
                x = block_size;
                for(int j = 1; j <= 10; j++)
                {
                    Postitions[i, j] = new Vector2(x , y);
                    x += block_size;
                }
                y += block_size;
            }

            for(int i = 1; i<= 10; i++)
            {
                Block_pos[21,i] = new Block(0 , 0 , new Color(0 , 0 , 0));
            }
        }

        

        public virtual void Update(GameTime gameTime)
        {
            if(current_Shape != null && current_Shape.Destruct)
            {
                for(int i = 1; i <= 20; i++)
                {
                    bool yes = true;
                    for (int j = 1; j <= 10; j++)
                    {
                        if(Block_pos[i,j] == null)
                        {
                            yes = false;
                        }
                    }
                    if(yes == true)
                    {
                        Main.Points += 10;
                        for(int j =1; j<= 10; j++)
                        {
                            Block_pos[i, j] = null; 
                            for(int z = i-1; z >= 1; z--)
                            {
                                if(Block_pos[z, j] == null)
                                {
                                    continue;
                                }
                                Block_pos[z,j].Move(0 , 1 );
                                Block_pos[z + 1, j] = Block_pos[z, j];
                                Block_pos[z, j] = null;
                            }
                        }
                    }
                }
                current_Shape = null;
            }
            if(current_Shape == null && next_Shape == null)
            {

                Random rand = new Random();
                current_Shape = new Shape(rand.Next(1, 10), _color[rand.Next(0, 4)]);
                next_Shape = new Shape(rand.Next(1, 10), _color[rand.Next(0 , 4)]);
            }
            if (current_Shape == null)
            {
                current_Shape = next_Shape;
                Random rand = new Random();
                next_Shape = new Shape(rand.Next(1, 10), _color[rand.Next(0, 4)]);
            }



            if (current_Shape != null)
            {


                current_Shape.Update(gameTime);
            }
            
         }

        public virtual void Draw()
        {
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (Block_pos[i, j] != null)
                    {
                        Block_pos[i, j].Draw();
                    }
                }
            }
            Main._spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            int x = 0;

            
            for(int i = 1; i <= 12; i++)
            {
                Main._spriteBatch.Draw(White_Block , new Vector2(x , Main.Height - Height - block_size *2) , Color.White);
                Main._spriteBatch.Draw(White_Block, new Vector2(x, Main.Height - block_size ), Color.White);
                x += block_size;
            }
            int y = Main.Height - Height - block_size * 2;
            for(int i = 1; i <= 22; i++)
            {
                Main._spriteBatch.Draw(White_Block, new Vector2(0, y), Color.White);
                Main._spriteBatch.Draw(White_Block, new Vector2(Width + block_size, y), Color.White);
                y += block_size;
            }

        
            
            Main._spriteBatch.End();
            if (current_Shape != null)
            {
                current_Shape.Draw();
            }
            if(next_Shape != null)
            {
                next_Shape.Draw_Next();
            }
        }
        

    }
}
