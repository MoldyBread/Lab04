using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KMA.ProgrammingInCSharp2019.Lab04.Enums;

namespace KMA.ProgrammingInCSharp2019.Lab04.Tools
{
    class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
                GeneratePersons();
                SaveChanges();
            }
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }

        public void EditPerson(Person person, Person changedPerson)
        {
            _persons[_persons.IndexOf(person)] = changedPerson;
        }

        public void DeletePerson(Person person)
        {
            _persons.Remove(person);
        }

        public void SaveData()
        {
            SaveChanges();
        }


        public List<Person> PersonsList
        {
            get { return _persons.ToList(); }
        }

        
        private void SaveChanges()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }



        private void GeneratePersons()
        {
            Random z = new Random();
            string email;
            DateTime birth;
            int month;
            for (int i = 0; i < 50; i++)
            {
                email = "" + (char)z.Next(97, 123) + (char)z.Next(97, 123) + "@" + (char)z.Next(97, 123) + (char)z.Next(97, 123) + "." + (char)z.Next(97, 123) + (char)z.Next(97, 123);
                month = z.Next(1, 13);
                birth = new DateTime(z.Next(1900, 2019), month, (month == 2 ? z.Next(1, 29) : z.Next(1, 31)));
                AddPerson(new Person(Enum.GetName(typeof(Names), z.Next(0, 14)), Enum.GetName(typeof(Surnames), z.Next(0, 9)), email, birth));
            }

        }
    }
}
