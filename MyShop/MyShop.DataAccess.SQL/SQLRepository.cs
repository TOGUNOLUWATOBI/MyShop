using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext DataContext;
        internal DbSet<T> dbSet;

        public SQLRepository (DataContext context)
        {
            this.DataContext = context;
            this.dbSet = DataContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if(DataContext.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }
            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            DataContext.Entry(t).State = EntityState.Modified;
        }
    }
}
