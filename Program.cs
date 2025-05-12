using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Pixel
{
    public int xPos { get; set; }
    public int yPos { get; set; }
    public ConsoleColor schermKleur { get; set; }
}

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        Pixel hoofd = new Pixel
        {
            xPos = screenwidth / 2,
            yPos = screenheight / 2,
            schermKleur = ConsoleColor.Red
        };

        string movement = "RIGHT";
        int score = 0;

        List<int> teljePositie = new List<int>();
        teljePositie.Add(hoofd.xPos);
        teljePositie.Add(hoofd.yPos);

        string obstacle = "*";
        int obstacleXpos = randomnummer.Next(1, screenwidth - 1);
        int obstacleYpos = randomnummer.Next(1, screenheight - 1);

        while (true)
        {
            Console.Clear();

            // Draw obstacle
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);

            // Draw borders
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            // Display score
            Console.SetCursorPosition(0, screenheight);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Score: " + score);

            // Draw Snake Head
            Console.ForegroundColor = hoofd.schermKleur;
            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);
            Console.Write("■");

            // Draw Tail
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 2; i < teljePositie.Count; i += 2)
            {
                Console.SetCursorPosition(teljePositie[i], teljePositie[i + 1]);
                Console.Write("■");
            }

            // Read user input
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        movement = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        movement = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        movement = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        movement = "RIGHT";
                        break;
                }
            }

            // Move snake
            switch (movement)
            {
                case "UP":
                    hoofd.yPos--;
                    break;
                case "DOWN":
                    hoofd.yPos++;
                    break;
                case "LEFT":
                    hoofd.xPos--;
                    break;
                case "RIGHT":
                    hoofd.xPos++;
                    break;
            }

            // Check collision with obstacle
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth - 1);
                obstacleYpos = randomnummer.Next(1, screenheight - 1);
            }

            // Add head position to tail
            teljePositie.Insert(0, hoofd.xPos);
            teljePositie.Insert(1, hoofd.yPos);
            if (teljePositie.Count > (score + 1) * 2)
            {
                teljePositie.RemoveAt(teljePositie.Count - 1);
                teljePositie.RemoveAt(teljePositie.Count - 1);
            }

            // Collision with wall
            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)
            {
                GameOver(score);
            }

            // Collision with tail
            for (int i = 2; i < teljePositie.Count; i += 2)
            {
                if (hoofd.xPos == teljePositie[i] && hoofd.yPos == teljePositie[i + 1])
                {
                    GameOver(score);
                }
            }

            Thread.Sleep(100);
        }
    }

    static void GameOver(int score)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(5, 5);
        Console.WriteLine("Game Over");
        Console.SetCursorPosition(5, 6);
        Console.WriteLine("Twój wynik: " + score);
        Environment.Exit(0);
    }
}
