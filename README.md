# GameServiceAPI

Uma Web API simples em ASP.NET Core que integra com a Free-To-Play Games Database ([https://www.freetogame.com/api-doc](https://www.freetogame.com/api-doc)) para recomendar jogos ao usuário com base em critérios como gêneros, plataforma e memória RAM disponível.

## Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Acesso à Internet para chamar a API externa FreeToGame

## Configuração

1. Abra o arquivo `appsettings.json` na raiz do projeto.
2. Preencha a seção `FreeToGameApi` com a URL base da API:

```json
{
  "FreeToGameApi": {
    "BaseUrl": "https://www.freetogame.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Como executar

No terminal, na raiz do projeto:

```bash
# Restaura pacotes
dotnet restore

# Compila e executa a API
dotnet run --project GameServiceAPI
```

A API subirá em `https://localhost:5001` (por padrão). Você pode acessar a documentação interativa via Swagger em:

```
https://localhost:5001/swagger
```

## Endpoints

### GET `/api/games/compatible`

Retorna um jogo recomendado aleatoriamente que atenda aos filtros especificados.

#### Query Parameters

| Nome       | Tipo      | Obrigatório | Descrição                                                                               |
| ---------- | --------- | ----------- | --------------------------------------------------------------------------------------- |
| `genres`   | string\[] | Sim         | Um ou mais gêneros (por exemplo: `Shooter`, `MMORPG`). Pode ser repetido para combinar. |
| `platform` | string    | Não         | Plataforma: `pc`, `browser` ou `all` (padrão: `all`).                                   |
| `memory`   | int       | Sim         | Quantidade de memória RAM disponível, em GB (deve ser > 0).                             |

#### Respostas

* **200 OK**

  ```json
  {
    "title": "Overwatch 2",
    "gameUrl": "https://www.freetogame.com/open/overwatch-2"
  }
  ```
* **400 Bad Request**
  Se `genres` não for informado ou `memory` for ≤ 0.
* **404 Not Found**
  Se nenhum jogo satisfizer os filtros. Retorna mensagem:

  ```
  Nenhum jogo encontrado com esses filtros. Tente alterar os critérios.
  ```

#### Exemplos de requisição

```http
GET https://localhost:5001/api/games/compatible?genres=Shooter&platform=pc&memory=8
```

```http
GET https://localhost:5001/api/games/compatible?genres=Strategy&genres=Adventure&platform=all&memory=4
```

## Estrutura do Projeto

```
GameServiceAPI/
├── Clients/                 # HTTP Client tipado para FreeToGame
│   ├ IFreeToGameClient.cs
│   └ FreeToGameClient.cs
│
├── Configuration/           # Classes de configuração (Options)
│   └ FreeToGameOptions.cs
│
├── Dtos/                    # Modelos de transferência de dados
│   ├ GameBasic.cs
│   ├ GameDetail.cs
│   └ RecommendedGameDto.cs
│
├── Services/                # Lógica de negócio e filtros
│   └ GamesService.cs
│
├── Controllers/             # Endpoints REST
│   └ GamesController.cs
│
├── Program.cs               # Configuração de DI, HttpClient e pipeline
└── appsettings.json         # Configurações de ambiente
```

## Melhorias Futuras

* Implementar cache (`IMemoryCache` ou Redis) para reduzir chamadas à API externa.
* Adicionar testes unitários para `GamesService` e testes de integração para controllers.
* Extrair domínio e aplicação para projetos separados (Domain, Application) conforme Clean Architecture.

## Licença

Projeto licenciado sob a [MIT License](LICENSE).
