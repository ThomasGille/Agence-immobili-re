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
using System.IO;

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

        public ObservableCollection<Image> listeImgMini;

        public MainWindow()
        {
            this.liste = null;
            this.Bien = null;
            this.listeImgMini = null;
            this.get_all();
            this.DataContext = this;
            InitializeComponent();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void get_all()
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
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
                ServiceAgence.ResultatListeBiensImmobiliers resultat = client.LireListeBiensImmobiliers(criteres, null, null);


                if (resultat.SuccesExecution)
                {
                    liste = resultat.Liste.List;
                }
                else
                {
                    liste = new ObservableCollection<ServiceAgence.BienImmobilierBase>();
                    foreach(ServiceAgence.BienImmobilierBase Item in liste)
                    {
                        if (Item.PhotoPrincipaleBase64 != null){
                            byte[] binaryData = Convert.FromBase64String(Item.PhotoPrincipaleBase64);

                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = new MemoryStream(binaryData);
                            bi.EndInit();

                            Image img = new Image();
                            img.Source = bi;
                            listeImgMini.Add(img);
                        }
                        else
                        {
                            // TODO: Put the unfound image
                            listeImgMini.Add(null);
                        }
                    }
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //faire une recherche pour récup le bien complet
            // TODO : 
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                int mId =((ServiceAgence.BienImmobilierBase)mListBox.SelectedItem).Id;
                ServiceAgence.ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(mId.ToString());
                this.Bien = resultat.Bien;
            }
        }
    }
}
