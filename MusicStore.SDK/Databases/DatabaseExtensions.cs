using MusicStore.SDK.Enumerations;
using MusicStore.SDK.Extensions;
using System;
using System.Linq;
using System.Text.Json;

namespace MusicStore.SDK.Databases
{
    public static class DatabaseExtensions
    {
        private static readonly ConsoleChainer Chainer = new ConsoleChainer();
        public static Database DisplayProducts(this Database database)
        {
            DisplayHeading();
            uint i = 1;
            foreach (var product in database.AvailableProducts)
            {
                Console.WriteLine(i + ". " + product.ToString());
                i++;
            }
            return database;
        }
        public static Database DisplaySorted(this Database database)
        {
            DisplayHeading();
            uint i = 1;
            foreach (var product in database.AvailableProducts.OrderBy(x => x.Price).ToList())
            {
                Console.WriteLine(i + ". " + product.ToString());
                i++;
            }
            return database;
        }
        public static Database DisplaySortedDescending(this Database database)
        {
            DisplayHeading();
            uint i = 1;
            foreach (var product in database.AvailableProducts.OrderByDescending(x => x.Price).ToList())
            {
                Console.WriteLine(i + ". " + product.ToString());
                i++;
            }
            return database;
        }
        public static Database DisplayAlphabetically(this Database database)
        {
            DisplayHeading();
            uint i = 1;
            foreach (var product in database.AvailableProducts.OrderBy(x => x.ModelName).ToList())
            {
                Console.WriteLine(i + ". " + product.ToString());
                i++;
            }
            return database;
        }
        public static Database DisplayAlphabeticallyDescending(this Database database)
        {
            DisplayHeading();
            uint i = 1;
            foreach (var product in database.AvailableProducts.OrderByDescending(x => x.ModelName).ToList())
            {
                Console.WriteLine(i++ + ". " +product);
            }
            return database;
        }
        public static Database AddNewProduct(this Database database)
        {
            SetProductsParameters(out string modelName, out Instruments instrument, out int price, out Brands brand);
            database.AvailableProducts.Add(new Product(brand, modelName, instrument, price, false, default));
            return database;
        }

        private static void SetProductsParameters(out string modelName, out Instruments instrument, out int price, out Brands brand)
        {
            Chainer
                .RetrieveInput("Enter the model name: ", out modelName)
                .RetrieveInput("Enter product's price: ", out string priceString)

                .DisplayTextInColumn("Enter the brand name: ")
                .RetrieveInput($"\nAvailable brands: { string.Join(" / ", typeof(Brands).GetEnumNames())} ", out string brandString)

                .DisplayTextInColumn("Enter the instrument type: ")
                .RetrieveInput($"\nAvailable types: { string.Join(" / ", typeof(Instruments).GetEnumNames())} ", out string instrumentString);
            instrument = Chainer.ParseInputAsEnum<Instruments>(ref instrumentString);
            price = Chainer.ParseInputAsInteger(ref priceString);
            brand = Chainer.ParseInputAsEnum<Brands>(ref brandString);
        }

        public static Database DeleteProduct(this Database database)
        {
            int parsedInput = SelectProduct();
            if (parsedInput <= database.AvailableProducts.Count && parsedInput >= 1)
            {
                database.AvailableProducts.RemoveAt(--parsedInput);
            }
            else
            {
                OutOfRangeSelection();
            }
            return database;
        }
        public static Database DeleteReservation(this Database database)
        {
            int parsedInput = SelectProduct();
            if (parsedInput <= database.AvailableProducts.Count && parsedInput >= 1)
                database.AvailableProducts[--parsedInput].DeleteReservation();
            else
                OutOfRangeSelection();
            return database;
        }

        public static Database Reservation(this Database database)
        {
            int parsedInput = SelectProduct();
            if (parsedInput <= database.AvailableProducts.Count && parsedInput >= 1)
            {
                database.AvailableProducts[--parsedInput].SetReservationDate().ConfirmReservation();
            }
            else
                OutOfRangeSelection();
            return database;
        }
        private static int SelectProduct()
        {
            Chainer.RetrieveInput("Which product to you want to select : ", out string input);
            int output = Chainer.ParseInputAsInteger(ref input);
            return output;
        }
        private static void OutOfRangeSelection()
        {
            _ = new ConsoleChainer()
                .DisplayTextInRow("There is no such product ")
                .PressToContinue();
        }
        public static Database Filter(this Database database)
        {
            Console.WriteLine("Filter by: ");
            Console.WriteLine("1. Instruments");
            Console.WriteLine("2. Brands");

            string input = Console.ReadLine();
            int parsedInput = Chainer.ParseInputAsInteger(ref input);

            switch (parsedInput)
            {
                case 1:
                    FilterByInstruments(database);
                    break;
                case 2:
                    FilterByBrands(database);
                    break;
                default:
                    Console.WriteLine("Wrong input. Try again");
                    Filter(database);
                    break;
            }
            PressToClear();
            return database;
        }
        private static Database FilterByBrands(this Database database)
        {
            Chainer.RetrieveInput($"\nWhich brand do you want to see?: " +
                $"{ string.Join(" / ", typeof(Brands).GetEnumNames())} ", out string brandString);
            var brand = Chainer.ParseInputAsEnum<Brands>(ref brandString);
            int i = 1;
            foreach (var product in database.AvailableProducts.FindAll(x => x.Brand == brand))
            {
                Console.WriteLine(i + ". " + product.ToString());
                i++;
            }
            return database;
        }
        public static Database FilterByInstruments(this Database database)
        {
            Chainer.RetrieveInput($"\nWhich instrument do you want to see?: " +
                $"{ string.Join(" / ", typeof(Instruments).GetEnumNames())}", out string instrumentString);
            var instrument = Chainer.ParseInputAsEnum<Instruments>(ref instrumentString);
            int i = 1;
            foreach (var product in database.AvailableProducts.FindAll(x => x.Instrument == instrument))
            {
                Console.WriteLine(i + ". " + product.ToString());
                i++;
            }
            return database;
        }
        public static void PressToClear()
        {
            Console.WriteLine("Press any key to clear ");
            Console.ReadKey();
            Console.Clear();
        }

        private static void DisplayHeading()
        {
            Console.WriteLine("  Model\t\tBrand\t\tType\t\tPrice\t\tReservation\tReservation date");
        }
        public static Database Clear(this Database database)
        {
            Console.Clear();
            return database;
        }
        public static Database SaveToFile(this Database database)
        {
            System.IO.File.WriteAllText(@"C:\Users\Kuba\Desktop\MusicStore\data.txt", 
                JsonSerializer.Serialize(database.AvailableProducts));
            return database;
        }
    }
}
