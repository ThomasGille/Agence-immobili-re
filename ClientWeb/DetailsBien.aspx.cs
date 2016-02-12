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

        protected void Page_Load(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                String mId = Request.QueryString["id"];
                ServiceAgence.ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(mId);
                this.Label1.Text= resultat.Bien.Adresse;
                this.Label1.Text = resultat.Bien.CodePostal;
                this.Label1.Text = resultat.Bien.DateMiseEnTransaction.ToString();
                this.Label1.Text = resultat.Bien.DateTransaction.ToString();
                this.Label1.Text = resultat.Bien.Description;
                this.Label1.Text = resultat.Bien.EnergieChauffage.ToString();
                this.Label1.Text = resultat.Bien.MontantCharges.ToString();
                this.Label1.Text = resultat.Bien.NbEtages.ToString();
                this.Label1.Text = resultat.Bien.NbPieces.ToString();
                this.Label1.Text = resultat.Bien.NumEtage.ToString();
                //this.Label1.Text = resultat.Bien.PhotoPrincipaleBase64;
                //this.Label1.Text = resultat.Bien.PhotosBase64;
                this.Label1.Text = resultat.Bien.Prix.ToString();
                this.Label1.Text = resultat.Bien.Surface.ToString();
                this.Label1.Text = resultat.Bien.Titre;
                this.Label1.Text = resultat.Bien.TransactionEffectuee.ToString();
                this.Label1.Text = resultat.Bien.TypeBien.ToString();
                this.Label1.Text = resultat.Bien.TypeChauffage.ToString();
                this.Label1.Text = resultat.Bien.TypeTransaction.ToString();
            }
        }
    }
}