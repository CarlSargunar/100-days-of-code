using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {

        private static List<Todo> _todoList { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Todo App in C#");


        }

        static void WriteToFile(){

        }

        static void ReadFromFile(){
            
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
