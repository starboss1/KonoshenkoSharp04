using System;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.CustomExceptions
{
    class PastBirthdayException : Exception
    {
        private string _message;

        public PastBirthdayException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }

    }
}
