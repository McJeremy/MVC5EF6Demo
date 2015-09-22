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
    public class BlogUserTypeConfiguration : EntityTypeConfiguration<BlogUser>
    {
        public BlogUserTypeConfiguration()
        {
            HasKey<Guid>(u => u.ID);

            Property(u => u.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(200);

            //Property(u => u.BlogID)
            //    .IsRequired();

            //Ignore(u => u.Blog);            

            //HasRequired(u => u.Blog)
            //    .WithRequiredDependent(b => b.BlogUser)
            //    .Map(key =>
            //    {
            //        key.MapKey("BlogID");
            //    });

            //HasOptional表示0..1-N的关系
            //而HasRequired标识1-N的关系

            //下面的方法会在SQL中自动生成BlogID字段
            //因此要求BLOG实体中不能再定义BlogID属性。            
            HasOptional(u => u.Blog)
                .WithOptionalDependent(p => p.BlogUser)
                .Map(key => key.MapKey("BlogID"));

            //下面的方法会在SQL中自动生成Blog_ID字段
            //HasRequired(u=>u.Blog)
            //    .WithRequiredDependent(p => p.BlogUser);

        }
    }
}
