using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class display_result : System.Web.UI.Page
    {
        /*protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("this is the end");
            Response.Write(Request.QueryString["type_transaction"]);
            Response.Write(Request.QueryString["town"]);

        }*/

        public string cutString(string initial_string, int size_max = 150)
        {
            if (initial_string.Length <= size_max)
                return initial_string;
            string output = initial_string.Substring(0, size_max);
            return output + "...";
        }

        protected void Page_Load(object sender, EventArgs e)
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
                if(Request.QueryString["prix_demande"] != null)
                {
                    string[] words = Request.QueryString["prix_demande"].TrimStart(',').Split(',');
                    if(words.Length == 2)
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
                        if(Convert.ToInt32(words[0]) > Convert.ToInt32(words[1]))
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

                criteres.TransactionEffectuee = (null != Request.QueryString["transaction_effectue"]  && "" != Request.QueryString["transaction_effectue"] ? Boolean.Parse(Request.QueryString["transaction_effectue"]) : (bool?) null);

                criteres.Ville = "" != Request.QueryString["town"] ? Request.QueryString["town"] : null;

                criteres.TypeBien = null;
                String key = localiseKey("type_bien");
                if(key != null)
                {
                    if (key != "-1")
                        criteres.TypeBien = (ServiceAgence.BienImmobilierBase.eTypeBien)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeBien), key);
                    else
                        criteres.EnergieChauffage = null;
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
                        criteres.EnergieChauffage = null;
                }

                criteres.TypeTransaction = null;
                key = localiseKey("type_transaction");
                System.Diagnostics.Debug.WriteLine(key);
                if (key != null)
                {
                    if (key != "-1")
                        criteres.TypeTransaction = (ServiceAgence.BienImmobilierBase.eTypeTransaction)Enum.Parse(typeof(ServiceAgence.BienImmobilierBase.eTypeTransaction), key);
                    else
                        criteres.EnergieChauffage = null;
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
            this.gvResultats.DataBind();*/

            this.rpResultats.DataSource = liste;
            this.rpResultats.DataBind();


        }

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
    }
}