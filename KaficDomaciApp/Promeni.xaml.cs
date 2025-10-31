using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace KaficDomaciApp
{
    /// <summary>
    /// Interaction logic for Promeni.xaml
    /// </summary>
    public partial class Promeni : Window
    {
        private PromeniSto ps;
        public Promeni(Sto s, ObservableCollection<Sto> stolovi)
        {
            InitializeComponent();
            this.stolovi = stolovi;
            ps = new PromeniSto { SelectedTable = s };
            DataContext = ps;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            update(ps.SelectedTable);
            Close();
        }
        private ObservableCollection<Sto> stolovi;

        public ObservableCollection<Sto> Stolovi
        {
            get { return stolovi; }
            set { stolovi = value; }
        }

        private void update(Sto updatedTable)
        {
            
            int index = stolovi.IndexOf(ps.SelectedTable);

            if (index != -1)
            {
                
                stolovi[index] = updatedTable;
            }
        }
    }
}
