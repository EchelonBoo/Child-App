using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class ChildDetails
    {
        public string name { get; set; }
        public DateTime DOB { get; set; }
        public string fact { get; set; }
        public int age { get; set; }


        public ChildDetails(string name, DateTime dob, string fact)
        {
            this.name = name;
            this.DOB = dob;
            this.fact = fact;
            this.age = CalcAge();

        }

        public ChildDetails()
        {
        }

         public int CalcAge()
         {
             DateTime today = DateTime.Today;

             int PersonAge = today.Year - DOB.Year;

             if (today.DayOfYear < DOB.DayOfYear)
             {
                 PersonAge--;
             }

             return PersonAge;
         }

        public string Display()
        {
            string details = name + "\t " + DOB.ToShortDateString() + "\t " + fact + "\t" + age;
            return details;
        }
    }
}
