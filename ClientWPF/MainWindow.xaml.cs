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
        ServiceAgence.CriteresRechercheBiensImmobiliers  _lastCritere;

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

        public void new_research(ServiceAgence.CriteresRechercheBiensImmobiliers criteres)
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
            this._lastCritere = criteres;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                if (((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem)==null) {
                    this.Bien = null;
                    return;
                }
                int mId = ((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem).Id;
                ServiceAgence.ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(mId.ToString());
                this.Bien = resultat.Bien;
            }
        }

        private void Supression_click(object sender, RoutedEventArgs e)
        {
            if (((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem) == null)
            {
                this.Bien = null;
                return;
            }
            int mId = ((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem).Id;

            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
               
                client.SupprimerBienImmobilier(mId.ToString());
                int selected_index = mListBox.SelectedIndex;
                if (selected_index != -1)
                    liste.RemoveAt(selected_index);
            }

        }

        private void MenuClickRechercheSimple(object sender, RoutedEventArgs e)
        {
            RechercheSimple windows = new RechercheSimple(this);
            windows.ShowDialog();
        }

        private void MenuClickRechercheAvancee(object sender, RoutedEventArgs e)
        {
            RechercheAvancee windows = new RechercheAvancee(this);
            windows.ShowDialog();
        }

        private void MenuAjout_Click(object sender, RoutedEventArgs e)
        {
            AjoutBien windows = new AjoutBien();
            windows.ShowDialog();
            refresh();
        }

        public void refresh()
        {
            new_research(_lastCritere);
        }

        private void EditionBien_click(object sender, RoutedEventArgs e)
        {
            if (((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem) == null)
            {
                this.Bien = null;
                return;
            }

            EditerBien window = new EditerBien(this.Bien);
            window.ShowDialog();
            refresh();
        }
    }
}
