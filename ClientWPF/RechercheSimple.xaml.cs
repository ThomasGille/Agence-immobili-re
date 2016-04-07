using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour RechercheSimple.xaml
    /// </summary>
    /// 
    
    public partial class RechercheSimple : Window, INotifyPropertyChanged
    {

        private String _town;
        public String town
        {
            get { return this._town; }
            set
            {
                if (this._town != value)
                {
                    this._town = value;
                    this.FirePropertyChanged("town");
                }
            }
        }

        private MainWindow parent_windows;

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public RechercheSimple(MainWindow _parent_windows)
        {
            InitializeComponent();
            parent_windows = _parent_windows;
            parent_windows.IsEnabled = false;

            this.Closed += new EventHandler(Window_Closed);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent_windows.IsEnabled = true;
        }

        private async void submit_form(object sender, RoutedEventArgs e)
        {
            ServiceAgence.CriteresRechercheBiensImmobiliers criteres = MainWindow.initNullCriteres();

            if (this.types_transaction.SelectedValue != null)
            {
                ServiceAgence.BienImmobilierBase.eTypeTransaction type_transaction_value = (ServiceAgence.BienImmobilierBase.eTypeTransaction)this.types_transaction.SelectedValue;
                criteres.TypeTransaction = type_transaction_value;
            }

            String town = this.textbox_town.Text;
            criteres.Ville = town;

            await this.parent_windows.new_research(criteres);
            this.Close();
        } 
    }
}
