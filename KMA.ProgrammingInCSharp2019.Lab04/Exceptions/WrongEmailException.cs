using System;


namespace KMA.ProgrammingInCSharp2019.Lab04.Exceptions
{
    internal class WrongEmailException : Exception
    {
        public WrongEmailException(string message)
            : base(message) { }
    }
}
