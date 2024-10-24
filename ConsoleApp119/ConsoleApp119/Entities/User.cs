using ConsoleApp112.DataBase;
using ConsoleApp112.Enum;
using golestan;

namespace ConsoleApp112.Entities
{
    public class User
    {
        private static int idCounter = 1;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        protected string Password { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; private set; }
        public StudentStatusEnum Status { get; set; }
        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = email;
            Id = idCounter++;
            IsActive = true;


        }

        public virtual Result changePassword(string currentPass, string newPass)
        {
            //if(currentPass == Password)
            //{
            //    return SetPassword(newPass);
            //}
            //else
            //{
            //    return new Result(false, "current Pass Is InCurrect.");
            //}

            if (!string.IsNullOrEmpty(currentPass) && newPass.Length >= 3)
            {
                if (currentPass == Password)
                {
                    Password = newPass;
                    return new Result(true, "Password Changed SuccesFuly.");

                }
                else
                    return new Result(false, "current Pass Is InCurrect.");

            }
            else
                return new Result(false, "NewPass Is Invalid");


        }
        public string ActivateUser()
        {
            IsActive = true;
            return "Succes";
        }
        public Result checkpasswor(string pass)
        {
            if (Password == pass)
                return new Result(true, null);
            else
                return new Result(false, "Password Is Incorrect. ");
        }
        public virtual Result SetPassword(string pass)
        {
            if (!string.IsNullOrEmpty(pass) && pass.Length >= 3)
            {
                Password = pass;
                return new Result(true, null);

            }
            else
                return new Result(false, "Password Is Invalid");
        }
        public string ActivateDeactivateUser(int userId, bool isActive)
        {
            var user = InMemoryDB.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsActive = isActive;
                return isActive ? "User has been activated." : "User has been deactivated.";
            }
            return "User not found.";
        }
        public void SetActiveStatus(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
