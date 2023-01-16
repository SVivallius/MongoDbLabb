﻿namespace MongoDb.UI.UI.Menu;

public static class Option1
{
    public static void Build()
    {
        Console.Clear();
        Console.Write("\tCreate item selected.\n" +
            "\t1) Add\n" +
            "\t2) Cancel\n\n" +
            "\tSelect option: ");
    }

    public static async Task SelectAsync(DbManager db)
    {
        int selection = InputManager.Selection(2);
        switch (selection)
        {
            case 1:
                Console.Write("\tItem type: ");
                string name = Console.ReadLine();

                bool parsedPrice = false;
                float price;
                do
                {
                    Console.Write("\tItem price: ");
                    parsedPrice = float.TryParse(Console.ReadLine(), out price);
                    if (!parsedPrice) Console.WriteLine("\tInvalid input! Try again!");
                }
                while (!parsedPrice);

                Console.Write("\tItem Description: ");
                string description = Console.ReadLine();

                Item input = new Item(name, price, description);

                bool result = await db.CreateOneAsync(input);
                if (!result) Console.WriteLine("\tOperation failed. Please try again.");
                else Console.WriteLine("\tThe operation was successful!");

                break;

            case 2:
                Console.WriteLine("\tReturning to main menu.");
                break;

            default:
                break;
        }
    }
}