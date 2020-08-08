using System;
using static System.Console;
using System.IO;

/*Create a program named WritePatientRecords that allows a doctor’s staff to enter
data about patients and saves the data to a file. Create a Patient class that contains
fields for ID number, name, and current CurrentBalance owed to the doctor’s office.*/
namespace WritePatientRecords
{
    class Program
    {
        static void Main(string[] args)
        {
            /*const char DELIM = ',';
            const string END = "999";
            const string FILENAME = @"../../Patients.txt";
            Patient patient = new Patient();
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            Write("Enter patient ID number or " + END +
            " to quit >> ");
            patient.IdNumber = ReadLine();
            while (patient.IdNumber != END)
            {
                Write("Enter last name >> ");
                patient.Name = ReadLine();
                Write("Enter CurrentBalance >> ");
                patient.CurrentBalance = Convert.ToDouble(ReadLine());
                WriteLine(patient.IdNumber + DELIM + patient.Name +
                DELIM + patient.CurrentBalance);
                writer.WriteLine(patient.IdNumber + DELIM + patient.Name +
                DELIM + patient.CurrentBalance);
                Write("Enter next patient ID number or " +
                END + " to quit >> ");
                patient.IdNumber = ReadLine();
            }
            writer.Close();
            outFile.Close();*/

            const char DELIM = ',';
            const string FILENAME = @"../../Patients.txt";
            Patient patient = new Patient();
            FileStream inFile = new FileStream(FILENAME, FileMode.Open,
            FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn;
            string[] fields;
            WriteLine("\n{0,-10}{1,-18}{2,10}\n", "IdNumber", "Name",
            "Balance");
            recordIn = reader.ReadLine();
            while (recordIn != null)
            {
                fields = recordIn.Split(DELIM);
                patient.IdNumber = fields[0];
                patient.Name = fields[1];
                patient.CurrentBalance = Convert.ToDouble(fields[2]);
                WriteLine("{0,-10}{1,-18}{2, 10}", patient.IdNumber, patient.Name,
                patient.CurrentBalance.ToString("C"));
                recordIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();
        }
    }
}
