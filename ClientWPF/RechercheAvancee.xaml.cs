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
    /// Logique d'interaction pour RechercheAvancee.xaml
    /// </summary>
    public partial class RechercheAvancee : Window, INotifyPropertyChanged
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

        public RechercheAvancee(MainWindow _parent_windows)
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
                criteres.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)this.types_transaction.SelectedValue;

            if(this.types_chauffages.SelectedValue != null)
                criteres.TypeChauffage = (ServiceAgence.BienImmobilierBase.eTypeChauffage)this.types_chauffages.SelectedValue;

            if (this.energies_chauffage.SelectedValue != null)
                criteres.EnergieChauffage = (ServiceAgence.BienImmobilierBase.eEnergieChauffage)this.energies_chauffage.SelectedValue;

            if (this.types_biens.SelectedValue != null)
                criteres.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)this.types_biens.SelectedValue;

            if (this.prix_demande_min.Text != "")
                criteres.Prix1 = double.Parse(prix_demande_min.Text, System.Globalization.CultureInfo.InvariantCulture);

            if (this.prix_demande_max.Text != "")
                criteres.Prix2 = Convert.ToDouble(this.prix_demande_max.Text);

            if (this.surface_demande_min.Text != "")
                criteres.Surface1 = Convert.ToDouble(this.surface_demande_min.Text);

            if (this.surface_demande_max.Text != "")
                criteres.Surface2 = Convert.ToDouble(this.surface_demande_max.Text);

            String town = this.textbox_town.Text;
            criteres.Ville = town;

            await this.parent_windows.new_research(criteres);
            this.Close();
        }

    }
}
