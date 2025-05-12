using System;
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

        // Gracz 2
        Pixel player2 = new Pixel
        {
            xPos = screenwidth / 2,
            yPos = screenheight / 2 + 2,
            schermKleur = ConsoleColor.Blue
        };
        string movement2 = "RIGHT";

        string obstacle = "*";
        int obstacleXpos = randomnummer.Next(1, screenwidth - 1);
        int obstacleYpos = randomnummer.Next(1, screenheight - 1);

        while (true)
        {
            Console.Clear();

            // Przeszkoda
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);

            // Ramka
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0); Console.Write("■");
                Console.SetCursorPosition(i, screenheight - 1); Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write("■");
                Console.SetCursorPosition(screenwidth - 1, i); Console.Write("■");
            }

            // Klawiatura
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    // Gracz 1
                    case ConsoleKey.UpArrow: movement1 = "UP"; break;
                    case ConsoleKey.DownArrow: movement1 = "DOWN"; break;
                    case ConsoleKey.LeftArrow: movement1 = "LEFT"; break;
                    case ConsoleKey.RightArrow: movement1 = "RIGHT"; break;

                    // Gracz 2
                    case ConsoleKey.W: movement2 = "UP"; break;
                    case ConsoleKey.S: movement2 = "DOWN"; break;
                    case ConsoleKey.A: movement2 = "LEFT"; break;
                    case ConsoleKey.D: movement2 = "RIGHT"; break;
                }
            }

            // Ruch gracza 1
            switch (movement1)
            {
                case "UP": player1.yPos--; break;
                case "DOWN": player1.yPos++; break;
                case "LEFT": player1.xPos--; break;
                case "RIGHT": player1.xPos++; break;
            }

            // Ruch gracza 2
            switch (movement2)
            {
                case "UP": player2.yPos--; break;
                case "DOWN": player2.yPos++; break;
                case "LEFT": player2.xPos--; break;
                case "RIGHT": player2.xPos++; break;
            }

            // Zjedzenie przeszkody przez gracza 1
            if (player1.xPos == obstacleXpos && player1.yPos == obstacleYpos)
            {
                Console.Beep(1000, 200); // 🔊 DŹWIĘK!
                obstacleXpos = randomnummer.Next(1, screenwidth - 1);
                obstacleYpos = randomnummer.Next(1, screenheight - 1);
            }

            // Rysuj gracza 1
            Console.ForegroundColor = player1.schermKleur;
            Console.SetCursorPosition(player1.xPos, player1.yPos);
            Console.Write("■");

            // Rysuj gracza 2
            Console.ForegroundColor = player2.schermKleur;
            Console.SetCursorPosition(player2.xPos, player2.yPos);
            Console.Write("■");

            Thread.Sleep(100);
        }
    }
}
