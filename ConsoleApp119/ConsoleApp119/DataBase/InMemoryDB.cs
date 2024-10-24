using ConsoleApp112.Entities;

namespace ConsoleApp112.DataBase
{
    public static class InMemoryDB
    {
        public static User CurrentUser { get; set; }
        public static List<User> Users { get; set; } = new List<User>();
        public static List<Course> Courses { get; set; } = new List<Course>();
        public static Course FindCourseByTitle(string nameSt)
        {
            foreach (var course in Courses)
            {
                if (course.Name.Equals(nameSt, StringComparison.OrdinalIgnoreCase))
                {
                    return course;
                }
            }

            return null;
        }
        public static User FindStudentById(int studentId)
        {
            return Users.FirstOrDefault(s => s.Id == studentId);
        }
        public static void AddCourse(Course course)
        {
            Courses.Add(course);
        }




    }
}
