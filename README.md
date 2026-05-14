# Studio 36 — Grupo 36

Repositório do projeto de grupo da unidade curricular **21179 - Laboratório de Desenvolvimento de Software**, da Universidade Aberta (2025/2026).

---

## Sobre o projeto

O Studio 36 é uma aplicação de consola desenvolvida em C# para **gestão de projetos e tarefas**. Permite autenticar utilizadores, gerir projetos, tarefas e membros de equipa, e gerar relatórios em formato PDF. A aplicação segue o padrão arquitetónico **MVC (Model-View-Controller)** e utiliza interfaces para reduzir dependências entre componentes.

---

## Enquadramento

Este repositório destina-se ao desenvolvimento do projeto de grupo da empresa simulada **SimProgramming**, no âmbito da unidade curricular Laboratório de Desenvolvimento de Software. O projeto está a ser desenvolvido em equipa, seguindo uma organização com papéis definidos, planeamento faseado e utilização de controlo de versões com GitHub.

---

## Estado atual

> **Fase em curso: Implementação** (abril 2026)

A fase de análise e arquitetura preliminar produziu o seguinte:

- Definição da arquitetura MVC e dos inputs/outputs do sistema
- Diagrama de sequência e diagrama de componentes
- Análise da API de negócio e proposta de aplicação-demonstradora
- Plano de verificações v2

A implementação da aplicação está em curso desde abril de 2026. A estrutura base está criada com a separação entre as três camadas MVC — Model, View e Controller — comunicando através de eventos e delegados, sem dependências diretas entre View e Model.

Foi também introduzida uma evolução arquitetónica para reduzir o acoplamento entre componentes: o `Controller` passou a depender dos contratos `IModel` e `IView`, em vez de depender diretamente das classes concretas `Model` e `View`. As classes concretas continuam a existir e implementam essas interfaces, mas são criadas no ponto de arranque da aplicação e injetadas no `Controller`.

O fluxo de autenticação já se encontra validado por testes automatizados. A aplicação permite também criar projetos a partir do menu principal, recolhendo nome, descrição, data de início e data de fim, com validação básica dos dados introduzidos. A listagem de tarefas por projeto inclui tratamento de erro para projetos inexistentes através da exceção `ProjectNotFoundException`, permitindo apresentar uma mensagem adequada ao utilizador, registar a ocorrência em log e mostrar a lista atualizada de projetos disponíveis.

---

## Tecnologias e ferramentas

| Tecnologia / Ferramenta | Utilização |
|---|---|
| C# / .NET | Linguagem e plataforma de desenvolvimento |
| Aplicação de consola | Interface com o utilizador |
| Visual Studio | Ambiente de desenvolvimento |
| GitHub | Controlo de versões e colaboração |
| Json.NET | Serialização/desserialização de dados JSON |
| PDFsharp | Geração de relatórios de projeto em PDF |
| Noto Sans | Fonte embutida usada nos relatórios PDF |

---

## Equipa

| Nome | Papel | N.º de aluno |
|---|---|---|
| Maria Sarabando | Líder de projeto | 2400294 |
| Cláudia Teixeira | Developer 1 | 2400262 |
| Joel Costa | Developer 2 | 2400125 |
| João Ferrão | Developer 3 | 2400245 |
| Marco Oliveira | Verificador | 2000537 |

---

## Organização do repositório

```
/
├── src/        → código-fonte da aplicação
├── tests/      → testes
└── docs/       → documentação do projeto
```

### Arquitetura MVC e interfaces

O projeto segue o padrão MVC:

- `View` recolhe dados do utilizador e apresenta resultados
- `Controller` coordena a comunicação entre View e Model
- `Model` contém a lógica de validação, autenticação e gestão de dados

Para reduzir dependências estruturais, foram criadas interfaces nas fronteiras entre os componentes:

- `IView` define o contrato usado pelo `Controller` para comunicar com a View
- `IModel` define o contrato usado pelo `Controller` para comunicar com o Model
- `IReportGenerator` define o contrato usado pelo `Controller` para gerar relatórios sem depender de uma implementação concreta de PDF

Assim, o `Controller` trabalha com:

```csharp
private readonly IModel model;
private readonly IView view;
private readonly IReportGenerator reportGenerator;
```

e recebe as dependências pelo construtor:

```csharp
public Controller(IModel model, IView view, IReportGenerator reportGenerator)
```

No arranque da aplicação, as implementações concretas são criadas e injetadas:

```csharp
IModel model = new Model();
IView view = new View();
IReportGenerator reportGenerator = new PdfReportGenerator();

Controller controller = new(model, view, reportGenerator);
```

Com esta abordagem, uma futura substituição da View de consola por outra implementação, ou do Model atual por outro mecanismo de dados, pode ser feita com menor impacto no `Controller`, desde que as novas classes respeitem os contratos definidos.

---

## Execução da aplicação

Para executar a aplicação principal, deve ser usado o projeto localizado em `src/Studio36`.

A partir da raiz do repositório:

```powershell
dotnet run --project .\src\Studio36\Studio36.csproj

```
---

## Testes automatizados

O projeto inclui uma aplicação de consola independente para execução de testes automatizados, localizada em tests/Studio36.Tests.

Estes testes validam o comportamento atualmente implementado na aplicação, nomeadamente a apresentação do menu inicial, a saída da aplicação, o fluxo básico de autenticação, o acesso à opção de registo, o tratamento de entradas inválidas, os fluxos MVC de criação, listagem, edição e eliminação de projetos, geração de relatórios, e o fluxo de erro na listagem de tarefas por projeto.

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

Os testes T26 e T27 validam especificamente a opção "List tasks by project" no menu principal. O T26 confirma que um `idProjeto` inexistente aciona o `ProjectNotFoundException`, apresenta a mensagem de erro, regista a ocorrência em log e mostra a lista atualizada de projetos. O T27 confirma que um ID não numérico é rejeitado com uma mensagem adequada, sem encerramento abrupto da aplicação.

Os testes T18, T19, T28, T29 e T30 validam o módulo inicial de projetos através da aplicação de consola. O T18 confirma que um projeto sem nome é rejeitado. O T19 confirma que uma data de fim anterior à data de início é rejeitada. O T28 valida o fluxo de criação de um projeto válido a partir do menu principal, confirmando que o novo projeto recebe um ID e fica disponível para operações seguintes, como a listagem de tarefas. O T29 confirma que a opção "List projects" apresenta a lista de projetos existentes. O T30 confirma que a opção "Edit project" atualiza os dados de um projeto existente.

Os testes T31 e T32 validam a evolução arquitetónica baseada em interfaces. O T31 confirma que o `Controller` depende de `IModel` e `IView`. O T32 usa implementações fake dessas interfaces para confirmar que o `Controller` consegue funcionar com outros objetos que cumpram os contratos, sem depender diretamente das classes concretas `Model` e `View`.

Os testes T33 e T34 validam fluxos MVC completos com View fake e Model real. O T33 confirma o percurso de criação de projeto: a View envia `CreateProjectRequestData`, o Controller encaminha o pedido, o Model guarda o projeto, devolve o resultado e a View recebe a mensagem final. O T34 confirma o percurso de listagem de projetos: a View pede a lista, o Controller consulta o Model e a View recebe a lista através de `ShowProjectList`.

Os testes T35, T36 e T37 validam o fluxo MVC de edição de projetos. O T35 confirma a edição válida de um projeto existente. O T36 confirma o tratamento de erro quando o `idProjeto` não existe. O T37 confirma que um projeto com nome vazio é rejeitado e que os dados originais permanecem inalterados.

O teste T38 valida o fluxo de eliminação de projeto através do menu da aplicação. Confirma que a opção "Delete project" pede o `idProjeto`, elimina o projeto existente, apresenta a mensagem de sucesso e que a listagem seguinte já não mostra o projeto removido.

Os testes T39, T40 e T41 validam o fluxo de geração de relatórios. O T39 confirma o percurso MVC completo com View fake, Model real e gerador de relatório fake. O T40 confirma que a opção "Generate report" gera um ficheiro PDF para um projeto existente. O T41 confirma que a tentativa de gerar relatório para um projeto inexistente apresenta erro adequado.

Os relatórios PDF são gerados com PDFsharp através do `PdfReportGenerator`. Para evitar dependência das fontes instaladas no sistema operativo, o projeto inclui a fonte Noto Sans em `src/Studio36/Assets/Fonts` e usa um `Studio36FontResolver`. Isto torna a geração de PDF mais previsível em Windows, macOS e Linux.

Nesta fase, os testes incidem apenas sobre funcionalidades já implementadas. Funcionalidades futuras, como gestão completa de tarefas, membros e persistência em JSON, deverão ser testadas quando a respetiva implementação estiver concluída.

## Modelo de desenvolvimento

O projeto utiliza uma estratégia de branching com três níveis:

- `main` → versão estável do projeto; só recebe alterações vindas de `develop`
- `develop` → branch de integração do desenvolvimento
- branches individuais ou de funcionalidade → desenvolvimento de tarefas específicas

### Fluxo de trabalho

```
branch individual / funcionalidade  →  develop  →  main
```

---

## Convenção de branches

Cada elemento da equipa tem uma **branch individual** para o seu trabalho contínuo. Quando uma tarefa é bem delimitada ou envolve mais do que um elemento, deve ser criada uma **branch de funcionalidade** (`feature/`) ou de **correção** (`fix/`).

| Tipo | Quando usar | Exemplos |
|---|---|---|
| Branch individual | Trabalho geral do elemento | `maria`, `claudia`, `joel`, `joao`, `marco` |
| `feature/` | Funcionalidade específica e delimitada | `feature/login`, `feature/dashboard`, `feature/relatorio-pdf` |
| `fix/` | Correção de um problema identificado | `fix/validacao-credenciais`, `fix/erro-listagem` |

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

## Regras de colaboração

### Desenvolvimento

- Nunca fazer push direto para `main` ou `develop`
- Cada elemento desenvolve o seu trabalho na respetiva branch individual ou numa branch de funcionalidade/correção
- Antes de abrir PR, garantir que a branch está atualizada em relação a `develop` para evitar conflitos

### Integração em `develop`

- As alterações concluídas são integradas em `develop` via **Pull Request**
- Em caso de conflitos, estes devem ser resolvidos na branch de origem antes de o PR ser aceite

### Integração em `main`

- Só após as alterações estarem testadas e validadas em `develop` é que seguem para `main`
- A integração em `main` deve ser articulada com a **líder de projeto** e com o **verificador**

---

## Validação antes de integrar

Antes de abrir Pull Request para `develop`, confirmar que:

- [ ] O código compila sem erros no Visual Studio ou através de `dotnet build`
- [ ] A aplicação executa corretamente através de `dotnet run`
- [ ] Os testes automatizados existentes passam com sucesso
- [ ] A funcionalidade implementada está de acordo com o planeado
- [ ] A branch está atualizada em relação a `develop` e não tem conflitos por resolver
- [ ] A alteração foi minimamente validada pela equipa

---

## Organização de tarefas

A distribuição de tarefas é definida pela equipa ao longo das diferentes fases do projeto, de acordo com o planeamento estabelecido.

- A **líder** coordena o trabalho, distribui tarefas e assegura a articulação entre os elementos
- Os **developers** executam as tarefas técnicas atribuídas em cada fase
- O **verificador** acompanha o trabalho realizado, efetua verificações parcelares e valida os resultados

---

## Notas

Este repositório será atualizado progressivamente à medida que o projeto evoluir, incluindo código, documentação técnica, planeamentos e outros artefactos relevantes.
