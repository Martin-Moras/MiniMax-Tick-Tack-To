class mainClass
{
    static char player;
    static char[] field;
    static bool usingAi;
    static int aiDimension = 9;
    static int stepCounter;

    static void Main()
    {
        //reset game
        player = 'X';
        field = new char[9] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }; 
        
        Console.WriteLine("\n");
        Console.WriteLine("play with AI = Y");

        if (Console.ReadKey().Key == ConsoleKey.Y)//should I play against ai?
        {
            usingAi = true;

            Console.WriteLine("\n");
            Console.WriteLine("AI starts = Y");

            if (Console.ReadKey().Key == ConsoleKey.Y)//should ai start?
            {
                player = 'O';
            }
        }
        Console.WriteLine("\n");
        Console.WriteLine("-------");
        Console.WriteLine("|" + 7 + "|" + 8 + "|" + 9 + "|");
        Console.WriteLine("|-----|");
        Console.WriteLine("|" + 4 + "|" + 5 + "|" + 6 + "|");
        Console.WriteLine("|-----|");
        Console.WriteLine("|" + 1 + "|" + 2 + "|" + 3 + "|");
        Console.WriteLine("-------");
        Console.WriteLine("\n");

        turn();
    }

    static void turn()
    {
        for (int i = 0; i < field.Length; i++)
        {
            if (usingAi && player == 'O')
            {
                ai();
                write(aiDimension, ref i);
            }
            else
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
            if (checkForWinner(field, player) == true)
            {
                Console.WriteLine("Player " + player + " WIN");
                Console.WriteLine("\n");
                break;
            }
            if (i == 8)// if is draw
            {
                Console.WriteLine("Draw");
            }
            else
            {
                Console.WriteLine("Player " + player); //say who's next
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
        if (field[fieldDimension] != ' ')//if selected field isn't free
        {
            counter--;
        }
        else
        {
            field[fieldDimension] = player;//place char in selected field and change player

            if (player == 'X')
            {
                player = 'O';
            }
            else
            {
                player = 'X';
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

    static bool checkForWinner(char[] aiField, char currentChar)
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

    static void ai()
    {
        aiDimension = firstAiMove((char[])field.Clone(), 'O');

        Console.WriteLine("amount of steps " + stepCounter);
        Console.WriteLine("final dimension " + aiDimension);

        stepCounter = 0;
    }

    static int firstAiMove(char[] aiField, char player)
    {
        int highScore = 0;
        int finalDimension = 0;
        bool changedHighscore = false;

        for (int counter = 0; counter < 9; counter++)
        {
            if (aiField[counter] == ' ')
            {
                char[] copietAiField = (char[])aiField.Clone();
                copietAiField[counter] = player;

                if (checkForWinner(copietAiField, 'O') == true)//if ai wins
                {
                    return counter;
                }
                else
                {
                    stepCounter++;
                    int thisScore = aiGetScore(copietAiField, 1, 'X');

                    if (thisScore > highScore || changedHighscore == false)
                    {
                        changedHighscore = true;
                        highScore = thisScore;
                        finalDimension = counter;
                    }
                }
            }
        }
        return finalDimension;
    }
    static int aiGetScore(char[] aiField, int depth, char player)
    {
        int score;
        int highScore = 0;
        bool changedHighscore = false;

        for (int counter = 0; counter < 9; counter++)
        {
            if (aiField[counter] == ' ')
            {
                char[] copietAiField = (char[])aiField.Clone();
                copietAiField[counter] = player;

                if (checkForWinner(copietAiField, 'X') == true) //if player wins
                {
                    highScore = -10 + depth;
                    counter = 9;
                }
                else if (checkForWinner(copietAiField, 'O') == true)//if ai wins
                {
                    highScore = 10 - depth;
                    counter = 9;
                }
                else
                {
                    stepCounter++;

                    int copyDepth = depth;
                    copyDepth++;
                    if (player == 'X')
                    {
                        score = aiGetScore(copietAiField, copyDepth, 'O');

                        if (score < highScore || changedHighscore == false)
                        {
                            changedHighscore = true;
                            highScore = score;
                        }
                    }
                    else
                    {
                        score = aiGetScore(copietAiField, copyDepth, 'X');
                        
                        if (score > highScore || changedHighscore == false)
                        {
                            changedHighscore = true;
                            highScore = score;
                        }
                    }
                }
            }
        }
        return highScore;
    }
}