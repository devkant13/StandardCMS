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
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<CommissionRule> CommissionRules { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring SaleAmount precision
            modelBuilder.Entity<Sale>()
                .Property(s => s.SaleAmount)
                .HasColumnType("decimal(18, 4)");

            // Configuring CommissionAmount precision
            modelBuilder.Entity<Commission>()
                .Property(c => c.CommissionAmount)
                .HasColumnType("decimal(18, 4)");

            // Configuring relationships
            modelBuilder.Entity<Commission>()
                .HasOne(c => c.Sale)
                .WithMany(s => s.Commissions)
                .HasForeignKey(c => c.SaleId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            modelBuilder.Entity<Commission>()
                .HasOne(c => c.Agent)
                .WithMany(a => a.Commissions)
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            base.OnModelCreating(modelBuilder);            

            // Defining the one-to-many relationship between Lead and LeadStatus
            modelBuilder.Entity<Lead>()
                .HasOne(l => l.LeadStatus)      // Lead has one LeadStatus
                .WithMany(s => s.Leads)         // LeadStatus has many Leads
                .HasForeignKey(l => l.StatusId) // Foreign key in Lead table
                .OnDelete(DeleteBehavior.Restrict); // Optional: to restrict delete actions
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Agent>()
        //        .HasOne(a => a.ParentAgent)
        //        .WithMany(a => a.ChildAgents)
        //        .HasForeignKey(a => a.ParentAgentId);

        //    base.OnModelCreating(modelBuilder);
        //}
    }

}
