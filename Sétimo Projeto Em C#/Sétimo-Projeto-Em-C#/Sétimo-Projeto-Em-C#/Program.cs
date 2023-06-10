using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Sétimo_Projeto_Em_C_
{
    internal class Program
    {
        static string marcaDoInicio;
        static string marcaDoFim;
        static string Nome;
        static string nomeDeUsuario;
        static string dataDeNascimento;
        static string Idade;
        static string numeroDoTelefone;
        static string enderecoDoArquivo;
        public struct cadastroDeUsuario
        {
            public string nome;
            public string nomeDeUsuario;
            public UInt32 idade;
            public DateTime dataDeNascimento;
            public string numeroDoTelefone;
        }

        public enum saida
        {
            Sucesso = 0,
            Sair = 1,
            Excecao = 2
        }

        public static void Exibirmensagem(string Exibirmensagem)
        {
            Console.Write(Exibirmensagem);
            Console.WriteLine("Aperte qualquer coisa pra prosseguir");
            Console.ReadKey(true);
            Console.Clear();
        }

        public static saida digitoUsuario(ref string digito, string mensagem)
        {
            saida retornar;
            Console.WriteLine(mensagem);
            string variavelTemporaria = Console.ReadLine();
            if (variavelTemporaria == "d" || variavelTemporaria == "D")
                retornar = saida.Sair;
            else
            {
                digito = variavelTemporaria;
                retornar = saida.Sucesso;
            }
            Console.Clear();
            return retornar;
        }

        //método pra data
        public static saida dataUsuario(ref DateTime data, string mensagem)
        {
            saida retornar;
            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string variavelTemporaria = Console.ReadLine();
                    if (variavelTemporaria == "d" || variavelTemporaria == "D")
                        retornar = saida.Sair;
                    else
                    {
                        data = Convert.ToDateTime(variavelTemporaria);
                        retornar = saida.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Aperte qualquer tecla pra prosseguir");
                    Console.ReadKey();
                    Console.Clear();
                    retornar = saida.Excecao;
                }
            } while (retornar == saida.Excecao);
            Console.Clear();
            return retornar;
        }

        //método pra Uint32
        public static saida idadeUsuario(ref UInt32 idade, string mensagem)
        {
            saida retornar;
            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string variavelTemporaria = Console.ReadLine();
                    if (variavelTemporaria == "d" || variavelTemporaria == "D")
                        retornar = saida.Sair;
                    else
                    {
                        idade = Convert.ToUInt32(variavelTemporaria);
                        retornar = saida.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Aperte qualquer tecla pra prosseguir");
                    Console.ReadKey();
                    Console.Clear();
                    retornar = saida.Excecao;
                }
            } while (retornar == saida.Excecao);
            Console.Clear();
            return retornar;
        }

        public static saida Telefone(ref string telefoneCelular, string mensagem)
        {
            saida retornar;
            Console.WriteLine(mensagem);
            string variavelTemporaria = Console.ReadLine();
            if (variavelTemporaria == "d" || variavelTemporaria == "D")
                retornar = saida.Sair;
            else
            {
                telefoneCelular = variavelTemporaria;
                retornar = saida.Sucesso;
            }
            Console.Clear();
            return retornar;
        }

        public static saida RegistraUsuario(ref List<cadastroDeUsuario> registrarUsuario)
        {
            cadastroDeUsuario CadastrarUsuario;
            CadastrarUsuario.nome = "";
            CadastrarUsuario.nomeDeUsuario = "";
            CadastrarUsuario.idade = 0;
            CadastrarUsuario.dataDeNascimento = new DateTime();
            CadastrarUsuario.numeroDoTelefone = "";
            if (digitoUsuario(ref CadastrarUsuario.nome, "Digite seu nome completo ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (digitoUsuario(ref CadastrarUsuario.nomeDeUsuario, "Digite seu login ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (idadeUsuario(ref CadastrarUsuario.idade, "Digite sua idade ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (dataUsuario(ref CadastrarUsuario.dataDeNascimento, "Digite sua data de nascimento no formato (DD/MM/AAAA) ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (digitoUsuario(ref CadastrarUsuario.numeroDoTelefone, "Digite seu número de telefone ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            registrarUsuario.Add(CadastrarUsuario);
            SalvaDados(enderecoDoArquivo, registrarUsuario);
            return saida.Sucesso;
        }

        public static void SalvaDados(string gravar, List<cadastroDeUsuario> registrarUsuario)
        {
            try
            {

                string armazenar = "";
                foreach (cadastroDeUsuario armazenarDados in registrarUsuario)
                {
                    armazenar += marcaDoInicio + "\r\n";
                    armazenar += Nome + armazenarDados.nome + "\r\n";
                    armazenar += nomeDeUsuario + armazenarDados.nomeDeUsuario + "\r\n";
                    armazenar += Idade + armazenarDados.idade + "\r\n";
                    armazenar += dataDeNascimento + armazenarDados.dataDeNascimento.ToString("dd/MM/yyyy") + "\r\n";
                    armazenar += numeroDoTelefone + armazenarDados.numeroDoTelefone + "\r\n";
                    armazenar += marcaDoFim + "\r\n";
                }
                File.WriteAllText(gravar, armazenar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void carregar(string dadosUsuario, ref List<cadastroDeUsuario> registrarUsuario)
        {
            try
            {
                if (File.Exists(dadosUsuario))
                {
                    string[] dados = File.ReadAllLines(dadosUsuario);
                    cadastroDeUsuario cadastrar;
                    cadastrar.nome = "";
                    cadastrar.nomeDeUsuario = "";
                    cadastrar.idade = 0;
                    cadastrar.dataDeNascimento = new DateTime();
                    cadastrar.numeroDoTelefone = "";

                    foreach (string cadastro in dados)
                    {
                        if (cadastro.Contains(marcaDoInicio))
                            continue;
                        if (cadastro.Contains(marcaDoFim))
                            registrarUsuario.Add(cadastrar);
                        if (cadastro.Contains(Nome))
                            cadastrar.nome = cadastro.Replace(Nome, "");
                        if (cadastro.Contains(nomeDeUsuario))
                            cadastrar.nomeDeUsuario = cadastro.Replace(nomeDeUsuario, "");
                        if (cadastro.Contains(Idade))
                            cadastrar.idade = Convert.ToUInt32(cadastro.Replace(Idade, ""));
                        if (cadastro.Contains(dataDeNascimento))
                            cadastrar.dataDeNascimento = Convert.ToDateTime(cadastro.Replace(dataDeNascimento, ""));
                        if (cadastro.Contains(numeroDoTelefone))
                            cadastrar.nome = cadastro.Replace(numeroDoTelefone, "");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public static void apagandoUsuario (ref List<cadastroDeUsuario> registrarUsuario)
        {
            Console.WriteLine("Digite o número do telefone para apagar um usuário");
            string temporaria = Console.ReadLine();
            bool apagaramUmUsuario = false;
            if (temporaria.ToLower() == "d")
                return;
            else
            {
                List<cadastroDeUsuario> registrarUsuarioTemporariamente = registrarUsuario.Where(x => x.numeroDoTelefone == temporaria).ToList();
                if (registrarUsuarioTemporariamente.Count >0)
                {
                    foreach(cadastroDeUsuario pessoa in registrarUsuarioTemporariamente)
                    {
                        registrarUsuario.Remove(pessoa);
                        apagaramUmUsuario = true;
                    }
                    if (apagaramUmUsuario)
                        SalvaDados(enderecoDoArquivo, registrarUsuario);
                }
                else
                {
                    Console.WriteLine("Não existe usuário com o número de telefone " + temporaria);
                }
            }
            Exibirmensagem("");
        }
        public static void procurarUsuario (List<cadastroDeUsuario> registrarUsuario)
        {
            Console.WriteLine("Digite o número do seu telefone para procurar um usuário");
            string temporaria = Console.ReadLine();
            if (temporaria.ToLower() == "d")
            
                return;
            else
                {
                List<cadastroDeUsuario> registrarUsuarioTemporariamente = registrarUsuario.Where(x => x.numeroDoTelefone == temporaria).ToList();
                if (registrarUsuarioTemporariamente.Count> 0)
                {
                    foreach(cadastroDeUsuario pessoa in registrarUsuarioTemporariamente)
                    {
                        Console.WriteLine(Nome + pessoa.nome);
                        Console.WriteLine(nomeDeUsuario + pessoa.nomeDeUsuario);
                        Console.WriteLine(Idade + pessoa.idade);
                        Console.WriteLine(dataDeNascimento + pessoa.dataDeNascimento.ToString("dd/MM/yyyy"));
                        Console.WriteLine(numeroDoTelefone + pessoa.numeroDoTelefone);
                    }
                }
                else
                {
                    Console.WriteLine("O telefone " + temporaria + " não existe");
                }
                Exibirmensagem("");
                }
           
        }
        static void Main(string[] args)
        {
            List<cadastroDeUsuario> registrarUsuario = new List<cadastroDeUsuario>();
            string botao = "";
            marcaDoInicio = "##### Inicio #####";
            marcaDoFim = "##### Fim #####";
            Nome = "Nome: ";
            nomeDeUsuario = "Nome_de_usuario = ";
            Idade = "Idade: ";
            dataDeNascimento = "Data_de_nascimento: ";
            numeroDoTelefone = "Numero_do_telefone: ";
            enderecoDoArquivo = @"DadosDosUsuarios.txt";

            carregar(enderecoDoArquivo, ref registrarUsuario);

            do
            {
                Console.WriteLine("Pressione R para registrar usuário ");
                Console.WriteLine("Pressione P para procurar um usuário ");
                Console.WriteLine("Pressione A para apagar um usuário");
                Console.WriteLine("Pressione D para sair");
                botao = Console.ReadKey(true).KeyChar.ToString().ToLower();

                if (botao == "r")
                {
                    //cadastrar um usuário novo
                    RegistraUsuario(ref registrarUsuario);
                        
                }
                else if (botao == "p")
                {
                    //procurar um usuário
                    procurarUsuario(registrarUsuario);
                }
                else if (botao == "a")
                {
                    //apagar um usuário
                    apagandoUsuario(ref registrarUsuario);
                }
                else if (botao == "d")
                {
                    Exibirmensagem("Obrigado e volte sempre, o programa será encerrado em breve");

                }
                else
                {
                    Exibirmensagem("Opção desconhecida, por favor, selecione uma das opções conhecidas pelo nosso sistema");
                }

            } while (botao != "d");
        }
    }
}
