using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MappingByCode.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MappingByCode.Mappings
{
    public class AnimalMap : ClassMapping<Animal>
    {
        public AnimalMap()
        {
            Id(x=>x.Id, map=>map.Generator(Generators.HighLow,gmap=>gmap.Params(new { max_low=100 })));
            Property(x=>x.Name, map => { map.NotNullable(true); map.Length(30);} );
            Property(x=>x.Sex, map => { map.NotNullable(true); map.Length(5);});
            Property(x=>x.Species, map => { map.NotNullable(true); map.Length(50);});
        }
    }
}
