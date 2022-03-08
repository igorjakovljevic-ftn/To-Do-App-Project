using System;
using System.Text.Json.Serialization;

namespace ToDoCore
{
    public class ListItem
    {

        public Guid Id { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public ToDoList ToDoList { get; set; } 

        public int Position { get; set; }

        public Guid ToDoListId { get; set; }

        public bool IsCompleted { get; set; }

        public void Update(ListItem item)
        {
            Text = item.Text;
            IsCompleted = item.IsCompleted;
        }

        public void UpdatePosition(ListItem item)
        {
            Position = item.Position;
        }

    }
}

