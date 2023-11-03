namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        public static List<SweEngGloss> Dictionary = new List<SweEngGloss>(); // TODO: Hitta ett bättre namn?
        public static string DefaultFile = @"dict\sweeng.lis";
        static void Main(string[] args)
        {
            string command = "";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split(); // Nämn om till 'arguments' och ta bort första indexet.
                command = argument[0];
                switch (command.ToLower()) {
                    case "load": Commands.LoadCommand(argument); break;
                    case "list": Commands.ListCommand(argument); break;
                    case "new": Commands.NewCommand(argument); break;
                    case "delete": Commands.DeleteCommand(argument); break;
                    case "translate": Commands.TranslateCommand(argument); break;
                    case "help": Commands.HelpCommand(argument); break;
                    default:
                        Console.WriteLine($"Unknown command: '{command}'");
                        break;
                }
            }
            while (command.ToLower() != "quit" );
        }
    }
    public class SweEngGloss {
        public string word_swe, word_eng;
        public SweEngGloss(string word_swe, string word_eng) {
            this.word_swe = word_swe; this.word_eng = word_eng;
        }
        public SweEngGloss(string line) {
            string[] words = line.Split('|');
            this.word_swe = words[0]; this.word_eng = words[1];
        }
    }
}