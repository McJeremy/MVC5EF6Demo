using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    public class Role : AggregateRoot
    {
        public string RoleDESC
        {
            get;set;
        }

        public string RoleName
        {
            get;set;
        }
    }
}
