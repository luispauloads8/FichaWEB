using EM.Domain;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        public override void Add(Aluno obj)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
                try
                {
                    string sql = "INSERT into TBALUNO Values(" + obj.Matricula + ",'" + obj.Nome + "','" + obj.CPF + "','" + obj.Nascimento.ToString("yyyy-MM-dd") + "','" + ((int)obj.Sexo) + "')";
                    FbCommand cmd = new FbCommand(sql, conexao);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Não Conectado!");
                }
                finally
                {
                    ConexaoFirebird.Desconectar();
                }
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

                try
                {
                    List<Aluno> alunos = new List<Aluno>();
                    string sql = "Select * from TBALUNO order by nome";
                    FbCommand data = new FbCommand(sql, conexao);
                    FbDataReader dr = data.ExecuteReader();

                    Aluno aluno = new Aluno();

                    while (dr.Read())
                    {
                        aluno = new Aluno()
                        {
                            Matricula = Convert.ToInt32(dr[0]),
                            Nome = dr[1].ToString(),
                            CPF = dr[2].ToString(),
                            Nascimento = Convert.ToDateTime(dr[3]),
                            Sexo = (EnumeradorSexo)Enum.ToObject(typeof(EnumeradorSexo), dr[4])
                        };
                        alunos.Add(aluno);
                    }

                    return alunos;
                }
                catch (Exception)
                {
                    throw new Exception("Não Conectado!");
                }
                finally
                {
                    ConexaoFirebird.Desconectar();
                }
            }
        }

        public override void Remove(Aluno obj)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
                try
                {
                    string sql = "DELETE from TBALUNO where matricula = " + obj.Matricula;
                    //const string sql = "DELETE FROM TBALUNO WHERE MATRICULA = @MATRICULA AND NOME = @NOME AND CPF = @CPF AND NASCIMENTO = @NASCIMENTO AND SEXO = @ SEXO";
                    DbCommand cmd = conexao.CreateCommand();
                    FbCommand data = new FbCommand(sql, conexao);
                    data.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ConexaoFirebird.Desconectar();
                }
            }
        }

        public override void Update(Aluno obj)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
                try
                {
                    string sql = "Update TBALUNO set matricula= '" + obj.Matricula + "', nome=  '" + obj.Nome + "', CPF= '" + obj.CPF + "', nascimento='" + obj.Nascimento.ToString("yyyy-MM-dd") + "',sexo='" + ((int)obj.Sexo) + "'" + " where matricula= " + obj.Matricula;
                    FbCommand cmd = new FbCommand(sql, conexao);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Não Conectado!");
                }
                finally
                {
                    ConexaoFirebird.Desconectar();
                }
            }
        }

        public Aluno GetByMatricula(int matricula)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
                try
                {
                    string sql = "Select * from TBALUNO where Matricula = " + matricula;
                    FbCommand data = new FbCommand(sql, conexao);
                    FbDataReader dr = data.ExecuteReader();
                    
                    Aluno aluno = new Aluno();

                    while (dr.Read())
                    {
                        aluno.Matricula = Convert.ToInt32(dr[0]);
                        aluno.Nome = dr[1].ToString();
                        aluno.CPF = dr[2].ToString();
                        aluno.Nascimento = Convert.ToDateTime(dr[3].ToString());
                        aluno.Sexo = (EnumeradorSexo)Enum.ToObject(typeof(EnumeradorSexo), dr[4]);

                    }
                    return aluno;

                }
                catch (Exception)
                {
                    throw new Exception("Não Conectado!");
                }
                finally
                {
                    ConexaoFirebird.Desconectar();
                }
            }
        }

        public IEnumerable<Aluno> GetByContendoNome(string partDoNome)
        {
            ConexaoFirebird.Conectar();
            using (FbConnection conexao = ConexaoFirebird.Conexao)
            {
                try
                {
                    IEnumerable<Aluno> alunos = GetAll();
                    alunos = alunos.Where(x => x.Nome.ToUpper().Contains(partDoNome.ToUpper()));
                    return alunos;
                }
                catch (Exception)
                {
                    throw new Exception("Não Conectado!");
                }
                finally
                {
                    ConexaoFirebird.Desconectar();
                }
            }

        }
    }
}
