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
    public class AccountContext :DbContext
    {
        //数据库试用DDDTest作为名称
        public AccountContext() : base("AccountContext")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
               .Configurations
               .Add(new UserTypeConfiguration())
               .Add(new UserRoleTypeConfiguration())
               .Add(new RoleTypeConfiguration());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
           
        }
    }
}
