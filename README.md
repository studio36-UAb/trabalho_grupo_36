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

A fase de análise e arquitetura preliminar foi concluída em março de 2026, tendo produzido:

- Definição da arquitetura MVC e dos inputs/outputs do sistema
- Diagrama de sequência e diagrama de componentes
- Análise da API de negócio e proposta de aplicação-demonstradora
- Plano de verificações v2

A implementação da aplicação está em curso desde abril de 2026. A estrutura base do projeto está criada com a separação entre as três camadas MVC — Model, View e Controller — comunicando exclusivamente através de eventos e delegados, sem dependências diretas entre View e Model.

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

- [ ] O código compila sem erros no Visual Studio
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
