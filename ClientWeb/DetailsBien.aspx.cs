using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class DetailsBien : System.Web.UI.Page
    {
        List<ServiceAgence.BienImmobilierBase> liste = null;
        List<String> mListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                String mId = Request.QueryString["id"];
                ServiceAgence.ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(mId);
                
                this.Adresse.Text= "<b>"+this.Adresse.ID + " :</b> " + resultat.Bien.Adresse+ "<br />";
                this.CodePostal.Text = "<b>" + this.CodePostal.ID + " :</b> " + resultat.Bien.CodePostal + "<br />";
                this.DateMiseEnTransaction.Text = "<b>" + this.DateMiseEnTransaction.ID + " :</b> " + resultat.Bien.DateMiseEnTransaction.ToString() + "<br />";
                this.DateTransaction.Text = "<b>" + this.DateTransaction.ID + " :</b> " + resultat.Bien.DateTransaction.ToString() + "<br />";
                this.Description.Text = "<b>" + this.Description.ID + " :</b> " + resultat.Bien.Description + "<br />";
                this.EnergieChauffage.Text = "<b>" + this.EnergieChauffage.ID + " :</b> " + resultat.Bien.EnergieChauffage.ToString() + "<br />";
                this.MontantCharges.Text = "<b>" + this.MontantCharges.ID + " :</b> " + resultat.Bien.MontantCharges.ToString() + "<br />";
                this.NbEtages.Text = "<b>" + this.NbEtages.ID + " :</b> " + resultat.Bien.NbEtages.ToString() + "<br />";
                this.NbPieces.Text = "<b>" + this.NbPieces.ID + " :</b> " + resultat.Bien.NbPieces.ToString() + "<br />";
                this.NumEtage.Text = "<b>" + this.NumEtage.ID + " :</b> " + resultat.Bien.NumEtage.ToString() + "<br />";
                this.Prix.Text = "<b>" + this.Prix.ID + " :</b> " + resultat.Bien.Prix.ToString() + "<br />";
                this.Surface.Text = "<b>" + this.Surface.ID + " :</b> " + resultat.Bien.Surface.ToString() + "<br />";
                this.Titre.Text = "<b>" + this.Titre.ID + " :</b> " + resultat.Bien.Titre + "<br />";
                this.TransactionEffectuee.Text = "<b>" + this.TransactionEffectuee.ID + " :</b> " + resultat.Bien.TransactionEffectuee.ToString() + "<br />";
                this.TypeBien.Text = "<b>" + this.TypeBien.ID + " :</b> " + resultat.Bien.TypeBien.ToString() + "<br />";
                this.TypeChauffage.Text = "<b>" + this.TypeChauffage.ID + " :</b> " + resultat.Bien.TypeChauffage.ToString() + "<br />";
                this.TypeTransaction.Text = "<b>" + this.TypeTransaction.ID + " :</b> " + resultat.Bien.TypeTransaction.ToString() + "<br />";
                this.Ville.Text = "<b>" + this.Ville.ID + " :</b> " + resultat.Bien.Ville + "<br />";

                rpResultats.DataSource =  resultat.Bien.PhotosBase64;
                this.rpResultats.DataBind();
            }
        }
    }
}