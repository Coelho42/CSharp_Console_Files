using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

/*
 * Teste Prático do capitulo 7
 * Cap07_Console_Files_TestePratico
 * 
 * Op1 - Com Collections e classes - Obrigatório o uso de ciclos forEach
 * 
 * Teste: Elaborar a seguinte app.
 * 
 * - A unidade de dados base da app é uma classe ou struct Pessoa com os seguintes atributos
 *      - Nome
 *      - Idade
 *      
 * - Menu com 4 opções 
 *      - Op1 - Recolhe os dados de uma pessoa, para um objeto Pessoa e adiciona à collection ou array de objetos
 *      - Op2 - Mostra os dados da collection ou array no ecrã
 *      - Op3 - Save: Guarda a collection ou array numa file binária
 *      - Op4 - Load: Lê a collection ou array da file binária e mostra no ecrã
 */

namespace Cap09_Console_Files_TestePratico
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variaveis para navegar no menu
            int i = 0;
            string escolha;

            List<Pessoa> listaDePessoas = new List<Pessoa>();   // Inicialização da lista de pessoas

            // Variaveis para usar no adicionamento de uma pessoa nova
            string nomeDaPessoa;
            int idadeDaPessoa;

            string fileAndLocation = "C:\\Users\\newma\\Desktop\\ficheiro.bin";   // Destino onde sera guardado o file binario

            // MENUS
            // Continuar a perguntar que opção o utilizador quer fazer até que o mesmo introduza uma opção valida
            do
            {
                // Desenha o MENU PRINCIPAL
                desenhaMenu();
                Console.WriteLine(Environment.NewLine);

                // Pede o numero da opção e guarda-o em string
                desenhaChar(10, ' ');
                Console.Write("Introduza o numero da opção que pretende escolher: ");
                escolha = Console.ReadLine();

                // Navega pelo menu dependendo do input do utilizador
                switch (escolha)
                {
                    // recolhe os dados de uma pessoa, para um objeto Pessoa e adiciona a collection ou array de objetos 
                    case "1":
                        Console.Clear();

                        Console.WriteLine("Introduza o nome da pessoa");
                        nomeDaPessoa = Console.ReadLine();

                        Console.WriteLine(Environment.NewLine);

                        Console.WriteLine("Introduza a idade da pessoa");
                        idadeDaPessoa = Convert.ToInt32(Console.ReadLine());

                        Pessoa pessoa = new Pessoa(nomeDaPessoa, idadeDaPessoa);

                        listaDePessoas.Add(pessoa);

                        Console.Clear();
                        break;

                    // mostra os dados da collection ou array no ecra
                    case "2":
                        Console.Clear();

                        // Para cada objeto Pessoa dentro da lista, imprime os seus atributos
                        foreach (Pessoa aluno in listaDePessoas)
                        {
                            Console.WriteLine("Nome: " + aluno.getNome());
                            Console.WriteLine("Idade: " + aluno.getIdade());
                            Console.WriteLine("");
                        }

                        Console.ReadKey();
                        Console.Clear();
                        break;

                    // Save: guarda a collection ou array numa file binaria
                    case "3":
                        Console.Clear();

                        Serializar(fileAndLocation, listaDePessoas);

                        Console.ReadKey();
                        Console.Clear();
                        break;

                    // load: lê a collection ou array da file binaria e lê no ecra
                    // Save: guarda a collection ou array numa file binaria
                    case "4":
                        Console.Clear();

                        List<Pessoa> listaDePessoas2 = (List<Pessoa>)Desserializar(fileAndLocation);    // Dessearialização do ficheiro binario para uma lista de pessoas

                        // para cada pessoa da lista de pessoas, imprime os seus atributos
                        foreach (Pessoa aluno in listaDePessoas2)
                        {
                            Console.WriteLine("Nome: " + aluno.getNome());
                            Console.WriteLine("Numero: " + aluno.getIdade());
                            Console.WriteLine("");
                        }

                        Console.ReadKey();
                        Console.Clear();
                        break;

                    // SAIR
                    case "0":
                        i = 1;
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        break;
                        // fim do switch de navegaçao 
                }
            } while (i == 0);
        }

        /// <summary>
        /// Metodo para serializar
        /// Ao ter no parametro o tipo Object, permite receber ali qualquer objeto
        /// </summary>
        /// <param name="fileLocation"> string com a localização e nome do file onde sera gravado o objeto </param>
        /// <param name="obj"> Objeto a serualizar (Object é o topo da hierarquia dos objetos nas POO) </param>
        public static void Serializar(string fileLocation, Object obj)
        {
            try
            {
                Stream streamToFile = File.OpenWrite(fileLocation);     // Cria Stream para abrir a escrever no file
                BinaryFormatter bf = new BinaryFormatter();             // Serializador / DesSerializador
                bf.Serialize(streamToFile, obj);                        // Escreve os bits do objeto no file binario
                streamToFile.Close();
                Console.WriteLine("Sarializado com sucesso para " + fileLocation);
            }
            catch (IOException e)
            {
                Console.WriteLine("ERRO: Impossivel criar ou abrir o ficheiro.");
                e.ToString();       // Imprimir o erro tecnico
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO: Ocorreu um problema inesperado.");
                Console.WriteLine(e.ToString());       // Imprimir o erro tecnico
            }

        }

        /// <summary>
        /// Metodo para deserializar
        /// recebe a string da localização e o nome do file que recebe o obj e o obj a desserializar
        /// </summary>
        /// <param name="fileLocation"> string com a localização e o nome da file a ser criada </param>
        /// <returns> objeto generico (topo da hierarquia dos objetos - tem que ser convertido com um CAST) </returns>
        public static Object Desserializar(string fileLocation)
        {
            try
            {
                Stream streamFromFile = File.OpenRead(fileLocation);    // Cria Stream
                BinaryFormatter bf = new BinaryFormatter();             // Serializador / DesSerializador
                Object obj = bf.Deserialize(streamFromFile);            // Recebe os 
                return obj;
            }
            catch (IOException e)
            {
                Console.WriteLine("ERRO: Impossivel criar ou abrir o ficheiro.");
                e.ToString();       // Imprimir o erro tecnico
                return null;        // Caso haja um erro, devolve um objeto null
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO: Ocorreu um problema inesperado.");
                e.ToString();       // Imprimir o erro tecnico
                return null;        // Caso haja um erro, devolve um objeto null
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Menu
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Número de astericos e tipo de carater que pretendemos utilizar para desenhar o Menu
        /// </summary>
        /// <param name="contador">Número de asteriscos</param>
        /// <param name="carater">Carater que pretendemos utilizar para desenhar o Menu</param>

        static void desenhaChar(int contador, char carater)
        {
            for (int i = 1; i <= contador; i++)
            {
                System.Console.Write(carater);
            }
        }

        /// <summary>
        /// Desenho e Opções do Menu
        /// </summary>
        static void desenhaMenu()
        {

            string menu = "Menu Principal";
            string opcao1 = "Adicionar uma pessoa";
            string opcao2 = "Mostrar as pessoas";
            string opcao3 = "Guardar numa file binaria";
            string opcao4 = "Ler da file binaria";
            string opcao5 = "Sair";

            int conta;

            // Cabeçalho do Menu
            desenhaChar(35, ' ');
            desenhaChar(50, '*');
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(2, '*');
            desenhaChar(16, ' ');
            conta = menu.Length;
            System.Console.Write(menu);
            desenhaChar(30 - conta, ' ');
            desenhaChar(2, '*');
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(50, '*');

            // Linhas Laterais
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais e 1 ª opção do Menu
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(5, ' ');
            System.Console.Write(" 1 - ");
            conta = opcao1.Length;
            System.Console.Write(opcao1);
            desenhaChar(38 - conta, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais e 2 ª opção do Menu
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(5, ' ');
            System.Console.Write(" 2 - ");
            conta = opcao2.Length;
            System.Console.Write(opcao2);
            desenhaChar(38 - conta, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais e 3 ª opção do Menu
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(5, ' ');
            System.Console.Write(" 3 - ");
            conta = opcao3.Length;
            System.Console.Write(opcao3);
            desenhaChar(38 - conta, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais e 4 ª opção do Menu
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(5, ' ');
            System.Console.Write(" 4 - ");
            conta = opcao4.Length;
            System.Console.Write(opcao4);
            desenhaChar(38 - conta, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais e 5 ª opção do Menu
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(5, ' ');
            System.Console.Write(" 0 - ");
            conta = opcao5.Length;
            System.Console.Write(opcao5);
            desenhaChar(38 - conta, ' ');
            desenhaChar(1, '*');

            // Linhas Laterais
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(1, '*');
            desenhaChar(48, ' ');
            desenhaChar(1, '*');

            // Linha de baixo do Menu
            System.Console.WriteLine(" ");
            desenhaChar(35, ' ');
            desenhaChar(50, '*');
            System.Console.WriteLine(" ");
            System.Console.WriteLine(" ");
        }
    }
} 
