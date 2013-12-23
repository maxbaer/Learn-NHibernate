using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using FluentMapping.Domain;

namespace FluentMapping.Mapping_classes
{
    public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Title).Length(250).Not.Nullable();
            Map(x => x.Description);
            Map(x => x.Director).Length(50).Not.Nullable();
        }
    }
}