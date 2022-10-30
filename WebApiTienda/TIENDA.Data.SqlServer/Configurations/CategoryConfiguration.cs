using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Data.Entities;

namespace TIENDA.Data.SqlServer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasMany(x=>x.Products)
                .WithOne(x=>x.Category)
                .HasForeignKey(x=>x.CategoryId);
        }
    }
}
