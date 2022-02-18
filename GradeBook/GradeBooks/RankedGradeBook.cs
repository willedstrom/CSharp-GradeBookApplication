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

            List<double> grades = new List<double>();
            Students.ForEach(delegate(Student student){ grades.Add(student.AverageGrade); });
            int percentile = (int)Math.Ceiling(grades.Count / 5.0);
            grades.Sort();
            grades.Reverse();

            List<char> gradeLetters = new List<char>() { 'A', 'B', 'C', 'D', 'F' };
            for (int i = 0; i < gradeLetters.Count; i++)
            {
                int target = percentile * (i + 1)-1;
                if (target >= grades.Count - 1 || grades[target] < averageGrade)
                {
                    return gradeLetters[i];
                }
            }
            return 'F';
        }

    }
}
