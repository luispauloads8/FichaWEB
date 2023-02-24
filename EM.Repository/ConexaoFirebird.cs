using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Repository
{

    public static class ConexaoFirebird
    {

        private static FbConnection _conexaoFB;


        public static FbConnection Conexao
        {
            get
            {
                return _conexaoFB;
            }
        }

        public static bool Conectar()
        {
            string sql = "User=SYSDBA;" +
                                  "Password=masterkey;" +
                                  "Database=C:\\Users\\luisp\\OneDrive\\Documentos\\BancoDados\\BANCOESTUDO.FDB;" +
                                  "DataSource=localhost;" +
                                  "Port=3050;" +
                                  "Dialect=3;" +
                                  "Charset=NONE;" +
                                  "Role=;" +
                                  "Connection lifetime=15;" +
                                  "Pooling=true;" +
                                  "MinPoolSize=0;" +
                                  "MaxPoolSize=50;" +
                                  "Packet Size=4096;" +
                                  "ServerType = 0;";

            _conexaoFB = new FbConnection(sql);
            _conexaoFB.Open();


            return true;
        }

        public static bool Desconectar()
        {
            _conexaoFB.Close();
            return false;
        }
    }







    //public class ConexaoFirebird
    //{
    //    private static string pUser = "SYSDBA";
    //    private static string pPassword = "masterkey";
    //    private static string pDatabase = "localhost:B:\\PROJETOS_ERICK_2019\\ForcaVendas\\dados\\ForcaVendasBD.fdb";
    //    private static int pPort = 3050;
    //    private static int pDialect = 3;
    //    private static string pCharset = FbCharset.Utf8.ToString();
    //    public bool bconexao { get; set; }
    //    private FbConnection connection;

    //    public ConexaoFirebird()
    //    {
    //        FbConnectionStringBuilder stringconection = new FbConnectionStringBuilder()
    //        {
    //            Port = pPort,
    //            UserID = pUser,
    //            Password = pPassword,
    //            Database = pDatabase,
    //            Dialect = pDialect,
    //            Charset = pCharset

    //        };
    //        try
    //        {
    //            connection = new FbConnection(stringconection.ToString());
    //            connection.Open();
    //            bconexao = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            bconexao = false;
    //            //throw;
    //        }

    //    }




    //}









    //public class ConexaoFirebird
    //{
    //    FbConnection fb = new FbConnection();

    //    public ConexaoFirebird()
    //    {

    //        fb.ConnectionString = "User=SYSDBA;" +
    //                              "Password=masterkey;" +
    //                              "Database=C:\\Users\\luisp\\OneDrive\\Documentos\\BancoDados\\BANCOESTUDO.FDB;" +
    //                              "DataSource=localhost;" +
    //                              "Port=3050;" +
    //                              "Dialect=3;" +
    //                              "Charset=NONE;" +
    //                              "Role=;" +
    //                              "Connection lifetime=15;" +
    //                              "Pooling=true;" +
    //                              "MinPoolSize=0;" +
    //                              "MaxPoolSize=50;" +
    //                              "Packet Size=4096;" +
    //                              "ServerType = 0;";
    //    }

    //    public FbConnection Conectar()
    //    {
    //        if (fb.State == System.Data.ConnectionState.Closed)
    //        {
    //            fb.Open();
    //        }
    //        return fb;
    //    }

    //    public void Desconectar()
    //    {
    //        if (fb.State == System.Data.ConnectionState.Open)
    //        {
    //            fb.Close();
    //        }
    //    }

    //}
}
