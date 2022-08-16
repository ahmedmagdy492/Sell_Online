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
    public class PostViewEntityConfiguration : IEntityTypeConfiguration<PostViews>
    {
        public void Configure(EntityTypeBuilder<PostViews> builder)
        {
            builder.HasKey(v => new { v.PostID, v.ViewerID });

            builder.Property(v => v.PostID)
                .IsRequired(true);

            builder.HasOne(v => v.Post)
                .WithMany(p => p.PostViews)
                .HasForeignKey(v => v.PostID);

            builder.HasOne(v => v.User)
                .WithMany(u => u.MyViews)
                .HasForeignKey(v => v.ViewerID);
        }
    }
}
