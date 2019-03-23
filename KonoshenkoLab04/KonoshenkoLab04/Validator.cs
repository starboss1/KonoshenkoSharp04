using System;
using System.Linq;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.CustomExceptions;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04
{
    internal class Validator
    {

        public static void CheckBirthday(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Today)
                throw new FutureBirthdayException("Unborn people!");
            if (DateTime.Today.Year - dateOfBirth.Year > 135)
                throw new PastBirthdayException("Too old people!");
        }

        public static void CheckEmail(string email)
        {
            if (email.Length < 3 || email.Count(f => f == '@') != 1 ||
                email.Count(f => f == '.') != 1 ||
                (email.IndexOf(".", StringComparison.Ordinal) == email.Length - 1) ||
                (email.IndexOf("@", StringComparison.Ordinal) == email.Length - 1) ||
                (email.IndexOf("@", StringComparison.Ordinal) == 0))
                throw new InvalidEmailException("Wrong email");
        }

        public static void CheckFirstName(string firstName)
        {
            if (firstName.Length < 3)
                throw new WrongFirstNameException($"Too small name: {firstName}");
        }

        public static void CheckLastName(string lastName)
        {
            if (lastName.Length < 3)
                throw new WrongLastNameException($"Too small last name: {lastName}");
        }
    }
}
