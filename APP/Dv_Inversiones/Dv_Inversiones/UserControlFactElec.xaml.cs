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
using System.Data;
using System.Data.SqlClient;
using MiLibreria;
using System.IO;
using Microsoft.Win32;
using RestSharp;
using System.Net;

namespace Dv_Inversiones
{
    /// <summary>
    /// Lógica de interacción para UserControlFactElec.xaml
    /// </summary>
    public partial class UserControlFactElec : UserControl
    {
        public UserControlFactElec()
        {
            InitializeComponent();
            btnGeneTxt.IsEnabled = false;
        }
        
        

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdFact.Text=="")
            {
                MessageBox.Show("Ingresae dato valido <123..>");
            }
            string cmd = string.Format("select cardCode,CardName,DocDate,DocTotal,FolioPref,FolioNum from OINV where docnum = {0}", txtIdFact.Text.Trim());
            DataSet ds = Utilidades.ejecutar(cmd);
            txtRuc.Text = "RUC: "+ds.Tables[0].Rows[0].ItemArray[0].ToString();
            txtRazoSoci.Text = "Razon Soci.: "+ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txtNumDoc.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString()+"-"+ds.Tables[0].Rows[0].ItemArray[5].ToString();
            txtDocTota.Text = "Total S/: "+ds.Tables[0].Rows[0].ItemArray[3].ToString();

            btnGeneTxt.IsEnabled = true;

        }

        private void btnGeneTxt_Click(object sender, RoutedEventArgs e)
        {
               ////Cabecera de factura//
            try
               {


                string nombre = "Factura.txt";

                   string cmd = string.Format("EXEC	 [dbo].[ConsultaFT] @DOCNUM = {0}", txtIdFact.Text.Trim());
                   DataSet ds = Utilidades.ejecutar(cmd);
                   TextWriter cabecera = new StreamWriter(nombre);



                   int Trows = ds.Tables[0].Rows.Count;
                   for (int f = 0; f < Trows; f++)
                   {
                       cabecera.WriteLine(ds.Tables[0].Rows[f].ItemArray[0].ToString() + "|" + ds.Tables[1].Rows[0].ItemArray[f].ToString() + "|");
                   }
                   cabecera.Close();
               }
               catch (Exception ex) { MessageBox.Show("Error en Escritura de cabecera de factura" + ex); }

               //Lineas de factura//

               try
               {
                string nombre = "Factura.txt";

                string cmd = string.Format("EXEC	[dbo].[ConsultaFT_Lineas] @DocNum={0}", txtIdFact.Text.Trim());
                   DataSet ds = Utilidades.ejecutar(cmd);
                   TextWriter lineasfactu = File.AppendText(nombre);
                   int trows = ds.Tables[0].Rows.Count;
                   int tcolu = ds.Tables[0].Columns.Count;
                   for (int i = 0; i < trows; i++)
                   {

                       for (int j = 0; j < tcolu; j++)
                       {
                           lineasfactu.Write(ds.Tables[0].Rows[i].ItemArray[j].ToString() + "|");
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
                string nombre = "Factura.txt";
                string cmd = string.Format("EXEC	 [dbo].[ConsultaFT_Guias] @DocNum = {0}", txtIdFact.Text.Trim());
                   DataSet ds = Utilidades.ejecutar(cmd);

                   TextWriter guias = File.AppendText(nombre);
                   int trows = ds.Tables[0].Rows.Count;
                   int tinde = ds.Tables[0].Columns.Count;
                   for (int i = 0; i < trows; i++)
                   {
                       for (int j = 0; j < tinde; j++)
                       {
                           guias.Write(ds.Tables[0].Rows[i].ItemArray[j].ToString()+"|");
                       }
                       guias.WriteLine();
                   }
                   guias.Close();

               }
               catch (Exception)
               {

                   MessageBox.Show("Error al escribir guias de remision");
               }

            MessageBox.Show("txt generado con exito");

        }

        private void btntest_Click(object sender, RoutedEventArgs e)
        {



            try {
                    StreamReader sr = new StreamReader("Factura.txt"); ///AQUI VA TU ARCHIVO TXT
                    string txt_sin_codificar = sr.ReadToEnd();
                    byte[] txt_bytes = Encoding.Default.GetBytes(txt_sin_codificar);
                    string txt_en_utf_8 = Encoding.UTF8.GetString(txt_bytes);
                    sr.Close();

                var client = new RestClient("https://www.nubefact.com/api/v1/21a1a00f-5c4c-48e3-ae8b-3124e9c63269"); //https://demo.nubefact.com/api/v1/03989d1a-6c8c-4b71-b1cd-7d37001deaa0
                var request = new RestRequest(Method.POST);
                    request.AddHeader("authorization", "2e08d04ad5bf48d3951ee5930827683cef04f4f24b1f4d33b98dbda178095fb4");
                    request.AddHeader("content-type", "text/plain");
                    request.AddParameter("text/plain",txt_en_utf_8, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                string respuesta = response.Content.ToString();
                MessageBox.Show(respuesta);




            }
            catch (WebException ex)
            {
                var respuesta = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                MessageBox.Show("123"+respuesta);
            }

        
        }
    }
}
