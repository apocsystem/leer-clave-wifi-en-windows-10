using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MisRedesWifi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string lanzaProceso(string Proceso, string Parametros)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Proceso, Parametros);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false; //No utiliza RunDLL32 para lanzarlo   //Opcional: establecer la carpeta de trabajo en la que se ejecutará el proceso   //startInfo.WorkingDirectory = "C:\\MiCarpeta\\";
            //Redirige las salidas y los errores
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process proc = Process.Start(startInfo); //Ejecuta el proceso
            proc.WaitForExit(); // Espera a que termine el proceso
            string error = proc.StandardError.ReadToEnd();
            if (error != null && error != "") //Error
                throw new Exception("Se ha producido un error al ejecutar el proceso '" + Proceso + "'\n" + "Detalles:\n" + "Error: " + error);
            else //Éxito
                return proc.StandardOutput.ReadToEnd(); //Devuelve el resultado 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var command = "";
            richTextBox1.Text = "";
            if(textBox1.Text == ""){
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text += "\n\n ================== REDES ================== \n\n";
                command = "netsh wlan show profiles";
            }
            else {
                richTextBox1.ForeColor = Color.Green;
                command = "netsh wlan show profile name=" + textBox1.Text + " key=clear";
            }
            try
            {
                var proc = lanzaProceso("cmd", "/c " + command);
                richTextBox1.Text += proc.Substring(proc.IndexOf("    Perfil de todos los usuarios     : ") + 1, proc.Length - proc.IndexOf("    Perfil de todos los usuarios     : ") - 1);
            }
            catch (Exception)
            {
                MessageBox.Show("A ocurrido un error");
            }

        }

        private void textBox1_ModifiedChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
