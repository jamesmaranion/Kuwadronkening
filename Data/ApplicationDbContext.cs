using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kuwadro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kuwadro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }
        public DbSet<Art> artList { get; set; }
        public DbSet<Kuwadro.Models.Commission> Commission { get; set; }

    }
}