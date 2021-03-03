using System;
using static System.Console;
using static System.ConsoleKey;

namespace Wumpus
{
    class Program
    {
        enum Action { Von, Shelest, Gul, Lose, Default }
        struct Coords
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        static Random rand = new Random();
        static Coords Wump = new Coords();
        static Coords Play = new Coords();

        static int CursPosX = CursorLeft + 1;
        static int CursPosY = CursorTop;
        private static void PlayObjInit()
        {
            Wump.X = rand.Next(0, 6);
            Wump.Y = rand.Next(0, 6);

            Play.X = 0;
            Play.Y = 0;
        }
        private static void Main(string[] args)
        {
            BufferHeight = 20;
            BufferWidth = 50;

            PlayObjInit();

            WriteLine("[ ][ ][ ][ ][ ][ ]");
            WriteLine("[ ][ ][ ][ ][ ][ ]");
            WriteLine("[ ][ ][ ][ ][ ][ ]");
            WriteLine("[ ][ ][ ][ ][ ][ ]");
            WriteLine("[ ][ ][ ][ ][ ][ ]");
            WriteLine("[ ][ ][ ][ ][ ][ ]");

            SetCursorPosition(Wump.X * 3 + 1, Wump.Y);
            WriteLine("W");

            ShowPlayer();

            do
            {
                if (KeyAvailable)
                {
                    var key = ReadKey(true).Key;

                    Step(key);

                    CursPosX = CursorLeft;
                    CursPosY = CursorTop;

                    Message();
                }
            } while (!(Play.X == Wump.X && Play.Y == Wump.Y));

            ReadKey();
        }
        private static void Message()
        {
            if (Play.X == Wump.X && Play.Y == Wump.Y) PrintMessage(Action.Lose);

            if (Play.X + 1 == Wump.X && Play.Y == Wump.Y) PrintMessage(Action.Von);

            if (Play.X - 1 == Wump.X && Play.Y == Wump.Y) PrintMessage(Action.Von);

            if (Play.X == Wump.X && Play.Y + 1 == Wump.Y) PrintMessage(Action.Von);

            if (Play.X == Wump.X && Play.Y - 1 == Wump.Y) PrintMessage(Action.Von);
        }
        private static void PrintMessage(Action action)
        {
            string[] str = new string[] { "Вонь", "Шелест", "Гул", "Поражение", "            " };
            SetCursorPosition(5, 10);
            Write(str[(int)action]);
        }
        private static void Step(ConsoleKey key)
        {
            PrintMessage(Action.Default);
            SetCursorPosition(CursPosX, CursPosY);

            if (IsKeyIsBuff(key))
            {
                Write(' ');

                if (key == LeftArrow)
                {
                    SetCursorPosition(CursorLeft - 4, CursorTop);
                    Play.X--;
                }
                if (key == UpArrow)
                {
                    SetCursorPosition(CursorLeft - 1, CursorTop - 1);
                    Play.Y--;
                }
                if (key == RightArrow)
                {
                    SetCursorPosition(CursorLeft + 2, CursorTop);
                    Play.X++;
                }
                if (key == DownArrow)
                {
                    SetCursorPosition(CursorLeft - 1, CursorTop + 1);
                    Play.Y++;
                }

                Write('@');

                SetCursorPosition(CursorLeft - 1, CursorTop);
            }
        }
        private static bool IsKeyIsBuff(ConsoleKey key)
        {
            bool[] BuffLimit = new bool[]
            {
                CursorLeft > 2,
                CursorTop  > 0,
                CursorLeft < 15,
                CursorTop  < 5
            };

            return (key == LeftArrow) ||
                    (key == UpArrow) ||
                    (key == RightArrow) ||
                    (key == DownArrow) ? BuffLimit[(int)key - 37] : false;
        }
        private static void ShowPlayer()
        {
            SetCursorPosition(1, 0);
            CursorVisible = false;
            ForegroundColor = ConsoleColor.Green;
            Write('@');
            SetCursorPosition(CursorLeft - 1, CursorTop);
        }
    }
}