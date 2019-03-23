using System;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.CustomExceptions
{
    class FutureBirthdayException : Exception
    {
        private string _message;

        public FutureBirthdayException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }

    }
}
