using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TestFile1
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CBS_LN_APP> CBS_LN_APP { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_APP_NO)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_ISIC1_CD)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_ISIC2_CD)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_ISIC3_CD)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_CA_NO)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_COMMITTEE)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_APPROVE_CD)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_REASON_CD)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_APPROVE_COMMENT)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_MATURITYDATE)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_COL)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_PAYSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.BATCH_UPDATE_DTM)
                .IsUnicode(false);

            modelBuilder.Entity<CBS_LN_APP>()
                .Property(e => e.CBS_CA_DATE)
                .IsUnicode(false);
        }
    }
}
