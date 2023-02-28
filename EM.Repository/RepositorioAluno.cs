using EM.Domain;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    { 

        public override void Add(Aluno obj)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao){

                const string sql = "INSERT into TBALUNO (MATRICULA, NOME, CPF, NASCIMENTO, SEXO) Values(@MATRICULA, @NOME, @CPF, @NASCIMENTO, @SEXO)";
                FbCommand cmd = new FbCommand(sql, conexao);
                cmd.Parameters.Add("@MATRICULA", obj.Matricula);
                cmd.Parameters.Add("@NOME", obj.Nome);
                cmd.Parameters.Add("@CPF", obj.CPF);
                cmd.Parameters.Add("@NASCIMENTO", obj.Nascimento);
                cmd.Parameters.Add("@SEXO", obj.Sexo);
                cmd.ExecuteNonQuery();
            }
        }

        public override IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            return Get(A => A.Matricula.Equals(predicate));
        }


        public override IEnumerable<Aluno> GetAll()
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {

            
                    List<Aluno> alunos = new List<Aluno>();
                    string sql = "SELECT * FROM TBALUNO ORDER BY NOME";
                    FbCommand data = new FbCommand(sql, conexao);
                    FbDataReader dr = data.ExecuteReader();

                    Aluno aluno = new Aluno();

                    while (dr.Read())
                    {
                        aluno = new Aluno()
                        {
                            Matricula = Convert.ToInt32(dr["MATRICULA"]),
                            Nome = dr["NOME"].ToString(),
                            CPF = dr["CPF"].ToString(),
                            Nascimento = Convert.ToDateTime(dr["NASCIMENTO"]),
                            Sexo = (EnumeradorSexo)Enum.ToObject(typeof(EnumeradorSexo), dr["SEXO"])
                        };
                        alunos.Add(aluno);
                    }

                return alunos;
            }

        }

        public override void Remove(Aluno obj)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
              
                    const string sql = "DELETE FROM TBALUNO WHERE MATRICULA = @MATRICULA AND NOME = @NOME AND CPF = @CPF AND NASCIMENTO = @NASCIMENTO AND SEXO = @SEXO";
                    FbCommand cmd = new FbCommand(sql, conexao);
                    cmd.Parameters.Add("@MATRICULA", obj.Matricula);
                    cmd.Parameters.Add("@NOME", obj.Nome);
                    cmd.Parameters.Add("@CPF", obj.CPF);
                    cmd.Parameters.Add("@NASCIMENTO", obj.Nascimento);
                    cmd.Parameters.Add("@SEXO", obj.Sexo);
                    cmd.ExecuteNonQuery();
            
            }
        }

        public override void Update(Aluno obj)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {

                string sql = "UPDATE TBALUNO SET MATRICULA = @MATRICULA, NOME = @NOME, CPF = @CPF, NASCIMENTO = @NASCIMENTO, SEXO = @SEXO WHERE MATRICULA = @MATRICULA";
                FbCommand cmd = new FbCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@MATRICULA", obj.Matricula);
                cmd.Parameters.AddWithValue("@NOME", obj.Nome);
                cmd.Parameters.AddWithValue("@CPF", obj.CPF);
                cmd.Parameters.AddWithValue("@NASCIMENTO", obj.Nascimento);
                cmd.Parameters.AddWithValue("@SEXO", obj.Sexo);
                cmd.ExecuteNonQuery();
            }
        }

        public Aluno GetByMatricula(int matricula)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {

                string sql = "SELECT * FROM TBALUNO WHERE MATRICULA = " + matricula;
                FbCommand data = new FbCommand(sql, conexao);
                
                FbDataReader dr = data.ExecuteReader();

                Aluno aluno = new Aluno();

                while (dr.Read())
                {
                    aluno.Matricula = Convert.ToInt32(dr["MATRICULA"]);
                    aluno.Nome = dr["NOME"].ToString();
                    aluno.CPF = dr["CPF"].ToString();
                    aluno.Nascimento = Convert.ToDateTime(dr["NASCIMENTO"].ToString());
                    aluno.Sexo = (EnumeradorSexo)Enum.ToObject(typeof(EnumeradorSexo), dr["SEXO"]);

                }
                return aluno;

            }
        }

        public IEnumerable<Aluno> GetByContendoNome(string partDoNome)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
                    IEnumerable<Aluno> alunos = GetAll();
                    alunos = alunos.Where(x => x.Nome.ToUpper().Contains(partDoNome.ToUpper()));
                    return alunos;
            }

        }
    }
}
