using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<string, (string grupo, double carga, int repeticoes)> exercicios
        = new Dictionary<string, (string, double, int)>();

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
                Console.WriteLine("Saindo..."); 
                break;
                default: Console.WriteLine("Opção inválida!"); break;
            }

        } while (opcao != 0);
    }

    static void Menu()
    {
        Console.WriteLine("\n=========== MENU =============");
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

        if (exercicios.ContainsKey(nome))
        {
            Console.WriteLine("Exercício já existe!");
            return;
        }

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        Console.Write("Carga (kg): ");
        if (!double.TryParse(Console.ReadLine(), out double carga) || carga < 0)
        {
            Console.WriteLine("Carga inválida!");
            return;
        }

        Console.Write("Repetições: ");
        if (!int.TryParse(Console.ReadLine(), out int repet) || repet < 1)
        {
            Console.WriteLine("Repetições inválidas!");
            return;
        }

        exercicios[nome] = (grupo, carga, repet);

        Console.WriteLine("Exercício adicionado");
    }

    static void ListarExercicios()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        Console.WriteLine("\n--- Lista de Exercícios ---");

        foreach (var item in exercicios)
        {
            Console.WriteLine($"{item.Key} - {item.Value.grupo} - {item.Value.carga}kg - {item.Value.repeticoes} reps");
        }
    }

    static void BuscarExercicio()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine();

        if (exercicios.TryGetValue(nome, out var dados))
        {
            Console.WriteLine($"{nome} - {dados.grupo} - {dados.carga}kg - {dados.repeticoes} reps");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }

    static void FiltrarGrupo()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o grupo muscular: ");
        string grupoBusca = Console.ReadLine();

        var resultados = exercicios
            .Where(e => e.Value.grupo.Equals(grupoBusca, StringComparison.OrdinalIgnoreCase));

        if (!resultados.Any())
        {
            Console.WriteLine("Nenhum exercício encontrado.");
            return;
        }

        foreach (var item in resultados)
        {
            Console.WriteLine(item.Key);
        }
    }

    static void CargaTotal()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        double total = exercicios.Sum(e => e.Value.carga);
        Console.WriteLine($"Carga total do treino: {total} kg");
    }

    static void MaisPesado()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        var maisPesado = exercicios
            .OrderByDescending(e => e.Value.carga)
            .First();

        Console.WriteLine($"Mais pesado: {maisPesado.Key} - {maisPesado.Value.carga} kg");
    }

    static void RemoverExercicio()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        Console.Write("Digite o nome do exercício: ");
        string nome = Console.ReadLine();

        if (exercicios.Remove(nome))
        {
            Console.WriteLine("Exercício removido");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }
}
