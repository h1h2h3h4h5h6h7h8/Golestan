using ConsoleApp112.DataBase;

namespace ConsoleApp112.Entities
{
    public class Operator : User
    {
        public Operator(string firstName, string lastName, string email) : base(firstName, lastName, email)
        {
        }
        public string ActivateDeactivateUser(int userId, bool isActive)
        {
            var user = InMemoryDB.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.SetActiveStatus(isActive);
                return isActive ? "User has been activated." : "User has been deactivated.";
            }
            return "User not found.";
        }
        public string SetStudentStatus(int studentId, bool isMashrot, bool isMomtaZ)
        {
            var student = InMemoryDB.Users.OfType<Student>().FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.IsMashrot = isMashrot;
                student.IsMomtaZ = isMomtaZ;
                return "Student status updated successfully.";
            }
            return "Student not found.";
        }


    }
}
