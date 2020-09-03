 namespace MusicStore.SDK.Extensions
{
    using System;
    using System.Collections.Generic;

    public sealed class ConsoleChainer
    {
        public ConsoleChainer Clear()
        {
            Console.Clear();
            return this;
        }

        public ConsoleChainer BreakLine()
        {
            Console.WriteLine();
            return this;
        }

        public ConsoleChainer PressToContinue()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            return this;
        }

        public ConsoleChainer SetTitle(string title)
        {
            Console.Title = title;
            return this;
        }

        public ConsoleChainer RetrieveInput(string text, out string input)
        {
            Console.Write(text);

            input = Console.ReadLine();

            return this;
        }

        public ConsoleChainer DisplayTextInRow(string text, string format = null)
        {
            Console.Write(format ?? $"{{0}}", text);
            return this;
        }

        public ConsoleChainer DisplayTextInColumn(string text, string format = null)
        {
            Console.WriteLine(format ?? $"{{0}}", text);
            return this;
        }

        public ConsoleChainer DisplayTextInRow(IEnumerable<string> text, string format = null)
        {
            foreach (var message in text)
                Console.Write(format ?? $"{{0}}", message);

            return this;
        }

        public ConsoleChainer DisplayTextInColumn(IEnumerable<string> text, string format = null)
        {
            foreach (var message in text)
                Console.WriteLine(format ?? $"{{0}}", message);

            return this;
        }

        public int ParseInputAsInteger(ref string text)
        {
            if (int.TryParse(text, out var result))
                return result;

            DisplayTextInColumn($"\nProvided parameter did not match any of the values. Will proceed with default value.");
            PressToContinue();
            return default;
        }
        public DateTime ParseInputAsDateTime(ref string text)
        {
            if (DateTime.TryParse(text, out var result))
                return result;
            return default;
        }
        public T ParseInputAsEnum<T>(ref string text) where T : struct
        {
            if (Enum.TryParse<T>(text, true, out var result))
                return result;

            DisplayTextInColumn($"\nProvided parameter did not match any of the values. Will proceed with default value.");
            PressToContinue();
            return default;
        }
    }
}
