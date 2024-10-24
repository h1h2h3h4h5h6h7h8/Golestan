using ConsoleApp112.DataBase;
using ConsoleApp112.Entities;
using ConsoleApp112.Enum;
using ConsoleApp112.Services;

Console.WriteLine("Wellcom To Golestan.");
SeedData.Seed();

CourseService cs = new CourseService();
cs.AddDefaultCourses();
SeedData.Seed();
MainMenu();





void MainMenu()
{

    Console.Clear();
    Console.WriteLine("*************************");
    Console.WriteLine("       Main Menu       ");
    Console.WriteLine("*************************");
    Console.WriteLine("1) Register");
    Console.WriteLine("2) Login");

    if (!Int32.TryParse(Console.ReadLine(), out int actionId))
    {
        Console.WriteLine("Selected Action Is Invalid.");
        MainMenu();
    }
    switch (actionId)
    {
        case 1:
            Register();
            break;
        case 2:
            Login();
            break;
        default:
            Console.WriteLine("Selected Action Is Invalid.");
            MainMenu();
            break;
    }
}
void EntakhabVahedMenu()
{




    if (InMemoryDB.CurrentUser == null || InMemoryDB.CurrentUser is not Student)
    {
        Console.WriteLine("Please Login.");
        Console.Write("Press Any Key To Continue: ");
        Console.ReadLine();
        Login();
    }

    Console.Clear();
    Console.WriteLine("*************************");
    Console.WriteLine("       Student Menu       ");
    Console.WriteLine("*************************");
    Console.WriteLine("*********************** Selected Courses *********************");

    UserServices userServices = new UserServices();
    Student currentUser = (Student)userServices.GetCurrentUser();

    int totalUnits = 0;

    foreach (var course in currentUser.Courses)
    {
        Console.WriteLine(course);
        totalUnits += course.Unit;
    }

    Console.WriteLine("Total Units: " + totalUnits);
    Console.WriteLine("*********************** Selected Courses *********************");

    Console.WriteLine("*********************** All Courses *********************");
    CourseService courseService = new CourseService();
    var allCourses = courseService.GetAllCourses();

    foreach (var course in allCourses)
    {
        Console.WriteLine($"Id: {course.Id}, Name: {course.Name}, Units: {course.Unit}, Start Time: {course.StartTime}, End Time: {course.EndTime}");
    }
    Console.WriteLine("*********************** All Courses *********************");


    Console.WriteLine("Please Select Your Course Id: ");
    var selectedCourseId = Int32.Parse(Console.ReadLine());

    bool courseAlreadySelected = false;
    foreach (var item in currentUser.Courses)
    {
        if (item.Id == selectedCourseId)
        {
            courseAlreadySelected = true;
            break;
        }
    }

    if (!courseAlreadySelected)
    {
        Course selectedCourse = null;
        foreach (var item in allCourses)
        {
            if (item.Id == selectedCourseId)
            {
                selectedCourse = item;
                break;
            }
        }

        if (selectedCourse != null)
        {
            bool timeConflict = false;

            foreach (var enrolledCourse in currentUser.Courses)
            {
                if (IsTimeConflicting(enrolledCourse.StartTime, enrolledCourse.EndTime, selectedCourse.StartTime, selectedCourse.EndTime))
                {
                    timeConflict = true;
                    break;
                }
            }

            if (!timeConflict)
            {
                currentUser.Courses.Add(selectedCourse);
                selectedCourse.Students.Add(currentUser);
                totalUnits += selectedCourse.Unit;
                Console.WriteLine("Course selected successfully.");
            }
            else
            {
                Console.WriteLine("Time conflict detected. Please select a different course.");
            }
        }
        else
        {
            Console.WriteLine("Course not found.");
        }

        Console.WriteLine("Do you want to select another course? (yes/no)");
        string choice = Console.ReadLine().ToLower();

        if (choice == "no")
        {
            Console.WriteLine("Returning to Student Menu.");
            StudentMenu();
        }
        else
        {
            EntakhabVahedMenu();
        }
    }
    else
    {
        Console.WriteLine("This course has already been selected.");
        EntakhabVahedMenu();
    }

    bool IsTimeConflicting(TimeOnly existingStart, TimeOnly existingEnd, TimeOnly newStart, TimeOnly newEnd)
    {
        return (existingStart < newEnd && newStart < existingEnd);
    }


}


void StudentMenu()
{
    if (InMemoryDB.CurrentUser == null || InMemoryDB.CurrentUser is not Student)
    {
        Console.WriteLine("Please Login. ");
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Login();
    }


    Console.Clear();
    Console.WriteLine("*************************");
    Console.WriteLine("       StudentMenu       ");
    Console.WriteLine("*************************");
    Console.WriteLine("1) Change Password");
    Console.WriteLine("2) EntakhabVahed");
    Console.WriteLine("3) View the class schedule");
    Console.WriteLine("4) Logout");
    if (!Int32.TryParse(Console.ReadLine(), out int actionId))
    {
        Console.WriteLine("Selected Action Id Invalid.");
        StudentMenu();
    }
    switch (actionId)
    {//*
        case 1:
            Console.WriteLine("Please Enter Your Current Password: ");
            var currentPass = Console.ReadLine();
            Console.WriteLine("Please Enter Your Current New Password: ");
            var newPass = Console.ReadLine();
            Student St = (Student)InMemoryDB.CurrentUser;
            var result = St.changePassword(currentPass, newPass);

            Console.WriteLine(result.Message);
            Console.Write("Press Any Key To Continiue: ");
            Console.ReadLine();
            StudentMenu();

            break;
        case 2:
            EntakhabVahedMenu();
            break;
        case 3:
            ((Student)InMemoryDB.CurrentUser).ViewSchedule();
            Console.Write("Press Any Key To Continue: ");
            Console.ReadLine();
            StudentMenu();
            break;
        case 4:
            InMemoryDB.CurrentUser = null;
            MainMenu();
            break;
        default:
            StudentMenu();
            break;

    }
}
void OperatorMenu()
{
    if (InMemoryDB.CurrentUser == null || InMemoryDB.CurrentUser is not Operator)
    {
        Console.WriteLine("Please Login. ");
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Login();
    }


    Console.Clear();
    Console.WriteLine("*************************");
    Console.WriteLine("       OperatorMenu       ");
    Console.WriteLine("*************************");
    Console.WriteLine("1) Change Password");
    Console.WriteLine("2) Activate User");
    Console.WriteLine("3) Add Coursee");
    Console.WriteLine("4) Change Student Status");
    Console.WriteLine("5) Logout");
    if (!Int32.TryParse(Console.ReadLine(), out int actionId))
    {
        Console.WriteLine("Selected Action Id Invalid.");
        StudentMenu();
    }
    switch (actionId)
    {//*
        case 1:
            Console.WriteLine("Please Enter Your Current Password: ");
            var currentPass = Console.ReadLine();
            Console.WriteLine("Please Enter Your Current New Password: ");
            var newPass = Console.ReadLine();
            Operator op = (Operator)InMemoryDB.CurrentUser;
            var result = op.changePassword(currentPass, newPass);

            Console.WriteLine(result.Message);
            Console.Write("Press Any Key To Continiue: ");
            Console.ReadLine();
            OperatorMenu();

            break;
        case 2:
            Console.WriteLine("Please Enter User ID to Activate/Deactivate: ");
            var userId = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter 1 to Activate, 0 to Deactivate: ");
            var action = Int32.Parse(Console.ReadLine());
            bool isActive = action == 1;

            Operator operatorUser = (Operator)InMemoryDB.CurrentUser;
            var activationResult = operatorUser.ActivateDeactivateUser(userId, isActive);

            Console.WriteLine(activationResult);
            Console.Write("Press Any Key To Continue: ");
            Console.ReadLine();
            OperatorMenu();
            break;
        case 3:
            Console.WriteLine("Please Enter Your Course Id: ");
            var courseId = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter Your Course Name: ");
            var courseName = Console.ReadLine();

            Console.WriteLine("Please Enter Your Course Unit: ");
            var courseUnit = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter Your Course Capacity: ");
            var courseCapacity = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter Your Course Schedule: ");
            var courseSchedule = Console.ReadLine();

            Console.WriteLine("Please Enter Course Start Time (HH:mm): ");
            var startTimeInput = Console.ReadLine();
            var startTime = TimeOnly.Parse(startTimeInput);

            Console.WriteLine("Please Enter Course End Time (HH:mm): ");
            var endTimeInput = Console.ReadLine();
            var endTime = TimeOnly.Parse(endTimeInput);


            Course course = new Course();
            course.Id = courseId;
            course.Name = courseName;
            course.Unit = courseUnit;
            course.Capacity = courseCapacity;
            course.Schedule = courseSchedule;
            course.StartTime = startTime;
            course.EndTime = endTime;
            CourseService courseService = new CourseService();
            courseService.SetCourse(course);
            OperatorMenu();

            break;
        case 4:

            Console.WriteLine("Enter Student ID:");
            int studentId = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Is the student Mashrot? (true/false):");
            bool isMashrot = bool.Parse(Console.ReadLine());

            Console.WriteLine("Is the student MomtaZ? (true/false):");
            bool isMomtaZ = bool.Parse(Console.ReadLine());
            Operator operatorUser1 = (Operator)InMemoryDB.CurrentUser;



            string resultMessage = operatorUser1.SetStudentStatus(studentId, isMashrot, isMomtaZ);
            Console.WriteLine(resultMessage);
            OperatorMenu();

            break;

        case 5:
            InMemoryDB.CurrentUser = null;
            MainMenu();
            break;
        default:
            OperatorMenu();
            break;

    }
}
void TeacherMenu()
{


    if (InMemoryDB.CurrentUser == null)
    {
        Console.WriteLine("Please Login. ");
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Login();
    }
    if (InMemoryDB.CurrentUser != null && InMemoryDB.CurrentUser is not Teacher)
    {
        Console.WriteLine("Access Denid . ");
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Login();
    }

    Console.Clear();
    Console.WriteLine("*************************");
    Console.WriteLine("       Teacher Menu       ");
    Console.WriteLine("*************************");
    Console.WriteLine("Please Select Your Action: ");
    Console.WriteLine("1) Change Password");
    Console.WriteLine("2) Add New Course");
    Console.WriteLine("3) View Registered Students");
    Console.WriteLine("4) Logout");

    if (!int.TryParse(Console.ReadLine(), out int actionId))
    {
        Console.WriteLine("Invalid Selection.");
        TeacherMenu();
    }

    switch (actionId)
    {
        case 1:
            Console.WriteLine("Please Enter Your Current Password: ");
            var currentPass = Console.ReadLine();
            Console.WriteLine("Please Enter Your Current New Password: ");
            var newPass = Console.ReadLine();
            Teacher te = (Teacher)InMemoryDB.CurrentUser;
            var result = te.changePassword(currentPass, newPass);

            Console.WriteLine(result.Message);
            Console.Write("Press Any Key To Continiue: ");
            Console.ReadLine();
            TeacherMenu();
            break;
        case 2:
            Console.Write("Enter Course Id: ");
            int id = Int32.Parse(Console.ReadLine());

            Console.Write("Enter Course Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Course Unit ");
            int unit = Int32.Parse(Console.ReadLine());


            Console.WriteLine("Please Enter Course Start Time (HH:mm): ");
            var startTimeInput = Console.ReadLine();
            var startTime = TimeOnly.Parse(startTimeInput);

            Console.WriteLine("Please Enter Course End Time (HH:mm): ");
            var endTimeInput = Console.ReadLine();
            var endTime = TimeOnly.Parse(endTimeInput);

            Course newCourse = new Course
            {
                Id = id,
                Name = name,
                Unit = unit,
                StartTime = startTime,
                EndTime = endTime



            };
            InMemoryDB.Courses.Add(newCourse);

            Console.WriteLine("Course Added Successfully!");
            TeacherMenu();
            break;
        case 3:
            Console.Write("Enter Course Name to view registered students: ");
            string courseNameForViewing = Console.ReadLine();
            var courseForViewing = InMemoryDB.FindCourseByTitle(courseNameForViewing);

            if (courseForViewing != null)
            {
                Console.WriteLine($"Course Found: {courseForViewing.Name}, Registered Students Count: {courseForViewing.Students.Count}");

                if (courseForViewing.Students.Count > 0)
                {
                    Console.WriteLine("Registered Students:");
                    foreach (var student in courseForViewing.Students)
                    {

                        Console.WriteLine(student);
                    }
                }
                else
                {
                    Console.WriteLine("No students registered for this course.");
                }
            }
            else
            {
                Console.WriteLine("Course not found.");
            }
            Console.ReadKey();
            TeacherMenu();
            break;
        case 4:
            InMemoryDB.CurrentUser = null;
            MainMenu();
            break;

        default:
            Console.WriteLine("Invalid Action.");
            TeacherMenu();
            break;
    }
}


void Login()
{
    Console.Clear();
    Console.WriteLine("*******************");
    Console.WriteLine("       Login       ");
    Console.WriteLine("*******************");
    Console.WriteLine(" 1) Student ");
    Console.WriteLine(" 2) Operator");
    Console.WriteLine(" 3) Teacher");

    if (!Int32.TryParse(Console.ReadLine(), out int roleId))
    {
        Console.WriteLine("Selected Role Id Invalid.");
        Login();
    }
    RoleEnum rol = (RoleEnum)roleId;

    Console.WriteLine("Pleas Enter Username:");
    var username = Console.ReadLine();

    Console.WriteLine("Pless Enter Password:");
    var password = Console.ReadLine();

    UserServices userServices = new UserServices();
    var result = userServices.Login(username, password);
    if (!result.IsSucces)
    {
        Console.WriteLine(result.Message);
        Console.WriteLine("User IS InActive. ");
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Login();
    }
    switch (rol)
    {
        case RoleEnum.Student:
            StudentMenu();
            break;
        case RoleEnum.Operator:
            OperatorMenu();
            break;
        case RoleEnum.Teacher:
            TeacherMenu();
            break;
        default:
            break;
    }

}
void Register(string? message = null)
{
    Console.Clear();
    if (message != null)
    {
        Console.WriteLine(message);
    }
    Console.WriteLine("*******************");
    Console.WriteLine("      Register     ");
    Console.WriteLine("*******************");
    Console.WriteLine(" 1) Student ");
    Console.WriteLine(" 2) Operator");
    Console.WriteLine(" 3) Teacher");

    if (!Int32.TryParse(Console.ReadLine(), out int roleId))
    {
        Console.WriteLine("Selected Role Id Invalid.");
        Register();
    }
    RoleEnum rol = (RoleEnum)roleId;

    Console.WriteLine("Pless Enter FirstName:");
    var firstName = Console.ReadLine();

    Console.WriteLine("Pless Enter LastName:");
    var lastName = Console.ReadLine();

    Console.WriteLine("Pless Enter Email:");
    var email = Console.ReadLine();

    Console.WriteLine("Pless Enter Password:");
    var password = Console.ReadLine();

    UserServices userServices = new UserServices();
    User user;
    switch (rol)
    {
        case RoleEnum.Student:

            user = new Student(firstName, lastName, email);
            break;
        case RoleEnum.Operator:
            user = new Operator(firstName, lastName, email);
            break;
        case RoleEnum.Teacher:
            user = new Teacher(firstName, lastName, email);
            break;

        default:
            user = new User(firstName, lastName, email);
            break;
    }
    var result = userServices.Registre(user, password);
    if (!result.IsSucces)
    {
        Console.WriteLine(result.Message);
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Register();
    }
    else
    {
        Console.WriteLine("Registeration Is Success. ");
        Console.Write("Press Any Key To Continiue: ");
        Console.ReadLine();
        Login();
    }

}//*