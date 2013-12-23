using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;


namespace AttributeMapping
{
    [Class(Name="AttributeMapping.Osoba, AttributeMapping", Lazy = false)]
    public class Osoba
    {
        [Id(0,Name="ID", TypeType = typeof (int))]
        [Generator(1, Class = "identity")]
        public virtual int ID { get; set; }
        [Property]
        public virtual string Name { get; set; }
        [Property]
        public virtual string Surname { get; set; }
        [Property]
        public virtual string Email { get; set; }
    }
}
