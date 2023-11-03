using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJU23v_D10_inl_sveng {
    internal class Commands {
        public static void LoadCommand(string[] args) {
            string file = Program.DefaultFile;
            if (args.Length > 1) file = @"dict\" + args[1];
            using (StreamReader sr = new StreamReader(file)) {
                Program.Dictionary = new List<SweEngGloss>(); // Empty it!
                string line;
                while((line = sr.ReadLine()) != null) {
                    SweEngGloss gloss = new SweEngGloss(line);
                    Program.Dictionary.Add(gloss);
                }
            }
        }

        public static void ListCommand(string[] args) {
            foreach (SweEngGloss gloss in Program.Dictionary) {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }

        public static void NewCommand(string[] args) {
            if(args.Length == 2) { Console.WriteLine("'new' does not take two arguments. Correct usage: new, new [swedish word] [english word]"); return; }
            if (args.Length > 2) {
                Program.Dictionary.Add(new SweEngGloss(args[1], args[2]));
                return;
            }
            string PrintGet(string text) {
                Console.Write(text);
                return Console.ReadLine();
            }

            Program.Dictionary.Add(new SweEngGloss(PrintGet("Write word in Swedish: "), PrintGet("Write word in English: ")));
        }

        public static void DeleteCommand(string[] args) {
            if(Program.Dictionary.Count == 0) { Console.WriteLine("Dictionary list is empty."); return; }
            if (args.Length == 2) { Console.WriteLine("'delete' does not take two arguments. Correct usage: delete, delete [swedish word] [english word]"); return; }

            string word_swe, word_eng;
            if(args.Length > 2) {
                word_swe = args[1]; word_eng = args[2];
            } else {
                Console.Write("Write word in Swedish: ");
                word_swe = Console.ReadLine();
                Console.Write("Write word in English: ");
                word_eng = Console.ReadLine();
            }
            SweEngGloss[] glossaries = Program.Dictionary.Where(g => g.word_swe == word_swe && g.word_eng == word_eng).ToArray();
            if (glossaries.Length == 0) {
                Console.WriteLine($"No result \"{word_swe}\" \"{word_eng}\" found.");
            } else {
                foreach (SweEngGloss glossary in glossaries) {
                    Program.Dictionary.Remove(glossary);
                }
            }
        }

        public static void TranslateCommand(string[] args) {
            string word = "";
            if (args.Length > 1) {
                word = args[1];
            } else {
                Console.Write("Write word to be translated: ");
                word = Console.ReadLine();
            }

            foreach (SweEngGloss gloss in Program.Dictionary) {
                if (gloss.word_swe == word)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == word)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        public static void HelpCommand(string[] args) {
            Console.WriteLine(
                "Commands:\n" +
                "load - Load default file.\n" +
                "load [filename] - Load file from dict/\n" +
                "list - Show all translations.\n" +
                "new - Add a new translation to the dictionary.\n" +
                "new [swedish word] [english word]\n" +
                "delete - Delete set of words from the dictionary.\n" +
                "delete [swedish word] [english word]\n" +
                "translate - Translate a word+\n" +
                "translate [word]"
                );
        }
    }
}
