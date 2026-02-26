using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Model;

namespace TaskManagerAPI.Data;

    public class AppDbContext : DbContext  //this class is used to interact with the database and it inherits from DbContext class which is provided by Entity Framework Core
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //this line is used to pass the options to the base class constructor
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskUser> Tasks { get; set; }

        
    }
