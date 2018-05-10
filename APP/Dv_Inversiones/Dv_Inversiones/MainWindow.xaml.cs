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
            ////Cabecera de factura//
            try
            {
                string cmd = string.Format("EXEC	 [dbo].[ConsultaFT] @DOCNUM = {0}", txtIDFactura.Text.Trim());
                DataSet ds = Utilidades.ejecutar(cmd);
                TextWriter cabecera = new StreamWriter ("C:\\Users/Juan/Desktop/FT ELECTRONICA/Sample.txt");

                /*FileStream stream = new FileStream("C:\\Users/Juan/Desktop/FT ELECTRONICA/Sample.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(stream);
                */

                int Trows = ds.Tables[0].Rows.Count;
                    for (int f = 0;f<Trows;f++)
                    {
                        cabecera.WriteLine(ds.Tables[0].Rows[f].ItemArray[0].ToString()+"|"+ds.Tables[1].Rows[0].ItemArray[f].ToString()+"|");
                    }
                    cabecera.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error en Escritura de cabecera de factura" + ex); }

            //Lineas de factura//
            
            try
            {
                string cmd = string.Format("EXEC	[dbo].[ConsultaFT_Lineas] @DocNum={0}",txtIDFactura.Text.Trim());
                DataSet ds = Utilidades.ejecutar(cmd);
                TextWriter lineasfactu = File.AppendText("C:\\Users/Juan/Desktop/FT ELECTRONICA/Sample.txt");
                int trows = ds.Tables[0].Rows.Count;
                int tcolu = ds.Tables[0].Columns.Count;
                for (int i = 0; i < trows; i++)
                {
                    
                    for (int j = 0; j < tcolu; j++)
                    {
                        lineasfactu.Write(ds.Tables[0].Rows[i].ItemArray[j].ToString()+"|");
                    }
                    lineasfactu.WriteLine();
                }
                lineasfactu.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Error en escribir las lineas de la factura");
            }
            //Guias de remision//

            try
            {
                string cmd = string.Format("EXEC	 [dbo].[ConsultaFT_Guias] @DocNum = {0}",txtIDFactura.Text.Trim());
                DataSet ds = Utilidades.ejecutar(cmd);

                TextWriter guias = File.AppendText("C:\\Users/Juan/Desktop/FT ELECTRONICA/Sample.txt");
                int trows = ds.Tables[0].Rows.Count;
                int tinde = ds.Tables[0].Columns.Count;
                for (int i = 0; i < trows; i++)
                {
                    for (int j = 0; j < tinde; j++)
                    {
                        guias.Write(ds.Tables[0].Rows[i].ItemArray[j].ToString());
                    }
                    guias.WriteLine();
                }
                guias.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Error al escribir guias de remision");
            }
            
        }
    }
}
