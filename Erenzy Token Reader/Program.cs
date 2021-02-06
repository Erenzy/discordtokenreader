using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Erenzy_Token_Reader
{

    public class Program
    {
        public static void Main()
        {
            new DiscordToken();
            Console.ReadLine();
        }
    }

    public class DiscordToken
    {
        public DiscordToken()
        {
            GetToken();
        }

        public void GetToken()
        {
            Console.Title = "Erenzy Token Reader";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("Results will appear on tokens.txt");
            var files = SearchForFile(); // to get ldb files
            if (files.Count == 0)
            {
                Console.WriteLine("No .ldb file found!\nPlease put .ldb files same directory with token reader!");
                return;
            }
            foreach (string token in files)
            {
                foreach (Match match in Regex.Matches(token, "[^\"]*"))
                {
                    if (match.Length == 59)
                    {
                        Console.WriteLine($"Token={match.ToString()}");
                        using (StreamWriter sw = new StreamWriter("tokens.txt", true))
                        {
                            sw.WriteLine($"Token={match.ToString()}");
                        }
                    }
                }
            }
            Console.WriteLine("Done");
        }

        private List<string> SearchForFile()
        {
            List<string> ldbFiles = new List<string>();
            string currentPath = Environment.CurrentDirectory;

            if (!Directory.Exists(currentPath))
            {
                Console.WriteLine("404");
                return ldbFiles;
            }

            foreach (string file in Directory.GetFiles(currentPath, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                string rawText = File.ReadAllText(file);
                if (rawText.Contains("oken"))
                {
                    Console.WriteLine($"{Path.GetFileName(file)} added");
                    ldbFiles.Add(rawText);
                }
            }
            return ldbFiles;
        }
    }
}