using ConsoleApp112.Enum;
using golestan;

namespace ConsoleApp112.Entities
{
    public class Student : User
    {
        public int StudentNo { get; private set; }
        public int Age { get; set; }
        public StudentStatusEnum Status { get; private set; }
        public GenderEnum Gender { get; set; }
        public bool IsMashrot { get; set; }
        public bool IsMomtaZ { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public Student(string firstName, string lastName, string email) : base(firstName, lastName, email)
        {
            Status = StudentStatusEnum.Inactive;
        }
        public Student(string firstName, string lastName, string email, int studeinNo, int age, GenderEnum gender) : this(firstName, lastName, email)
        {
            StudentNo = studeinNo;
            Age = age;
            Gender = gender;
        }
        public override Result changePassword(string currentPass, string newPass)
        {

            if (!string.IsNullOrEmpty(currentPass) && newPass.Length >= 6)
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
        public string Activate()
        {
            if (Status == StudentStatusEnum.Active)
                return "Student Already Actived.";
            if (Status == StudentStatusEnum.Suspend)
                return "Cannot Active Student.";
            Status = StudentStatusEnum.Active;
            return "Student Active SuccessFuly.";
        }
        public string setMashrot()
        {
            IsMashrot = true;
            return "Success";
        }
        public override Result SetPassword(string pass)
        {
            if (!string.IsNullOrEmpty(pass) && pass.Length >= 6)
            {
                Password = pass;
                return new Result(true, null);

            }
            else
                return new Result(false, "Password Is Invalid");
        }
        public string Setmomtaz()
        {
            IsMashrot = true;
            return "Succsess";
        }
        public string AddCourse(Course course)
        {

            int maxUnits = IsMashrot ? 14 : IsMomtaZ ? 24 : 20;
            int currentTotalUnits = 0;
            foreach (var existingCourse in Courses)
            {
                currentTotalUnits += existingCourse.Unit;
            }
            if (currentTotalUnits + course.Unit > maxUnits)
            {
                return $"Cannot add course. Exceeds maximum allowed units of {maxUnits}.";
            }
            Courses.Add(course);
            return "Course added successfully.";
        }
        public void ViewSchedule()
        {
            if (Courses.Count == 0)
            {
                Console.WriteLine("You have not selected any courses yet.");
            }
            else
            {
                Console.WriteLine("Your class schedule:");
                foreach (var course in Courses)
                {
                    Console.WriteLine($"Course: {course.Name} | Teacher: {course.Name} | Schedule: {course.Schedule}");
                }
            }
        }


        //************************************************************************************************//
        private bool IsOverlapping(Course existingCourse, Course newCourse)
        {
            return (newCourse.StartTime < existingCourse.EndTime) && (existingCourse.StartTime < newCourse.EndTime);
        }
        public bool CanAddCourse(Course newCourse)
        {
            foreach (var course in Courses)
            {
                if (IsOverlapping(course, newCourse))
                {
                    return false;
                }
            }
            return true;

        }


    }
}
