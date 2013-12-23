using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLmapping.Model
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual bool Discontinued { get; set; }

        public override string ToString()
        {
            return string.Format("Product: {0} [Category: {1}, Id: {2}]",Name,Category,Id);
        }
    }
}
