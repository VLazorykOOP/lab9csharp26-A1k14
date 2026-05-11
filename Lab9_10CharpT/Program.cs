using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Lab9_10CharpT {
    public class Lab9Task1 {
        public void Run() {
            // Task 1.9
            string formula = "m(9,p(p(3,5),m(3,8)))";
            Console.WriteLine($"Формула: {formula}");
            try {
                int res = Evaluate(formula);
                Console.WriteLine($"Результат: {res}");
            } catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
            // Task 1.9 end
        }

        private int Evaluate(string expr) {
            Stack<char> ops = new Stack<char>();
            Stack<int> vals = new Stack<int>();
            for (int i = 0; i < expr.Length; i++) {
                char c = expr[i];
                if (c == 'm' || c == 'p') ops.Push(c);
                else if (char.IsDigit(c)) vals.Push(c - '0');
                else if (c == ')') {
                    if (vals.Count >= 2 && ops.Count > 0) {
                        int b = vals.Pop(), a = vals.Pop();
                        char op = ops.Pop();
                        vals.Push(op == 'm' ? ((a - b) % 10 + 10) % 10 : (a + b) % 10);
                    }
                }
            }
            return vals.Count > 0 ? vals.Pop() : 0;
        }
    }

    public class Lab9Task2 {
        public void Run() {
            // Task 2.9
            string[] data = { "Іванов 5 4 5", "Петров 3 2 4", "Сидоров 4 4 4" };
            Queue<string> queue = new Queue<string>();
            foreach (var s in data) queue.Enqueue(s);

            Console.WriteLine("Студенти, що здали сесію:");
            while (queue.Count > 0) {
                string s = queue.Dequeue();
                var parts = s.Split(' ');
                bool success = true;
                for (int i = 1; i < parts.Length; i++) if (int.Parse(parts[i]) < 4) success = false;
                if (success) Console.WriteLine(s);
            }
            // Task 2.9 end
        }
    }

    public class Lab9Task3 {
        public void Run() {
            // Task 3
            ArrayList list = new ArrayList { "Іванов 5 4 5", "Петров 3 2 4", "Сидоров 4 4 4" };
            Console.WriteLine("Успішні студенти (ArrayList):");
            foreach (string s in list) {
                var parts = s.Split(' ');
                bool success = true;
                for (int i = 1; i < parts.Length; i++) if (int.Parse(parts[i]) < 4) success = false;
                if (success) Console.WriteLine(s);
            }
            // Task 3 end
        }
    }

    public class CD {
        public string Title { get; set; }
        public string Artist { get; set; }
        public List<string> Songs { get; set; } = new List<string>();

        public CD(string title, string artist) {
            Title = title;
            Artist = artist;
        }

        public override string ToString() {
            return $"{Artist} - {Title} ({Songs.Count} пісень)";
        }
    }

    public class Lab9Task4 {
        private Hashtable catalog = new Hashtable();

        public void AddDisk(string title, string artist) {
            if (!catalog.ContainsKey(title)) {
                catalog[title] = new CD(title, artist);
            }
        }

        public void RemoveDisk(string title) {
            catalog.Remove(title);
        }

        public void AddSong(string diskTitle, string songTitle) {
            if (catalog.ContainsKey(diskTitle)) {
                ((CD)catalog[diskTitle]!).Songs.Add(songTitle);
            }
        }

        public void RemoveSong(string diskTitle, string songTitle) {
            if (catalog.ContainsKey(diskTitle)) {
                ((CD)catalog[diskTitle]!).Songs.Remove(songTitle);
            }
        }

        public void PrintCatalog() {
            Console.WriteLine("--- Весь каталог ---");
            foreach (DictionaryEntry entry in catalog) {
                Console.WriteLine(entry.Value);
            }
        }

        public void PrintDisk(string title) {
            if (catalog.ContainsKey(title)) {
                CD cd = (CD)catalog[title]!;
                Console.WriteLine($"Диск: {cd.Artist} - {cd.Title}");
                foreach (var song in cd.Songs) Console.WriteLine($"  * {song}");
            }
        }

        public void SearchByArtist(string artist) {
            Console.WriteLine($"--- Пошук за виконавцем: {artist} ---");
            foreach (DictionaryEntry entry in catalog) {
                CD cd = (CD)entry.Value!;
                if (cd.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine(cd);
                }
            }
        }

        public void Run() {
            Console.WriteLine("--- Завдання 4 (Каталог дисків) ---");
            AddDisk("Meteora", "Linkin Park");
            AddDisk("Mutter", "Rammstein");
            AddDisk("Hybrid Theory", "Linkin Park");

            AddSong("Meteora", "Somewhere I Belong");
            AddSong("Meteora", "Numb");
            AddSong("Mutter", "Sonne");

            PrintCatalog();
            Console.WriteLine();
            PrintDisk("Meteora");
            Console.WriteLine();
            SearchByArtist("Linkin Park");

            Console.WriteLine("\nВидаляємо 'Numb' з Meteora та весь диск Mutter...");
            RemoveSong("Meteora", "Numb");
            RemoveDisk("Mutter");
            PrintCatalog();
            PrintDisk("Meteora");
        }
    }

    class Program {
        static void Main() {
            Console.OutputEncoding = Encoding.UTF8;
            new Lab9Task1().Run();
            new Lab9Task2().Run();
            new Lab9Task3().Run();
            new Lab9Task4().Run();
            Console.ReadKey();
        }
    }
}
