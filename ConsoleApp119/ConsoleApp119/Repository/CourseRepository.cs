using ConsoleApp112.Contract;
using ConsoleApp112.DataBase;
using ConsoleApp112.Entities;

namespace ConsoleApp112.Repository
{
    public class CourseRepository : ICourseRepository
    {

        public void AddCourse(Course course)
        {
            InMemoryDB.Courses.Add(course);
        }

        public List<Course> GetCourses()
        {
            return InMemoryDB.Courses;
        }

    }
}
