using System.Windows;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.ViewModels;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04
{
    /// <summary>
    /// Логика взаимодействия для PersonsBrowse.xaml
    /// </summary>
    public partial class PersonsBrowseWindow : Window, INavigatable
    {


        public PersonsBrowseWindow()
        {
            InitializeComponent();
            DataContext = new PersonsBrowseViewModel(delegate() { Dispatcher.Invoke(PersonsDataGrid.Items.Refresh); });
        }
    }
}
