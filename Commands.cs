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
            if (args.Length == 3) {
                int index = -1;
                for (int i = 0; i < Program.Dictionary.Count; i++) { // TODO: Förkorta.
                    SweEngGloss gloss = Program.Dictionary[i];
                    if (gloss.word_swe == args[1] && gloss.word_eng == args[2])
                        index = i;
                }
                Program.Dictionary.RemoveAt(index);
            } else if (args.Length == 1) {
                // TODO: Kolla om listan är tom.
                Console.WriteLine("Write word in Swedish: ");
                string s = Console.ReadLine();
                Console.Write("Write word in English: ");
                string e = Console.ReadLine();
                int index = -1;
                for (int i = 0; i < Program.Dictionary.Count; i++) // TODO: Förkorta.
                {
                    SweEngGloss gloss = Program.Dictionary[i];
                    if (gloss.word_swe == s && gloss.word_eng == e)
                        index = i;
                }
                Program.Dictionary.RemoveAt(index); // FIXME: Checka om out-of-bounds först.
            }
        }

        public static void TranslateCommand(string[] args) {
            if (args.Length == 2) {
                foreach (SweEngGloss gloss in Program.Dictionary) {
                    if (gloss.word_swe == args[1])
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == args[1])
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            } else if (args.Length == 1) {
                Console.WriteLine("Write word to be translated: ");
                string s = Console.ReadLine();
                foreach (SweEngGloss gloss in Program.Dictionary) {
                    if (gloss.word_swe == s)
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == s)
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
        }
    }
}
