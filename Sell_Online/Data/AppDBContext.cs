using Microsoft.EntityFrameworkCore;
using Sell_Online.EntityConfig;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostViews> PostViews { get; set; }
        public virtual DbSet<PostImages> PostImages { get; set; }
        public virtual DbSet<PostStates> PostStates { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PostViewEntityConfiguration());
        }
    }
}
