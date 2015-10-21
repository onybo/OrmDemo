using System.Collections.Generic;
using System.Linq;
using OrmDemo.Domain;
using System.Data.SqlClient;
using Dapper;

namespace OrmDemo.Dapper
{
    public class PersonLoader
    {
        public static IEnumerable<Person> LoadPersons(IEnumerable<string> names)
        {
            const string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrmDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            const string sql = @"select * from people p
                            join organizations o on p.MemberOf_Id = o.Id
                            where p.Name = @name";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var name in names)
                {
                    yield return connection.Query<Person, Organization, Person>(sql,
                        (person, org) => { person.MemberOf = org; return person; },
                        new { name })
                        .First();
                }
                connection.Close();
            }
        }
    }
}
