using Newtonsoft.Json;

namespace PoemCollectionApp
{
    public class Poem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }

        public Poem(string title, string author, int year, string text, string theme)
        {
            Title = title;
            Author = author;
            Year = year;
            Text = text;
            Theme = theme;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Year: {Year}, Theme: {Theme}\nText: {Text}";
        }
    }
}

namespace PoemCollectionApp
{
    public class PoemCollection
    {
        private List<Poem> poems = new List<Poem>();

        public void AddPoem(Poem poem)
        {
            poems.Add(poem);
        }

        public void DeletePoem(string title)
        {
            var poem = poems.FirstOrDefault(p => p.Title == title);
            if (poem != null)
            {
                poems.Remove(poem);
            }
        }

        public void UpdatePoem(string title, Poem updatedPoem)
        {
            var poem = poems.FirstOrDefault(p => p.Title == title);
            if (poem != null)
            {
                poem.Title = updatedPoem.Title;
                poem.Author = updatedPoem.Author;
                poem.Year = updatedPoem.Year;
                poem.Text = updatedPoem.Text;
                poem.Theme = updatedPoem.Theme;
            }
        }

        public List<Poem> SearchPoems(string searchTerm)
        {
            return poems.Where(p => p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                     p.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                     p.Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                     p.Theme.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SaveToFile(string filePath)
        {
            var json = JsonConvert.SerializeObject(poems, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                poems = JsonConvert.DeserializeObject<List<Poem>>(json);
            }
        }

        public void ListPoems()
        {
            foreach (var poem in poems)
            {
                Console.WriteLine(poem);
            }
        }
    }
}

namespace PoemCollectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PoemCollection collection = new PoemCollection();
            string filePath = "poems.json";

            while (true)
            {
                Console.WriteLine("\nPoem Collection Application");
                Console.WriteLine("1. Add Poem");
                Console.WriteLine("2. Delete Poem");
                Console.WriteLine("3. Update Poem");
                Console.WriteLine("4. Search Poems");
                Console.WriteLine("5. Save Collection to File");
                Console.WriteLine("6. Load Collection from File");
                Console.WriteLine("7. List All Poems");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPoem(collection);
                        break;
                    case "2":
                        DeletePoem(collection);
                        break;
                    case "3":
                        UpdatePoem(collection);
                        break;
                    case "4":
                        SearchPoems(collection);
                        break;
                    case "5":
                        collection.SaveToFile(filePath);
                        Console.WriteLine("Collection saved to file.");
                        break;
                    case "6":
                        collection.LoadFromFile(filePath);
                        Console.WriteLine("Collection loaded from file.");
                        break;
                    case "7":
                        collection.ListPoems();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddPoem(PoemCollection collection)
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Enter text: ");
            string text = Console.ReadLine();
            Console.Write("Enter theme: ");
            string theme = Console.ReadLine();

            Poem poem = new Poem(title, author, year, text, theme);
            collection.AddPoem(poem);
            Console.WriteLine("Poem added.");
        }

        static void DeletePoem(PoemCollection collection)
        {
            Console.Write("Enter the title of the poem to delete: ");
            string title = Console.ReadLine();
            collection.DeletePoem(title);
            Console.WriteLine("Poem deleted.");
        }

        static void UpdatePoem(PoemCollection collection)
        {
            Console.Write("Enter the title of the poem to update: ");
            string title = Console.ReadLine();
            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Enter new author: ");
            string newAuthor = Console.ReadLine();
            Console.Write("Enter new year: ");
            int newYear = int.Parse(Console.ReadLine());
            Console.Write("Enter new text: ");
            string newText = Console.ReadLine();
            Console.Write("Enter new theme: ");
            string newTheme = Console.ReadLine();

            Poem updatedPoem = new Poem(newTitle, newAuthor, newYear, newText, newTheme);
            collection.UpdatePoem(title, updatedPoem);
            Console.WriteLine("Poem updated.");
        }

        static void SearchPoems(PoemCollection collection)
        {
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine();
            var results = collection.SearchPoems(searchTerm);
            Console.WriteLine("Search results:");
            foreach (var poem in results)
            {
                Console.WriteLine(poem);
            }
        }
    }
}

