using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {

            if(Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            double percentile = Students.Count / 4;
            List<double> grades = new List<double>();
            Students.ForEach(delegate(Student student){ grades.Add(student.Grades.Average()); });
            grades.Sort();

            List<char> gradeLetters = new List<char>() { 'A', 'B', 'C', 'D', 'F' };
            for(int i = 0; i < grades.Count; i++)
            {
                if(i == grades.Count - 1 || averageGrade > grades[(int)Math.Round(percentile * (i+1))])
                {
                    return gradeLetters[i];
                }
            }
            return 'F';
        }

    }
}
