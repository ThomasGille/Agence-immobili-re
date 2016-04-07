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
    public partial class AjoutBien : Window , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        System.Collections.ArrayList mesImages = new System.Collections.ArrayList() ;

        public AjoutBien()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            // get l'image et la resize
            BitmapImage no = new BitmapImage();
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir la photo";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(op.FileName);
                double[] sizeImage = getRatioImage(bmp);
                if (sizeImage == null)
                {
                    return;
                }
                bmp = new System.Drawing.Bitmap(bmp, new System.Drawing.Size((int)sizeImage[0], (int)sizeImage[1]));
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                no.BeginInit();
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                no.StreamSource = stream;
                no.EndInit();

                // Convertis une Bitmapimage en StringBase64
                MemoryStream ms = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(no));
                encoder.Save(ms);
                byte[] bitmapdata = ms.ToArray();

                mesImages.Add(Convert.ToBase64String(bitmapdata));
                nbSelected.Content = mesImages.Count.ToString() + " selected";
            }
        }

        private double[] getRatioImage(System.Drawing.Bitmap bmp)
        {
            double[] tmp = new double[2];
            double ratio = ((double)bmp.Width / (double)bmp.Height);
            double targetHeight;
            /*le 250 c'est la valeur min que vous voulez en taille de photo(250*250)*/
            if (bmp.Width <=400 && bmp.Height <= 220)
            {
                return null;
            }
            double targetWidth = targetHeight = 0;
            if (ratio == 1)
            {
                targetHeight = 220;
                targetWidth = 220;
            }
            else if (ratio < 1)
            {
                targetHeight = 220;
                targetWidth = 220 * ratio;
            }
            else
            {
                targetHeight = 400 / ratio;
                targetWidth = 400;
            }
            tmp[0] = targetWidth;
            tmp[1] = targetHeight;
            return tmp;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                try {
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

                    mBien.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)type_bien.SelectedValue;
                    mBien.TypeChauffage = (ServiceAgence.BienImmobilierBase.eTypeChauffage)type_chauffages.SelectedValue;
                    mBien.EnergieChauffage = (ServiceAgence.BienImmobilierBase.eEnergieChauffage)type_energies_chauffage.SelectedValue;
                    mBien.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)type_transaction.SelectedValue;


                    /*
                    Création de la galerie de photos
                    */
                    ObservableCollection<String> mCollection = new ObservableCollection<string>();
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
                }
                catch (InvalidCastException inv)
                {
                    Console.WriteLine(inv.StackTrace);
                }
                catch (FormatException form)
                {
                    Console.WriteLine(form.StackTrace);
                }
                this.Close();
            }
        }
    }
}
