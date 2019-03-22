using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp2019.Lab04.Tools
{
    interface IDataStorage
    {
        void SaveData();
        void AddPerson(Person person);
        void EditPerson(Person person,Person changedPerson);
        void DeletePerson(Person person);
        List<Person> PersonsList { get; }
    }
}
