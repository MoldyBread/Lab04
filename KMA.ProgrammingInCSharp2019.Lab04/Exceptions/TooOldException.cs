using System;


namespace KMA.ProgrammingInCSharp2019.Lab04.Exceptions
{
    internal class TooOldException : Exception
    {
        public TooOldException(string message)
            : base(message) { }
    }
}
