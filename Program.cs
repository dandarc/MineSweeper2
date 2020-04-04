using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class Program
    {
        static int width;
        static int height;
        static int mines;
        const int startGrid = 2;
        static Mine game;
        static int x = 0;
        static int y = 0;

        static void Main(string[] args)
        {
            //validate args
            if (!validateArgs(ref args)) return;

            //initialize object
            game = new Mine(width, height, mines);

            //display
            initDisplay();


            System.ConsoleKeyInfo key;
            //loop
            while (true)
            {
                key = Console.ReadKey();
                if (evaluateKey(key))
                {
                    //select cell
                    if(!game.CheckSquare(x-startGrid, y - startGrid))
                    {
                        Console.SetCursorPosition(0, game.Height + startGrid + 5);
                        if (game.Win)
                        {                           
                            Console.Write("You win!");
                        }
                        else if (game.Loss)
                        {
                            Console.Write("You lose");
                        }
                        displayGrid(x, y);
                        Console.SetCursorPosition(0, game.Height + startGrid + 7);
                        pressAnyKey();
                        break;
                    }
                }
                //redisplay
                displayGrid(x,y);

            }
        }

        static bool validateArgs(ref string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: Minesweeper.exe width length height");
                pressAnyKey();
                return false;
            }
            if (!Int32.TryParse(args[0], out width) 
                || !Int32.TryParse(args[1], out height) 
                || !Int32.TryParse(args[2], out mines))
            {
                Console.WriteLine("All parameters should be positive integers");
                pressAnyKey();
                return false;
            }
            return true;
        }

        static void pressAnyKey()
        {
            Console.WriteLine("Press any key to contiue.");
            Console.ReadKey();
        }

        static void initDisplay()
        {
            Console.Clear();

            Console.SetWindowSize(game.Width + 25, game.Height + 25);

            Console.SetCursorPosition(0, 0);
            Console.Write("Mines Left: " + game.MinesLeft());

            displayGrid(startGrid,startGrid);

            Console.SetCursorPosition(0, game.Height + startGrid + 1);
            Console.Write("Select position. \nPress space to try square.");

            Console.SetCursorPosition(startGrid, startGrid);
        }

        static void displayGrid(int csrRow, int csrCol)
        {
            for (int i =0; i < game.Width; i++)
            {
                for (int j=0; j< game.Height; j++)
                {
                    Console.SetCursorPosition(startGrid + i, startGrid + j);
                    if (game.intClicked[i, j] == 1)
                    {
                        if (game.intMainGrid[i, j] < 0) Console.Write("*");
                        else Console.Write(game.intMainGrid[i, j]);
                    }
                    else Console.Write("X");
                }
            }
            Console.SetCursorPosition(csrRow, csrCol);
        }

        static bool evaluateKey(System.ConsoleKeyInfo key)
        {
            x = Console.CursorLeft - 1;
            y = Console.CursorTop;
            if (key.Key == ConsoleKey.Spacebar)
            {
                return true;
            }
            
            if (key.Key == ConsoleKey.LeftArrow)
            {
                x -= 1;
                if (x < startGrid) x = startGrid;
            }
            if (key.Key == ConsoleKey.RightArrow)
            {
                x += 1;
                if (x > startGrid + game.Width - 1) x = startGrid + game.Width - 1;
            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                y -= 1;
                if (y < startGrid) y = startGrid;
            }
            if (key.Key == ConsoleKey.DownArrow)
            {
                y += 1;
                if (y > startGrid + game.Height - 1) y = startGrid + game.Height - 1;
            }

            Console.SetCursorPosition(x, y);

            return false;
        }
    }
}
