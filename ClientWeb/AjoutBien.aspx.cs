using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class AjoutBien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Response.Write(Request.QueryString["code_postal"]);
            Response.Write("sqdfg");*/

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                ServiceAgence.BienImmobilier mBien = new ServiceAgence.BienImmobilier();
                //On crée un bien complet
        
                mBien.Titre = BoxTitre.Text;
                mBien.Prix = Double.Parse(BoxPrixDemande.Text);
                mBien.MontantCharges = Double.Parse(BoxMontantCharges.Text);
                mBien.Ville = BoxVille.Text;
                mBien.CodePostal = BoxCodePostal.Text;
                mBien.TransactionEffectuee = false;
                //On récupère la date du jour et l'heure            MARCHE!!!!!!!!!
                DateTime localDate = DateTime.Now;
                mBien.DateMiseEnTransaction = localDate;
                mBien.DateTransaction = null;


                /*
                Non affichés lors du mode admin
                */

                mBien.Description = BoxDescription.Text;
                mBien.Surface = Double.Parse(BoxSurface.Text);
                mBien.Adresse = BoxAdresse.Text;
                mBien.NbPieces = int.Parse(BoxNbPiece.Text);
                mBien.NbEtages = int.Parse(BoxNbPiece.Text);
                mBien.NumEtage = int.Parse(BoxNumEtage.Text);
                
                

                mBien.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeBien), type_bien.SelectedValue);
                mBien.TypeChauffage = (ServiceAgence.BienImmobilierBase.eTypeChauffage)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeChauffage), type_chauffage.SelectedValue);
                mBien.EnergieChauffage = (ServiceAgence.BienImmobilierBase.eEnergieChauffage)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eEnergieChauffage), type_energie_chauffage.SelectedValue);
                mBien.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeTransaction), type_transaction.SelectedValue);

                /*
                Création de la galerie de photos
                */
                List<String> mListe = new List<string>();
                int iLen;
                byte[] btArr;
                IList<HttpPostedFile> listePhoto =  FileuploadGroup.PostedFiles;
                foreach (var item in listePhoto)
                {
                    iLen = item.ContentLength;
                    btArr = new byte[iLen];
                    item.InputStream.Read(btArr, 0, iLen);
                    mListe.Add((Convert.ToBase64String(btArr)));
                }
                mBien.PhotosBase64 = mListe;

                /*
                ajouter la gestion des erreurs
                */
                //On ajoute dans la BD
                client.AjouterBienImmobilier(mBien);
                //On modifie le label pour dire que l'action est faite
                mLabel.Text = "Insertion faite";
            }



        }
    }
}