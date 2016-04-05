using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Logique d'interaction pour AjoutBien.xaml
    /// </summary>
    public partial class AjoutBien : Window
    {
        System.Collections.ArrayList mesImages = new System.Collections.ArrayList() ;
        public String _calculatorOutput;

        public AjoutBien()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(op.FileName));
                MemoryStream ms = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(ms);
                byte[] bitmapdata = ms.ToArray();

                String mS= Convert.ToBase64String(bitmapdata);
                mesImages.Add(mS);
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                ServiceAgence.BienImmobilier mBien = new ServiceAgence.BienImmobilier();
                //On crée un bien complet

                mBien.Titre = BoxTitre.Text;
                mBien.Prix = Double.Parse(BoxPrix.Text);
                mBien.MontantCharges = Double.Parse(BoxMontantCharges.Text);
                mBien.Ville = BoxVille.Text;
                mBien.CodePostal = BoxCodePostal.Text;
                mBien.TransactionEffectuee = false;
                //On récupère la date du jour et l'heure
                DateTime localDate = DateTime.Now;
                mBien.DateMiseEnTransaction = localDate;
                mBien.DateTransaction = null;

                mBien.Description = BoxDescription.Text;
                mBien.Surface = Double.Parse(BoxSurface.Text);
                mBien.Adresse = BoxAdresse.Text;
                mBien.NbPieces = int.Parse(BoxNbPiece.Text);
                mBien.NbEtages = int.Parse(BoxNbEtage.Text);
                mBien.NumEtage = int.Parse(BoxNumEtage.Text);

                //////////////////////////////

                mBien.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien) type_bien.SelectedValue;
                mBien.TypeChauffage = (ServiceAgence.BienImmobilierBase.eTypeChauffage)type_chauffages.SelectedValue;
                mBien.EnergieChauffage = (ServiceAgence.BienImmobilierBase.eEnergieChauffage)type_energies_chauffage.SelectedValue;
                mBien.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)type_transaction.SelectedValue;


                /*
                Création de la galerie de photos
                */
                ObservableCollection<String> mCollection = new  ObservableCollection<string>();
                foreach (String item in mesImages)
                {
                    mCollection.Add(item);
                }
                mBien.PhotosBase64 = mCollection;
                /*
                ajouter la gestion des erreurs
                */
                //On ajoute dans la BD
                client.AjouterBienImmobilier(mBien);
                //On modifie le label pour dire que l'action est faite
                this.Close();
            }
        }
    }
}
