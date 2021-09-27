using System;

namespace Dio.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        
        private static int entradaGenero, entradaAno;
        private static string entradaDescricao, entradaTitulo;

        static void Main(string[] args)
        {
            string opcaoUsuario = ObeterOpcao();
            
            while (opcaoUsuario.ToUpper() != "X")
            {

                switch (opcaoUsuario)
                {
                    case "1":
                        ListaSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSeries();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSeries();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObeterOpcao();
            }
            Console.WriteLine("Obrigado! Volte Sempre!");
        }

        private static void ListaSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                if (excluido == true)
                {
                    Console.WriteLine("#ID " + serie.retornaId()+ ": - " + serie.retornaTitulo() + " *Excluido*");
                } 
                else
                {
                    Console.WriteLine("#ID " + serie.retornaId()+ ": - " + serie.retornaTitulo());
                }
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}--{1}", i, Enum.GetName(typeof(Genero),i));
            }

            entradaGenero = CadastroG();
            entradaTitulo = CadastroT();
            entradaAno = CadastroA();
            entradaDescricao = CadastroD();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSeries()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}--{1}", i, Enum.GetName(typeof(Genero),i));
            }

            entradaGenero = CadastroG();
            entradaTitulo = CadastroT();
            entradaAno = CadastroA();
            entradaDescricao = CadastroD();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Tem certeza da exclusão?");
            Console.WriteLine("Escolha a opção abaixo para confirmar:");
            int Sim = 1;
            Console.WriteLine("1 - Sim"); 
            int Nao = 2;
            Console.WriteLine("2 - Não");
            int escolha = int.Parse(Console.ReadLine());

            if(escolha == Sim)
            {
                Console.WriteLine("Excluido com sucesso");
                repositorio.Exclui(indiceSerie);
                
            }
            else if (escolha == Nao)
            {
                Console.WriteLine("Cuidado com a opção escolhida");
                Console.WriteLine("Retornando ao Menu Principal...");
            }
        }

        private static void VisualizarSeries()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static int CadastroG()
        {
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return entradaGenero;
        }

        private static string CadastroT()
        {
            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();
            Console.WriteLine();
            return entradaTitulo;
        }

        private static int CadastroA()
        {
            Console.WriteLine("Digite o Ano de Inicio da Série");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return entradaAno;
        }

        private static string CadastroD()
        {
            Console.WriteLine("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();
            Console.WriteLine();
            return entradaDescricao;
        }

        // Como chamar: "opcaoUsuario = ObeterOpcao()";
        private static string ObeterOpcao()
        {
            Console.WriteLine();
            Console.WriteLine("Cadastro de Série - Menu Principal");
            Console.WriteLine();
            Console.WriteLine("Infome a opção desejada:");
            
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
