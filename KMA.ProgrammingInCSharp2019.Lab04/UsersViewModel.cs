using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Lab04.Tools;

namespace KMA.ProgrammingInCSharp2019.Lab04
{
    class UsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;

        private RelayCommand<object> _add;
        private RelayCommand<object> _edit;
        private RelayCommand<object> _delete;
        private RelayCommand<object> _save;

        private Visibility _loaderVisibility = Visibility.Hidden;
        private Visibility _dataGridVisibility = Visibility.Visible;

        internal UsersViewModel()
        {
            DataGridVisibility = Visibility.Hidden;
            LoaderVisibility = Visibility.Visible;
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            DataGridVisibility = Visibility.Visible;
            LoaderVisibility = Visibility.Hidden;
        }

        public Person SelectedItem
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
            }
        }

        public ObservableCollection<Person> Persons
        {
            get => _persons;
            private set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set
            {
                _dataGridVisibility = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand<object> Add
        {
            get
            {
                return _add ?? (_add = new RelayCommand<object>(
                           AddImplementation));
            }
        }

        public RelayCommand<object> Edit
        {
            get
            {
                return _edit ?? (_edit = new RelayCommand<object>(
                           EditImplementation));
            }
        }

        public RelayCommand<object> Delete
        {
            get
            {
                return _delete ?? (_delete = new RelayCommand<object>(
                           DeleteImplementation));
            }
        }

        public RelayCommand<object> Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand<object>(
                           SaveImplementation));
            }
        }

        private void AddImplementation(object obj)
        {
            PersonDialog personDialog = new PersonDialog();
            personDialog.ShowDialog();
            Loading();
            MessageBox.Show("Person was added to the end of the list");
        }

        private void EditImplementation(object obj)
        {
            PersonDialog personDialog = new PersonDialog(_selectedPerson);
            personDialog.ShowDialog();
            Loading();
        }

        private void DeleteImplementation(object obj)
        {
            if (_selectedPerson != null)
            {
                if (MessageBox.Show("Are you sure you want to delete " + _selectedPerson, "Question",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    StationManager.DataStorage.DeletePerson(_selectedPerson);
                    Loading();
                    _selectedPerson = null;
                }
            }
            else
            {
                MessageBox.Show("Choose the person","!!!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private async void Loading()
        {
            DataGridVisibility = Visibility.Hidden;
            LoaderVisibility = Visibility.Visible;
            await Task.Run(() => UpdatePersons());
            DataGridVisibility = Visibility.Visible;
            LoaderVisibility = Visibility.Hidden;
        }

        private void UpdatePersons()
        {
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
        }

        private void SaveImplementation(object obj)
        {
            if (MessageBox.Show("Do you want to save data? ", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                StationManager.DataStorage.SaveData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
