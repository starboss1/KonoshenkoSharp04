using System;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.CustomExceptions
{
    class InvalidEmailException : Exception
    {
        private string _message;

        public InvalidEmailException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }


    }
}
