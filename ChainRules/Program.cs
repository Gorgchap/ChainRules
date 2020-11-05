namespace ChainRules
{
    class Program
    {
        private static Logic logic;

        static void Main(string[] args)
        {
            string input = new System.IO.StreamReader(args.Length > 0 ? args[0] : "01.txt").ReadToEnd();
            string[] items = input.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            string rules = "";
            for (int i = 2; i < items.Length; i++)
                rules += items[i] + '\n';
            logic = new Logic(items[0], items[1], rules);
            System.Console.WriteLine("Множество терминальных символов: " + items[0]);
            System.Console.WriteLine("Множество нетерминальных символов: " + items[1]);
            System.Console.WriteLine("Множество правил:");
            foreach (string rule in logic.GetRules())
                System.Console.WriteLine(rule);
            
            System.Console.WriteLine("\nПосле удаления цепных правил:");
            foreach (string rule in logic.Transform())
                System.Console.WriteLine(rule);

            System.Console.WriteLine("\nДля завершения нажмите любую клавишу...");
            System.Console.ReadKey();
        }
    }
}
