using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Lab04.Tools;


namespace KMA.ProgrammingInCSharp2019.Lab04
{
    class PersonDialogViewModel : INotifyPropertyChanged
    {
        private Person _person;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth = DateTime.Today;
        private readonly bool isForEdit;
        private readonly string _buttonContent;

        private RelayCommand<object> _submitCommand;

        internal PersonDialogViewModel(Person person)
        {
            _person = person;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Email = person.Email;
            DateOfBirth = person.DateOfBirth;
            isForEdit = true;
            _buttonContent = "Accept changes";
        }

        internal PersonDialogViewModel()
        {
            _buttonContent = "Add";
        }

        public Action CloseAction { get; set; }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand<object>(
                    SubmitImplementation, o => CanExecuteCommand()));
            }
        }


        private void SubmitImplementation(object obj)
        {
            try
            {
                Person inp = new Person(FirstName, LastName, Email, DateOfBirth);

                if (isForEdit)
                {
                    if (MessageBox.Show("Are you sure you want to accept changes", "Question",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        StationManager.DataStorage.EditPerson(_person,new Person(FirstName,LastName,Email,DateOfBirth));
                        CloseAction();
                    }
                }
                else
                {
                    StationManager.DataStorage.AddPerson(new Person(FirstName, LastName, Email, DateOfBirth));
                    CloseAction();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_firstName) && !string.IsNullOrWhiteSpace(_lastName) && !string.IsNullOrWhiteSpace(_email);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
