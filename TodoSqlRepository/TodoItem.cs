using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        /// <summary>
        /// User id that owns this Todoitem
        /// </summary>
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public bool IsCompleted
        {
            get
            {
                return DateCompleted.HasValue;
            }
        }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// List of labels associated with TodoItem
        /// </summary>
        public List<TodoItemLabel> Labels { get; set; }
        
        /// <summary>
        /// Date due. If null, no date was set by the user.
        /// </summary>
        public DateTime? DateDue { get; set; }

        /// <param name="text"></param>
        public TodoItem(string text)
        {
            Id = Guid.NewGuid(); //  Generates  new  unique  identifier

            // DateTime.Now returns local time, it wont alwys be what you expect
            //(depending where server is)
            // We want to use universal (UTC) timw which we can easily convert
            //to local when needed 
            //ussually done in browser on the client side
            DateCreated = DateTime.UtcNow;

            Text = text;
            

        }
        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }
        public TodoItem()
        {
            // entity framework needs this one 
            // not for use :)
        }


        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            TodoItem a = (TodoItem)obj; 
            if (a.Id == this.Id)
            {
                return true;
            }
            return false;
        }
        //public static bool operator==(TodoItem a, TodoItem b)
        //{
          //  if (a.Equals(b))
            //{
             //   return true;
            //}
            //return false;
        //}
        //public static bool operator!=(TodoItem a, TodoItem b)
        //{
        //    if (a.Equals(b))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }

    /// <summary>
    /// Labels describing the TodItem.
    /// e.g. Critical, Personal, Work...
    /// </summary>
    public class TodoItemLabel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// All TodoItems that are associated with this label
        /// </summary>
        public List<TodoItem> LabelTodoItems { get; set; }
        public TodoItemLabel(string value)
        {
            Id = Guid.NewGuid();
            Value = value;
            LabelTodoItems = new List<TodoItem>();
        }
    }
}
