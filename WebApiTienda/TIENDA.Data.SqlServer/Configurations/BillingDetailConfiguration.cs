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
    public class BillingDetailConfiguration : IEntityTypeConfiguration<BillingDetail>
    {
        public void Configure(EntityTypeBuilder<BillingDetail> builder)
        {
            builder.HasKey(x=>x.Id);
        }
    }
}
