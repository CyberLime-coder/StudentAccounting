using System.Collections.ObjectModel;
using StudentAccounting.Models;

namespace StudentAccounting.Services
{
    public interface IDataService
    {
        ObservableCollection<Student> GetStudents();
        ObservableCollection<Discipline> GetDisciplines();
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
        void AddDiscipline(Discipline discipline);
        void UpdateDiscipline(Discipline discipline);
        void DeleteDiscipline(int id);
        void SaveChanges();
    }
}