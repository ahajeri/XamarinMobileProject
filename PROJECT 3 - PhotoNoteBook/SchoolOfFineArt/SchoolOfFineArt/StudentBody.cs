using System;
using System.Collections.ObjectModel;

namespace SchoolOfFineArt
{
    public class StudentBody : ViewModelBase
    {
        string school;
        ObservableCollection<Student> students = new ObservableCollection<Student>();

        public string School
        {
            set { SetProperty(ref school, value); }
            get { return school; }
        }

        public ObservableCollection<Student> Students
        {
            set { SetProperty(ref students, value); }
            get { return students; }
        }

        // Methods to implement commands to move and remove students.
        public void MoveStudentToTop(Student student)
        {
            Students.Move(Students.IndexOf(student), 0);
        }

        public void MoveStudentToBottom(Student student)
        {
            Students.Move(Students.IndexOf(student), Students.Count - 1);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        public void AddStudent(Student student)
        {
            Student itm = new Student();
            itm.FullName = "New Image";
            itm.PhotoFilename = "http://icons.iconarchive.com/icons/artdesigner/emoticons/128/idiotic-smile-icon.png";
            itm.LastName = "New";
            Students.Insert(Students.IndexOf(student), itm);
        }
    }
}