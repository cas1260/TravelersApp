//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using System.Xml.Serialization;
//using AppIsphereTravelers.Classes;
//using AppIsphereTravelers.Models;
//using Newtonsoft.Json;
//using Xamarin.Forms;

//namespace AppIsphereTravelers.Classes
//{
//    public class StringWriterUtf8 : System.IO.StringWriter
//    {
//        public override Encoding Encoding
//        {
//            get
//            {
//                return Encoding.UTF8;
//            }
//        }
//    }

//    [Serializable]
//    public class ClsSync
//    {
//        private string _codigoEmpresa;
//        private Label _label;
//        public ClsSync(string codigoEmpresa, Label lblinfor)
//        {
//            _codigoEmpresa = codigoEmpresa;
//            _label = lblinfor;
//        }

//        public string SerializeObject<T>(T toSerialize)
//        {
            
//            System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(T));
//            StringWriterUtf8 text = new StringWriterUtf8();
//            xml.Serialize(text, toSerialize);
//            return text.ToString();

//            //return utf8;
//            /*
//            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

//            using (StringWriter textWriter = new StringWriter())
//            {
//                xmlSerializer.Serialize(textWriter, toSerialize);
//                return textWriter.ToString();
//            }*/
//        }
//        public async Task<bool> SyncEnviaPedidoAsync()
//        {
//            var Api = new clsDownload(_codigoEmpresa, _label);
//            var Cn = new clsBd().AbreBanco(_codigoEmpresa);

//            var ped = SerializeObject<List<tblpedido>>(Cn.Table<tblpedido>().ToList());
//            var Item = SerializeObject<List<tblpedidoitem>>(Cn.Table<tblpedidoitem>().ToList());

//            var retorno = await Api.Upload("https://app.webfinan.com.br/erp/webservices/apiupload.php?serial=" + _codigoEmpresa + "&", $"pedido={ped}&Item={Item}", "Enviado dados de pedido.");
            
//            return retorno == "OK" ? true : false;
//        }
//        public async Task SyncUserAsync()
//        {
//            await SyncTableAsync<tblUsuario>("Select Id, login, senha, Nome from tblusuario", "Baixando dados de Usuarios");
//        }
//        public async Task SyncEmpresaAsync()
//        {
//            await SyncTableAsync<tblEmpresa>("Select id, codigo, razao, documento from tblempresa where ativo=1 order by razao", "Baixando dados da Empresa");
//        }

//        public async Task SyncFormaAsync()
//        {
//            await SyncTableAsync<tblforma>("Select Id, Descricao, idempresa, tipo, parcela, intervalo, carencia from tblforma", "Baixando dados de forma de pgt.");
//        }
//        public async Task SyncProdutosAsync()
//        {
//            await SyncTableAsync<tblproduto>("SELECT a.Id, a.codigo, a.descricao, b.id as idgrade, b.descricao as descricaograde, b.valorvenda as valorgrade, a.idempresa FROM tblproduto a inner join tblprodutograde b on a.id = b.idproduto order by a.descricao, b.descricao", "Baixando dados de produto");
//        }
//        public async Task SyncClienteAsync()
//        {
//            var Cli = await SyncTableAsync<tblcliente>("Select Id, Codigo, Nome, Ativo, idempresa, Email, Empresa, Endereco, Bairro, Cidade, Cep, UF, Telefone1, Telefone2 from tblcliente where Tipo=1 order by Nome", "Baixando dados de cliente");

//            /*
//            var Api = new clsDownload();
//            Action x = new Action(async () =>
//            {
//                var Cn = new clsBd().AbreBanco();
//                var Clientes = await Api.DoSql<List<tblcliente>>("Select Id, Codigo, Nome, Ativo from tblcliente where Tipo=1 order by Nome");

//                Cn.DropTable<tblcliente>();
//                Cn.CreateTable<tblcliente>();
//                Cn.InsertAll(Clientes);
//                Cn.Close();

//            });

//            x.BeginInvoke(null, null);
//            */

//        }
//        public async Task<List<T>> SyncTableAsync<T>(string sql, string informacao)
//        {
//            var Api = new clsDownload(_codigoEmpresa, _label);

//            var Cn = new clsBd().AbreBanco();
//            var Retorno = await Api.DoSql<List<T>>(sql, informacao);

//            Cn.DropTable<T>();
//            Cn.CreateTable<T>();
//            Cn.InsertAll(Retorno);
//            Cn.Close();
//            Cn = null;
//            return Retorno;
//        }

//        public override bool Equals(object obj)
//        {
//            return base.Equals(obj);
//        }

//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }

//        public override string ToString()
//        {
//            return base.ToString();
//        }
//    }
//}
