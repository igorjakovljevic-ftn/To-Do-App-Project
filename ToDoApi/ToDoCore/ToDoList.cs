using System;
using System.Collections.Generic;

namespace ToDoCore
{
    public class ToDoList
    {

        public Guid Id { get; set; }

        
        public string Title { get; set; }

        public int Position { get; set; }

        public List<ListItem> ListItems { get; set; }

        public void Update(ToDoList list)
        {
            Title = list.Title;
        }
    }
}

