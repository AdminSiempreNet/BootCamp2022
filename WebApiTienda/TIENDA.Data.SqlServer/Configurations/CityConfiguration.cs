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
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasMany(x=>x.Customers)
                .WithOne(x=>x.City)
                .HasForeignKey(x=>x.CityId);
        }
    }
}
