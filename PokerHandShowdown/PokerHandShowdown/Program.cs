using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables
            string input = string.Empty;
            int numberOfPlayers = 0;
            bool playAgain = true;
            List<Player> players = new List<Player>();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            FiveCardPokerGame game;

            // Ask user how many players will be playing
            Console.WriteLine("Welcome to Poker Hand Showdown, how many useres will be playing?(2-6) :");
            input = Console.ReadLine();

            // Check to ensure user entered a number between 2 and 6
            while (!ValidateFiveCardPokerGame.ValidateNumberOfPlayers(input))
            {
                Console.WriteLine("Entered invlaid amount of players try again (2-6): ");
                input = Console.ReadLine();
            }

            // Create players for game.
            int.TryParse(input, out numberOfPlayers);
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                string username = string.Empty;

                Console.WriteLine($"Please enter player{i}'s username");
                username = Console.ReadLine().Trim();

                while (!ValidateFiveCardPokerGame.ValidateUniqueUserNames(username, players))
                {
                    Console.WriteLine($"User name already taken please enter player{i}'s username");
                    username = Console.ReadLine().Trim();
                }

                Player player = new Player(username);
                players.Add(player);
            }

            // Create Game.
            game = new FiveCardPokerGame(players);

            while (playAgain)
            {
                string msg = string.Empty;
               
                Console.WriteLine("\n\nDealing Hands...\n");

                // Starts the round and deals players their hands.
                game.BeginRound();

                Console.WriteLine(game.ShowHands());

                // Retrieves the winner/winners as a formated message.
                msg = game.GetRoundWinner();
                Console.WriteLine(msg);

                Console.WriteLine("Would you like to play another hand? (y/n):");
                input = Console.ReadLine().Trim().ToUpper();

                // Checks its user would like to play again.
                while (input != "Y" && input != "N")
                {
                    Console.WriteLine("Invalid entry please type (y/n):");
                    input = Console.ReadLine().Trim().ToUpper();
                }

                if (input.Trim().ToUpper().Equals("N"))
                {
                    playAgain = false;
                }
                else
                {
                    Console.WriteLine("\n\n\n\nNew Round!\n\n\n\n");
                }
            }

            Console.WriteLine("\n\nThank you for playing! press any key to close");
            Console.ReadKey();
        }
    }
}
