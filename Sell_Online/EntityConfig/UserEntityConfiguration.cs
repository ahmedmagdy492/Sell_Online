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
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(i => i.UserID);

            builder.Property(i => i.IsVerified)
                .HasDefaultValue(false);

            builder.HasMany(i => i.Notifications)
                .WithOne(i => i.User);

            builder.HasMany(i => i.Posts)
                .WithOne(i => i.User);

            builder.HasMany(i => i.PhoneNumbers)
                .WithOne(i => i.User);
        }
    }
}
