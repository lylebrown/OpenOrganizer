using Microsoft.EntityFrameworkCore;
using OpenOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOrganizerAPI
{
    public class APIDBContext : DbContext
    {
        private static bool _created = false;
        public APIDBContext()
        {
            if (!_created)
            {
                _created = true;
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=L:\OpenOrganizer.db");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemAttachment> ItemAttachments { get; set; }
        public DbSet<ItemField> ItemFields { get; set; }
        public DbSet<ItemFieldValue> ItemFieldValues { get; set; }
        public DbSet<ItemTag> ItemTags { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
