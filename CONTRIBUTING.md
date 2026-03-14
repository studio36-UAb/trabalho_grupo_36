# Contributing
Este documento define as regras básicas de colaboração no projeto.

## Branches
O repositório utiliza as seguintes branches principais:

- `main` – versão final e estável do projeto
- `develop` – branch de integração e testes

Além destas, cada elemento do grupo deve trabalhar na sua **branch individual**.

## Forma de trabalho
O trabalho deve seguir, sempre que possível, este fluxo:

1. Atualizar a branch `develop`
2. Atualizar a branch individual
3. Desenvolver e testar as alterações na branch individual, no Visual Studio
4. Fazer commit na branch individual
5. Enviar a branch para o repositório remoto
6. Integrar as alterações na `develop`
7. Depois de testadas e validadas na `develop`, integrar na `main`

## Branches individuais
Cada elemento deve trabalhar na sua própria branch.

- `claudia`
- `joao`
- `joel`
- `maria`
- `marco`

Cada elemento deve trabalhar na sua própria branch, que deve ser criada a partir da branch `develop`.
Se quiserem optar por outro nome convém que seja claro e identificável como pertencente a cada elemento do grupo.

## Commits
As mensagens de commit devem ser claras e descritivas.


## Pull Requests
Antes de integrar alterações na `develop`, deve confirmar-se que:

- o projeto compila corretamente
- as alterações foram testadas
- não existem conflitos com a `develop`
- a descrição do Pull Request é clara

## Regras gerais
- Não devem ser feitas alterações diretamente na branch **main**.
- Cada elemento deve desenvolver o seu trabalho na respetiva **branch individual**.
- As alterações concluídas devem ser integradas na branch **develop**.
- Antes de integrar alterações, deve verificar-se se o projeto compila e funciona corretamente no **Visual Studio**.
- Depois de testadas e validadas na **develop**, as alterações poderão ser integradas na **main**.

## Organização de tarefas

As tarefas poderão ser geridas através das **Issues** do GitHub, caso o grupo considere útil.
