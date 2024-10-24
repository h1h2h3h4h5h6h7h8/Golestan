using ConsoleApp112.Contract;
using ConsoleApp112.DataBase;
using ConsoleApp112.Entities;

namespace ConsoleApp112.Repository
{
    internal class TeacherRepository : ITeacherRepository
    {
        public Teacher GetTeacher()
        {
            foreach (var user in InMemoryDB.Users)
            {
                if (user is Teacher)
                {
                    return (Teacher)user;
                }
            }
            return new Teacher("test", "test", "test");
        }
    }
}
