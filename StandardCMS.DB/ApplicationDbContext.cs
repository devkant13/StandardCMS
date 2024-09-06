using Microsoft.EntityFrameworkCore;
using StandardCMS.DB.Models;
using System.Collections.Generic;

namespace StandardCMS.DB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<Content> Contents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
    
}
