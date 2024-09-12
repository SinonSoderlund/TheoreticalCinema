namespace TheoreticalCinema
{
    using UtilitySpace;
    internal class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {
                UtilSpace.ShowMainMenu();
                switch ((MenuSelector)UtilSpace.GetIntSilent("Number"))
                {
                    case MenuSelector.TicketFinder:
                        UtilSpace.TicketFinder();
                        break;
                    case MenuSelector.PartyOrganizer:
                        UtilSpace.PartyOrganizer();
                        break;
                    case MenuSelector.TheRepeater:
                        UtilSpace.TheRepeater();
                        break;
                        case MenuSelector.ThirdExtractor:
                            UtilSpace.ThirdExtractor();
                        break;
                    case MenuSelector.Exit:
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("\nUnrecognized input, press enter to continue and try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
