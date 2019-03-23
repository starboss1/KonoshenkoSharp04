using System;


namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.CustomExceptions
{
    class WrongLastNameException : Exception
    {
        private string _message;

        public WrongLastNameException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}
