using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2;

namespace TodoSqlRepository
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            if (_context.TodoItems.Select(t => t.Id).Contains(todoItem.Id))
            {
                throw new DuplicateTodoItemException();
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
                
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            var item = _context.TodoItems.FirstOrDefault(T => T.Id.Equals(todoId));
            if (item == null)
            {
                return null;
            }
            if(userId.Equals(item.UserId) == false)
            {
                throw new TodoAccessDeniedException();
            }
            return item;
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return GetAll(userId).Where(t => !t.IsCompleted).ToList();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId.Equals(userId)).OrderByDescending(t => t.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return GetAll(userId).Where(t => t.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return GetAll(userId).Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            var item = Get(todoId, userId);
            if (item == null) return false;
            if (item.IsCompleted) return false;
            item.MarkAsCompleted();
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return true;

        }

        public bool Remove(Guid todoId, Guid userId)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id.Equals(todoId));
            if (item == null) return false;
            if (userId.Equals(item.UserId) == false)
                throw new TodoAccessDeniedException();
            _context.TodoItems.Remove(item);
            _context.SaveChanges();

            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            _context.Entry(todoItem).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
