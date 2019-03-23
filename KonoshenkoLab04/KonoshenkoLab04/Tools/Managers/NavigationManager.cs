using System;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Models;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools.Managers
{
    internal class NavigationManager
    {
        private static readonly object Locker = new object();
        private static NavigationManager _instance;

        internal static NavigationManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    return _instance ?? (_instance = new NavigationManager());
                }
            }
        }


        internal void Navi(Action<Person> action, Person person=null)
        {
            if (person == null)
            {
                var window = new PersonRegisterEditWindow(action);
                window.Show();
            }
            else
            {
                 var window = new PersonRegisterEditWindow(action, person);
                 window.Show();
            }
            
        }
    }
}
