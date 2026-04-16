using System.Linq;

namespace KooliProjekt.Application.Data
{
    public class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context.Products.Any() || context.ToDoLists.Any() || context.ToDoItems.Any())
            {
                return;
            }

            for (int i = 1; i <= 10; i++)
            {
                context.Products.Add(new Product
                {
                    Name = "Product " + i,
                    Description = "Description " + i,
                    Price = i * 10
                });
            }

            for (int i = 1; i <= 10; i++)
            {
                context.ToDoLists.Add(new ToDoList
                {
                    Name = "List " + i
                });
            }

            context.SaveChanges();

            var lists = context.ToDoLists.ToList();

            for (int i = 1; i <= 10; i++)
            {
                context.ToDoItems.Add(new ToDoItem
                {
                    Title = "Task " + i,
                    IsDone = i % 2 == 0,
                    ToDoListId = lists[(i - 1) % lists.Count].Id
                });
            }

            context.SaveChanges();
        }
    }
}