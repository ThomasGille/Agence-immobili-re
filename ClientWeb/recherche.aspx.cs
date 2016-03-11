using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  ServiceAgence.CriteresRechercheBiensImmobiliers.eChampTri
        }

        protected void button_valider_Click(object sender, EventArgs e)
        {
            int a=456;
            Response.Redirect("display_result.aspx?type_chauffage=" + Request["ctl00$ContentPlaceHolder1$type_chauffage"]
                + "&type_transaction=" + Request["ctl00$ContentPlaceHolder1$type_transaction"]
                + "&type_bien=" + Request["ctl00$ContentPlaceHolder1$type_bien"]
                + "&energie_chauffage=" + Request["ctl00$ContentPlaceHolder1$energie_chauffage"]
                + "&prix_demande=" + Request["prix_demande"]
                + "&surface_demande=" + Request["surface_demande"]
                + "&nombre_piece_demande=" + Request["nombre_piece_demande"]);
            /*string query = "display_result.aspx?";
            foreach(String key in Request.QueryString.AllKeys)
            {
                
            }
            Response.Redirect(query);*/
        }

        protected void button_valider_Admin(object sender, EventArgs e)
        {
            int a = 456;
            Response.Redirect("admin.aspx?type_chauffage=" + Request["ctl00$ContentPlaceHolder1$type_chauffage"]
                + "&type_transaction=" + Request["ctl00$ContentPlaceHolder1$type_transaction"]
                + "&type_bien=" + Request["ctl00$ContentPlaceHolder1$type_bien"]
                + "&energie_chauffage=" + Request["ctl00$ContentPlaceHolder1$energie_chauffage"]
                + "&prix_demande=" + Request["prix_demande"]
                + "&surface_demande=" + Request["surface_demande"]
                + "&nombre_piece_demande=" + Request["nombre_piece_demande"]);
            /*string query = "display_result.aspx?";
            foreach(String key in Request.QueryString.AllKeys)
            {
                
            }
            Response.Redirect(query);*/
        }
    }
}