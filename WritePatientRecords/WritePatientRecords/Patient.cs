using System;
using System.Collections.Generic;
using System.Text;
/*Create a Patient class that contains
fields for ID number, name, and current balance owed to the doctor’s office*/
namespace WritePatientRecords
{
    class Patient
    {
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public double CurrentBalance { get; set; }

        public new string ToString()
        {
            return ("#" + IdNumber + ',' + Name + ',' + CurrentBalance);
        }
    }
}
