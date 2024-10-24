namespace ConsoleApp112.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public string Schedule { get; set; }
        public int Capacity { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        

        public override string ToString()
        {
            return $"{Id}-{Name}({Unit}  _  {Teacher.FirstName}  {Teacher.LastName})";
        }






    }
}
