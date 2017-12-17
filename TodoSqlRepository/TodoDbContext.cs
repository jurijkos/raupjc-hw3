using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Task2;

namespace TodoSqlRepository
{
    public class TodoDbContext :DbContext
    {
        public IDbSet<TodoItem> TodoItems { get; set; }
        public TodoDbContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.UserId).IsRequired();
        }
    }
}
