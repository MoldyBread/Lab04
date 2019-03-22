using System;

namespace KMA.ProgrammingInCSharp2019.Lab04.Exceptions
{
    internal class UnbirthException : Exception
    {
        public UnbirthException(string message)
            : base(message) { }
    }
}
