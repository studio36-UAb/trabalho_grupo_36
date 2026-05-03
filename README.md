# Studio 36 — Grupo 36

Repositório do projeto de grupo da unidade curricular **21179 - Laboratório de Desenvolvimento de Software**, da Universidade Aberta (2025/2026).

---

## Sobre o projeto

O Studio 36 é uma aplicação de consola desenvolvida em C# para **gestão de projetos e tarefas**. Permite autenticar utilizadores, gerir projetos, tarefas e membros de equipa, e gerar relatórios em formato PDF. A aplicação segue o padrão arquitetónico **MVC (Model-View-Controller)** e utiliza o **Json.NET** para comunicação entre camadas e o **PDFsharp** para geração de relatórios.

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

A implementação da aplicação está em curso desde abril de 2026. A estrutura base está criada com a separação entre as três camadas MVC — Model, View e Controller — comunicando exclusivamente através de eventos e delegados, sem dependências diretas entre View e Model.

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
| PDFsharp | Geração de relatórios em PDF |

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

Estes testes validam o comportamento atualmente implementado na aplicação, nomeadamente a apresentação do menu inicial, a saída da aplicação, o fluxo básico de autenticação, o acesso à opção de registo, o tratamento de entradas inválidas, a criação de projetos e o fluxo de erro na listagem de tarefas por projeto.

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

Testes atualmente disponíveis
ID	Descrição
T01	Apresentação do menu inicial
T02	Terminar aplicação pela opção 3
T03	Login com credenciais válidas
T04	Login com credenciais inválidas
T05	Acesso à opção de registo
T06	Opção inválida no menu
T07	Input não numérico no menu
T08	Input vazio no menu
T09	Model valida credenciais válidas
T10	Model rejeita credenciais inválidas
T18	Criar projeto sem nome
T19	Criar projeto com datas inválidas
T26	Listar tarefas de projeto inexistente
T27	Introduzir ID de projeto não numérico na listagem de tarefas
T28	Criar projeto válido
T29	Listar projetos
T30	Editar projeto válido

Ao executar um teste com sucesso, deverá ser apresentada uma mensagem de aprovação correspondente ao respetivo identificador.

Os testes T26 e T27 validam especificamente a opção "List tasks by project" no menu principal. O T26 confirma que um `idProjeto` inexistente aciona o `ProjectNotFoundException`, apresenta a mensagem de erro, regista a ocorrência em log e mostra a lista atualizada de projetos. O T27 confirma que um ID não numérico é rejeitado com uma mensagem adequada, sem encerramento abrupto da aplicação.

Os testes T18, T19, T28, T29 e T30 validam o módulo inicial de projetos. O T18 confirma que um projeto sem nome é rejeitado. O T19 confirma que uma data de fim anterior à data de início é rejeitada. O T28 valida o fluxo de criação de um projeto válido a partir do menu principal, confirmando que o novo projeto recebe um ID e fica disponível para operações seguintes, como a listagem de tarefas. O T29 confirma que a opção "List projects" apresenta a lista de projetos existentes. O T30 confirma que a opção "Edit project" atualiza os dados de um projeto existente.

Nesta fase, os testes incidem apenas sobre funcionalidades já implementadas. Funcionalidades futuras, como eliminação de projetos, gestão completa de tarefas, membros, persistência em JSON e geração de relatórios PDF, deverão ser testadas quando a respetiva implementação estiver concluída.

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
