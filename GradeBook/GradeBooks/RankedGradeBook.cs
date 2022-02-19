using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {

            if(Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            List<double> grades = new List<double>();
            Students.ForEach(delegate(Student student){ grades.Add(student.AverageGrade); });
            int threshold = (int)Math.Ceiling(grades.Count * 0.2);
            grades.Sort();
            grades.Reverse();

            if (grades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }
            else if (grades[threshold * 2 - 1] <= averageGrade)
            {
                return 'B';
            }
            else if (grades[threshold * 3 - 1] <= averageGrade)
            {
                return 'C';
            }
            else if (grades[threshold * 4 - 1] <= averageGrade)
            {
                return 'D';
            }
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
