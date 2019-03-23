using System;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Models;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.ViewModels
{
    internal class PersonRegisterEditViewModel:BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate = DateTime.Today;
        private RelayCommand _signInCommand;
        private readonly Action<Person> _onRegisterAction;

        internal PersonRegisterEditViewModel(Person person, Action<Person> onRegisterAction)
        {
            if (person != null)
            {
                FirstName = person.FirstName;
                LastName = person.LastName;
                Email = person.Email;
                BirthDate = person.DateOfBirth;

            }

            _onRegisterAction = onRegisterAction;

        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public string BirthDateText { get; set; }

        public RelayCommand RegisterCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand(RegisterImplementation,
                           o => !string.IsNullOrWhiteSpace(_firstName) &&
                                !string.IsNullOrWhiteSpace(_lastName) &&
                                !string.IsNullOrWhiteSpace(_email) &&
                                !string.IsNullOrWhiteSpace(BirthDateText)));
            }
        }

        private async void RegisterImplementation(object obj)
        {
            
            Person person = null;
            await Task.Run((() =>
            {
                try
                {
                    person = new Person(_firstName, _lastName, _email, _birthDate);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);

                }
            }));


            if (person != null)
            {

                _onRegisterAction(person);
            }

        }

    }
}
