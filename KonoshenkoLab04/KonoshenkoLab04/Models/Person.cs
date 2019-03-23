using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Models
{
    [Serializable]
    internal class Person
    {
        private string _firstName;
        private DateTime _dateOfBirth;
        private string _lastName;
        private string _email;

        
        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            Validator.CheckFirstName(firstName);
            _firstName = firstName;
            Validator.CheckLastName(lastName);
            _lastName = lastName;
            Validator.CheckEmail(email);
            _email = email;
            Validator.CheckBirthday(dateOfBirth);
            _dateOfBirth = dateOfBirth;
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, DateTime.Today)
        {
        }

        public Person(string firstName, string lastName, DateTime dateOfBirth)
            : this(firstName, lastName, "No Email", dateOfBirth)
        {

        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
            }
        }


        public bool IsAdult
        {
            get
            {
                DateTime today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (DateOfBirth.Year * 100 + DateOfBirth.Month) * 100 + DateOfBirth.Day;
                return (a - b) / 10000 >= 18;
            }
        }

        public string SunSign
        {
            get
            {
                var day = DateOfBirth.Day;
                int westZodiacNum;
                switch (DateOfBirth.Month)
                {
                    case 1: //Jan
                        westZodiacNum = day <= 20 ? 9 : 10;
                        break;
                    case 2: //Feb
                        westZodiacNum = day <= 19 ? 10 : 11;
                        break;
                    case 3: //Mar
                        westZodiacNum = day <= 20 ? 11 : 0;
                        break;
                    case 4: //Apr
                        westZodiacNum = day <= 20 ? 0 : 1;
                        break;
                    case 5: //May
                        westZodiacNum = day <= 20 ? 1 : 2;
                        break;
                    case 6: //Jun
                        westZodiacNum = day <= 20 ? 2 : 3;
                        break;
                    case 7: //Jul
                        westZodiacNum = day <= 21 ? 3 : 4;
                        break;
                    case 8: //Aug
                        westZodiacNum = day <= 22 ? 4 : 5;
                        break;
                    case 9: //Sep
                        westZodiacNum = day <= 21 ? 5 : 6;
                        break;
                    case 10: //Oct
                        westZodiacNum = day <= 21 ? 6 : 7;
                        break;
                    case 11: //Nov
                        westZodiacNum = day <= 21 ? 7 : 8;
                        break;
                    case 12: //Dec
                        westZodiacNum = day <= 21 ? 8 : 9;
                        break;
                    default:
                        westZodiacNum = 0;
                        break;
                }

                return WesternZodiaсList[westZodiacNum];
            }
        }

        public string ChineseSign => ChineseZodiaсList[(DateOfBirth.Year + 8) % 12];

        public bool IsBirthday => DateOfBirth.DayOfYear == DateTime.Today.DayOfYear;


        private static readonly string[] ChineseZodiaсList = {"Rat","Ox","Tiger","Rabbit",
            "Dragon","Snake","Horse","Goat","Monkey","Rooster","Dog","Pig"
        };

        private static readonly string[] WesternZodiaсList = {"Aries","Taurus","Gemini","Cancer",
            "Leo","Virgo","Libra","Scorpio","Sagittarius","Capricorn","Aquarius","Pisces"
        };

        public void CopyPerson(Person person)
        {
            FirstName = person.FirstName;
             LastName = person.LastName;
            Email = person.Email;
            DateOfBirth = person.DateOfBirth;
        }

        private void SaveTo(string filename)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Directory.CreateDirectory(Path.GetDirectoryName(filename) ?? throw new InvalidOperationException());
                Stream stream = new FileStream(path: filename, mode: FileMode.Create, access: FileAccess.Write,
                    share: FileShare.None);
                formatter.Serialize(serializationStream:stream, graph:this);
                stream.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static Person LoadFrom(string filename)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                var person = (Person) formatter.Deserialize(stream);
                stream.Close();
                return person;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async void LoadAllInfo(List<Person> persons, Action action)
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists("/"+_dataFilePath))
                {
                    Directory.CreateDirectory("/"+_dataFilePath);
                    persons.AddRange(PersonGenerator.GeneratePersons(50));
                    SaveAll(persons);
                }
                else
                {
                    persons.AddRange(Directory.EnumerateFiles("/"+_dataFilePath).Select(LoadFrom));
                }

                action();
            });
        }

        public static void SaveAll(List<Person> persons)
        {
            var i = 0;
            persons.ForEach(delegate(Person p)
            {
                p.SaveTo(Path.Combine("/"+_dataFilePath, string.Format(_tempFiles, i++)));
            });
            string extraFile;
            while(File.Exists(extraFile = Path.Combine("/" + _dataFilePath, string.Format(_tempFiles, i++))))
                File.Delete(extraFile);
        }

        private static readonly string _dataFilePath = "database";
        private static readonly string _tempFiles = "person{0}.csharp";
    }



    internal static class PersonGenerator
    {
        private static string[] names =
        {
            "Adah", "Shantell", "Yan", "Conception", "Cesar", "Kymberly", "Alix", "Bennett", "Florentina", "Leah",
            "Tish", "Coral",
            "Iola", "Fletcher", "Sammie", "Andra", "Merna", "Janelle", "Marisol", "Miguel", "Harriette", "Dorie",
            "Jesse", "Shena", "Carmelina", "Buford", "Santiago",
            "Mozell", "Kellie", "Dannielle", "France", "Jenny", "Jeraldine", "Dyan", "Colette", "Katlyn", "Cortney",
            "Janean", "Gertrud", "Edmund", "Theron", "Gigi",
            "Raleigh", "Sharee", "Janessa", "Shayne", "Gladis", "Marquerite", "Adam", "Pok"
        };

        private static string[] surnames =
        {
            "Dyky", "Trinchieri", "Goetsch", "Robinson", "Laird", "Frechette", "Mink", "Tyrrell-simpson", "Mckenna",
            "Caples", "Wall", "Byse", "Zaphiris", "Manners",
            "Cannon", "Vonnegut", "Brewster", "Cornell", "Mahler", "Cavat", "Pollard", "Horton", "Livers", "Crauel",
            "Leman", "Lansky", "Gutierrez", "Aitchison", "Watkins",
            "Mccartney", "Kinchla", "Jewell", "Carbone", "Snodgrass", "Jose", "Perlman", "Laplume", "Sterling", "Lecar",
            "Sallustio", "Jung", "Rothschild",
            "Adams", "Wainford", "Daveiga", "Gwyn", "Chaudhry", "Moy", "Lessi", "Szerencsi",
        };

        internal static List<Person> GeneratePersons(int preferableCapacity)
        {
            var res = new List<Person>();
            var gen = new Random();
            for (var i = 0; i < names.Length && i < surnames.Length && i < preferableCapacity; i++)
            {
                res.Add(new Person(names[i], surnames[i], $"{names[i]}@{surnames[i]}.com", RandomDate(gen)));
            }

            return res;
        }

        private static DateTime RandomDate(Random r)
        {
            var year = r.Next(-60, -10);
            var month = r.Next(-12, 0);
            var day = r.Next(-31, 0);
            return DateTime.Now.AddYears(year).AddMonths(month).AddDays(day);
        }


    }
}
