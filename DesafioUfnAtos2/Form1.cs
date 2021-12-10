using DesafioUfnAtos2.Banco;
using DesafioUfnAtos2.Helpers;
using System.Data;

namespace DesafioUfnAtos2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MostrarRemediosCadastrados();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Helper helper = new(openFileDialog1.FileName);
            MessageBox.Show(helper.ImportarDados());
            MostrarRemediosCadastrados();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string agora = textBox1.Text;// DateTime.Now.ToString("HH:mm:ss");
                Contexto banco = new();
                DataTable dt = new();
                string sql = $"select Nome from remedios where Horario = '{agora}'";
                dt = banco.ExecutarConsultaGenerica(sql);

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn colum in dt.Columns)
                        {
                            MessageBox.Show("é hora de tomar seu " + row[colum].ToString() + ".");
                        }
                    }
                }
            }
        }

        private void MostrarRemediosCadastrados()
        {
            Contexto banco = new();
            string sql = "select horario,nome From Remedios order by Horario";
            DataTable dt = new();
            dt = banco.ExecutarConsultaGenerica(sql);
            dataGridView1.DataSource = dt;
        }
    }
}