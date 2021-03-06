﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class Mine
    {
        public int[,] intMainGrid;
        public int[,] intClicked;

        public int Width;
        public int Height;
        public int Mines;
        public int MinesID;

        public bool Win = false;
        public bool Loss = false;

        public Mine(int width, int height, int mines)
        {
            intMainGrid = new int[width, height];
            intClicked = new int[width, height];
            bool minePlaced = false;
            var rand = new Random();
            for (int i = 0; i < mines; i++)
            {
                int j;
                int k;
                while (!minePlaced)
                {
                    j = rand.Next(width);
                    k = rand.Next(height);

                    if (intMainGrid[j, k] == 0)
                    {
                        intMainGrid[j, k] = -1;
                        minePlaced = true;
                    }

                }
                minePlaced = false;
            }

            this.Width = width;
            this.Height = height;
            this.Mines = mines;
        }

        public int MinesLeft()
        {
            return Mines - MinesID;
        }
        
        //returns true if continue, returns false if not continue
        public bool CheckSquare(int x, int y)
        {
            this.intClicked[x, y] = 1;
            if (this.intMainGrid[x, y] == -1)
            {
                Loss = true;
                clickAll();
                return false;
            }
            else
            {
                //sum adjacent squares
                addSquares(x, y);

                //check for win
                int sum = 0;
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        if (intClicked[i, j] == 1)
                        {
                            sum++;
                        }
                    }
                }
                if (sum == Width * Height - Mines)
                {
                    Win = true;
                    clickAll();
                    return false;
                }

                

                return true;
            }
        }

        public void markSquare(int x, int y)
        {
            if (intClicked[x,y] == 0)
            {
                intClicked[x, y] = 2;
                MinesID += 1;
            }
            else if (intClicked[x,y] == 2)
            {
                intClicked[x, y] = 0;
                MinesID -= 1;
            }
        }

        void clickAll()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    intClicked[i,j] = 1;
                    if (intMainGrid[i,j] >= 0) addSquares(i, j);
                }
            }
        }
        void addSquares(int x, int y)
        {
            int adjMines = 0;

            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    if (i >= 0 && i <= Width - 1 && j >=0 && j <= Height - 1 )
                    {
                        if (intMainGrid[i,j] == -1)
                        {
                            adjMines += 1;
                        }
                    }
                }
            }

            intMainGrid[x, y] = adjMines;

            if (adjMines == 0)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i <= Width - 1 && j >= 0 && j <= Height - 1)
                        {
                            if (intClicked[i, j] < 1)
                            {
                                intClicked[i, j] = 1;
                                addSquares(i, j);
                            }
                        }
                    }
                }
            }
        }
    }
}
