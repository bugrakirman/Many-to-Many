using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    [Table("Product")]
    public class Product
    {
        public int ProductID { get; set; }
        public int? Qnt { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }

    [Table("Package")]
    public class Package
    {
        [Key]
        public int PackageID { get; set; }

        public int Size { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }

    [Table("Order")]
    public class Order
    {   
        public int OrderID { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }

    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Column(Order = 2)]
        public int PackageID { get; set; }
        [Column(Order = 3)]
        public int OrderID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int OrderDetailID { get; set; }

        [ForeignKey("PackageID")]
        public virtual Package Package { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }

    public class MyContext : DbContext
    {
        public MyContext() : base("name=MyCon")
        {
        }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }

    public class RepositoryBase<T, TId> : IDisposable where T : BaseEntity<TId>
    {
        protected internal static MyContext db;

        protected RepositoryBase()
        {
            db = db ?? new MyContext();
        }

        public virtual List<T> GetAll()
        {
            try
            {
                return db.Set<T>().ToList();
            }
            catch
            {
                throw;
            }
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await db.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public virtual T GetById(TId id)
        {
            try
            {
                return db.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        public virtual int Insert(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                return db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public virtual int Delete(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                return db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public virtual int Update()
        {
            try
            {
                return db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<int> UpdateAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public virtual IQueryable<T> Queryable()
        {
            try
            {
                return db.Set<T>().AsQueryable();
            }
            catch
            {
                throw;
            }
        }

        public virtual List<T> GetAll(Func<T, bool> predicate)
        {
            try
            {
                return db.Set<T>().Where(predicate).ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            db.Dispose();
            db = new MyContext();
        }
    }

    public abstract class BaseEntity<T>
    {
        [Key]
        [Column(Order = 1)]
        public T ID { get; set; }
    }
}
