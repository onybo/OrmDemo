using System;
using System.Collections.Generic;
using System.Linq;

namespace OrmDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessDonations(new[]
            {
                new Tuple<string, int>("A", 10),
                new Tuple<string, int>("B",  20)
            });
            //EF.PersonLoader.TestIdentityMap();
        }

        private static void ProcessDonations(IReadOnlyCollection<Tuple<string, int>> donations)
        {
            // Change the Fs to EF or Dapper for different ORM implementation
            var persons = Fs.PersonLoader.LoadPersons(donations.Select(x => x.Item1)).ToList();

            AddDonations(donations, persons);

            LogOrgAmounts(persons);
        }

        private static void AddDonations(IReadOnlyCollection<Tuple<string, int>> donations, IEnumerable<Domain.Person> persons)
        {
            foreach (var person in persons)
            {
                person.MemberOf.AddAmount(
                    donations.FirstOrDefault(d => d.Item1 == person.Name)?.Item2 ?? 0);
            }
        }

        private static void LogOrgAmounts(List<Domain.Person> persons)
        {
            persons
                .ToList()
                .ForEach(p =>
                    Console.WriteLine(
                        $"Org name = {p.MemberOf.Name}, amount = {p.MemberOf.Amount}"));
        }
    }
}
