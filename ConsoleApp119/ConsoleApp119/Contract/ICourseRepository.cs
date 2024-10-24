using ConsoleApp112.Entities;

namespace ConsoleApp112.Contract
{
    public interface ICourseRepository
    {
        void AddCourse(Course course);
        List<Course> GetCourses();
    }
}
