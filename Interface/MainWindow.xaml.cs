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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Interface
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Tipo_ricerca;
        List<UIElement> fase2 = new List<UIElement>();
        List<UIElement> fase1 = new List<UIElement>();
        public MainWindow()
        {
            InitializeComponent();
            fase2.Add(lbl_name);
            fase2.Add(lbl_genere);
            fase2.Add(lbl_film);
            fase2.Add(txt_genere);
            fase2.Add(txt_name);
            fase2.Add(txt_titolo);
            fase2.Add(CmB_Tipo_Ricerca);
            fase2.Add(btn_Elimina_Tag);
            fase2.Add(btn_Modifica_Genere);
            fase2.Add(Btn_New_File);
            fase2.Add(Btn_Ricerca);
            fase2.Add(CmB_Tipo_Ricerca);
            fase2.Add(txt_genere);
            fase2.Add(txt_name);
            fase2.Add(txt_titolo);
            fase2.Add(lbx_titoli);
            fase2.Add(btn_genere);
            fase1.Add(btn_crea);
            fase1.Add(btn_carica);
            fase1.Add(txt_path);
            
        }

        XDocument documento;
        private void btn_carica_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = txt_path.Text;
                documento = XDocument.Parse(File.ReadAllText(path, System.Text.Encoding.UTF8), LoadOptions.None);
                for (int i = 0; i < fase1.Count; i++)
                    fase1[i].Visibility = Visibility.Hidden;
                for (int i = 0; i < fase2.Count; i++)
                    fase2[i].Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("Path Errato");
            }

        }

        private void btn_crea_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < fase1.Count; i++)
                fase1[i].Visibility = Visibility.Hidden;
            for (int i = 0; i < fase2.Count; i++)
                fase2[i].Visibility = Visibility.Visible;

        }

        private void Btn_Ricerca_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbx_titoli.Items.Clear();

                if (Tipo_ricerca == "Autore")
                {
                    IEnumerable<string> nomi = from libri in documento.Descendants("wiride")
                                               where libri.Element("autore").Element("nome").Value == txt_name.Text
                                               select libri.Element("titolo").Value;
                    foreach (string nome in nomi)
                        lbx_titoli.Items.Add(nome);
                }
                else
                {
                    int x = 0;
                    IEnumerable<string> nomi = from libri in documento.Descendants("wiride")
                                               where libri.Element("titolo").Value == txt_name.Text
                                               select libri.Element("titolo").Value;
                    foreach (string nome in nomi)
                    {
                        x++;
                    }
                    lbx_titoli.Items.Add(x);
                }

            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            Tipo_ricerca = "Autore";
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Tipo_ricerca = "Frase";
        }

        private void btn_genere_Click(object sender, RoutedEventArgs e)
        {
            lbx_titoli.Items.Clear();

            int x = 0;
            IEnumerable<string> nomi = from libri in documento.Descendants("wiride")
                                       where libri.Element("genere").Value == "romanzo"
                                       select libri.Element("genere").Value;
            foreach (string nome in nomi)
            {
                x++;
            }
            lbx_titoli.Items.Add(x);
        }

    }
}
