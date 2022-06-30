using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.EntityConfig
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(i => i.PostID);

            builder.Property(i => i.CreationDate)
                .HasDefaultValue(DateTime.Now);

            builder.Property(i => i.IsEdited)
                .HasDefaultValue(false);

            builder.Property(i => i.EditDate)
                .HasDefaultValue(DateTime.Now);
            
            builder.Property(i => i.SoldDate)
                .HasDefaultValue(DateTime.Now);

            builder.HasMany(i => i.PostImages)
                .WithOne(i => i.Post);

            builder.HasMany(i => i.PostViews)
                .WithOne(i => i.Post);

            builder.HasOne(i => i.PostCategory)
                .WithMany(i => i.Posts);
        }
    }
}
