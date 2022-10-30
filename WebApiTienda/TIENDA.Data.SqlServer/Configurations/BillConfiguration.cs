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
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(key=>key.Id);

            builder.HasMany(x=>x.Details)
                .WithOne(x=>x.Bill)
                .HasForeignKey(x=>x.BillingId);            
        }

    }
}
