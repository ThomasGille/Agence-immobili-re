using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour EditerBien.xaml
    /// </summary>
    /// 



    public partial class EditerBien : Window
    {

        private ServiceAgence.BienImmobilier _myBien;
        public ServiceAgence.BienImmobilier myBien
        {
            set { this._myBien = (value); }
            get { return _myBien; }
        }

        public EditerBien(ServiceAgence.BienImmobilier myBien)
        {
            this.myBien = myBien;

            this.DataContext = this;
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                client.ModifierBienImmobilier(this.myBien);
            }
            this.Close();
        }
    }

}
