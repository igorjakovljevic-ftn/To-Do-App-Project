using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Services
{
    public class ToDoListsService
    {
        private readonly ToDoDbContext _context;

        public ToDoListsService(ToDoDbContext context)
        {
            _context = context;
        }

        public List<ToDoList> GetLists()
        {
            return _context.ToDoLists.Include(x => x.ListItems).OrderByDescending(x => x.Position).ToList();
        }

        public ToDoList GetList(Guid id)
        {
            var list = _context.ToDoLists.Include(x => x.ListItems).FirstOrDefault(l => l.Id == id);

            if(list == null)
                return null;

            return list;
        }

        public ToDoList GenerateListPosition(ToDoList list)
        {
            list.Position = _context.ToDoLists.Count() + 1;
            return list;
        }

        public ToDoList CreateList(ToDoList newList)
        {
            if(newList.Title == null)
            {
                newList.Title = "";
            }
            _context.ToDoLists.Add(GenerateListPosition(newList));
            try
            {
                _context.SaveChanges();
            }
            catch(Exception exception)
            {
                return null;
            }
            
            return newList;
        }

        public ToDoList UpdateList(ToDoList list, Guid id)
        {
            ToDoList toUpdate = GetList(id);

            toUpdate.Update(list);
   
            _context.SaveChanges();

            return toUpdate;
        }

        public ToDoList DeleteList(Guid id)
        {
            ToDoList toDelete = _context.ToDoLists.Find(id);
            if (toDelete == null)
                return null;

            _context.ToDoLists
                .Where(list => list.Position > toDelete.Position).ToList()
                .ForEach(list => list.Position--);
            
            _context.ToDoLists.Remove(toDelete);
            _context.SaveChanges();
            return toDelete;
        }


        public List<ToDoList> FindByTitle(string title)
        {
            List<ToDoList> found = _context.ToDoLists.Where(list => list.Title.Contains(title)).ToList();
            if (!found.Any())
                return null;
            return found;
        }

        public ToDoList UpdateListPosition(Guid id, int newPosition)
        {
            ToDoList toUpdate = _context.ToDoLists.Find(id);
            int oldPosition = toUpdate.Position;

            //azuriramo samo one pozicije koje su izmedju oldPosition i newPosition
            //u zavisnosti od toga koji je smer menjanja smanjicemo ili uvecati za 1 pozicije svih elemenata izmedju
            if (newPosition > oldPosition) {
                _context.ToDoLists
                    .Where(list => list.Position > oldPosition && list.Position <= newPosition).ToList()
                    .ForEach(list => list.Position--);
                toUpdate.Position = newPosition;  
            }
            else if(newPosition < oldPosition)
            {
                _context.ToDoLists
                    .Where(list => list.Position >= newPosition && list.Position < oldPosition).ToList()
                    .ForEach(list => list.Position++);
                toUpdate.Position = newPosition;
            }
            
            _context.SaveChanges();

            return toUpdate;
        }


        //List items
        public List<ListItem> GetListItems(Guid listId)
        {
            var found = _context.ListItems.Where(item => item.ToDoListId == listId).ToList();
            
            if (!found.Any())
                return null;
            
            return found;
        }

        public ListItem GetListItem(Guid listId, Guid listItemId)
        {
            return _context.ListItems.FirstOrDefault(item => item.ToDoListId == listId && item.Id == listItemId);
        }

        public ListItem GenerateItemPosition(ListItem item)
        {
            item.Position = _context.ListItems.Count() + 1;
            return item;
        }

        public ListItem CreateListItem(Guid listId, ListItem newItem)
        {
            newItem.ToDoListId = listId;
            ListItem x = GenerateItemPosition(newItem);
            _context.ListItems.Add(GenerateItemPosition(newItem));

            try
            {
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                return null;
            }
            return newItem;
        }

        public ListItem DeleteItem(Guid listId, Guid listItemId)
        {
            var toDelete = _context.ListItems.FirstOrDefault(x => x.Id == listItemId && x.ToDoListId == listId);
            if (toDelete == default)
                return null;

            _context.ListItems
                .Where(item => item.Position > toDelete.Position).ToList()
                .ForEach(item => item.Position--);

            _context.ListItems.Remove(toDelete);
            _context.SaveChanges();
            return toDelete;
        }

        public ListItem UpdateItem(ListItem item, Guid listId, Guid id)
        {
            ListItem toUpdate = GetListItem(listId, id);

            toUpdate.Update(item);
           

            _context.SaveChanges();

            return toUpdate;
        }

        public ListItem UpdateItemPosition(Guid listId, Guid id, int newPosition)
        {
            ListItem toUpdate = _context.ListItems.FirstOrDefault(item => item.ToDoListId == listId && item.Id == id);
            int oldPosition = toUpdate.Position;
            
            //azuriramo samo one pozicije koje su izmedju oldPosition i newPosition
            if (newPosition > oldPosition)
            {
                _context.ListItems
                    .Where(list => list.Position > oldPosition && list.Position <= newPosition).ToList()
                    .ForEach(item => item.Position--);
                toUpdate.Position = newPosition;
            }
            else if (newPosition < oldPosition)
            {
                _context.ListItems.Where(list => list.Position >= newPosition && list.Position < oldPosition).ToList().ForEach(item => item.Position++);
                toUpdate.Position = newPosition;
            }
            
            _context.SaveChanges();

            return toUpdate;
        }
    }
}
