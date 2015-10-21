using OrmDemo.Domain;
using System.Data.Entity;

namespace OrmDemo.EF
{
    public class PersonContext : DbContext
    {
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<Organization> Orgs { get; set; }

        public PersonContext(string nameOrConnectionString): base(nameOrConnectionString)
        {
        }
    }
}
