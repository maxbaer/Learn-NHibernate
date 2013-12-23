using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentMapping.Domain
{
    public class Movie
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Director { get; set; }

        public override string ToString()
        {
            return string.Format("{0} rez {1}", Title, Director);
        }
    }
}
