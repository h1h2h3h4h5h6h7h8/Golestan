using ConsoleApp112.Contract;
using ConsoleApp112.DataBase;
using ConsoleApp112.Entities;
using ConsoleApp112.Repository;

namespace ConsoleApp112.Services
{
    public class CourseService
    {
        private static List<Course> courses = new List<Course>();
        ICourseRepository _courseRepository;
        ITeacherRepository _teacherpository;
        public CourseService()
        {
            _courseRepository = new CourseRepository();
            _teacherpository = new TeacherRepository();
        }
        public void SetCourse(Course course)
        {
            course.Teacher = _teacherpository.GetTeacher();
            _courseRepository.AddCourse(course);
        }
        public List<Course> GetCourses()
        {
            return _courseRepository.GetCourses();
        }
        public void AddDefaultCourses()
        {
            InMemoryDB.Courses.Add(new Course { Id = 1, Name = "Mathematics", Unit = 3, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(10, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 2, Name = "Physics", Unit = 4, StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(12, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 3, Name = "Chemistry", Unit = 3, StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(14, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 4, Name = "Computer Science", Unit = 4, StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(16, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 5, Name = "History", Unit = 2, StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(9, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 6, Name = "Literature", Unit = 3, StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(11, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 7, Name = "Biology", Unit = 3, StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(13, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 8, Name = "Psychology", Unit = 2, StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(15, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 9, Name = "Art", Unit = 2, StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(17, 30) });
            InMemoryDB.Courses.Add(new Course { Id = 10, Name = "Music", Unit = 1, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(18, 0) });
        }
        public List<Course> GetAllCourses()//*
        {

            if (InMemoryDB.Courses == null || !InMemoryDB.Courses.Any())
            {
                Console.WriteLine("No courses available.");
                return new List<Course>();
            }


            return InMemoryDB.Courses;
        }
    }
}
