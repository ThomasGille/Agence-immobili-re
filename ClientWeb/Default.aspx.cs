using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class Default : System.Web.UI.Page
    {
        private string cutString(string initial_string, int size_max = 150)
        {
            if (initial_string.Length <= size_max)
                return initial_string;
            string output = initial_string.Substring(0, size_max);
            return output + "...";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                //client.AjouterBienImmobilier();
            }
        }
    }
}