using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<string> nomes = new List<string>();
    static List<string> grupos = new List<string>();
    static List<double> cargas = new List<double>();
    static List<int> repeticoes = new List<int>();

    static void Main()
    {
        int opcao;

        do
        {
            Menu();
            Console.Write("Escolha uma opção: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: 
                    AdicionarExercicio();
                    break;
                case 2:
                    ListarExercicios(); 
                    break;
                case 3: 
                    BuscarExercicio(); 
                    break;
                case 4: 
                    FiltrarGrupo(); 
                    break;
                case 5: 
                    CargaTotal(); 
                    break;
                case 6: 
                    MaisPesado(); 
                    break;
                case 7: 
                    RemoverExercicio(); 
                    break;
                case 0: 
                    Console.WriteLine("Exit..."); 
                    break;
                default: Console.WriteLine("Opção inválida!"); break;
            }

        } while (opcao != 0);
    }

    static void Menu()
    {
        Console.WriteLine("\n==== MENU ====");
        Console.WriteLine("1 - Adicionar exercício");
        Console.WriteLine("2 - Listar exercícios");
        Console.WriteLine("3 - Buscar exercício por nome");
        Console.WriteLine("4 - Filtrar por grupo muscular");
        Console.WriteLine("5 - Calcular carga total do treino");
        Console.WriteLine("6 - Exibir exercício mais pesado");
        Console.WriteLine("7 - Remover exercício");
        Console.WriteLine("0 - Sair");
    }

    static void AdicionarExercicio()
    {
        Console.Write("Nome do exercício: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome não pode ser vazio");
            return;
        }

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        double carga;
        Console.Write("Carga (kg): ");
        if (!double.TryParse(Console.ReadLine(), out carga) || carga < 0)
        {
            Console.WriteLine("Carga inválida!");
            return;
        }

        int repet;
        Console.Write("Repetições: ");
        if (!int.TryParse(Console.ReadLine(), out repet) || repet < 1)
        {
            Console.WriteLine("Repetições inválidas!");
            return;
        }

        nomes.Add(nome);
        grupos.Add(grupo);
        cargas.Add(carga);
        repeticoes.Add(repet);

        Console.WriteLine("Exercício adicionado");
    }

    static void ListarExercicios()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        Console.WriteLine("\n--- Lista de Exercícios ---");

        for (int i = 0; i < nomes.Count; i++)
        {
            Console.WriteLine($"{nomes[i]} - {grupos[i]} - {cargas[i]}kg - {repeticoes[i]} reps");
        }
    }

    static void BuscarExercicio()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome: ");
        string busca = Console.ReadLine();

        var resultados = nomes
            .Select((n, i) => new { Nome = n, Index = i })
            .Where(x => x.Nome.Equals(busca, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("Exercício não encontrado.");
            return;
        }

        foreach (var item in resultados)
        {
            int i = item.Index;
            Console.WriteLine($"{nomes[i]} - {grupos[i]} - {cargas[i]}kg - {repeticoes[i]} reps");
        }
    }

    static void FiltrarGrupo()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o grupo muscular: ");
        string grupoBusca = Console.ReadLine();

        var resultados = grupos
            .Select((g, i) => new { Grupo = g, Index = i })
            .Where(x => x.Grupo.Equals(grupoBusca, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("Nenhum exercício encontrado.");
            return;
        }

        Console.WriteLine("\nExercícios encontrados:");
        foreach (var item in resultados)
        {
            Console.WriteLine(nomes[item.Index]);
        }
    }

    static void CargaTotal()
    {
        if (cargas.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        double total = cargas.Sum();
        Console.WriteLine($"Carga total do treino: {total} kg");
    }

    static void MaisPesado()
    {
        if (cargas.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        double max = cargas.Max();
        int index = cargas.IndexOf(max);

        Console.WriteLine($"Mais pesado: {nomes[index]} - {max} kg");
    }

    static void RemoverExercicio()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome do exercício: ");
        string nome = Console.ReadLine();

        int index = nomes.FindIndex(n => n.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (index == -1)
        {
            Console.WriteLine("Exercício não encontrado.");
            return;
        }

        nomes.RemoveAt(index);
        grupos.RemoveAt(index);
        cargas.RemoveAt(index);
        repeticoes.RemoveAt(index);

        Console.WriteLine("Exercício removido");
    }
}
