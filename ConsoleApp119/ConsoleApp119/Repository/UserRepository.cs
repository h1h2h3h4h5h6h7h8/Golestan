using ConsoleApp112.Contract;
using ConsoleApp112.DataBase;
using ConsoleApp112.Entities;

namespace ConsoleApp112.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetCurrentUser()
        {
            return InMemoryDB.CurrentUser ?? new User("test", "test", "test");
        }

        public List<User> GetUsers()
        {
            return InMemoryDB.Users;
        }



        public void Login(User user)
        {
            InMemoryDB.CurrentUser = user;
        }

        public void Register(User user)
        {
            InMemoryDB.Users.Add(user);
        }
    }
}
