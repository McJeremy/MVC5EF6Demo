using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.Model.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public class DDDTestContext :DbContext
    {
        //使用defaultConnectionFactory的配置时，需要指定数据库名称
        //LocalDbConnectionFactory，可以改为：SqlConnectionFactory
        /*
            <entityFramework>
                <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
                  <parameters>
                    <parameter value="mssqllocaldb" />
                  </parameters>
                </defaultConnectionFactory>
                <providers>
                  <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
                </providers>
            </entityFramework>
        */
        //public DDDTestContext() : base("DDDTestContext")
        //{
        //    this.Configuration.AutoDetectChangesEnabled = true;
        //    this.Configuration.LazyLoadingEnabled = true;
        //}

        //在没有使用defaultConnectionFactory的配置前提下，而是用connectionstring配置时，
        //不需要传递数据库名称，如下，将使用DDDTest作为数据库名称
        /*
        <connectionStrings>
            <add name="DDDTestContext" providerName="System.Data.SqlClient" connectionString="Server=xuzz\SQL;DataBase=DDDTest;uid=sa;pwd=123;"/>
        </connectionStrings>
        */
        public DDDTestContext()
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users
        {
            get
            {
                return Set<User>();
            }
        }

        public DbSet<Role> Roles
        {
            get
            {
                return Set<Role>();
            }
        }

        public DbSet<UserRole> UserRoles
        {
            get
            {
                return Set<UserRole>();
            }
        }

        public DbSet<Blog> Blogs
        {
            get
            {
                return Set<Blog>();
            }
        }

        public DbSet<BlogPost> BlogPosts
        {
            get
            {
                return Set<BlogPost>();
            }
        }

        public DbSet<BlogUser> BlogUsers
        {
            get
            {
                return Set<BlogUser>();
            }
        }

        public DbSet<BlogPostAuthor> BlogPostAuthors
        {
            get
            {
                return Set<BlogPostAuthor>();
            }
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
               .Configurations
               .Add(new UserTypeConfiguration())
               .Add(new UserRoleTypeConfiguration())
               .Add(new RoleTypeConfiguration())
               .Add(new BlogTypeConfiguration())
               .Add(new BlogUserTypeConfiguration())
               .Add(new BlogPostTypeConfiguration())
               .Add(new BlogPostAuthorTypeConfiguration());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
           
        }
    }
}
