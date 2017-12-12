using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCodeFirstApp
{
    public class Context : DbContext
    {
        public Context() : base("name=FirstCodeFirstApp")
        {

        }

        //public DbSet<Donator> Donators { get; set; }
        //public DbSet<PayWay> PayWays { get; set; }
        //public DbSet<DonatorType> DonatorTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Donator>().ToTable("Donatores").HasKey(m => m.DonatorId);//映射到表Donators,DonatorId当作主键对待
            //modelBuilder.Entity<Donator>().Property(m => m.DonatorId).HasColumnName("Id");//映射到数据表中的主键名为Id而不是DonatorId
            //modelBuilder.Entity<Donator>().Property(m => m.Name)
            //    .IsRequired()//设置Name是必须的，即不为null,默认是可为null的
            //    .IsUnicode()//设置Name列为Unicode字符，实际上默认就是unicode,所以该方法可不写
            //    .HasMaxLength(10);//最大长度为10

            //modelBuilder.Entity<Donator>().HasRequired<Province>(c => c.Province).WithMany(c => c.Donators).HasForeignKey(c=>c.PId);

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Donator> Donators { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
    }

    public class Donator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonateDate { get; set; }

        public virtual Province Province { get; set; }//新增字段
    }

    public class Province
    {
        public Province()
        {
            Donators = new HashSet<Donator>();
        }

        public int Id { get; set; }
        [StringLength(100)]
        public string ProvinceName { get; set; }

        public virtual ICollection<Donator> Donators { get; set; }
    }

    //public class Person
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string PhoneNumber { get; set; }
    //}

    //[Table("Employees")]
    //public class Employee : Person
    //{
    //    public decimal Salary { get; set; }
    //}
    //[Table("Vendors")]

    //public class Vendor : Person
    //{
    //    public decimal HourlyRate { get; set; }
    //}



















    public class Initializer : DropCreateDatabaseAlways<Context>
    {

    }


}
