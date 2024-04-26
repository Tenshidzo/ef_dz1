using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ef_dz1.models;

namespace ef_dz1
{
    internal class TasksService
    {
        private readonly AppDbContext context;
        public TasksService(AppDbContext context)
        {
            this.context = context;
        }
        public void Create(Tasks1 task)
        {
            context.TaskContext.Add(task);
            context.SaveChanges();
        }
        public List<Tasks1> Read()
        {
            return context.TaskContext.ToList();
        }
        public void Update(Tasks1 Task)
        {
            context.Entry(Task).State = EntityState.Detached;
            context.TaskContext.Update(Task);
            context.SaveChanges();
        }
        public void Delete(int id)
        {

            var Task = context.TaskContext.Find(id);
            if (Task != null)
            {
                context.Entry(Task).State = EntityState.Detached;
                context.TaskContext.Remove(Task);
                context.SaveChanges();
            }

        }
        public Tasks1 GetById(int id)
        {
            return context.TaskContext.Find(id) as Tasks1;
        }
        public void AddTasks(IEnumerable<Tasks1> Tasks)
        {
            context.TaskContext.AddRange(Tasks);
            context.SaveChanges();
        }
    }
}
