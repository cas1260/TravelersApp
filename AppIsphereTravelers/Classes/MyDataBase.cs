/*using System;
using System.Collections.Generic;
using System.Text;
using MySql;
using System.Data;
using MySql.Data.MySqlClient;

namespace IFControl.Classes
{
    class MyDataBase
    {
        public MySqlConnection Cn;
        public MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        public string NomeBanco { get; set; }
        public void doSql(string sql)
        {
            if (NomeBanco == "")
            {
                NomeBanco = "1105";
            }

            try
            {
                Cn = new MySqlConnection();
                Cn.ConnectionString = CriaConexao();
                Cn.Open();

                MySqlDataReader rdr = null;
                MySqlCommand cmd = new MySqlCommand(sql, Cn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.Write(rdr);
                    /*Produto p = new Produto();
                    p.Nome = rdr["nome"].ToString();
                    p.Preco = Convert.ToDecimal(rdr["preco"]);
                    lista.Add(p.Nome + "            " + p.Preco);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cn.Close();
            }

        }
        private string CriaConexao()
        {
            builder.Server = "app.webfinan.com.br";
            builder.Database = $"webfinan_{NomeBanco}";
            builder.UserID = $"webfinan_{NomeBanco}";
            builder.Password = "6pagek+ruP";
            return $"Server={builder.Server};Database={builder.Database};Uid={builder.UserID};Pwd={builder.Password};";
        }



    }
}
*/