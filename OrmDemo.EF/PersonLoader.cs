using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using OrmDemo.Domain;

namespace OrmDemo.EF
{
    public class PersonLoader
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrmDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static IEnumerable<Person> LoadPersons(IEnumerable<string> names)
        {

            using (var ctx = new PersonContext(ConnectionString))
            {
                return ctx
                    .Persons
                    .Include(p => p.MemberOf)
                    .Where(p => names.Contains(p.Name))
                    .ToList();
            }
        }

        public static void TestIdentityMap()
        {
            using (var ctx = new PersonContext(ConnectionString))
            {
                var personA1 = GetPersonByName(ctx, "A");
                personA1.MemberOf.AddAmount(100);
                var personA2 = GetPersonByName(ctx, "A");

                var orgA = ctx
                    .Orgs
                    .ToList()
                    .First(o => o.Name.StartsWith("OrgA"));
                var orgB = ctx
                    .Orgs
                    .ToList()
                    .First(o => o.Name.StartsWith("OrgB"));
            }
        }

        private static Person GetPersonByName(PersonContext ctx, string name)
        {
            return ctx
                    .Persons
                    .Include(p => p.MemberOf)
                    .AsNoTracking()
                    .First(p => p.Name == name);
        }
    }
}
