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

### Executar todos os testes automatizados

```powershell
dotnet run --project .\tests\Studio36.Tests\Studio36.Tests.csproj
```

### Executar um teste específico

Quando necessário, pode ser executado apenas um teste através do respetivo identificador.

Exemplo:

```powershell
dotnet run --project .\tests\Studio36.Tests\Studio36.Tests.csproj -- T01
```

### Critério mínimo antes de abrir Pull Request

Antes de abrir Pull Request para `develop`, confirmar que:

- [ ] O projeto compila sem erros
- [ ] A aplicação executa corretamente
- [ ] Os testes automatizados existentes passam com sucesso
- [ ] A alteração realizada não quebra funcionalidades já implementadas
- [ ] O código alterado respeita a arquitetura MVC definida para o projeto
- [ ] A branch está atualizada em relação a `develop`

Caso algum teste falhe, a causa deve ser analisada e corrigida antes da submissão do Pull Request. Se a falha estiver relacionada com uma alteração intencional do comportamento da aplicação, o respetivo teste deve ser atualizado de forma coerente com a nova implementação.

---

## Regras gerais

- Nunca fazer push direto para `main`
- Cada elemento desenvolve o seu trabalho na respetiva branch individual ou de funcionalidade
- A integração em `main` deve ser sempre articulada com a líder de projeto e o verificador

---

## Arquitetura MVC - regras de separação de camadas

O projeto sgue o padrão MVC. Para manter o acoplamento baixo devem ser respeitadas as seguinte regras:

- A **View não pode depender do Model** - não deve importar o namespace `ModelComponent` nem receber instâncias do Model
- O **Model não pode depender da View** - não deve importar o namespace `ViewComponent`
- Toda a comunicação entre Model e View é feita **exclusivamente através de eventos**, com os parâmetros necessarios incluidos no proprio evento
- O **Controller é o único intermediário** - subscreve os eventos do Model e chama os métodos públicos da View para atualizar o ecrã

### Exemplo correto de eventos com dados

```csharp
//Model - publica estado diretamente no evento
public event Action<bool>? SendLoginState;
SendLoginState?.Invoke(IsLoggedIn);

//View - recebe dados como parâmetro, sem aceder ao Model
public void ShowLoginResult(bool isLoggedIn) { ... }

//Controller - faz a ligação
model.SendLoginState += isLoggedIn => view.ShowLoginResult(isLoggedIn);
```