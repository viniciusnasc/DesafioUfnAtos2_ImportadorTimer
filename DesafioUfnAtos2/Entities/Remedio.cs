using DesafioUfnAtos2.Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioUfnAtos2.Entities
{
    internal class Remedio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TimeSpan Horario { get; set; }

        public Remedio(string nome, TimeSpan horario)
        {
            Nome = nome;
            Horario = horario;
        }

        public bool GravarRemedio()
        {
            Contexto bd = new();

            SqlConnection cn = bd.AbrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = $"insert into Remedios values(@Nome, @Horario)";
            command.Parameters.Add("@Nome", SqlDbType.VarChar);
            command.Parameters.Add("@Horario", SqlDbType.Time);
            command.Parameters[0].Value = Nome;
            command.Parameters[1].Value = Horario;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                bd.FecharConexao();
            }
        }


    }
}
