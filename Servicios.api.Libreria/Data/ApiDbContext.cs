using Microsoft.EntityFrameworkCore;
using Servicios.api.Libreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext (DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<Driver> Drivers { get; set; }
    }
}
