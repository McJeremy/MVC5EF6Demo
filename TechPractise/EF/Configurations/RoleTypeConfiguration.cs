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
    public class RoleTypeConfiguration :EntityTypeConfiguration<Role>
    {
        public RoleTypeConfiguration()
        {
            HasKey<Guid>(r => r.ID);

            Property(r => r.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(200);

            Property(r => r.RoleDESC)
                .IsRequired()
                .HasMaxLength(200);

        }
    }
}
