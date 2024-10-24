using ConsoleApp112.Entities;

namespace ConsoleApp112.DataBase
{
    public static class SeedData
    {
        public static void Seed()
        {
            Operator op = new Operator("Admin", "Admin", "Admin@gmail.com");
            op.SetPassword("123456");
            op.ActivateUser();
            InMemoryDB.Users.Add(op);

            Teacher teacher = new Teacher("hosein", "you", "h@gmail.com");
            teacher.SetPassword("123456");
            teacher.ActivateUser();
            InMemoryDB.Users.Add(teacher);

        }
    }
}
