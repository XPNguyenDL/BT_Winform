using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCodeFirst.Model;

namespace EntityFrameworkCodeFirst.DAL
{
    public class QLSinhVienDBContext : DbContext
    {
        public QLSinhVienDBContext() : base("DBSinhVien")
        {
            
        }

        public static QLSinhVienDBContext Cteate()
        {
            return new QLSinhVienDBContext();
        }

        public DbSet<SinhVien> SinhVien { get; set; }
        public DbSet<Lop> Lop { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lop>()
                .HasMany(a => a.SinhVien)
                .WithRequired(o => o.Lop)
                .HasForeignKey(o => o.MaLop)
                .WillCascadeOnDelete(false);
        }
    }
}
