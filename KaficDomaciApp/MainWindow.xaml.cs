using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace KaficDomaciApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        private ObservableCollection<Sto> stolovi = new ObservableCollection<Sto>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Color boja = Color.FromRgb(((byte)r.Next(1,255)), ((byte)r.Next(1, 255)), ((byte)r.Next(1, 255)));
            int broj = r.Next(1, 50);
            Sto s = new Sto() { Broj = broj, X = r.Next(1, 200), Y = r.Next(1, 200), H = 50, W = 125, Boja= boja};
            stolovi.Add(s);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stolovi = new ObservableCollection<Sto>();
            grid1.ItemsSource = stolovi;
            list1.ItemsSource = stolovi;
            items1.ItemsSource = stolovi;
        }

        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            List<Sto> selectedTables = new List<Sto>(grid1.SelectedItems.Cast<Sto>());

            foreach (var table in selectedTables)
            {
                stolovi.Remove(table);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.SelectedItem is Sto selectedTable)
            {
                // Otvori novi prozor sa selektovanim stolom
                Promeni p = new Promeni(selectedTable, stolovi);
                p.ShowDialog();
            }
        }

        private void MenuItem_UcitajStolove_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija učitavanja iz fajla (XML ili JSON)
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                if (System.IO.Path.GetExtension(filePath).Equals(".xml", System.StringComparison.OrdinalIgnoreCase))
                {
                    // Učitavanje iz XML fajla
                    UcitajIzXml(filePath);
                }
                else if (System.IO.Path.GetExtension(filePath).Equals(".json", System.StringComparison.OrdinalIgnoreCase))
                {
                    // Učitavanje iz JSON fajla
                    UcitajIzJson(filePath);
                }
                else
                {
                    MessageBox.Show("Nepodržan format fajla.");
                }
            }
        }

        private void MenuItem_SnimiStolove_Click(object sender, RoutedEventArgs e)
        {
            // Implementacija snimanja u fajl (XML ili JSON)
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                if (System.IO.Path.GetExtension(filePath).Equals(".xml", System.StringComparison.OrdinalIgnoreCase))
                {
                    // Snimanje u XML fajl
                    SnimiUXml(filePath);
                }
                else if (System.IO.Path.GetExtension(filePath).Equals(".json", System.StringComparison.OrdinalIgnoreCase))
                {
                    // Snimanje u JSON fajl
                    SnimiUJson(filePath);
                }
                else
                {
                    MessageBox.Show("Nepodržan format fajla.");
                }
            }
        }

        private void UcitajIzXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Sto>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    ObservableCollection<Sto> ucitaniStolovi = (ObservableCollection<Sto>)serializer.Deserialize(fs);
                    stolovi.Clear();
                    foreach (var sto in ucitaniStolovi)
                    {
                        stolovi.Add(sto);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom učitavanja iz XML fajla: {ex.Message}");
            }
        }

        private void SnimiUXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Sto>));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fs, stolovi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom snimanja u XML fajl: {ex.Message}");
            }
        }

        private void UcitajIzJson(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                ObservableCollection<Sto> ucitaniStolovi = JsonConvert.DeserializeObject<ObservableCollection<Sto>>(jsonContent);
                stolovi.Clear();
                foreach (var sto in ucitaniStolovi)
                {
                    stolovi.Add(sto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom učitavanja iz JSON fajla: {ex.Message}");
            }
        }

        private void SnimiUJson(string filePath)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(stolovi, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom snimanja u JSON fajl: {ex.Message}");
            }
        }

        private bool isDragging = false;
        private Point startPoint;

        private void pritisni(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                startPoint = e.GetPosition(items1);
                (sender as Rectangle).CaptureMouse();
            }
        }

        private void pomeri(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(items1);
                Vector offset = currentPoint - startPoint;

                // Prilagodite svojstva kvadrata (X i Y) na osnovu offset-a
                Sto selectedSto = (sender as FrameworkElement).DataContext as Sto;
                selectedSto.X += offset.X;
                selectedSto.Y += offset.Y;

                startPoint = currentPoint;
            }
        }

        private void pusti(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                (sender as Rectangle).ReleaseMouseCapture();
            }
        }
    }
}