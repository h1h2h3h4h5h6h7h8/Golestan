using ConsoleApp112.Entities;

namespace ConsoleApp112.Contract
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void Login(User user);
        void Register(User user);
        User GetCurrentUser();

    }
}
