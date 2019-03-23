using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Models;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools.Managers;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Tools.Navigation;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04.ViewModels
{
    internal class PersonsBrowseViewModel : BaseViewModel, ILoaderOwner, INavigatable
    {

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        private List<Person> _personList;
        private Person _selectedPerson;
        private readonly Action _refreshPersonsAction;
        private string _filterQuery;
        private bool _sortingAsc = true;

        private RelayCommand _deleteCommand;
        private RelayCommand _editCommand;
        private RelayCommand _registerCommand;
        private RelayCommand _sortCommand;
        private RelayCommand _clearFilterCommand;

        private static CollectionView _sortFilterOptionsCollection;

        public string SelectedSortFilterProperty { get; set; }
        public string SelectedPersonShort { get; private set; }

        public static CollectionView SortFilterOptions => _sortFilterOptionsCollection ??
                                                          (_sortFilterOptionsCollection =
                                                              new CollectionView(SortExtension.SortFilterOptions));

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        internal PersonsBrowseViewModel(Action updateGridItems)
        {
            LoaderManager.Instance.Initialize(this);
            _refreshPersonsAction = updateGridItems;
            _personList = new List<Person>();
            Person.LoadAllInfo(PersonsListToShow, UpdateUsersGrid);
        }

        public string FilterQuery
        {
            get => _filterQuery;
            set
            {
                _filterQuery = value;
                SelectedPerson = null;
                UpdateUsersGrid();
            }
        }

        public List<Person> PersonsListToShow =>
            (string.IsNullOrEmpty(SelectedSortFilterProperty) || string.IsNullOrEmpty(FilterQuery))
                ? _personList
                : _personList.FilterByProperty(SelectedSortFilterProperty, FilterQuery);

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                if (_selectedPerson == null) return;
                SelectedPersonShort = $"{_selectedPerson.FirstName} {_selectedPerson.LastName}";
                OnPropertyChanged($"SelectedPersonShort");
            }
        }

        public RelayCommand DeleteCommand => _deleteCommand ??
                                             (_deleteCommand = new RelayCommand(DeleteImplementation,
                                                 o => _selectedPerson != null));

        private async void DeleteImplementation(object obj)
        {
            await Task.Run((() =>
            {
                _personList.Remove(SelectedPerson);
                UpdateUsersGrid();
            }));

        }

        public RelayCommand EditCommand =>
            _editCommand ?? (_editCommand = new RelayCommand(EditImplementation, o => _selectedPerson != null));

        private void EditImplementation(object obj)
        {
            var personToEdit = _selectedPerson;
            NavigationManager.Instance.Navi(delegate (Person edited)
            {
                personToEdit.CopyPerson(edited);
                UpdateUsersGrid();
            }, _selectedPerson);

        }

        public RelayCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new RelayCommand(RegisterImplementation, o => true));


        private void RegisterImplementation(object obj)
        {
            NavigationManager.Instance.Navi(delegate (Person newPerson)
            {
                PersonsListToShow.Add(newPerson);
                UpdateUsersGrid();
            });
        }

        public RelayCommand SortCommand =>
            _sortCommand ?? (_sortCommand =
                new RelayCommand(SortImplementation, o => true));

        private async void SortImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                _sortingAsc = !(SelectedSortFilterProperty.Equals("IsAdult") || (SelectedSortFilterProperty.Equals("IsBirthday")));
                _personList = _personList.SortByProperty(SelectedSortFilterProperty, _sortingAsc);
                UpdateUsersGrid();
            });
            LoaderManager.Instance.HideLoader();
        }

        public RelayCommand ClearFilterCommand => _clearFilterCommand ?? (_clearFilterCommand = new RelayCommand(
                                                      (obj) =>
                                                      {
                                                          FilterQuery = "";
                                                          OnPropertyChanged($"FilterQuery");
                                                      },
                                                      obj => !string.IsNullOrEmpty(FilterQuery)));

        private void UpdateUsersGrid()
        {
            Person.SaveAll(_personList);
            OnPropertyChanged($"PersonsListToShow");
            _refreshPersonsAction();
        }
    }
}
