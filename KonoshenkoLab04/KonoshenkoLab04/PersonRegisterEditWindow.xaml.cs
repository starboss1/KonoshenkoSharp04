using System;
using System.Windows;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Models;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.ViewModels;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04
{
    /// <summary>
    /// Логика взаимодействия для PersonRegisterEditWindow.xaml
    /// </summary>
    public partial class PersonRegisterEditWindow : Window, INavigatable
    {

        internal PersonRegisterEditWindow(Action<Person> onRegisterAction, Person person = null)
        {
            InitializeComponent();
            DataContext = new PersonRegisterEditViewModel(person, delegate(Person newPerson)
            {
                Close();
                onRegisterAction(newPerson);
            });
        }
    }
}
