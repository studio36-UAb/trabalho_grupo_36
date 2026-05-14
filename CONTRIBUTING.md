# Contributing

Este documento define as regras de colaboração no repositório do projeto Studio 36.

---

## Branches principais

| Branch | Finalidade |
|---|---|
| `main` | Versão estável do projeto; só recebe alterações vindas de `develop` |
| `develop` | Branch de integração do desenvolvimento |

---

## Convenção de branches

Cada elemento da equipa tem uma branch individual para o seu trabalho contínuo. Para tarefas bem delimitadas ou que envolvam mais do que um elemento, deve ser criada uma branch de funcionalidade ou de correção.

| Tipo | Quando usar | Exemplos |
|---|---|---|
| Branch individual | Trabalho geral do elemento | `maria`, `claudia`, `joel`, `joao`, `marco` |
| `feature/` | Funcionalidade específica e delimitada | `feature/login`, `feature/dashboard` |
| `fix/` | Correção de um problema identificado | `fix/validacao-credenciais` |

Todas as branches devem ser criadas a partir de `develop`.

---

## Fluxo de trabalho

Sempre que possível, o trabalho deve seguir este fluxo:

1. Atualizar a branch `develop` local (`git pull origin develop`)
2. Criar ou atualizar a branch individual/funcionalidade a partir de `develop`
3. Desenvolver e testar as alterações no Visual Studio
4. Fazer commit com uma mensagem clara e descritiva
5. Enviar a branch para o repositório remoto (`git push`)
6. Abrir **Pull Request para `develop`**
7. Após validação em `develop`, a integração em `main` é feita em coordenação com a **líder de projeto** e o **verificador**

```
branch individual / funcionalidade  →  develop  →  main
```

---

## Mensagens de commit

As mensagens de commit devem ser claras, objetivas e descritivas. Exemplos:

```
Criar estrutura inicial do projeto
Adicionar formulário de login
Corrigir validação de credenciais
Atualizar documentação do projeto
```

---

## Validação e testes antes de contribuir

Antes de submeter alterações através de Pull Request, cada elemento da equipa deve confirmar localmente que o projeto compila e que os testes automatizados existentes continuam a passar.

### Compilar o projeto principal

A partir da raiz do repositório:

```powershell
dotnet build .\src\Studio36\Studio36.csproj
```

### Executar a aplicação principal

```powershell
dotnet run --project .\src\Studio36\Studio36.csproj
```

## Executar todos os testes

A partir da raiz do repositório:

```powershell
dotnet run --project .\tests\Studio36.Tests\Studio36.Tests.csproj
```

### Executar um teste específico

Quando necessário, pode ser executado apenas um teste através do respetivo identificador.

Exemplo (para o teste T01 Apresentação do menu inicial):

```powershell
dotnet run --project .\tests\Studio36.Tests\Studio36.Tests.csproj -- T01
```

Testes atualmente disponíveis:

| ID | Descrição |
|---|---|
| T01 | Apresentação do menu inicial |
| T02 | Terminar aplicação pela opção 3 |
| T03 | Login com credenciais válidas |
| T04 | Login com credenciais inválidas |
| T05 | Acesso à opção de registo |
| T06 | Opção inválida no menu |
| T07 | Input não numérico no menu |
| T08 | Input vazio no menu |
| T09 | Model valida credenciais válidas |
| T10 | Model rejeita credenciais inválidas |
| T18 | Criar projeto sem nome |
| T19 | Criar projeto com datas inválidas |
| T26 | Listar tarefas de projeto inexistente |
| T27 | Introduzir ID de projeto não numérico na listagem de tarefas |
| T28 | Criar projeto válido |
| T29 | Listar projetos |
| T30 | Editar projeto válido |
| T31 | Controller depende de IModel e IView |
| T32 | Controller funciona com implementações fake das interfaces |
| T33 | Fluxo MVC completo na criação de projeto |
| T34 | Fluxo MVC completo na listagem de projetos |
| T35 | Fluxo MVC completo na edição de projeto válido |
| T36 | Fluxo MVC na edição de projeto inexistente |
| T37 | Fluxo MVC na edição de projeto com nome vazio |
| T38 | Eliminar projeto válido |
| T39 | Fluxo MVC completo na geração de relatório |
| T40 | Gerar relatório válido |
| T41 | Gerar relatório de projeto inexistente |

Ao executar um teste com sucesso, deverá ser apresentada uma mensagem de aprovação correspondente ao respetivo identificador.

Os testes T26 e T27 cobrem o fluxo de listagem de tarefas por projeto. O T26 valida o tratamento de `ProjectNotFoundException`, incluindo mensagem de erro, registo em log e apresentação da lista atualizada de projetos. O T27 valida a rejeição de IDs de projeto não numéricos, garantindo que a aplicação continua em execução.

Os testes T18, T19, T28, T29 e T30 cobrem o módulo inicial de projetos através da aplicação de consola. O T18 confirma que um projeto sem nome é rejeitado. O T19 confirma que uma data de fim anterior à data de início é rejeitada. O T28 confirma que a opção "New Project" recolhe os dados necessários, cria o projeto com sucesso e permite consultar a lista de tarefas do projeto criado. O T29 confirma que a opção "List projects" apresenta os projetos existentes. O T30 confirma que a opção "Edit project" atualiza os dados de um projeto existente.

Os testes T31 e T32 cobrem a evolução arquitetónica baseada em interfaces. O T31 valida que o `Controller` depende dos contratos `IModel` e `IView`. O T32 valida que o `Controller` consegue funcionar com implementações fake dessas interfaces, demonstrando que não está preso às classes concretas `Model` e `View`.

Os testes T33 e T34 cobrem fluxos MVC completos com View fake e Model real. O T33 valida o percurso de criação de projeto, desde o pedido da View até à mensagem final apresentada. O T34 valida o percurso de listagem de projetos, confirmando que a View recebe a lista devolvida pelo Model através do Controller.

Os testes T35, T36 e T37 cobrem o fluxo MVC de edição de projetos. O T35 valida a edição de um projeto existente. O T36 valida a tentativa de editar um projeto inexistente. O T37 valida a rejeição de uma edição com nome vazio, garantindo que os dados originais não são alterados.

O teste T38 cobre o fluxo de eliminação de projeto através do menu da aplicação. Valida que a opção "Delete project" recolhe o `idProjeto`, remove o projeto existente, apresenta a mensagem de sucesso e confirma a remoção através da listagem de projetos.

Os testes T39, T40 e T41 cobrem o fluxo de geração de relatórios. O T39 valida o percurso MVC completo com View fake, Model real e gerador de relatório fake. O T40 valida a geração de um ficheiro PDF para um projeto existente. O T41 valida o tratamento de erro quando o projeto indicado não existe.

Os relatórios são gerados com PDFsharp. Para manter compatibilidade entre Windows, macOS e Linux, a fonte Noto Sans está incluída no projeto em `src/Studio36/Assets/Fonts` e é carregada pelo `Studio36FontResolver`. Não devem ser usadas fontes dependentes apenas do sistema operativo, como Arial, sem um resolver próprio.

### Critério mínimo antes de abrir Pull Request

Antes de abrir Pull Request para `develop`, confirmar que:

- [ ] O projeto compila sem erros
- [ ] A aplicação executa corretamente
- [ ] Os testes automatizados existentes passam com sucesso
- [ ] A alteração realizada não quebra funcionalidades já implementadas
- [ ] O código alterado respeita a arquitetura MVC definida para o projeto
- [ ] O `Controller` continua a depender de `IModel` e `IView`, e não diretamente das classes concretas `Model` e `View`
- [ ] Novos fluxos de erro ou exceções relevantes têm testes automatizados associados, sempre que possível
- [ ] A branch está atualizada em relação a `develop`

Caso algum teste falhe, a causa deve ser analisada e corrigida antes da submissão do Pull Request. Se a falha estiver relacionada com uma alteração intencional do comportamento da aplicação, o respetivo teste deve ser atualizado de forma coerente com a nova implementação.

---

## ### Regra de integração

É obrigatória a utilização de **Pull Request** para integração de alterações na branch `develop`.

Não devem ser realizados merges diretos para `develop` sem revisão, aprovação e validação prévia das alterações.

---

## Arquitetura MVC - regras de separação de camadas

O projeto segue o padrão MVC. Para manter o acoplamento baixo devem ser respeitadas as seguintes regras:

- A **View não pode depender do Model** - não deve importar o namespace `ModelComponent` nem receber instâncias do Model
- O **Model não pode depender da View** - não deve importar o namespace `ViewComponent`
- Toda a comunicação entre Model e View é feita **exclusivamente através de eventos**, com os parâmetros necessários incluídos no próprio evento
- O **Controller é o único intermediário** - subscreve os eventos do Model e chama os métodos públicos da View para atualizar o ecrã
- O **Controller deve depender das interfaces** `IModel` e `IView`, não das classes concretas `Model` e `View`
- O **Controller deve depender da interface** `IReportGenerator` para gerar relatórios, não de uma classe concreta nem de detalhes de PDF
- As classes concretas devem ser criadas no ponto de arranque da aplicação e injetadas no `Controller`
- Exceções do Model, como `ProjectNotFoundException`, devem ser tratadas no Controller, que decide que mensagens e atualizações devem ser enviadas para a View

### Interfaces entre componentes

As interfaces devem representar apenas os contratos necessários para a comunicação entre componentes. Não devem ser usadas como cópias completas das classes concretas nem como listas genéricas de métodos futuros.

Exemplo do padrão esperado:

```csharp
IModel model = new Model();
IView view = new View();
IReportGenerator reportGenerator = new PdfReportGenerator();

Controller controller = new(model, view, reportGenerator);
```

No `Controller`, o campo deve manter o tipo da interface:

```csharp
private readonly IModel model;
private readonly IView view;
private readonly IReportGenerator reportGenerator;
```

Isto permite substituir futuramente a View ou o Model por outra implementação, desde que respeite o contrato definido.

### Exemplo correto de eventos com dados

```csharp
// Model - publica estado diretamente no evento
public event Action<LoginResultData>? SendLoginState;
SendLoginState?.Invoke(new LoginResultData(true, "Login successful."));

// View - recebe dados como parâmetro, sem aceder ao Model
public void ShowLoginResult(bool isLoggedIn, string message) { ... }

// Controller - faz a ligação
model.SendLoginState += result => view.ShowLoginResult(result.IsSuccessful, result.Message);
```
