using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public sealed class DDDTestContextInitailizer : DropCreateDatabaseIfModelChanges<DDDTestContext>
    {
        public static void Initialize()
        {
            Database.SetInitializer<DDDTestContext>(null);
        }

        /*
        //用seed的时候，需要在CONFIG中的context指定初始化器
        protected override void Seed(AccountContext context)
        {
            base.Seed(context);
        }*/
    }
}
