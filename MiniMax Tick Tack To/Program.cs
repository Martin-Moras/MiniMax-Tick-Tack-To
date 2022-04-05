using System;

class mainClass
{
    static int player = 1;
    static string[] field;
    static bool gameHasWinner = false;
    static bool usingAi;
    static int aiDimension = 9;

    static void Main()
    {
        Console.WriteLine("\n");
        Console.WriteLine("AI = Y");
        if (Console.ReadKey().Key == ConsoleKey.Y) usingAi = true;

        Console.WriteLine("\n");
        Console.WriteLine("-------");
        Console.WriteLine("|" + 7 + "|" + 8 + "|" + 9 + "|");
        Console.WriteLine("|-----|");
        Console.WriteLine("|" + 4 + "|" + 5 + "|" + 6 + "|");
        Console.WriteLine("|-----|");
        Console.WriteLine("|" + 1 + "|" + 2 + "|" + 3 + "|");
        Console.WriteLine("-------");
        Console.WriteLine("\n");

        player = 1;
        gameHasWinner = false;
        field = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " ", }; //reset field


        turn();
    }

    static void turn()
    {
        for (int i = 0; i < field.Length; i++)
        {
            if (gameHasWinner == true)
            {
                break;
            }

            if (player == 2 && usingAi)
            {
                ai();
            }
            if (aiDimension == 9)
            {
                ConsoleKey consoleKey = Console.ReadKey().Key;


                switch (consoleKey)
                {
                    case ConsoleKey.NumPad9:
                    case ConsoleKey.D9:
                        write(8, ref i);
                        break;
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.D8:
                        write(7, ref i);
                        break;
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.D7:
                        write(6, ref i);
                        break;
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.D6:
                        write(5, ref i);
                        break;
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                        write(4, ref i);
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        write(3, ref i);
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        write(2, ref i);
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        write(1, ref i);
                        break;
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        write(0, ref i);
                        break;
                    case ConsoleKey.R:
                        Main();
                        break;
                    default:
                        i--;
                        break;
                }
            }
            else
            {
                write(aiDimension, ref i);
                aiDimension = 9; 
            }
            checkForWinner(player);

            if (gameHasWinner == false && i != 8)
            {
                if (player == 1)
                {
                    Console.WriteLine("Player X");
                }
                else
                {
                    Console.WriteLine("Player O");
                }
            }
            if (gameHasWinner == false && i == 8)
            {
                Console.WriteLine("Draw");
            }
        }

        Console.WriteLine("R to restart");
        if (Console.ReadKey().Key == ConsoleKey.R)
        {
            Main(); //restart
        }
    }

    static void write(int fieldDimension, ref int counter)
    {
        if (field[fieldDimension] != " ")
        {
            counter--;
        }
        else
        {
            if (player == 1)
            {
                field[fieldDimension] = "X";
                player = 2;
            }
            else
            {
                field[fieldDimension] = "O";
                player = 1;
            }
        }

        Console.WriteLine("\n");
        Console.WriteLine("-------");
        Console.WriteLine("|" + field[6] + "|" + field[7] + "|" + field[8] + "|");
        Console.WriteLine("|-----|");
        Console.WriteLine("|" + field[3] + "|" + field[4] + "|" + field[5] + "|");
        Console.WriteLine("|-----|");
        Console.WriteLine("|" + field[0] + "|" + field[1] + "|" + field[2] + "|");
        Console.WriteLine("-------");
        Console.WriteLine("\n");
    }

    static void checkForWinner(int currentPlayer)
    {
        String currentChar;

        if (currentPlayer == 1)
        {
            currentChar = "O";
        }
        else
        {
            currentChar = "X";
        }

        if (
            ((field[0] == currentChar) && (field[1] == currentChar) && (field[2] == currentChar)) ||
            ((field[3] == currentChar) && (field[4] == currentChar) && (field[5] == currentChar)) ||
            ((field[6] == currentChar) && (field[7] == currentChar) && (field[8] == currentChar)) ||
            ((field[0] == currentChar) && (field[3] == currentChar) && (field[6] == currentChar)) ||
            ((field[1] == currentChar) && (field[4] == currentChar) && (field[7] == currentChar)) ||
            ((field[2] == currentChar) && (field[5] == currentChar) && (field[8] == currentChar)) ||
            ((field[0] == currentChar) && (field[4] == currentChar) && (field[8] == currentChar)) ||
            ((field[2] == currentChar) && (field[4] == currentChar) && (field[6] == currentChar))
            )
        {
            Console.WriteLine("Player " + currentChar + " WIN");
            Console.WriteLine("\n");
            gameHasWinner = true;
        }
    }
    static void ai()
    {
        string[] copiedField = (string[])field.Clone();
        int finalDimension = 0;
        int highestScore = -10;

        firstAiMove(copiedField, ref finalDimension, ref highestScore, "X");
        aiDimension = finalDimension;
    }

    static void firstAiMove(string[] aiField, ref int finalDimension, ref int highestScore, string player)
    {
        int score;
        for (int counter = 0; counter < 9; counter++)
        {
            if (aiField[counter] == " ") 
            {
                aiField[counter] = player;

                if (aiCheckForWinner(aiField, "X") == true)//if player wins
                {
                    score = -10 + counter;
                    if (score > highestScore)
                    {
                        if (score > highestScore)
                        {
                            highestScore = score;
                            finalDimension = counter;
                        }
                        if (player == "X")
                        {
                            aiMove(aiField, ref finalDimension, ref highestScore, counter, "O");
                        }
                        else aiMove(aiField, ref finalDimension, ref highestScore, counter, "X");
                    }

                }
                else if (aiCheckForWinner(aiField, "O") == true)//if ai wins
                {
                    score = 10 - counter;
                    counter = 9;
                    if (score > highestScore)
                    {
                        highestScore = score;
                        finalDimension = counter;
                    }
                }
                else
                {
                    score = 0;
                    if (player == "X")
                    {
                        aiMove(aiField, ref finalDimension, ref highestScore, counter, "O");
                    }
                    else aiMove(aiField, ref finalDimension, ref highestScore, counter, "X");
                }
            }
        }
    }
    static void aiMove(string[] aiField, ref int finalDimension, ref int highestScore, int myDimension, string player)
    {
        int score;
        for (int counter = 0; counter < 9; counter++)
        {
            if (aiField[counter] == " ") 
            {
                aiField[counter] = player;

                if (aiCheckForWinner(aiField, "X") == true) //if player wins
                {
                    score = -10 + counter;
                    if (score > highestScore)
                    {
                        highestScore = score;
                        finalDimension = myDimension;
                    }
                }
                else if (aiCheckForWinner(aiField, "O") == true)//if ai wins
                {
                    score = 10 - counter;
                    if (score > highestScore)
                    {
                        highestScore = score;
                        finalDimension = myDimension;
                    }
                    counter = 9;
                }
                else
                {
                    score = 0;
                    if (score > highestScore)
                    {
                        highestScore = score;
                        finalDimension = myDimension;
                    }
                    if (player == "X")
                    {
                        aiMove(aiField, ref finalDimension, ref highestScore, myDimension, "O");
                    }
                    else aiMove(aiField, ref finalDimension, ref highestScore, myDimension, "X");
                }
            }
        }
    }
    static bool aiCheckForWinner(string[] aiField, string currentChar)
    {
        if (
            ((aiField[0] == currentChar) && (aiField[1] == currentChar) && (aiField[2] == currentChar)) ||
            ((aiField[3] == currentChar) && (aiField[4] == currentChar) && (aiField[5] == currentChar)) ||
            ((aiField[6] == currentChar) && (aiField[7] == currentChar) && (aiField[8] == currentChar)) ||
            ((aiField[0] == currentChar) && (aiField[3] == currentChar) && (aiField[6] == currentChar)) ||
            ((aiField[1] == currentChar) && (aiField[4] == currentChar) && (aiField[7] == currentChar)) ||
            ((aiField[2] == currentChar) && (aiField[5] == currentChar) && (aiField[8] == currentChar)) ||
            ((aiField[0] == currentChar) && (aiField[4] == currentChar) && (aiField[8] == currentChar)) ||
            ((aiField[2] == currentChar) && (aiField[4] == currentChar) && (aiField[6] == currentChar))
            )
        {
            return true;
        }
        return false;
    }
}
