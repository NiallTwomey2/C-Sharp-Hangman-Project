// Niall Twomey L00213591
// CA01 Hangman Program

using System;
using System.Collections.Generic; // taken from https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic?view=net-11.0-pp for collection (lists) management 

namespace Hangman_game
// defining the classes objects for the hangman game before creating the main while loop and for loops for the game to iterate

{
    public class Program
    {
        public static void Main(string[] args) // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/main-command-line
        {
            Menu_CLI menu = new Menu_CLI(); // instantiating menu to reference Menu_CLI, it is easier for cross method referencing and satisfy the error log
            menu.Menu_Options();
        }
    }
    public class Menu_CLI
    {
        // instantiating the following fields to be populated for the statistics option, these values are passed through to these variables when the game ends
        // public int is used for cross method reference, which is needed, no restrictions for these statistics variables that will populate and update when the game ends, taken from https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/public
        public int gamesPlayed = 0;
        public int gamesLost = 0;
        public int gamesWon = 0;
        public int totalGuesses = 0;
        public int incorrectGuesses = 0;
        public int correctGuesses = 0;
        public string playerName = "";

        public double GetPercentage(int value, int total, int decimals = 2) // taken from https://carlpaton.github.io/2017/12/c-sharp-calc-percentage/
        {
            if (total == 0)
            {
                return 0;
            }
            double percentage = ((double)value / total) * 100;
            return Math.Round(percentage, decimals);
        }

        public void Menu_Options() // public void syntax taken from: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void
        {
            while (true) //while loop to keep the menu
            {
                Console.WriteLine("Hello and welcome to Hangman");
                Console.WriteLine("\n");
                Console.WriteLine("========== Main Menu ==========");
                Console.WriteLine("\n");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. View Statistics");
                Console.WriteLine("3. Quit");
                Console.WriteLine("\n");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    playerName = ValidatePlayerName();

                    if (!string.IsNullOrEmpty(playerName)) // validation check to see if the player name is empty or not, if not empty go ahead and run RunGame() method
                    {
                        RunGame game = new RunGame(); // initializing a new game as option 1 is selected, redefining with variable 'game'
                        game.StartGame(playerName, this); // 'this' sends the most up-to-date statistics to be updated to pass through the StartGame() method, clarifying the most current instance of the statistics, taken from https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/this
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Hangman Game Statistics for " + playerName);
                    Console.WriteLine("\n");
                    Console.WriteLine("Games Played: " + gamesPlayed);
                    Console.WriteLine("Games Won: " + gamesWon);
                    Console.WriteLine("Games Lost: " + gamesLost);
                    Console.WriteLine("Total Guesses overall on Hangman: " + totalGuesses);
                    Console.WriteLine("Total Incorrect Guesses overall on Hangman: " + incorrectGuesses);
                    Console.WriteLine("Total Correct Guesses overall on Hangman: " + correctGuesses);
                    Console.WriteLine("Percentage of Wins to total games played: " + GetPercentage(gamesWon, gamesPlayed) + "%"); // taken from https://carlpaton.github.io/2017/12/c-sharp-calc-percentage/
                    Console.WriteLine("Percentage of Losses to total games played: " + GetPercentage(gamesLost, gamesPlayed) + "%");
                    Console.WriteLine("\n");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Quitting the game...");
                    Console.WriteLine("\n");
                    Environment.Exit(0); // exiting the program taken from: https://www.geeksforgeeks.org/c-sharp/c-sharp-program-to-demonstrate-the-use-of-exit-method-of-environment-class/
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine("\n");
                }

            }

        }
        public string ValidatePlayerName()
        {
            string playerName = "";
            // string.isNullOrEmpty method is taken from: https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-10.0
            int maxNameAttempts = 3;
            int maxNameLength = 20;

            for (int attempt = 1; attempt <= maxNameAttempts; attempt++)
            {
                Console.WriteLine("Please enter your player name (max 20 characters) and must not be empty: ");
                Console.WriteLine("\n");
                playerName = Console.ReadLine();
                if (string.IsNullOrEmpty(playerName))
                {
                    Console.WriteLine("Please enter your player name.");
                    Console.WriteLine("\n");
                }
                else if (playerName.Length > maxNameLength)
                {
                    Console.WriteLine("Player name must not exceed 20 characters.");
                    Console.WriteLine("\n");
                }
                else if (attempt == maxNameAttempts)
                {
                    Console.WriteLine("Too many invalid attempts. Exiting the player menu.");
                    return ""; // exiting the method if the player name is invalid after 3 attempts, open the Menu options again.
                }
                else
                {
                    Console.WriteLine("Welcome to the game, " + playerName);
                    Console.WriteLine("\n");
                    return playerName; // returning the validated player name to pass through to the RunGame() method
                }
            }
            return "";
        }
    }

    public class Round_Identifier
    {
        public string Stickman_per_Round(int roundNumber)
        {
            switch (roundNumber) //stickman taken from: https://stackoverflow.com/questions/76563897/formatting-a-stickman-shape-in-a-string-want-the-line-feed-inside-the-string
            {
                case 1:
                    return """ 
**************************************  O
                                      / | \
                                      /   \*****
""";
                case 2:
                    return """
**************************************  O
                                      / | \
                                      /   *****
""";
                case 3:
                    return """
**************************************  O
                                      / | \
                                          *****
""";
                case 4:
                    return """
**************************************  O
                                      / | 
                                          *****
""";
                case 5:
                    return """
**************************************  O
                                      / 
                                          *****
""";
                case 6:
                    return """
**************************************  O
                                          *****
""";
                case 7:
                    return "Stickman is no more. You lost, exiting the game.";
                default:
                    return "\n";
            }
        }

        public string film_Answer(int roundNumber)
        {
            // List of films - collection 
            List<string> films = new List<string>();
            films.Add("the godfather");
            films.Add("the dark knight");
            films.Add("anora");
            films.Add("bugonia");
            films.Add("titanic");
            films.Add("the lego movie");
            films.Add("the matrix");
            films.Add("little women");
            films.Add("the lord of the rings");
            films.Add("mamma mia");

            switch (roundNumber)
            {
                case 1:
                    return films[1];
                case 2:
                    return films[2];
                case 3:
                    return films[3];
                case 4:
                    return films[4];
                case 5:
                    return films[5];
                case 6:
                    return films[6];
                case 7:
                    return films[7];
                case 8:
                    return films[8];
                case 9:
                    return films[9];
                case 10:
                    return films[10];
                default:
                    return "invalid movie string";
            }
        }
    }

    public class RunGame
    {

        // then initializing the game statistic variables for storage of the player's stats
        int guesses = 0;
        int incorrectGuesses = 0;
        int correctGuesses = 0;
        int maxincorrectGuesses = 7;
        int totalGuesses = 0;
        int roundNumber = 0;
        List<string> guessedLetters = new List<string>();
        Random random = new Random(); // random taken from brocode on youtube: https://www.youtube.com/watch?v=1gSFd-YVFP8
        bool GameResult = false; // boolean sets the game result variable to false, when it is set to true (all letters successfully guessed), it will change the value from 'false' to 'true' and pass a 'win' value to the EndGame() method

        public void StartGame(string playerName, Menu_CLI menu)
        {
            Console.WriteLine("Starting the game...");
            int selectNumber_for_case = random.Next(1, 11); //random number between 1 and 10 taken from brocode on youtube: https://www.youtube.com/watch?v=1gSFd-YVFP8
            // Game logic goes here
            Round_Identifier roundIdentifier = new Round_Identifier(); // initializing the Round_Identifer with 'roundIdentifier' variable, refreshing with a new round using the round_identifier method
            string filmAnswer = roundIdentifier.film_Answer(selectNumber_for_case); // randomly selecting a film answer for the round from the swiitch case statements in the Round_Identifier class, using the random number generated to select a random film answer

            List<char> displayWord = new List<char>(); //display word list to store and reinitiate a new list of characters

            foreach (char c in filmAnswer) // 'c' takes the iteration in the for each loop, for each character that is in the film, replace the character with either '_' or ' ' (whitespace)
            {
                if (c == ' ')
                    displayWord.Add(' '); // maintaining white spaces in the film names
                else
                    displayWord.Add('_'); // template for the number of characters the film name has
            }

            while (incorrectGuesses < maxincorrectGuesses && !GameResult)
            {
                Console.WriteLine("\n" + roundIdentifier.Stickman_per_Round(roundNumber)); // referencing the switch cases of stickman
                Console.WriteLine("\n");
                Console.WriteLine("Film: " + string.Join(" ", displayWord)); // string.Join is taken from https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0
                Console.Write("Enter a single letter guess:");
                Console.WriteLine("\n");
                string roundGuess = Console.ReadLine();

                if (!string.IsNullOrEmpty(roundGuess) && roundGuess.Length == 1)
                {
                    char roundGuessChar = char.ToLower(roundGuess[0]); // converting to lowercase for the films to make it easier for the game to run

                    if (char.IsLetter(roundGuessChar))
                    {
                        string guessedString = roundGuessChar.ToString(); // converting char data type to string, so that the letter already guessed can be displayed to notify the user, taken from https://learn.microsoft.com/en-us/dotnet/api/system.object.tostring?view=net-10.0
                        if (guessedLetters.Contains(guessedString))
                        {
                            Console.WriteLine($"You already guessed {roundGuessChar}. Try a different letter");
                            continue;
                        }
                        guessedLetters.Add(guessedString);
                        guesses++;
                        totalGuesses++;
                        menu.totalGuesses++; // passing the totalGuesses value to the Menu_CLI method, for the statistics page to update.

                        if (filmAnswer.ToLower().Contains(roundGuessChar))
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine($"{roundGuessChar} is in the film title"); // expanding the variable using the '$', using the same string expansion from BASH syntax, taken from here https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
                            correctGuesses++;
                                menu.correctGuesses++;


                            for (int letter = 0; letter < filmAnswer.Length; letter++)
                            {
                                if (char.ToLower(filmAnswer[letter]) == roundGuessChar)
                                {
                                    displayWord[letter] = filmAnswer[letter]; // displaying the correct letter/s that are on the film title, subsituting '_' for the correct letter
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"'{roundGuessChar}' is not in the film title");
                            incorrectGuesses++;
                            menu.incorrectGuesses++;
                            roundNumber++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid letter between a-z.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter exactly one character between a-z.");
                }

                if (!displayWord.Contains('_'))
                {
                    GameResult = true;
                }
            }
            string results = GameResult ? "Win" : "Lose"; // setting the results string value to Win if GameResult is true as a result of all the '_' characters becoming letters, if not GameResult remains 'false' and player loses, reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
            EndGame(playerName, results, menu, filmAnswer);
        }


        public void EndGame(string playerName, string endOfgameResult, Menu_CLI menu, string filmAnswer)
        {
            Console.WriteLine($"\nWell done {playerName}, game is finished. The film was '{filmAnswer}' Result: '{endOfgameResult}'");
            Console.WriteLine("\n");

            if (endOfgameResult == "Win")
            {
                menu.gamesWon++;
                menu.gamesPlayed++;
            }
            else if (endOfgameResult == "Lose")
            {
                menu.gamesLost++;
                menu.gamesPlayed++;
            }

            while (true)
            {
                Console.WriteLine("Want to play again?");
                Console.WriteLine("===== Play again menu =====");
                Console.WriteLine("\n");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No (Return to main menu)");
                Console.WriteLine("\n");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Starting a new game...");
                    RunGame newGame = new RunGame();
                    newGame.StartGame(playerName, menu);
                    break;
                }
                else if (option == "2")
                {
                    Console.WriteLine("Returning to the main menu...");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Option. Enter either option 1 or option 2.");
                }
            }
        }

    }
}