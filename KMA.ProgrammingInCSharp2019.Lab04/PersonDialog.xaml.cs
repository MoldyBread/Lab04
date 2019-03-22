using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KMA.ProgrammingInCSharp2019.Lab04
{
    /// <summary>
    /// Interaction logic for PersonDialog.xaml
    /// </summary>
    public partial class PersonDialog : Window
    {
        public PersonDialog(Person person)
        {
            InitializeComponent();
            PersonDialogViewModel pdvm = new PersonDialogViewModel(person);
            DataContext = pdvm;
            addCloseAction(pdvm);
        }

        public PersonDialog()
        {
            InitializeComponent();
            PersonDialogViewModel pdvm = new PersonDialogViewModel();
            DataContext = pdvm;
            addCloseAction(pdvm);
        }

        private void addCloseAction(PersonDialogViewModel pdvm)
        {
            if (pdvm.CloseAction == null)
                pdvm.CloseAction = new Action(this.Close);
        }
    }
}
