using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace SchoolOfFineArt
{
    public class SchoolViewModel : ViewModelBase
    {
        StudentBody studentBody;
        Random rand = new Random();

        public SchoolViewModel() : this(null)
        {
        }

        public SchoolViewModel(IDictionary<string, object> properties)
        {
            // Avoid problems with a null or empty collection.
            StudentBody = new StudentBody();
            StudentBody.Students.Add(new Student());

            var assembly = typeof(SchoolViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("SchoolOfFineArt.xmlfile.xml");

            using (var reader = new System.IO.StreamReader(stream))
            {
                XmlSerializer xml = new XmlSerializer(typeof(StudentBody));
                StudentBody = xml.Deserialize(reader) as StudentBody;

                // Enumerate through all the students
                foreach (Student student in StudentBody.Students)
                {
                    // Set StudentBody property in each Student object.
                    student.StudentBody = StudentBody;
                }

            }

            // Adjust GradePointAverage randomly.
            Device.StartTimer(TimeSpan.FromSeconds(0.1),
                () =>
                {
                    if (studentBody != null)
                    {
                        int index = rand.Next(studentBody.Students.Count);
                        Student student = studentBody.Students[index];
                        double factor = 1 + (rand.NextDouble() - 0.5) / 5;
                        student.GradePointAverage = Math.Round(
                            Math.Max(0, Math.Min(5, factor * student.GradePointAverage)), 2);
                    }
                    return true;
                });
        }

        public StudentBody GetObjectPhotoFile()
        {
            return StudentBody;
        }
        // Save Notes in properties dictionary for program in Chapter 25.
        public void SaveNotes(IDictionary<string, object> properties)
        {
            foreach (Student student in StudentBody.Students)
            {
                properties[student.FullName] = student.Notes;
            }
        }

        public StudentBody StudentBody
        {
            protected set { SetProperty(ref studentBody, value); }
            get { return studentBody; }
        }
    }
}
