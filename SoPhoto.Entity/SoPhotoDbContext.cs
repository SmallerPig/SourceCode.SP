using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class SoPhotoDbContext :DbContext
    {

        public SoPhotoDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<SoPhotoDbContext>(null);
        }


        public DbSet<SP_AD> SP_AD { get; set; }

        public DbSet<SP_Banner> SP_Banner { get; set; }

        public DbSet<SP_Pics> SP_Pics { get; set; }

        public DbSet<SP_Topic> SP_Topic { get; set; }

        public DbSet<SP_News> SP_News { get; set; }


        public DbSet<Partner> Partner { get; set; }

        public DbSet<PartnerSale> PartnerSale { get; set; }


        public DbSet<SP_Admin> SP_Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //取消默认复数表名的设置
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
