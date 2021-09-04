using System;
namespace ConsoleApp
{
    public class Todo
    {
        public string Task { get; set; }
        public string Notes { get; internal set; }
        public bool Completed { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
