using System;
using PersonalExpanseManager.Enums;
using PersonalExpenseManager.Presentation.Controller;
using Spectre.Console;

namespace PersonalExpenseManager.Presentation.CLI;

public class Menu
{
    private readonly DespesaController _controller;

    public Menu(DespesaController controller) => _controller = controller;

    public void Iniciar()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(new Rule($"[yellow]Bem Vindo(a) ao PEM[/]").RuleStyle("yellow").Centered());

            var escolha = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold green]Escolha uma opção:[/]")
                    .PageSize(10)
                    .AddChoices(
                    [
                        "Adicionar nova despesa",
                        "Listar todas as despesas",
                        "Filtrar despesas",
                        "Remover uma despesa",
                        "Gerar relatório",
                        "Sair"
                    ])
            );

            switch (escolha)
            {
                case "Adicionar nova despesa":
                    AdicionarDespesa();
                    break;
                case "Listar todas as despesas":
                    ListarDespesas();
                    break;
                case "Filtrar despesas":
                    FiltrarDespesa();
                    break;
                case "Remover uma despesa":
                    RemoverDespesa();
                    break;
                case "Gerar relatório":
                    GerarRelatorio();
                    break;
                case "Sair":
                    Console.Clear();
                    return;
            }
        }
    }

    private void AdicionarDespesa()
    {
        Console.Clear();
        AnsiConsole.Markup("[bold yellow]Adicionar uma nova despesa[/]\n");

        var valor = AnsiConsole.Ask<decimal>("Digite o [green]valor[/] da despesa:");
        var categoria = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Escolha a [green]categoria[/]")
                .AddChoices(Enum.GetNames<Categoria>()));

        var data = AnsiConsole.Ask<DateOnly>("Digite a data [green](AAAA-MM-DD)[/]");

        _controller.AdicionarDespesa(valor, categoria, data);

        AnsiConsole.Markup("[gray](Pression qualquer tecla para voltar ao menu)[/]");

        Console.ReadKey();
    }

    private void ListarDespesas()
    {
        var despesas = _controller.ListarDespesa();

        if (despesas.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Nenhuma despesa encontrada.[/]");
        }
        else
        {
            var table = new Table();
            table.AddColumn("Valor");
            table.AddColumn("Categoria");
            table.AddColumn("Data");

            foreach (var despesa in despesas)
            {
                table.AddRow(
                    despesa.Valor.ToString("C"),
                    despesa.Categoria,
                    despesa.Data.ToString("dd/MM/yyyy")
                    );
            }

            AnsiConsole.Write(table);
        }

        AnsiConsole.Markup("[gray](Pressione qualquer tecla para voltar ao menu)[/]");
        Console.ReadKey();
    }

    private void FiltrarDespesa()
    {
        string categoria = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Escolha a categoria para filtrar")
                .AddChoices(Enum.GetNames<Categoria>())
        );

        var despesasFiltradas = _controller.FiltrarDespesas(categoria);

        if (despesasFiltradas.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Nenhuma despesa encontrada para essa categoria.[/]");
        }
        else
        {
            var table = new Table();
            table.AddColumn("Valor");
            table.AddColumn("Categoria");
            table.AddColumn("Data");

            foreach (var despesa in despesasFiltradas)
            {
                table.AddRow(
                    despesa.Valor.ToString("C"),
                    despesa.Categoria,
                    despesa.Data.ToString("dd/MM/yyyy")
                );
            }

            AnsiConsole.Write(table);
        }

        AnsiConsole.Markup("[gray](Pressione qualquer tecla para voltar ao menu)[/]");
        Console.ReadKey();
    }

    private void RemoverDespesa()
    {
        var despesa = _controller.ListarDespesa();

        if (despesa.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Nenhuma despesa encontrada![/]");
            return;
        }

        var escolha = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selecione a [green]despesa[/] para remover:")
                .PageSize(10)
                .AddChoices(despesa.Select(d => $"{d.Id} | {d.Categoria} - {d.Valor:C}").ToList())
        );

        // Extrai o ID da string
        var idEscolhido = escolha.Split(" | ")[0];

        if (Guid.TryParse(idEscolhido, out Guid idDespesa))
        {
            _controller.RemoverDespesa(idDespesa);
            AnsiConsole.MarkupLine("[green]Despesa removida com sucesso![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Erro ao interpretar o ID da despesa[/]");
        }

        AnsiConsole.MarkupLine("[gray](Pressione qualquer tecla para voltar ao menu)[/]");
        Console.ReadKey();
    }

    private void GerarRelatorio()
    {
        int mes = AnsiConsole.Prompt(
            new TextPrompt<int>("Digite o [green]mês (1-12)[/] para gerar o relatório:")
                .ValidationErrorMessage("[red]Mês inválido![/]")
                .Validate(input => input >= 1 && input <= 12 ? ValidationResult.Success() : ValidationResult.Error("[red]Digite um mês válido (1 a 12)![/]"))
        );

        int ano = AnsiConsole.Prompt(
            new TextPrompt<int>("Digite o [green]ano[/] para gerar o relatório:")
                .ValidationErrorMessage("[red]Ano inválido![/]")
                .Validate(input => input > 2000 ? ValidationResult.Success() : ValidationResult.Error("[red]Digite um ano válido![/]"))
        );

        var relatorio = _controller.GerarRelatorio(mes, ano);

        // Criando a tabela para exibir os valores por categoria
        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.AddColumn("[bold]Categoria[/]");
        table.AddColumn("[bold]Total Gasto (R$)[/]");

        foreach (var categoria in relatorio.TotaisPorCategoria)
        {
            table.AddRow(categoria.Key.ToString(), $"R$ {categoria.Value:F2}");
        }

        AnsiConsole.Write(new Rule($"[yellow]Relatório de {mes:D2}/{ano}[/]").RuleStyle("yellow").Centered());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine($"[bold yellow]Total Geral: R$ {relatorio.TotalGeral:F2}[/]");

        AnsiConsole.Markup("\n[gray](Pressione qualquer tecla para voltar ao menu...)[/]");
        Console.ReadKey();
    }

}
