using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppIsphereTravelers.Classes
{
    public class TransclsTranslatorEmun
    {
        public string Idioma { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
    public class clsTranslator
    {
        public clsTranslator(string idiomaPadrao)
        {
            IdiomaPadrao = (idiomaPadrao == "" || idiomaPadrao ==null) ?  "EN" : idiomaPadrao;
            Load();
        }

        public List<TransclsTranslatorEmun> Textos { get; set; } = new List<TransclsTranslatorEmun>();
        public string IdiomaPadrao { get; set; }
        public void Load()
        {
            add("lblAguarde", "Aguarde...", "Wait...");

            add("TituloAtecao", "Atenção", "Attention");
            add("ErroLogin", "Login invalido", "Invalid login");
            add("ErroSenha", "Senha invalido", "Invalid password");
            add("ErroAoLogin", "Usuário ou senha invalido!", "Invalid user or password!");
            add("Cadastra-se", "Cadastra-se", "Register");
            add("Cadastrar", "Cadastrar", "Register");

            add("LabelWELCOME1", "BEM-VINDO SUA", "WELCOME TO");
            add("LabelWELCOME2", "REDE DE VIAGEM", "YOUR PRIVATE");
            add("LabelWELCOME3", "PRIVADA", "TRAVEL NETWORK");

            add("btnHome", "Pagina Inicial", "Home");

            add("lblPerfil", "Meu Perfil", "My profile");
            add("lblProdutos", "Meus Produtos", "My Products");
            add("lblAlertaMedicado", "Alerta de Medicamentos", "Medication Alert");
            add("MinhaRede", "Minha rede social", "My social network");
            add("BkpViagem", "Backup de viagem", "Travel Backup");
            add("lblSair", "Sair", "Exit");

            add("ConfirmSair", "Deseja sair?", "Do you want to quit?");
            add("sim", "Sim", "Yes");
            add("nao", "Não", "No");

            add("LoadInfor", "Carregando dados...", "Loading....");
            add("contentPai", "Meus Produtos", "My Products");

            add("Detalhes", "Detalhes", "Details");
            add("Relatorio", "Relatorio", "Report");
            add("Ativar", "Ativar", "Enable");
            add("NaoAtivo", "Produto não ativo", "Product not active");
            add("Logar", "Logando no sistema", "Logging on to the system");
            add("Qr", "Codigo QR invalida.", "QR code invalid.");
            add("Parabens", "Parabéns", "Congratulations");
            add("ProdutoAtivo", "Produto ativado com sucesso.", "Product activated successfully.");
            add("LeitorQR", "Leitor de QrCode", "QrCode Reader");

            add("ErroCamera", "Câmera não disponível.", "Camera not available.");

            add("txtlogin", "Digite seu login", "Enter your login");
            add("lblsenha", "Senha", "Password");
            add("txtSenha", "Digite sua senha", "Type your password");
            add("cmdlogar", "Entrar", "Log in");
            add("cmdNovo", "Cadastrar", "Register");
        }

        public void add(string chave, string valorPT, string ValorEN)
        {
            Textos.Add(new TransclsTranslatorEmun
            {
                Chave = chave,
                Idioma = "PT",
                Valor = valorPT
            });

            Textos.Add(new TransclsTranslatorEmun
            {
                Chave = chave,
                Idioma = "EN",
                Valor = ValorEN
            });
        }

        public string Get(string chave, string idioma = null)
        {
            return Textos.FirstOrDefault(x => x.Idioma == (idioma ?? IdiomaPadrao) && x.Chave == chave)?.Valor ?? "";
        }

    }
}
