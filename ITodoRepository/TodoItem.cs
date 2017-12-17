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
}
