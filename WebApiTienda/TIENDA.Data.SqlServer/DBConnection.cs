using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.SqlServer.Configurations;

namespace TIENDA.Data.SqlServer
{
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options): base(options)
        {

        }

        #region DbSets

        public virtual DbSet<Bill> Billing { get; set; }
        public virtual DbSet<BillingDetail> BillingDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        #endregion

        #region Configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new BillConfiguration());
           modelBuilder.ApplyConfiguration(new BillingDetailConfiguration());
           modelBuilder.ApplyConfiguration(new CategoryConfiguration());
           modelBuilder.ApplyConfiguration(new CityConfiguration());
           modelBuilder.ApplyConfiguration(new CustomerConfiguration());
           modelBuilder.ApplyConfiguration(new ProductConfiguration());
           modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        #endregion

        #region Transactions
        public MsgResult Save()
        {
            var res = new MsgResult();
            try
            {
                int count = this.SaveChanges();
                res.IsSuccess = true;
                res.Count = count;
                res.Code = 1;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Code = 0;
                res.Error = ex;
            }

            return res;
        }

        public async Task<MsgResult> SaveAsync()
        {
            var res = new MsgResult();

            try
            {
                int count = await this.SaveChangesAsync();
                res.IsSuccess = true;
                res.Count = count;
                res.Code = 1;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Code = 0;
                res.Error = ex;
            }

            return res;
        }

        #endregion
    }
}
