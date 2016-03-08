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
            Response.Redirect("display_result.aspx" + Request.Url.Query);

        }
    }
}