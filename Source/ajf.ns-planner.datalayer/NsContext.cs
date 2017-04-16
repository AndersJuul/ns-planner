using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer
{
    public class NsContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Counsellor> Counsellors { get; set; }
        public DbSet<CounsellorDate> CounsellorDates { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<SchoolEvent> SchoolEvents { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasRequired(c => c.Request)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Match>()
                .HasRequired(c => c.CounsellorDate)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Match>()
                .HasRequired(c => c.SchoolEvent)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CounsellorDate>()
                .HasRequired(c => c.Project)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<CounsellorDate>()
                .HasRequired(c => c.Counsellor)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolEvent>()
                .HasRequired(c => c.Project)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            foreach (var entityEntry in ChangeTracker.Entries<BaseEntity>())
            {
                //Debug.WriteLine(entityEntry);
                if (entityEntry.State != EntityState.Unchanged)
                {
                    //Debug.WriteLine("Committing change: " + entityEntry.Entity);
                    entityEntry.Entity.LatestChange = DateTime.Now;
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbException)
            {
                Debug.WriteLine("Errors in db validation: ");
                Debug.WriteLine("--------");
                foreach (var validationResult in dbException.EntityValidationErrors)
                {
                    Debug.WriteLine(validationResult.Entry.Entity);
                    foreach (var dbValidationError in validationResult.ValidationErrors)
                    {
                        Debug.WriteLine(dbValidationError.PropertyName);
                        Debug.WriteLine(dbValidationError.ErrorMessage);
                        Debug.WriteLine("--------");
                    }
                    Debug.WriteLine("--------");
                }
                Debug.WriteLine("--------");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}