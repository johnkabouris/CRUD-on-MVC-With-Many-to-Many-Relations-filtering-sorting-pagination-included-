namespace TrainerAssignment.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using TrainerAssignment.DAL;
    using TrainerAssignment.Models.ViewModels;
    using TrainerAssignment.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MyDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MyDatabase context)
        {

            #region  SEED EVERYTHING I DONT KNOW WHAT AM I DOING FOR REAL IN THIS SECTION


            //Categories
            Category cat1 = new Category() { Subject = "C#" };
            Category cat2 = new Category() { Subject = "Java" };
            Category cat3 = new Category() { Subject = "C++" };
            Category cat4 = new Category() { Subject = "Python" };
            Category cat5 = new Category() { Subject = "Javascript" };
            Category cat6 = new Category() { Subject = "Php" };
            Category cat7 = new Category() { Subject = "Ruby" };
            //Trainers
            Trainer tr1 = new Trainer() { FirstName = "John", LastName = "Kabouris", Salary = 50000, IsAvailable = true, DateHire = new DateTime(2021, 01, 14)};
            tr1.Categories = new List<Category>() { cat1, cat2, cat3 };
            Trainer tr2 = new Trainer() { FirstName = "Nikos", LastName = "Karageorgos", Salary = 30000, IsAvailable = false, DateHire = new DateTime(2021, 02, 14)};
            tr2.Categories = new List<Category>() { cat2 };
            Trainer tr3 = new Trainer() { FirstName = "Hector", LastName = "Gkatsos", Salary = 50000, IsAvailable = true, DateHire = new DateTime(2021, 04, 20)};
            tr3.Categories = new List<Category>() { cat1,cat2,cat3,cat4,cat5,cat6,cat7 };
            Trainer tr4 = new Trainer() { FirstName = "Artemis", LastName = "Kouniakis", Salary = 60000, IsAvailable = true, DateHire = new DateTime(2020, 01, 14)};
            tr4.Categories = new List<Category>() { cat1,  cat5 };
            Trainer tr5 = new Trainer() { FirstName = "Kostas", LastName = "Takakis", Salary = 20000, IsAvailable = false, DateHire = new DateTime(2019, 01, 14)};
            tr5.Categories = new List<Category>() { cat1, cat5,cat4 };
            Trainer tr6 = new Trainer() { FirstName = "Giwrgos", LastName = "Karageorgos", Salary = 30000, IsAvailable = true, DateHire = new DateTime(2021, 03, 25)};
            tr6.Categories = new List<Category>() { cat6, cat7 };
            Trainer tr7 = new Trainer() { FirstName = "Apostolis", LastName = "Dimitriou", Salary = 40000, IsAvailable = false, DateHire = new DateTime(2021, 01, 09)};
            tr7.Categories = new List<Category>() { cat1, cat2, };
            Trainer tr8 = new Trainer() { FirstName = "Theodora", LastName = "Kontotasiou", Salary = 60000, IsAvailable = true, DateHire = new DateTime(2021, 05, 02)};
            tr8.Categories = new List<Category>() { cat2, cat5 };
            Trainer tr9 = new Trainer() { FirstName = "Vasilis", LastName = "Akouloglou", Salary = 90000, IsAvailable = false, DateHire = new DateTime(2020, 04, 23)};
            tr9.Categories = new List<Category>() { cat1, cat7 };
            Trainer tr10 = new Trainer() { FirstName = "Panos", LastName = "Antoniadis", Salary = 20000, IsAvailable = false, DateHire = new DateTime(2021, 03, 07)};

            tr10.Categories = new List<Category>() { cat5, cat7,cat6 };


            context.Trainers.AddOrUpdate(x => new { x.FirstName, x.LastName }, tr1, tr2, tr3, tr4, tr5, tr6, tr7, tr8, tr9, tr10);
            context.SaveChanges();



            #endregion


        }
    }
}
