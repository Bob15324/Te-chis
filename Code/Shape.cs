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
    public class Block
    {
        public Color color;
        public int x;
        public int y;
        Texture2D _texture;
        private int size = Board.block_size;

        

        public Block(int _x , int _y , Color _color)
        {
            
            x = _x;
            y = _y;
            color =  _color;
            _texture = Main.Create_Rec(size, size, _color);
        }

        public virtual void  Move(int val_x , int val_y)
        {
            x += val_x;
            y += val_y;
        }

        public virtual bool MoveDown()
        {
            if(y == 0 && Board.Block_pos[y+1 , x] != null)
            {
                Main.GameOver = true;
            }
            if( y <=0 || Board.Block_pos[y+1 , x] == null)
            {
                
                return true;
            }
            return false;
        }


        public virtual bool MoveLeft()
        {
            if(x -1 > 0 && ( y <= 0|| Board.Block_pos[y , x-1] == null ) )
            {
                return true;
            }
            return false;
        }

        public virtual void Done()
        {
            if(y <= 0)
            {
                return;
            }
            Board.Block_pos[y, x] = this;
        }
        public virtual bool MoveRight()
        {

            if (x + 1 <= 10 && (y <= 0 || Board.Block_pos[y, x + 1] == null))
            {
                return true;
            }
            return false;
        }

        public virtual void Draw()
        {
            if(y <= 0)
            {
                return;
            }
            Main._spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Main._spriteBatch.Draw(_texture , new Vector2(Board.Postitions[y , x].X , Board.Postitions[y ,x].Y)  , Color.White);

            Main._spriteBatch.End();
        }

        public virtual void Draw_Next(int _block_size)
        {
            Main._spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Main._spriteBatch.Draw(_texture, new Rectangle(x , y , _block_size , _block_size),  Color.White);

            Main._spriteBatch.End();
        }
    }


    public class Shape
    {

        List<Block> Block_List;
        List<Block> Fake_Block;
        int timing = 0;
        int limit = 50;

        public bool Destruct;

        public Shape(int type , Color color)
        {
            Destruct = false;
            Block_List = new List<Block>();
            Fake_Block = new List<Block>();
            if(type == 1)
            {
                Block_List.Add(new Block(5 , -1 ,color ));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(6, -1, color));
                Block_List.Add(new Block(6, 0, color));

                Fake_Block.Add(new Block(550, 500, color));
                Fake_Block.Add(new Block(550, 540, color));
                Fake_Block.Add(new Block(590, 500, color));
                Fake_Block.Add(new Block(590, 540, color));
                
            }

            if(type == 2)
            {
                Block_List.Add(new Block(5, -2 ,color));
                Block_List.Add(new Block(5, -1, color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(6, 0, color));

                Fake_Block.Add(new Block(550, 500, color));
                Fake_Block.Add(new Block(550, 540, color));
                Fake_Block.Add(new Block(550, 580, color));
                Fake_Block.Add(new Block(590, 580, color));
            }

            if(type == 3)
            {
                Block_List.Add(new Block(4 ,-1 , color ));
                Block_List.Add(new Block(5, -1, color));
                Block_List.Add(new Block(6, -1, color));
                Block_List.Add(new Block(5, 0, color));

                Fake_Block.Add(new Block(530, 540, color));
                Fake_Block.Add(new Block(570, 540, color));
                Fake_Block.Add(new Block(610, 540, color));
                Fake_Block.Add(new Block(570, 580, color));
            }
            if(type == 4)
            {
                Block_List.Add(new Block(4 , -1 , color));
                Block_List.Add(new Block(5, -1, color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(6, 0, color));

                Fake_Block.Add(new Block(530 , 540 , color));
                Fake_Block.Add(new Block(570, 540, color));
                Fake_Block.Add(new Block(570, 580, color));
                Fake_Block.Add(new Block(610, 580, color));
            }
            if(type == 5)
            {
                Block_List.Add(new Block(4 , 0 , color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(5, -1, color));
                Block_List.Add(new Block(6, -1, color));

                Fake_Block.Add(new Block(530 , 580 , color));
                Fake_Block.Add(new Block(570, 580, color));
                Fake_Block.Add(new Block(570, 540, color));
                Fake_Block.Add(new Block(610, 540, color));
            }

            if(type == 6)
            {
                Block_List.Add(new Block(5, -2, color));
                Block_List.Add(new Block(5, -1, color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(4 , 0 , color));

                Fake_Block.Add(new Block(570 , 510 , color));
                Fake_Block.Add(new Block(570, 550, color));
                Fake_Block.Add(new Block(570, 590, color));
                Fake_Block.Add(new Block(530, 590, color));
            }
            if(type == 7)
            {
                Block_List.Add(new Block(4 , 0 , color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(6, 0, color));
                Block_List.Add(new Block(6, -1, color));

                Fake_Block.Add(new Block(530 , 550 , color));
                Fake_Block.Add(new Block(570, 550, color));
                Fake_Block.Add(new Block(610, 550, color));
                Fake_Block.Add(new Block(610, 510 , color));
            }

            if(type == 8)
            {
                Block_List.Add(new Block(4 ,-1 , color));
                Block_List.Add(new Block(4, 0, color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(6, 0, color));

                Fake_Block.Add(new Block(530, 510, color));
                Fake_Block.Add(new Block(530, 550, color));
                Fake_Block.Add(new Block(570, 550, color));
                Fake_Block.Add(new Block(610, 550, color));
            }

            if(type == 9)
            {
                Block_List.Add(new Block(5 , -3 , color));
                Block_List.Add(new Block(5, -2, color));
                Block_List.Add(new Block(5, -1, color));
                Block_List.Add(new Block(5, 0, color));

                Fake_Block.Add(new Block(570, 510, color));
                Fake_Block.Add(new Block(570, 550, color));
                Fake_Block.Add(new Block(570, 590, color));
                Fake_Block.Add(new Block(570, 630, color));
            }

            if(type == 10)
            {
                Block_List.Add(new Block(3, 0, color));
                Block_List.Add(new Block(4, 0, color));
                Block_List.Add(new Block(5, 0, color));
                Block_List.Add(new Block(6, 0, color));

                Fake_Block.Add(new Block(510, 550, color));
                Fake_Block.Add(new Block(550, 550, color));
                Fake_Block.Add(new Block(590, 550, color));
                Fake_Block.Add(new Block(630, 550, color));
            }
        }

        


        private void Update()
        {
            Keyboard.GetState();
            if(Keyboard.HoldKey(Keys.Space))
            {
                if(timing  > 5)
                {
                    timing = 5;
                }
                limit = 5;
            }else
            {
                limit = 20;
            }

            if(Keyboard.HasBeenPressed(Keys.Left))
            {
                bool yes = true;
                foreach (Block blocks in Block_List)
                {
                    yes = blocks.MoveLeft();
                    if(yes == false)
                    {
                        break;
                    }
                }

                if(yes)
                {
                    foreach (Block blocks in Block_List)
                    {
                        blocks.Move(-1 , 0);
                    }
                }
            }

            if(Keyboard.HasBeenPressed(Keys.Right))
            {
                bool yes = true;
                foreach (Block blocks in Block_List)
                {
                    yes = blocks.MoveRight();
                    if (yes == false)
                    {
                        break;
                    }
                }

                if (yes)
                {
                    foreach (Block blocks in Block_List)
                    {
                        blocks.Move(1, 0);
                    }
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            timing++;
            Update();
            if(timing == limit)
            {
                timing = 0;
                bool yes = true;
                foreach (Block blocks in Block_List)
                {
                    yes = blocks.MoveDown();     
                    if(yes == false)
                    {
                        break;
                    }
                }
                if(yes)
                {
                    foreach(Block blocks in Block_List)
                    {
                        blocks.Move(0, 1);
                    }
                }
                if(!yes)
                {
                    Destruct = true;
                    foreach(Block blocks in Block_List)
                    {
                        blocks.Done();
                    }
                }

            }
        }

        public virtual void Draw()
        {
            foreach(Block blocks in Block_List)
            {
                blocks.Draw();
            }
        }

        public virtual void Draw_Next()
        {
            foreach(Block blocks in Fake_Block)
            {
                blocks.Draw_Next(40);
            }
        }



    }
}
