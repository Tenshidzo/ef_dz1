using ef_dz1.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace ef_dz1
{
     public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory() + @"\Config")
               .AddJsonFile("appsettings.json")
               .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var context = new AppDbContext(optionsBuilder.Options, config))
            {

                // Перевіряємо, чи є категорії в базі даних, інакше додаємо тестові категорії
                if (!context.Categories.Any())
                {
                    var categories = new[]
                    {
                        new Category { Name = "Work", Description = "Tasks related to work" },
                        new Category { Name = "Home", Description = "Tasks related to home" }
                    };
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }

                // Перевіряємо, чи є завдання в базі даних, інакше додаємо тестові завдання
                if (!context.TaskContext.Any())
                {
                    var tasks = new[]
                    {
                        new Tasks1 { Title = "Complete project proposal", Description = "Write and submit project proposal for review", IsCompleted = false, CreatedAt = DateTime.Now, CategoryId = 1 },
                        new Tasks1 { Title = "Buy groceries", Description = "Buy fruits, vegetables, and milk", IsCompleted = false, CreatedAt = DateTime.Now, CategoryId = 2 }
                    };
                    context.TaskContext.AddRange(tasks);
                    context.SaveChanges();
                }

                // Виведемо список всіх категорій
                Console.WriteLine("Categories:");
                foreach (var category in context.Categories)
                {
                    Console.WriteLine($"{category.Id}. {category.Name} - {category.Description}");
                }

                // Виведемо список всіх завдань
                Console.WriteLine("\nTasks:");
                foreach (var task in context.TaskContext)
                {
                    Console.WriteLine($"{task.Id}. {task.Title} - {task.Description} - Completed: {task.IsCompleted} - Created At: {task.CreatedAt}");
                }
            }
        }
    }
}
