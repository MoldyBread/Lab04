using System;
using System.Text.RegularExpressions;
using KMA.ProgrammingInCSharp2019.Lab04.Exceptions;

namespace KMA.ProgrammingInCSharp2019.Lab04
{
    [Serializable]
    public class Person
    {
        #region Fields

        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _email;
        private readonly DateTime _dateOfBirth;

        private readonly bool _isAdult;
        private readonly bool _isBirthday;
        private readonly string _sunSign;
        private readonly string _chineseSign;

        #endregion

        #region Properties

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string Email
        {
            get { return _email; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public bool IsBirthday
        {
            get { return _isBirthday; }
        }

        #endregion

        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;

            if (!EmailValid(email)) throw new WrongEmailException("WrongEmailException, email is wrong");
            _email = email;

            if (Age(dateOfBirth) > 135)
                throw new TooOldException("TooOldException, person can`t be more than 135 years old");
            if (Age(dateOfBirth) < 0) throw new UnbirthException("UnbirthException, person must be born already");
            _dateOfBirth = dateOfBirth;

            _isAdult = Age(dateOfBirth) > 18;
            _sunSign = WesternZodiac();
            _chineseSign = ChinaZodiac();
            _isBirthday = BirthdayCheck();
        }

        private string ChinaZodiac()
        {
            switch (DateOfBirth.Year % 12)
            {
                case 1:
                    return "Rooster";
                case 2:
                    return "Dog";
                case 3:
                    return "Pig";
                case 4:
                    return "Rat";
                case 5:
                    return "Ox";
                case 6:
                    return "Tiger";
                case 7:
                    return "Rabbit";
                case 8:
                    return "Dragon";
                case 9:
                    return "Snake";
                case 10:
                    return "Horse";
                case 11:
                    return "Goat";
                default:
                    return "Monkey";
            }
        }

        private string WesternZodiac()
        {
            if ((DateOfBirth.Month == 3 && DateOfBirth.Day >= 21) || (DateOfBirth.Month == 4 && DateOfBirth.Day <= 20))
                return "Aries";
            if ((DateOfBirth.Month == 4 && DateOfBirth.Day >= 21) || (DateOfBirth.Month == 5 && DateOfBirth.Day <= 20))
                return "Taurus";
            if ((DateOfBirth.Month == 5 && DateOfBirth.Day >= 21) || (DateOfBirth.Month == 6 && DateOfBirth.Day <= 21))
                return "Gemini";
            if ((DateOfBirth.Month == 6 && DateOfBirth.Day >= 22) || (DateOfBirth.Month == 7 && DateOfBirth.Day <= 22))
                return "Cancer";
            if ((DateOfBirth.Month == 7 && DateOfBirth.Day >= 23) || (DateOfBirth.Month == 8 && DateOfBirth.Day <= 23))
                return "Leo";
            if ((DateOfBirth.Month == 8 && DateOfBirth.Day >= 24) || (DateOfBirth.Month == 9 && DateOfBirth.Day <= 23))
                return "Virgo";
            if ((DateOfBirth.Month == 9 && DateOfBirth.Day >= 24) || (DateOfBirth.Month == 10 && DateOfBirth.Day <= 22))
                return "Libra";
            if ((DateOfBirth.Month == 10 && DateOfBirth.Day >= 23) ||
                (DateOfBirth.Month == 11 && DateOfBirth.Day <= 22))
                return "Scorpio";
            if ((DateOfBirth.Month == 11 && DateOfBirth.Day >= 23) ||
                (DateOfBirth.Month == 12 && DateOfBirth.Day <= 21))
                return "Sagittarius";
            if ((DateOfBirth.Month == 12 && DateOfBirth.Day >= 22) || (DateOfBirth.Month == 1 && DateOfBirth.Day <= 20))
                return "Capricorn";
            if ((DateOfBirth.Month == 1 && DateOfBirth.Day >= 21) || (DateOfBirth.Month == 2 && DateOfBirth.Day <= 19))
                return "Aquarius";
            return "Pisces";
        }

        private int Age(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;

            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age;
        }

        private bool BirthdayCheck()
        {
            return DateOfBirth.Month == DateTime.Today.Month && DateOfBirth.Day == DateTime.Today.Day;
        }

        private bool EmailValid(string email)
        {
            Regex validEmailRegex = new Regex(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                              + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                              + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$",
                RegexOptions.IgnoreCase);
            return validEmailRegex.IsMatch(email);

        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
