using System;

class Program
{
    static void Main(string[] args)
    {
        string[] grid = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        bool isPlayer1Turn = true;
        int numTurns = 0;
        bool playAgainstCPU = false;

        Console.WriteLine("Select mode:\n1 for Player vs Player, 2 for Player vs CPU");
        string modeChoice = Console.ReadLine();
        playAgainstCPU = modeChoice == "2";

        while (!CheckVictory(grid) && numTurns < 9)
        {
            PrintGrid(grid);
            if (!isPlayer1Turn && playAgainstCPU)
            {
                Console.WriteLine("CPU's Turn!");
                PerformCpuMove(grid);
                if (CheckVictory(grid))
                {
                    PrintGrid(grid);
                    Console.WriteLine("CPU wins!");
                    return;
                }
            }
            else
            {
                Console.WriteLine((isPlayer1Turn ? "Player 1's" : "Player 2's") + " Turn!");
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out int numChoice) && numChoice >= 1 && numChoice <= 9 && grid[numChoice - 1] != "X" && grid[numChoice - 1] != "O")
                {
                    grid[numChoice - 1] = isPlayer1Turn ? "X" : "O";
                    if (CheckVictory(grid))
                    {
                        PrintGrid(grid);
                        Console.WriteLine("Congratulations! " + (isPlayer1Turn ? "Player 1" : "Player 2") + " wins!");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, try again.");
                    continue; // Skip the toggling of isPlayer1Turn
                }
            }

            isPlayer1Turn = !isPlayer1Turn;
            numTurns++;
        }

        PrintGrid(grid);
        Console.WriteLine("It's a tie!");
    }

    static void PrintGrid(string[] grid)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(grid[i * 3 + j] + "|");
            }
            Console.WriteLine("\n------");
        }
    }

    static bool CheckVictory(string[] grid)
    {
        // Check rows for victory
        for (int row = 0; row < 3; row++)
        {
            if (grid[row * 3] == grid[row * 3 + 1] && grid[row * 3 + 1] == grid[row * 3 + 2] && grid[row * 3] != " ")
                return true;
        }

        // Check columns for victory
        for (int col = 0; col < 3; col++)
        {
            if (grid[col] == grid[col + 3] && grid[col + 3] == grid[col + 6] && grid[col] != " ")
                return true;
        }

        // Check diagonals for victory
        if ((grid[0] == grid[4] && grid[4] == grid[8] && grid[0] != " ") || 
            (grid[6] == grid[4] && grid[4] == grid[2] && grid[6] != " "))
            return true;

        return false;
    }

    static void PerformCpuMove(string[] grid)
    {
        Random rand = new Random();
        int choice;
        do
        {
            choice = rand.Next(0, 9);
        } while (grid[choice] == "X" || grid[choice] == "O");

        grid[choice] = "O";
        Console.WriteLine("CPU chooses: " + (choice + 1));
    }
}