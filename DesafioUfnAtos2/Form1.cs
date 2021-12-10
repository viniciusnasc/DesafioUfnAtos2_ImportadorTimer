using DesafioUfnAtos2.Banco;
using DesafioUfnAtos2.Entities;
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

                // Este if está tratando para não dar erro com o teste - o tick pode acionar enquanto estiver escrevendo o horario
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        string mensagem = null;

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn colum in dt.Columns)
                            {
                                if (mensagem != null)
                                    mensagem += " e seu " + row[colum].ToString();

                                else
                                    mensagem = row[colum].ToString();
                            }
                        }
                        MessageBox.Show("é hora de tomar seu " + mensagem + ".");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                try
                {

                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtHorario.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    btnDelete.Enabled = true;
                    btnCadastro.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnDeleteAll.Text += dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().ToLower();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Contexto banco = new();
            string sql = $"delete from Remedios where Horario = '{txtHorario.Text}' and Nome = '{txtName.Text}'";
            DataTable dt = new();
            dt = banco.ExecutarConsultaGenerica(sql);
            MessageBox.Show("Você acaba de deletar um horario de sua rotina, caso tenha clicado errado, é só importar novamente!");
            MostrarRemediosCadastrados();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            Contexto banco = new();
            string sql = $"delete from Remedios where Nome = '{txtName.Text}'";
            DataTable dt = new();
            dt = banco.ExecutarConsultaGenerica(sql);
            MessageBox.Show($"Atenção! Você acabou de tirar de sua rotina o {txtName.Text}. Parabens, ou você está melhor ou acabou o dinheiro e voce esta mais perto da morte!");
            MostrarRemediosCadastrados();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Atenção! Somente o seu médico pode cadastrar horários. Utilize o importador para cadastrar sua receita!");
        }
    }
}