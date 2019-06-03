using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Worker.dao;

namespace Worker.context
{
    public class ApplicationContext : DbContext
    {
		private const string ConnectionString = "Server=DESKTOP-FN08SHG;Database=wks;Trusted_Connection=True;";

		public DbSet<Worker> Workers { get; set; }
		public DbSet<Project> Projects { get; set; }

		public ApplicationContext() {
			Console.WriteLine(Database.EnsureCreated()); // creates db if not exists
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlServer(ConnectionString);


		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			//Database.ExecuteSqlCommand("ALTER TABLE dbo.Projects ADD CONSTRAINT Project_wks FOREIGN KEY (WorkerId) REFERENCES dbo.Workers (Id) ON DELETE NO ACTION");
			
			//modelBuilder.Entity<Project>().HasData(
			//	new Project[]
			//	{
			//	new Project(1, "Построить дом", 10),
			//	new Project (2, "Вырастить сына", 15),
			//	new Project (3, "Посадить дерево", 5, null),
			//	new Project (4, "Построить баню", 5, null),
			//	});

			//base.OnModelCreating(modelBuilder);
		}
	}
}
