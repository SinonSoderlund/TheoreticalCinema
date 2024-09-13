using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheoreticalCinema
{
    namespace UtilitySpace
    {

        public enum MenuSelector
        {
            TicketFinder = 1,
            PartyOrganizer = 2,
            TheRepeater = 3,
            ThirdExtractor = 4,
            Exit = 0
        }

        public enum TicketTypes
        {
            Free = 0,
            Youth = 80,
            Retiree = 90,
            Normal = 120
        }
        public static class UtilSpace
        {
            private static readonly int REPEATER_TARGET = 11;
            /// <summary>
            /// Function to request and retreive desired string type data from a user via console
            /// </summary>
            /// <param name="RequestedInput">The type of input that is desired from the user</param>
            /// <param name="acceptBlank">Controlls if a blank or otherwise empty return string is accepted, default no</param>
            /// <returns></returns>
            public static string GetInputString(string RequestedInput, bool acceptBlank = false)
            {
                return GetInputString(RequestedInput, string.Empty, acceptBlank);
            }
            /// <summary>
            /// Function to request and retreive desired string type data from a user via console
            /// </summary>
            /// <param name="RequestedInput">The type of input that is desired from the user</param>
            /// <param name="additionalPrompt">Preface text to give additional context to the thing being requested, for example if age is requested then a suitable additionalPrompt me be 'please enter your'</param>
            /// <param name="acceptBlank">Controlls if a blank or otherwise empty return string is accepted, default no</param>
            /// <returns></returns>
            public static string GetInputString(string RequestedInput, string additionalPrompt, bool acceptBlank = false)
            {
                if(string.IsNullOrEmpty(additionalPrompt))
                    Console.WriteLine(RequestedInput);
                else
                    Console.WriteLine($"{additionalPrompt} {RequestedInput}");
                if (!acceptBlank)
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input))
                            return input;
                        else
                            ValidRequestor(RequestedInput);
                    }
                else
                    return Console.ReadLine();
            }
            /// <summary>
            /// Runs GetInputString And then converts it into a valid int
            /// </summary>
            /// <param name="RequestedInput">The type of input that is desired from the user</param>
            /// <returns></returns>
            public static int GetInputInt(string RequestedInput)
            {
                return GetInputInt(RequestedInput, string.Empty);
            }
            /// <summary>
            ///  Runs GetInputString And then converts it into a valid int
            /// </summary>
            /// <param name="RequestedInput">The type of input that is desired from the user</param>
            /// <param name="additionalPrompt">Preface text to give additional context to the thing being requested, for example if age is requested then a suitable additionalPrompt me be 'please enter your'</param>
            /// <returns></returns>
            public static int GetInputInt(string RequestedInput, string additionalPrompt)
            {
                while (true)
                {
                    int input;
                    if (int.TryParse(GetInputString(RequestedInput, additionalPrompt), out input))
                    {
                        return input;
                    }
                    else
                        ValidRequestor(RequestedInput);
                }
            }
            /// <summary>
            /// Function for getting an int without an initial prompt
            /// </summary>
            /// <param name="RequestedInput">the input that was requested, only displayed if the user inputs a non-int</param>
            /// <returns></returns>
            public static int GetIntSilent(string RequestedInput)
            {
                while (true)
                {
                    int input;
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        return input;
                    }
                    else
                        ValidRequestor(RequestedInput);
                }
            }
            /// <summary>
            /// Short function that displays a request for the user to input a valid value
            /// </summary>
            /// <param name="RequestedInput">The type of input that is desired from the user</param>
            public static void ValidRequestor(string RequestedInput)
            {
                Console.WriteLine($"Please input a valid {RequestedInput}");
            }
            /// <summary>
            /// Runs a switch expression the assign a ticket type based on age
            /// </summary>
            /// <param name="age">age of participant</param>
            /// <returns></returns>
            private static TicketTypes TicketDecider(int age)
            {
                TicketTypes ticketType = age switch
                {
                    < 5 => TicketTypes.Free,
                    >= 5 and < 20 => TicketTypes.Youth,
                    >= 20 and <= 64 => TicketTypes.Normal,
                    > 64 and <= 100 => TicketTypes.Retiree,
                    > 100 => TicketTypes.Free
                };
                return ticketType;
            }
            /// <summary>
            /// Requests user age, deduces user ticket type and prints information to console.
            /// </summary>
            public static void TicketFinder()
            {
                TicketTypes yourTicket = TicketDecider(GetInputInt("Age", "Please enter your"));
                Console.WriteLine($"Your ticket type is {yourTicket}, which costs {((int)yourTicket)} sek.");
                WaitToContinue();
            }
            /// <summary>
            /// Function that asks how many people will be in the party, then iterates over that number asking for their ages, acesseing their tickets types to add cost to total, then prints number of people and total.
            /// </summary>
            public static void PartyOrganizer()
            {
                int total = 0;
                int nrOfPeople = GetInputInt("Number of people in your party", "Please enter the");
                for (int i = 0; i < nrOfPeople; i++)
                {
                    total += ((int)TicketDecider(GetInputInt("age", $"For person nr {i + 1} please enter their")));
                }
                Console.WriteLine($"\nNumber of people in your party: {nrOfPeople}\nTotal cost for cinema visit: {total}.");
                WaitToContinue();
            }
            /// <summary>
            /// Informs the user of the ffunctioning of the repeater, then takes a user input and repeats it 10 times on the same line with ennumeration
            /// </summary>
            public static void TheRepeater()
            {
                Console.WriteLine("Welcome to the repeater! Here you words (or non-words) will get repeated 10 times!");
                string echo = GetInputString("Text!", "Please enter an arbitrary line of");
                StringBuilder output = new StringBuilder();
                for (int i = 1; i < REPEATER_TARGET; i++)
                {
                    output.Append($"{i}. {echo}, ");
                }
                Console.WriteLine(output);
                WaitToContinue();
            }
            /// <summary>
            /// Figured out regex a bit, takes an input string, matches continues non-whitespace sections using regex, then checks if the matchescollection contains at least 3 elements, if it does, prints out the third one
            /// </summary>
            public static void ThirdExtractor()
            {
                Console.WriteLine("Welcome to the third extractor, where we extract the third word (or unintelligeble utterance) from your sentence, for as long as it has a minimum of threee words.");
                while (true)
                {
                    string input = GetInputString("Sentence", "Please input a valid");
                    MatchCollection matches = Regex.Matches(input, @"\S+");
                    if (matches.Count < 3)
                        Console.WriteLine("Please enter a sentence containing at least 3 words seperated by spaces.");
                    else
                    {
                        Console.WriteLine(matches[2].ToString());
                        break;
                    }
                }
                WaitToContinue();

            }
            /// <summary>
            /// Clears the console and shows the main menu text
            /// </summary>
            public static void ShowMainMenu()
            {
                Console.Clear();
                Console.WriteLine($"Welcome to Theoretical Cinema!\nWhat services would you like to make use of today?\n{((int)MenuSelector.TicketFinder)}: Ticket Finder - Which ticket price class are you egible for?\n{((int)MenuSelector.PartyOrganizer)}: Party Organizer - How much will it cost for your party to visit the cinema?\n{((int)MenuSelector.TheRepeater)}: The Repeater - As we all know cinemas normally have echo rooms, where your words get repeated, here is ours!\n{((int)MenuSelector.ThirdExtractor)}: Third Extractor - Ironically in 4th place is our third extractor, which extracts the 3rd word in a sentence, normal cinema stuff.\n{((int)MenuSelector.Exit)}: Exit program");
            }
            /// <summary>
            /// Simple function to stall program until user presses enter
            /// </summary>
            public static void WaitToContinue()
            {
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }
    }
}
