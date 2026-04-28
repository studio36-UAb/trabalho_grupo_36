using Studio36.Tests;

List<TestCase> tests = new()
{
    new("T01", "Apresentacao do menu inicial", T01_MenuInicial.Run),
    new("T02", "Terminar aplicacao pela opcao 3", T02_TerminarOpcao3.Run),
    new("T03", "Login com credenciais validas", T03_LoginValido.Run),
    new("T04", "Login com credenciais invalidas", T04_LoginInvalido.Run),
    new("T05", "Acesso a opcao de registo", T05_AcessoRegisto.Run),
    new("T06", "Opcao invalida no menu", T06_OpcaoInvalida.Run),
    new("T07", "Input nao numerico no menu", T07_InputNaoNumerico.Run),
    new("T08", "Input vazio no menu", T08_InputVazio.Run),
    new("T09", "Model valida credenciais validas", T09_ModelLoginValido.Run),
    new("T10", "Model rejeita credenciais invalidas", T10_ModelLoginInvalido.Run)
};

if (args.Length > 0)
{
    HashSet<string> requestedIds = args
        .Select(arg => arg.Trim().ToUpperInvariant())
        .Where(arg => !string.IsNullOrWhiteSpace(arg))
        .ToHashSet();

    tests = tests
        .Where(test => requestedIds.Contains(test.Id))
        .ToList();

    if (tests.Count == 0)
    {
        Console.WriteLine($"No matching tests found for: {string.Join(", ", requestedIds)}");
        Environment.Exit(1);
    }
}

int failed = 0;

foreach (TestCase test in tests)
{
    try
    {
        test.Run();
        Console.WriteLine($"PASS {test.Id} - {test.Description}");
    }
    catch (Exception ex)
    {
        failed++;
        Console.WriteLine($"FAIL {test.Id} - {test.Description}: {ex.Message}");
    }
}

if (failed > 0)
{
    Console.WriteLine($"{failed} test(s) failed.");
    Environment.Exit(1);
}

Console.WriteLine("All tests passed.");
