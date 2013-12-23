using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMapping.Domain;
using NHibernate.Criterion;

namespace FluentMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Movie { Title = "Pulp Fiction", Description = "dfsd", Director = "Tarantino"};
            var p2 = new Movie { Title = "Roki", Description = "dfsd", Director = "ktos" };
            var p3 = new Movie { Title = "Kewin", Description = "dfsd", Director = "ktos2" };
            try
            {
                Insert(p1);
                p1.Title = "Jaro";
                Update(p1);
                Insert(p2);
                Remove(p2);
                Insert(p3);
                var tmp1 = GetByName("Kewin");
                Console.WriteLine(tmp1.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
        private static void Insert(Movie osoba)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(osoba);
                    transaction.Commit();
                }
            }
        }

        private static void Update(Movie osoba)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(osoba);
                    transaction.Commit();
                }
            }
        }

        private static void Remove(Movie osoba)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(osoba);
                    transaction.Commit();
                }
            }
        }

        private static Movie GetByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var product = session.CreateCriteria(typeof(Movie))
                                         .Add(Restrictions.Eq("Title", name))
                                         .UniqueResult<Movie>();
                return product;
            }
        }
    }
}
