using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TrainerAssignment.Models;

namespace TrainerAssignment.DAL
{
    public class MyDatabase:DbContext
    {
        public MyDatabase():base("ONOMA")
        {

        }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }

     
    }
}