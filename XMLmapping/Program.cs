using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using XMLmapping.Model;

namespace XMLmapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Product {Category = "Car", Discontinued = false, Name = "Seat"};
            var p2 = new Product {Category = "Fruit", Discontinued = true, Name = "Apple"};
            var p3 = new Product {Category = "Smartphone", Discontinued = false, Name = "Nexus"};
            try
            {
                Insert(p1);
                p1.Name = "Fiat";
                Update(p1);
                Insert(p2);
                Remove(p2);
                Insert(p3);
                var tmp1 = GetByName("Nexus");
                Console.WriteLine(tmp1.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        private static void Insert(Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                }
            }
        }

        private static void Update(Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(product);
                    transaction.Commit();
                }
            }
        }

        private static void Remove(Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
            }
        }

        private static Product GetByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var product = session.CreateCriteria(typeof (Product))
                                         .Add(Restrictions.Eq("Name", name))
                                         .UniqueResult<Product>();
                return product;
            }
        }

    }
}
