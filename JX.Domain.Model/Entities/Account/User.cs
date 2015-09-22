using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    public class User :AggregateRoot
    {
        public string UserName
        { get;set; }

        public string UserPWD
        { get;set; }
    }
}
