using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    public class UserRole : AggregateRoot
    {
        public Guid UserID
        {
            get;set;
        }

        public Guid RoleID
        {
            get;set;
        }
    }
}
