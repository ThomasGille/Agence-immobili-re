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
                //creer un objet Bien complet
                mBien.DateMiseEnTransaction = null;
                
                
                
                
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
                Ajouter ces critères dans l'affichage
                */

                mBien.Description = BoxDescription.Text;
                mBien.Surface = Double.Parse(BoxSurface.Text);
                mBien.Adresse = BoxAdresse.Text;
                mBien.NbPieces = int.Parse(BoxNbPiece.Text);
                mBien.NbEtages = int.Parse(BoxNbPiece.Text);
                mBien.NumEtage = int.Parse(BoxNumEtage.Text);
                
                /*
                Ne fonctionne pas, voir comment mieux caster
                */
                mBien.TypeChauffage = (ServiceAgence.BienImmobilierBase.eTypeChauffage)int.Parse(type_chauffage.SelectedValue);
                mBien.EnergieChauffage = (ServiceAgence.BienImmobilierBase.eEnergieChauffage)int.Parse(type_energie_chauffage.SelectedValue);
                mBien.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)int.Parse(type_bien.SelectedValue);
                mBien.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)int.Parse(type_transaction.SelectedValue);


                //rajouter les photos                       NE MARCHE PAS
                int iLen = flupUpload.PostedFile.ContentLength;
                byte[] btArr = new byte[iLen];
                flupUpload.PostedFile.InputStream.Read(btArr, 0, iLen);
                mBien.PhotoPrincipaleBase64=(Convert.ToBase64String(btArr));


                //Trouver comment faire le multi                       
                mBien.PhotosBase64 = null;

                /*
                ajouter la gestion des erreurs
                */
                //On ajoute dans la BD
                client.AjouterBienImmobilier(mBien);
                //Modifier le label pour dire que l'action est faite
                mLabel.Text = "Insertion faite";
            }



        }
    }
}