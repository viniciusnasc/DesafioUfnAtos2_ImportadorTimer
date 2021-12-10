using DesafioUfnAtos2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioUfnAtos2.Helpers
{
    internal class Helper
    {
        public string Path { get; private set; }

        public Helper(string path)
        {
            Path = path;
        }

        private List<string> LerArquivo()
        {
            string linha;
            List<string> list = new List<string>();
            using(StreamReader sr = File.OpenText(Path))
            {
                while ((linha = sr.ReadLine()) != null)
                {
                    list.Add(linha);
                }
            }
            return list;
        }

        public string ImportarDados()
        {
            List<string> list = LerArquivo();
            int count = 0;

            foreach(var linha in list)
            {
                string[] entidade = linha.Split(" ");
                
                for(int i = 0; i < entidade.Length-1; i++)
                {
                    string horario = entidade[i + 1].Replace("(", "");
                    horario = horario.Replace(")", "");
                    horario = horario.Replace(",", "");
                    Remedio rem = new(entidade[0], TimeSpan.Parse(horario));

                    if (!rem.ExisteRemedio(rem))
                    {
                        rem.GravarRemedio();
                        count++;
                    }
                }
            }

            return $"Foram gravados {count} remedios";
        }
    }
}
