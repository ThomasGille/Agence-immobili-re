using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ServiceAgence.BienImmobilierBase> liste = null;

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
                    liste = new List<ServiceAgence.BienImmobilierBase>();
                    this.mLabel.Text = resultat.ErreursBloquantes.ToString();
                }
            }
            /*this.gvResultats.DataSource = liste;
            this.gvResultats.DataBind();*/
            Session["TaskTable"] = liste;
            //this.gvResultats.DataSource = liste;
            //this.gvResultats.DataBind();
            BindData();
        }

        protected void TaskGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            gvResultats.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void TaskGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            gvResultats.EditIndex = -1;
            //Bind data to the GridView control.
            BindData();
        }

        protected void TaskGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Retrieve the table from the session object.
           // DataTable dt = (DataTable)Session["TaskTable"];
            List<ServiceAgence.BienImmobilierBase> mList =(List < ServiceAgence.BienImmobilierBase >) Session["TaskTable"];
            GridViewRow row = gvResultats.Rows[e.RowIndex];
            ServiceAgence.BienImmobilierBase mBI=mList[row.DataItemIndex];

            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                
                ServiceAgence.ResultatBienImmobilier mResult = client.LireDetailsBienImmobilier(mBI.Id.ToString());


                if (mResult.SuccesExecution)
                {
                   
                }
                else
                {
                    this.mLabel.Text = mResult.ErreursBloquantes.ToString();
                }
               
                Double mDouble;
                // probleme de chemin
                if (Double.TryParse( e.NewValues["Prix"].ToString(),out mDouble)){
                    mResult.Bien.Prix = mDouble;
                    mList[row.DataItemIndex].Prix = mDouble;
                   
                }
                client.ModifierBienImmobilier(mResult.Bien);
            }
            //Update the values.
            
            //Reset the edit index.
            gvResultats.EditIndex = -1;

            //Bind data to the GridView control.
            BindData();
        }

        private void BindData()
        {
            gvResultats.DataSource = Session["TaskTable"];
            gvResultats.DataBind();
        }

        protected void gvResultats_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}