using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp
{
    /*
    Todo:

    Debug messages aren't working.
    Use String interpolation instead of switch statement
    */
    class Program
    {
        private const string _todoFile = "todo.json";

        static void Main(string[] args)
        {
            var _todoList = new List<Todo>();
            Console.WriteLine("Todo App in C#");

            ReadFromFile();

            var action = MainAction.Waiting;
            while (action != MainAction.Exit)
            {
                if (action == MainAction.Waiting)
                {
                    ShowList(_todoList);
                }
                else if (action == MainAction.Select_Item)
                {
                    ItemOperations(_todoList);
                }
                else if (action == MainAction.NewItem)
                {
                    var item = NewItem();
                    _todoList.Add(item);
                }

                action = MainOperations();
            }

            WriteToFile (_todoList);
        }

        /// <summary>
        /// Save the live data to the output file
        /// </summary>
        /// <param name="_todoList"></param>
        static void WriteToFile(List<Todo> _todoList)
        {
            var op = JsonConvert.SerializeObject(_todoList);
            File.WriteAllText (_todoFile, op);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static List<Todo> ReadFromFile()
        {
            //Debug.WriteLine("Reading from File");
            var _todoList = new List<Todo>();

            if (File.Exists(_todoFile))
            {
                //Debug.WriteLine("File found");
                var fileContent = File.ReadAllText(_todoFile);
                //Debug.WriteLine($"Contents: {fileContent}");

                try
                {
                    var items = JsonConvert.DeserializeObject<List<Todo>>(fileContent);
                    Console.WriteLine($"Found {items.Count} Todo Items");
                    _todoList = items;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }

            return _todoList;
        }


        /// <summary>
        /// Display the list of Todo Items
        /// </summary>
        /// <param name="_todoList"></param>
        static void ShowList(List<Todo> _todoList)
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


        static void ItemOperations(List<Todo> _todoList)
        {
            Console.WriteLine("Select Item - Enter item number:");
            var ixStr = Console.ReadLine();
            
            int ix = 0;
            if (int.TryParse(ixStr,out ix))
            {
                var item = _todoList[ix - 1];
                if (item != null)
                {
                    Console.WriteLine($"Task : {item.Task}");
                }
            }
        }

        static Todo NewItem()
        {
            Console.WriteLine("New Item. Enter Todo Text");
            var text = Console.ReadLine();
            Console.WriteLine("Enter Notes");
            var notes = Console.ReadLine();
            var item = new Todo() { Task = text, Notes = notes };
            return item;
        }

        static MainAction MainOperations(){

            MainAction response =  MainAction.Waiting;
            Console.WriteLine("Choose Action. Type H for help");

            // Thread will be blocked here
            var ip = Console.ReadKey().KeyChar.ToString().ToUpperInvariant();
            //Debug.WriteLine($"You chose {ip}");
            Console.WriteLine();


            // change to String Interpolation
            switch (ip){
                case "H":
                    Console.WriteLine("Console Todo App :");
                    Console.WriteLine(" H - This help page");
                    Console.WriteLine(" N - New ToDo");
                    Console.WriteLine(" S - Select Todo");
                    response = MainAction.Waiting;
                    break;

                case "N":
                    response = MainAction.NewItem;
                    break;


                case "S":
                    response = MainAction.Select_Item;
                    break;

                case "X":
                default:
                    Console.WriteLine("Exit");
                    response = MainAction.Exit;
                    break;

            }

            return response;
        }
    }
}
