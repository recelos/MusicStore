using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MusicStore.SDK.Databases
{
    public class Database
    {
        public Database() => RetrieveData();
        public Database RetrieveData()
        {
            string productsString = System.IO.File.ReadAllText(@"C:\Users\Kuba\Desktop\MusicStore\data.txt", Encoding.UTF8);
            availableProducts = JsonSerializer.Deserialize<List<Product>>(productsString);
            return this;
        }

        public List<Product> availableProducts = new List<Product>
        {

        };
        public List<Product> AvailableProducts { get => availableProducts; set => availableProducts = value; }

    }
}
