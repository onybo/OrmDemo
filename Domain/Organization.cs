namespace OrmDemo.Domain
{
    public class Organization
    {
        public Organization()
        {
        }

        public Organization(int id, string name, decimal amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public int Id { get; set; }
        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public void AddAmount(decimal x)
        {
            Amount += x;
        }
    }
}
