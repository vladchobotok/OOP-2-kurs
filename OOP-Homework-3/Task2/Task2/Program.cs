using System;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Group group = new Group("K-25");
            group.AddStudent(new GoodStudent("Alex"));
            group.AddStudent(new BadStudent("Petro"));
            group.GetInfo();
            group.GetFullInfo();
        }
    }

    public abstract class Student
    {
        public string name { get; set; }
        public string state { get; set; }

        public Student(string _name)
        {
            name = _name;
            state = null;
        }

        public void Relax(){
            state += "Relax";
        }

        public void Read()
        {
            state += "Read";
        }

        public void Write()
        {
            state += "Write";
        }

        public abstract void Study();
    }

    public class GoodStudent : Student
    {
        public GoodStudent(string _name) : base(_name)
        {
            state += "good";
            Study();
        }
        
        public override void Study()
        {
            Read();
            Write();
            Read();
            Write();
            Relax();
        }
    }
    public class BadStudent : Student
    {
        public BadStudent(string _name) : base(_name)
        {
            state += "bad";
            Study();
        }

        public override void Study()
        {
            Relax();
            Relax();
            Relax();
            Relax();
            Read();
        }
    }

    public class Group
    {
        private string nameOfTheGroup;
        private List<Student> listOfStudents = new List<Student>();

        public Group(string _name)
        {
            nameOfTheGroup = _name;
        }

        public void AddStudent(Student student)
        {
            listOfStudents.Add(student);
        }

        public void GetInfo()
        {
            Console.WriteLine(nameOfTheGroup);
            listOfStudents.ForEach(delegate (Student st)
            {
                Console.WriteLine(st.name);
            });
        }
        public void GetFullInfo()
        {
            Console.WriteLine(nameOfTheGroup);
            listOfStudents.ForEach(delegate (Student st)
            {
                Console.WriteLine(st.name);
                Console.WriteLine(st.state);
            });
        }
    }
}
