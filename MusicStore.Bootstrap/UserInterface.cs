using System;
using MusicStore.SDK.Databases;
using MusicStore.SDK.Extensions;

namespace MusicStore.Bootstrap
{
    public sealed class UserInterface
    {

        public Database database = new Database();
        private static readonly ConsoleChainer chainer = new ConsoleChainer();
        bool IsDisplayedSorted = false;
        bool IsDisplayedAlphabetically = false;
        public UserInterface ControlPanel()
        {
            Console.WriteLine();
            Console.WriteLine("Press: ");
            Console.WriteLine("1 to Sort by price");
            Console.WriteLine("2 to Sort alphabetically");
            Console.WriteLine("3 to Add a new product");
            Console.WriteLine("4 to Delete a product");
            Console.WriteLine("5 to Make a reservation");
            Console.WriteLine("6 to Delete a reservation");
            Console.WriteLine("7 to Filter");
            Console.WriteLine("8 to Exit");
            Console.WriteLine("Any key to Display in default order");
            return this;
        }

        public UserInterface Run()
        {

            chainer.RetrieveInput("", out string input);
            int parsedInput = chainer.ParseInputAsInteger(ref input);
            switch (parsedInput)
            {
                case 1 :
                    if (!IsDisplayedSorted)
                    {
                        database
                            .Clear()
                            .DisplaySorted();
                        IsDisplayedAlphabetically = false;
                        IsDisplayedSorted = true;
                    }
                    else
                    {
                        database
                            .Clear()
                            .DisplaySortedDescending();
                        IsDisplayedAlphabetically = false;
                        IsDisplayedSorted = false;
                    }
                    break;
                case 2:
                    if(!IsDisplayedAlphabetically)
                    {
                        database
                            .Clear()
                            .DisplayAlphabetically();
                        IsDisplayedAlphabetically = true;
                        IsDisplayedSorted = false;
                    }
                    else
                    {
                        database
                            .Clear()
                            .DisplayAlphabeticallyDescending();
                        IsDisplayedAlphabetically = false;
                        IsDisplayedSorted = false;
                    }
                    break;
                case 3:
                    database
                        .AddNewProduct()
                        .Clear()
                        .DisplayProducts()
                        .SaveToFile();
                    IsDisplayedAlphabetically = false;
                    IsDisplayedSorted = false;
                    break;
                case 4:
                    database
                        .Clear()
                        .DisplayProducts()
                        .DeleteProduct()
                        .SaveToFile()
                        .Clear()
                        .DisplayProducts();
                    IsDisplayedAlphabetically = false;
                    IsDisplayedSorted = false;
                    break;
                case 5:
                    database
                        .Clear()
                        .DisplayProducts()
                        .Reservation()
                        .SaveToFile()
                        .Clear()
                        .DisplayProducts();
                    IsDisplayedAlphabetically = false;
                    IsDisplayedSorted = false;
                    break;
                case 6:
                    database
                        .Clear()
                        .DisplayProducts()
                        .DeleteReservation()
                        .SaveToFile()
                        .Clear()
                        .DisplayProducts();
                    IsDisplayedAlphabetically = false;
                    IsDisplayedSorted = false;
                    break;
                case 7:
                    database
                        .Filter()
                        .DisplayProducts();
                    IsDisplayedAlphabetically = false;
                    IsDisplayedSorted = false;
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
                default:
                    database
                        .Clear()
                        .DisplayProducts();
                    IsDisplayedAlphabetically = false;
                    IsDisplayedSorted = false;
                    break;
            }
            ControlPanel();
            Run();
            return this;
        }
    }
}
