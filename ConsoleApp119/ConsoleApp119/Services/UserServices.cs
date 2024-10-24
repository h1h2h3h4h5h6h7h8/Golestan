using ConsoleApp112.Contract;
using ConsoleApp112.Entities;
using ConsoleApp112.Repository;
using golestan;

namespace ConsoleApp112.Services
{
    public class UserServices
    {
        IUserRepository userRepo;
        public UserServices()
        {
            userRepo = new UserRepository();
        }
        public Result Login(string username, string password)
        {

            var users = userRepo.GetUsers();
            foreach (var user in users)
            {
                if (user.Username == username)
                {

                    var res = user.checkpasswor(password);
                    if (res.IsSucces)
                    {
                        if (user.IsActive)
                        {
                            userRepo.Login(user);
                            return new Result(true);
                        }
                        else
                        {
                            return new Result(false, "User Is InActive. ");
                        }

                    }
                    else
                    {
                        return new Result(false, "Password Is Invalid. ");
                    }

                }

            }
            return new Result(false, "User Not Found. ");
        }
        public Result Registre(User user, string pass)
        {

            var result = user.SetPassword(pass);
            if (result.IsSucces)
            {
                userRepo.Register(user);
                return new Result(true);
            }
            else
            {
                return result;
            }

        }
        public User GetCurrentUser()
        {
            return userRepo.GetCurrentUser();
        }
    }
}
