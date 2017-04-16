using System;
using System.Data.Entity.Migrations;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<NsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var e2015 = new Project
            {
                Name = "Efterår 2015",
                FirstDate = new DateTime(2015, 08, 01),
                LastDate = new DateTime(2015, 12, 23)
            };
            var test2015 = new Project
            {
                Name = "TEST",
                FirstDate = new DateTime(2015, 08, 01),
                LastDate = new DateTime(2015, 12, 23)
            };
            context.Projects.AddOrUpdate(
                p => p.Name,
                e2015);

            context.Counsellors.AddOrUpdate(
                c=>c.Initials,
                new Counsellor { Email = "tn@sn.dk", FullName = "Tine Nord", Initials = "TN", Phone = "555-1", Project = e2015 },
                new Counsellor { Email = "minn@sn.dk", FullName = "Michael NN", Initials = "MINN", Phone = "555-2", Project = e2015 },
                new Counsellor { Email = "mnn@sn.dk", FullName = "Mette NN", Initials = "MNN", Phone = "555-3", Project = e2015 }
                );

            context.SchoolEvents.AddOrUpdate(
                p => p.FullName,
                new SchoolEvent {FullName = "Harry Potter",Project = e2015},
                new SchoolEvent { FullName = "Leg med ild", Project = e2015 },
                new SchoolEvent { FullName = "Stik den! Om bier og hvepse", Project = e2015 }
                );
        }
    }
}