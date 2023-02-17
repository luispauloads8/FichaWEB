using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        public EnumeradorSexo Sexo { get; set; }


        public override bool Equals(object obj)
        {
            return obj is Aluno aluno &&
                   Matricula.Equals(aluno.Matricula);
        }

        public override int GetHashCode()
        {
            return Matricula.GetHashCode();
        }

        public override string ToString()
        {
            return "Matricula" + Matricula
                 + "Nome" + Nome
                 + "CPF" + CPF
                 + "Nascimento" + Nascimento
                 + "Sexo" + Sexo;
        }
    }
}
