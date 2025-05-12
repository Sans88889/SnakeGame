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

        // Gracz 1
        Pixel player1 = new Pixel
        {
            xPos = screenwidth / 2,
            yPos = screenheight / 2,
            schermKleur = ConsoleColor.Red
        };
        string movement1 = "RIGHT";

        int score = 0;

        List<int> teljePositie = new List<int>();
        teljePositie.Add(player1.xPos);
        teljePositie.Add(player1.yPos);

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

            // Rysuj ogon gracza 1
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 2; i < teljePositie.Count; i += 2)
            {
                Console.SetCursorPosition(teljePositie[i], teljePositie[i + 1]);
                Console.Write("■");
            }

            // Obsługa klawiatury
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        movement1 = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        movement1 = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        movement1 = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        movement1 = "RIGHT";
                        break;
                }
            }

            // Logika ruchu
            switch (movement1)
            {
                case "UP":
                    player1.yPos--;
                    break;
                case "DOWN":
                    player1.yPos++;
                    break;
                case "LEFT":
                    player1.xPos--;
                    break;
                case "RIGHT":
                    player1.xPos++;
                    break;
            }

            // Rysuj gracza 1
            Console.ForegroundColor = player1.schermKleur;
            Console.SetCursorPosition(player1.xPos, player1.yPos);
            Console.Write("■");

            // Kolizja z przeszkodą
            if (player1.xPos == obstacleXpos && player1.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth - 1);
                obstacleYpos = randomnummer.Next(1, screenheight - 1);
            }

            // Aktualizacja ogona
            teljePositie.Insert(0, player1.xPos);
            teljePositie.Insert(1, player1.yPos);
            if (teljePositie.Count > (score + 1) * 2)
            {
                teljePositie.RemoveAt(teljePositie.Count - 1);
                teljePositie.RemoveAt(teljePositie.Count - 1);
            }

            // Kolizja ze ścianą
            if (player1.xPos == 0 || player1.xPos == screenwidth - 1 || player1.yPos == 0 || player1.yPos == screenheight - 1)
            {
                GameOver(score);
            }

            // Kolizja z ogonem
            for (int i = 2; i < teljePositie.Count; i += 2)
            {
                if (player1.xPos == teljePositie[i] && player1.yPos == teljePositie[i + 1])
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
