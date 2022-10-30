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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasMany(x=>x.Bills)
                .WithOne(x=>x.Customer)
                .HasForeignKey(x=>x.CustomerId);

        }
    }
}
