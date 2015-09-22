using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.Model.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public class BlogTypeConfiguration :EntityTypeConfiguration<Blog>
    {
        public BlogTypeConfiguration()
        {
            HasKey<Guid>(p => p.ID);

            Property(p => p.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            //Property(p => p.UserID)
            //    .IsRequired();

            //Ignore(b => b.BlogUser);                    

        }
    }
}
