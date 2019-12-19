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
            #region variables
            int numberOfPLayers = 0;
            string input = string.Empty;
            bool isValidNumberOfPLayers = false;
            List<Player> players = new List<Player>();
            #endregion

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Ask user how many players will be playing
            Console.WriteLine("Welcome to Poker Hand Showdown, how many useres will be playing?(2-6) :");
            input = Console.ReadLine();

            // Check to ensure user entered a number between 2 and 6
            while (!isValidNumberOfPLayers)
            {
                int.TryParse(input, out numberOfPLayers);
                if (numberOfPLayers <= 1 || numberOfPLayers > 6)
                {
                    Console.WriteLine("Entered invlaid amount of players try again (2-6): ");
                    input = Console.ReadLine();
                }
                else
                {
                    isValidNumberOfPLayers = true;
                }
            }

            // Create players
            for (int i = 1; i <= numberOfPLayers; i++)
            {
                bool validUserName = false;
                string username = string.Empty;

                Console.WriteLine($"Please enter player{i}'s username");

                while (!validUserName)
                {
                    username = Console.ReadLine().Trim();
                    if(players.Count(x => x.Name.ToUpper() == username.ToUpper()) == 1)
                    {
                        Console.WriteLine($"User name already taken please enter player{i}'s username");
                    }
                    else
                    {
                        validUserName = true;
                    }
                }
                Player player = new Player(username);
                players.Add(player);
            }

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            bool playAgain = true;
            while (playAgain)
            {
                string msg = string.Empty;
               
                Console.WriteLine("\n\nDealing Hands...\n");
                game.BeginRound();
                Console.WriteLine(game.ShowHands());

                msg = game.GetRoundWinner();

                Console.WriteLine(msg);
                Console.WriteLine("Would you like to play another hand? (y/n):");
                input = Console.ReadLine().Trim().ToUpper();
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
            ICheckCards s = new Player("name");
            Console.WriteLine("\n\nThank you for playing! press any key to close");
            Console.ReadKey();
        }
    }
}
