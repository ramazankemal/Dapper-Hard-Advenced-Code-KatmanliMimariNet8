using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class OperationClaim:IEntity
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
    }
}
