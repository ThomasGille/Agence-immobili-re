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
        private String localiseKey(String key_to_find)
        {
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key.EndsWith(key_to_find))
                {
                    return Request.QueryString[key];
                }
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                button_valider_Click();
            }
            else { 

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
            List<ServiceAgence.BienImmobilierBase> mList = (List<ServiceAgence.BienImmobilierBase>)Session["TaskTable"];
            GridViewRow row = gvResultats.Rows[e.RowIndex];
            ServiceAgence.BienImmobilierBase mBI = mList[row.DataItemIndex];

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
                //Prix
                Double mDouble;
                if (Double.TryParse(e.NewValues["Prix"].ToString(), out mDouble))
                {
                    mResult.Bien.Prix = mDouble;
                    mList[row.DataItemIndex].Prix = mDouble;
                }
                //titre
                String mString;
                mString = e.NewValues["Titre"].ToString();
                mResult.Bien.Titre = mString;
                mList[row.DataItemIndex].Titre = mString;
                //MontantCharges
                if (Double.TryParse(e.NewValues["MontantCharges"].ToString(), out mDouble))
                {
                    mResult.Bien.MontantCharges = mDouble;
                    mList[row.DataItemIndex].MontantCharges = mDouble;
                }
                //Ville
                mString = e.NewValues["Ville"].ToString();
                mResult.Bien.Ville = mString;
                mList[row.DataItemIndex].Ville = mString;
                //CodePostal
                mString = e.NewValues["CodePostal"].ToString();
                mResult.Bien.CodePostal = mString;
                mList[row.DataItemIndex].CodePostal = mString;
                //TransactionEffectuee && //DateTransaction
                mString = e.NewValues["TransactionEffectuee"].ToString();
                if (mString == "True")
                {

                    mResult.Bien.TransactionEffectuee = true;
                    mList[row.DataItemIndex].TransactionEffectuee = true;
                    /*
                    L'insertion auto de la date ne marche pas
                    */
                    DateTime localDate = DateTime.Now;
                    mResult.Bien.DateTransaction = localDate;
                    mList[row.DataItemIndex].DateTransaction = localDate;
                }
                else
                {
                    mResult.Bien.TransactionEffectuee = false;
                    mList[row.DataItemIndex].TransactionEffectuee = false;
                    mResult.Bien.DateTransaction = null;
                    mList[row.DataItemIndex].DateTransaction = null;
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

        protected void TaskGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                List<ServiceAgence.BienImmobilierBase> mList = (List<ServiceAgence.BienImmobilierBase>)Session["TaskTable"];
                GridViewRow row = gvResultats.Rows[e.RowIndex];
                ServiceAgence.BienImmobilierBase mBI = mList[row.DataItemIndex];

                client.SupprimerBienImmobilier(mBI.Id.ToString());
                mList.Remove(mBI);
                BindData();
            }
        }

        protected void button_valider_Click()
        {

            List<ServiceAgence.BienImmobilierBase> liste = null;
            String nbpieces1 = null;
            String nbpieces2 = null;
            String prix1 = null;
            String prix2 = null;
            String surface1 = null;
            String surface2 = null;

            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                if (Request.QueryString["prix_demande"] != null)
                {
                    string[] words = Request.QueryString["prix_demande"].TrimStart(',').Split(',');
                    if (words.Length == 2)
                    {
                        if (Convert.ToInt32(words[0]) > Convert.ToInt32(words[1]))
                        {
                            prix1 = words[1];
                            prix2 = words[0];
                        }
                        else
                        {
                            prix1 = words[0];
                            prix2 = words[1];
                        }
                    }
                }
                if (Request.QueryString["surface_demande"] != null)
                {
                    string[] words = Request.QueryString["surface_demande"].TrimStart(',').Split(',');
                    if (words.Length == 2)
                    {
                        if (Convert.ToInt32(words[0]) > Convert.ToInt32(words[1]))
                        {
                            surface1 = words[1];
                            surface2 = words[0];
                        }
                        else
                        {
                            surface1 = words[0];
                            surface2 = words[1];
                        }
                    }
                }
                if (Request.QueryString["nombre_piece_demande"] != null)
                {
                    string[] words = Request.QueryString["nombre_piece_demande"].TrimStart(',').Split(',');
                    if (words.Length == 2)
                    {
                        nbpieces1 = words[0];
                        nbpieces2 = words[1];
                    }
                }


                ServiceAgence.CriteresRechercheBiensImmobiliers criteres = new ServiceAgence.CriteresRechercheBiensImmobiliers();
                criteres.DateMiseEnTransaction1 = (null != Request.QueryString["date_mise_transaction_1"] && "" != Request.QueryString["date_mise_transaction_1"] ? DateTime.Parse(Request.QueryString["date_mise_transaction_1"]) : (DateTime?)null);
                criteres.DateMiseEnTransaction2 = (null != Request.QueryString["date_mise_transaction_2"] && "" != Request.QueryString["date_mise_transaction_2"] ? DateTime.Parse(Request.QueryString["date_mise_transaction_2"]) : (DateTime?)null);
                criteres.DateTransaction1 = (null != Request.QueryString["date_transaction_1"] && "" != Request.QueryString["date_transaction_1"] ? DateTime.Parse(Request.QueryString["date_transaction_1"]) : (DateTime?)null);
                criteres.DateTransaction2 = (null != Request.QueryString["date_transaction_2"] && "" != Request.QueryString["date_transaction_2"] ? DateTime.Parse(Request.QueryString["date_transaction_2"]) : (DateTime?)null);
                criteres.MontantCharges1 = (null != Request.QueryString["montantcharges1"] && "" != Request.QueryString["montantcharges1"] ? int.Parse(Request.QueryString["montantcharges1"]) : -1);
                criteres.MontantCharges2 = (null != Request.QueryString["montantcharges2"] && "" != Request.QueryString["montantcharges2"] ? int.Parse(Request.QueryString["montantcharges2"]) : -1);

                criteres.NbEtages1 = (null != Request.QueryString["nbetages1"] && "" != Request.QueryString["nbetages1"] ? int.Parse(Request.QueryString["nbetages1"]) : -1);
                criteres.NbEtages2 = (null != Request.QueryString["nbetages2"] && "" != Request.QueryString["nbetages2"] ? int.Parse(Request.QueryString["nbetages2"]) : -1);
                criteres.NumEtage1 = (null != Request.QueryString["numetage1"] && "" != Request.QueryString["numetage1"] ? int.Parse(Request.QueryString["numetage1"]) : -1);
                criteres.NumEtage2 = (null != Request.QueryString["numetage2"] && "" != Request.QueryString["numetage2"] ? int.Parse(Request.QueryString["numetage2"]) : -1);

                criteres.NbPieces1 = (null != nbpieces1 && "" != nbpieces1 ? int.Parse(nbpieces1) : -1);
                criteres.NbPieces2 = (null != nbpieces2 && "" != nbpieces2 ? int.Parse(nbpieces2) : -1);
                criteres.Prix1 = (null != prix1 && "" != prix1 ? Double.Parse(prix1) : -1);
                criteres.Prix2 = (null != prix2 && "" != prix2 ? Double.Parse(prix2) : -1);
                criteres.Surface1 = (null != surface1 && "" != surface1 ? Double.Parse(surface1) : -1);
                criteres.Surface2 = (null != surface2 && "" != surface2 ? Double.Parse(surface2) : -1);

                criteres.TransactionEffectuee = (null != Request.QueryString["transaction_effectue"] && "" != Request.QueryString["transaction_effectue"] ? Boolean.Parse(Request.QueryString["transaction_effectue"]) : (bool?)null);

                criteres.Ville = "" != Request.QueryString["town"] ? Request.QueryString["town"] : null;

                criteres.TypeBien = null;
                String key = localiseKey("type_bien");
                if (key != null)
                {
                    if (key != "-1")
                        criteres.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeBien), key);
                    else
                        criteres.TypeBien = null;
                }

                criteres.TypeBien = null;
                key = localiseKey("energie_chauffage");
                if (key != null)
                {
                    if (key != "-1")
                        criteres.EnergieChauffage = (ServiceAgence.BienImmobilierBase.eEnergieChauffage)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eEnergieChauffage), key);
                    else
                        criteres.EnergieChauffage = null;
                }

                criteres.TypeChauffage = null;
                key = localiseKey("type_chauffage");
                if (key != null)
                {
                    if (key != "-1")
                        criteres.TypeChauffage = (ServiceAgence.BienImmobilierBase.eTypeChauffage)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeChauffage), key);
                    else
                        criteres.TypeChauffage = null;
                }

                criteres.TypeTransaction = null;
                key = localiseKey("type_transaction");
                System.Diagnostics.Debug.WriteLine(key);
                if (key != null)
                {
                    if (key != "-1")
                        criteres.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeTransaction), key);
                    else
                        criteres.TypeTransaction = null;
                }



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
            this.gvResultats.DataBind();

            this.rpResultats.DataSource = liste;
            this.rpResultats.DataBind();*/

            Session["TaskTable"] = liste;
            BindData();
        }
    }
}