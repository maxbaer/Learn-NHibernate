using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingByCode.Domain
{
    public class Animal
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Species { get; set; }
        public virtual string Sex { get; set; }
    }
}
