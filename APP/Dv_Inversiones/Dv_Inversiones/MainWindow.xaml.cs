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
using MiLibreria;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Dv_Inversiones
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cmd = string.Format("select cardCode,CardName,DocCur,DocTotal from OINV where docnum = {0}", txtIDFactura.Text.Trim());
                DataSet ds = Utilidades.ejecutar(cmd);
                DataTable dt = new DataTable();
                lCode.Content = ds.Tables[0].Rows[0].ItemArray[0];
                lName.Content = ds.Tables[0].Rows[0].ItemArray[1];
                ltotal.Content = ds.Tables[0].Rows[0].ItemArray[3];
            }
            catch (Exception ex){ MessageBox.Show("error"+ex); }
        }

        private void btnBeneratxt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cmd = string.Format("EXEC	 [dbo].[ConsultaFT] @DOCNUM = {0}", txtIDFactura.Text.Trim());
                DataSet ds = Utilidades.ejecutar(cmd);

                    FileStream stream = new FileStream("C:\\Users/Juan/Desktop/FT ELECTRONICA/Sample.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(stream);

                int Trows = ds.Tables[0].Rows.Count;
                    for (int f = 0;f<Trows;f++)
                    {
                        writer.WriteLine(ds.Tables[0].Rows[f].ItemArray[0].ToString()+"|"+ds.Tables[1].Rows[0].ItemArray[f].ToString()+"|");
                    }
                    writer.Close();
                

            }
            catch (Exception ex) { MessageBox.Show("Error" + ex); }
        }
    }
}
