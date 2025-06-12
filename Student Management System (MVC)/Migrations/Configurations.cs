using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using Student_Management_System__MVC_.Data;
using MySql.Data.EntityFramework;

namespace Student_Management_System__MVC_.Migrations
{
	public class Configurations : DbMigrationsConfiguration<StudentDBContext>
	{
		public Configurations()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
			SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
		}

		protected override void Seed(StudentDBContext context)
		{
			//  This method will be called after migrating to the latest version.
			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
		}
	}
}