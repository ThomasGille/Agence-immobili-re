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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {

        private ObservableCollection<ServiceAgence.BienImmobilierBase> _liste;
        public ObservableCollection<ServiceAgence.BienImmobilierBase> liste 
        {
            set { this._liste=(value);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("liste"));
                };
            }
            get { return _liste; }
        }

        private ServiceAgence.BienImmobilier _Bien;
        public ServiceAgence.BienImmobilier Bien
        {
            set
            {
                this._Bien = (value);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Bien"));
                };
            }
            get { return _Bien; }
        }

        public MainWindow()
        {
            this.liste = null;
            this.Bien = null;
            this.new_research(MainWindow.initNullCriteres());
            this.DataContext = this;
            InitializeComponent();
            
        }

        public static ServiceAgence.CriteresRechercheBiensImmobiliers initNullCriteres()
        {
            ServiceAgence.CriteresRechercheBiensImmobiliers criteres = new ServiceAgence.CriteresRechercheBiensImmobiliers();
            criteres.DateMiseEnTransaction1 = null;
            criteres.DateMiseEnTransaction2 = null;
            criteres.DateTransaction1 = null;
            criteres.DateTransaction2 = null;
            criteres.EnergieChauffage = null;
            criteres.MontantCharges1 = -1;
            criteres.MontantCharges2 = -1;
            criteres.NbEtages1 = -1;
            criteres.NbEtages2 = -1;
            criteres.NbPieces1 = -1;
            criteres.NbPieces2 = -1;
            criteres.NumEtage1 = -1;
            criteres.NumEtage2 = -1;
            criteres.Prix1 = -1;
            criteres.Prix2 = -1;
            criteres.Surface1 = -1;
            criteres.Surface2 = -1;
            criteres.TransactionEffectuee = null;
            criteres.TypeBien = null;
            criteres.TypeChauffage = null;
            criteres.TypeTransaction = null;
            return criteres;
        }

        void new_research(ServiceAgence.CriteresRechercheBiensImmobiliers criteres)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                ServiceAgence.ResultatListeBiensImmobiliers resultat = client.LireListeBiensImmobiliers(criteres, null, null);

                if (resultat.SuccesExecution)
                {
                    liste = resultat.Liste.List;
                }
                else
                {
                    liste = new ObservableCollection<ServiceAgence.BienImmobilierBase>();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //faire une recherche pour récup le bien complet
            // TODO : 
           /* ((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem).Id;
            ServiceAgence.ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(mId);*/
        }

        private void MenuClickRechercheSimple(object sender, RoutedEventArgs e)
        {
            RechercheSimple windows = new RechercheSimple(this);
            windows.ShowDialog();
            //this.Close();
        }

        private void MenuClickRechercheAvancee(object sender, RoutedEventArgs e)
        {

        }
    }
}
