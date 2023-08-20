using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Drawing;
using UserMgmtApi.Models;

namespace UserMgmtApi.Data.Config
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        void IEntityTypeConfiguration<UserDetail>.Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(e => e.Name, name => { 
                name.Property(a => a.Firstname).HasColumnName("Firstname").IsRequired();
                name.Property(a => a.Lastname).HasColumnName("Lastname").IsRequired();
            });

            builder.OwnsOne(e => e.Email, email => {
                email.Property(a => a.Value).HasColumnName("Email").IsRequired();
            });

            builder.OwnsOne(e => e.MobileNo, mobileNumber => {
                mobileNumber.Property(a => a.Value).HasColumnName("MobileNo").IsRequired();
            });

            builder.OwnsOne(e => e.PassportNo, passportNum => {
                passportNum.Property(a => a.Value).HasColumnName("PassportNo").IsRequired();
            });

            builder.Property(p => p.EmployeeType).HasColumnName("EmployeeType").HasConversion(
                p => p.Value,
                p => EmployeeType.FromValue(p)
            );

        }
    }
}
