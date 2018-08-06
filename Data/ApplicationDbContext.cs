using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OryxWestAfrica.Models;

namespace OryxWestAfrica.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
    }
}
