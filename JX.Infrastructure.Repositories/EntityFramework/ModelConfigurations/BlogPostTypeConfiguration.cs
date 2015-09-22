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
    public class BlogPostTypeConfiguration : EntityTypeConfiguration<BlogPost>
    {
        public BlogPostTypeConfiguration()
        {
            HasKey<Guid>(p => p.ID);

            Property(p => p.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            Property(p => p.CreateDate)
                .IsRequired();

            //配置Blog和Post的一对多关系，Blog对Post是可选的，外键BlogId，并设置为级联删除            
            HasRequired(pt => pt.Blog)
                .WithMany(b=>b.BlogPosts)
                .Map(key =>
                {
                    key.MapKey("BlogID");
                });

            //一般在从表中定义主从关系。避免主表有较多的从表时，显得混乱
            // LeftKey指向的是HasMany的实体，而不是TagetEntity
            // RightKey指向的是WithMany的实体，而不是SourceEntity
            //HasMany(p => p.BlogPostAuthors)
            //    .WithMany(pa => pa.BlogPosts)
            //    .Map(key =>
            //    {
            //        key.ToTable("BlogPostMapAuthor");
            //        key.MapLeftKey("PostID");
            //        key.MapRightKey("AuthorID");
            //    });
        }
    }
}
