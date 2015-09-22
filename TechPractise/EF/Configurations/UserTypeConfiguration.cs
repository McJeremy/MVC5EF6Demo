using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.Model.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechPractise.EF
{
    public class UserTypeConfiguration :EntityTypeConfiguration<User>
    {
        public UserTypeConfiguration()
        {
            HasKey<Guid>(u => u.ID);

            Property(u => u.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(200);

            Property(u => u.UserPWD)
                .IsRequired()
                .HasMaxLength(200);

        }
    }
}
