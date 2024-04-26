using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ef_dz1.models;

using Microsoft.EntityFrameworkCore;

namespace ef_dz1
{
    internal class CategorieService
    {
        private readonly AppDbContext context;
        public CategorieService(AppDbContext context)
        {
            this.context = context;
        }
        public void Create(Category Categorie)
        {
            context.Categories.Add(Categorie);
            context.SaveChanges();
        }
        public List<Category> Read()
        {
            return context.Categories.ToList();
        }
        public void Update(Category Categorie)
        {
            context.Entry(Categorie).State = EntityState.Detached;
            context.Categories.Update(Categorie);
            context.SaveChanges();
        }
        public void Delete(int id)
        {

            var Categorie = context.Categories.Find(id);
            if (Categorie != null)
            {
                context.Entry(Categorie).State = EntityState.Detached;
                context.Categories.Remove(Categorie);
                context.SaveChanges();
            }

        }
        public Category GetById(int id)
        {
            return context.Categories.Find(id) as Category;
        }
        public void AddCategories(IEnumerable<Category> Categories)
        {
            context.Categories.AddRange(Categories);
            context.SaveChanges();
        }
    }
}
