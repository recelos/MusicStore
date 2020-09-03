using MusicStore.SDK.Extensions;
using MusicStore.SDK.Databases;

namespace MusicStore.Bootstrap
{
    class Bootstrap
    {
        private static readonly ConsoleChainer Chainer = new ConsoleChainer();
        private static void Main()
        {
            Chainer.SetTitle("guitarcenter.com");

            Database database = new Database();

            database.RetrieveData()
                .DisplayProducts();

             _ = new UserInterface()
                .ControlPanel()
                .Run();
          
        }
    }
}
