using System;
using System.Collections.Generic;
using ConsoleTableExt;

namespace DrinksInfo
{
    public class userInput
    {
        public static void MainMenu() {
            Console.Clear();

            Console.WriteLine("Main Menu");

            Console.Write("\n");

            var MenuList = Controller.GetCatList();

                ConsoleTableBuilder
                .From(MenuList)
                .WithFormat(ConsoleTableBuilderFormat.Minimal)
                .WithColumn("Drinks Categories")
                .ExportAndWriteLine();

            Console.Write("\n");

            Console.WriteLine("\nEnter a category number from above to get started, or press 0 to close the app\n");

            int NumChoice = GetListItem(MenuList);

            for(int i = 0; i < MenuList.Count + 1; i++) {
                if(i == NumChoice) {
                    string CatListName = MenuList[i - 1].MenuItem;

                    DrinkMenu(CatListName);
                }
            }
        }

        public static void DrinkMenu(string DrinkListChoice) {

            var catItems = Controller.GetCatItems(DrinkListChoice.Replace(" ", "_"));

            Console.Clear();

            ConsoleTableBuilder
            .From(catItems)
            .WithFormat(ConsoleTableBuilderFormat.Minimal)
            .WithColumn("Drinks List")
            .ExportAndWriteLine();

            Console.WriteLine("\nEnter a drink number from above to get started, or press 0 to close the app\n");

            int NumChoice = GetListItem(catItems);

            for(int i = 0; i < catItems.Count + 1; i++) {
                if(i == NumChoice) {
                    string DrinkName = catItems[i - 1].MenuItem;

                    GetDrink(DrinkName.Replace(" ", "_"), catItems);
                }
            }
        }

        public static void GetDrink(string DrinkName, List<MenuList> List) {



        }

        public static int GetListItem(List<MenuList> List) {
            string StringChoice = Console.ReadLine();

            int NumChoice = List.Count + 1;

            bool ParseSuccess = Int32.TryParse(StringChoice, out NumChoice);
           
            while(ParseSuccess != true || NumChoice > List.Count) {
                Console.WriteLine($"Invalid Number, please Select a number between 1 and {List.Count}, or press 0 to close the app.");

                StringChoice = Console.ReadLine();

                ParseSuccess = Int32.TryParse(StringChoice, out NumChoice);
            }

            if(NumChoice == 0) {
                Console.Clear();

                Console.WriteLine("Goodbye..");

                System.Environment.Exit(1);
            }

            return NumChoice;
        }
    }
}