using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;

namespace AttributeMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Osoba { Name="Tomasz", Surname = "Malinowski", Email = "malinowski@gmail.com"};
            var p2 = new Osoba { Name = "Dorota", Surname = "P", Email = "dorotap@gmail.com" };
            var p3 = new Osoba { Name = "Przemek", Surname = "Przemek", Email = "pp@gmail.com" };
            try
            {
                Insert(p1);
                p1.Name = "Jarosław";
                Update(p1);
                Insert(p2);
                Remove(p2);
                Insert(p3);
                var tmp1 = GetByName("Przemek");
                Console.WriteLine(tmp1.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        private static void Insert(Osoba osoba)
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

        private static void Update(Osoba osoba)
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

        private static void Remove(Osoba osoba)
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

        private static Osoba GetByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var product = session.CreateCriteria(typeof(Osoba))
                                         .Add(Restrictions.Eq("Name", name))
                                         .UniqueResult<Osoba>();
                return product;
            }
        }
       
    }
}
