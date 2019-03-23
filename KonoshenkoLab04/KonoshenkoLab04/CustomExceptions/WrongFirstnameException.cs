using System;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.CustomExceptions
{
    class WrongFirstNameException : Exception
    {
        private string _message;

        public WrongFirstNameException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}
