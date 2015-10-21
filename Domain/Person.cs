namespace OrmDemo.Domain
{
    public class Person
    {
        public Person()
        {
        }

        public Person(int id, string name, Organization memberOf)
        {
            Id = id;
            Name = name;
            MemberOf = memberOf; 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Organization MemberOf { get; set; }
    }
}
