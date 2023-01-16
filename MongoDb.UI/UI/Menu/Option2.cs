namespace MongoDb.UI.UI.Menu
{
    public static class Option2
    {
        public static void Build()
        {
            Console.Clear();
            Console.Write("\tReading item data.\n\n" +
                "\t1) Input item search criteria\n" +
                "\t2) Cancel\n\n" +
                "\tPlease select: ");
        }

        public static async Task Selection(DbManager db)
        {
            int selection = InputManager.Selection(2);
            switch (selection)
            {
                case 1:
                    await SubSelect(db);
                    break;

                case 2:
                    Console.Clear();
                    Console.Write("\tReturning to main menu.");
                    break;

                default:
                    break;
            }
        }

        private static async Task SubSelect(DbManager db)
        {
            Console.Clear();
            Console.Write("\t1) Search by ID\n" +
                "\t2) Search by item Name\n" +
                "\t3) Cancel\n\n" +
                "\tPlease select: ");

            int selection = InputManager.Selection(3);
            switch (selection)
            {
                case 1:
                    Console.Clear();
                    Console.Write("\tPlease enter item ID." +
                        "\tLeave empty to cancel: ");

                    string ID = Console.ReadLine();

                    if (ID.Length < 1)
                    {
                        Console.WriteLine("\tReturning to main menu.");
                        break;
                    }

                    string idResult = await db.ReadOneAsync("Id", ID);

                    if (idResult != null) Console.WriteLine(idResult);
                    else Console.Write("\tEntry not found. Returning to main menu.");
                    break;

                case 2:
                    Console.Clear();
                    Console.Write("\tPlease enter item Name." +
                        "\tLeave empty to cancel: ");

                    string name = Console.ReadLine();

                    if (name.Length < 1)
                    {
                        Console.WriteLine("\tReturning to main menu.");
                        break;
                    }
                    string nameResult = await db.ReadOneAsync("Name", name);

                    if (nameResult != null) Console.WriteLine(nameResult);
                    else Console.Write("\tEntry not found. Returning to main menu.");
                    break;

                case 3:
                    Console.Clear();
                    Console.Write("\tReturning to main menu.");
                    break;

                default:
                    break;
            }
        }
    }
}
