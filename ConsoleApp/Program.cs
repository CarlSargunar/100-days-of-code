using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {

        private const string _todoFile = "todo.json";
        private static List<Todo> _todoList { get; set; }

        static void Main(string[] args)
        {
            _todoList = new List<Todo>();
            Console.WriteLine("Todo App in C#");

            ReadFromFile();

            _todoList.Add(new Todo() { Task = "Tesst", Completed = false });
            _todoList.Add(new Todo() { Task = "Teadwawd", Completed = true });


            WriteToFile();

        }

        static void WriteToFile()
        {
            var op = JsonConvert.SerializeObject(_todoList);
            File.WriteAllText(_todoFile, op);

        }

        static void ReadFromFile(){
            Console.WriteLine("Reading from File");
            if (File.Exists(_todoFile))
            {
                Console.WriteLine("File found");

                var fileContent = File.ReadAllText(_todoFile);
                Console.WriteLine($"Contents: {fileContent}");
            }
        }

        static void ShowList()
        {
            foreach (var todo in _todoList)
            {
                Console.WriteLine($"[ ]{todo.Task}");
                if (string.IsNullOrEmpty(todo.Notes))
                {
                    Console.WriteLine($"    {todo.Notes}");
                }
            }
        }
    }
}
