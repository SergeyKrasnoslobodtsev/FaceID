using FaceID.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace FaceID.Data
{
    public interface IDataContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Vectors> Vectors { get; set; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
    public class FaceIdContext : DbContext, IDataContext
    {
        

        public FaceIdContext() { }
        public FaceIdContext(DbContextOptions<FaceIdContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vectors> Vectors { get; set; }

        private IDbContextTransaction _transaction;
        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();

                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
