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
    public class BlogPostAuthorTypeConfiguration : EntityTypeConfiguration<BlogPostAuthor>
    {
        public BlogPostAuthorTypeConfiguration()
        {
            HasKey<Guid>(u => u.ID);

            Property(u => u.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(200);

            //配置和文章的多对多关系
            HasMany(author => author.BlogPosts)                
                .WithMany(post => post.BlogPostAuthors)
                .Map(key =>
                {
                    //为多对多的映射在数据库中生成一张表
                    key.ToTable("BlogPostMapAuthor");
                    key.MapLeftKey("AuthorID");  //取左边 author的键
                    key.MapRightKey("PostID");   //取右表 post的键
                });

        }
    }
}
