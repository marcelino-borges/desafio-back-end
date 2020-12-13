# Desafio Backend ConexaLabs 2020

Quer fazer parte da transformação do campo ~~escrevendo~~ codando o futuro do agronegócio?

Se deseja participar do nosso processo seletivo, siga as instruções deste desafio e execute os seguintes passos: 

* Nos mande sua resolução em um *pull request* neste repositório.

* Deixe a aplicação disponível publicamente em imagem docker em qualquer host. Na descrição do PR passe o link para que consigamos usar sua imagem.

* Por último, **caso ainda não esteja cadastrado no processo seletivo**, envie um email para [renatto.machado@hubconexa.com](mailto:renatto.machado@hubconexa.com) com seu CV anexado e o link da aplicação (se já estiver no processo seletivo, não precisa);

  

# Sobre a Conexa

A [Conexa](http://hubconexa.com/) é um hub de inovação que vive o agronegócio e é protagonista em sua transformação e unimos pessoas que compartilham a crença de que o mundo pode ser mais sustentável e que o trabalho pode ser mais prazeroso.

A equipe da Conexa Labs tem o propósito de tornar o agro mais simples, usando o que há de mais avançado em tecnologia para construir produtos e ferramentas que conectam pessoas e negócios aos resultados desejados.



# O desafio

Crie um microsserviço capaz de aceitar solicitações RESTful que recebam como parâmetro o nome da cidade ou as coordenadas (*latitude e longitude*) e retorne uma sugestão de playlist (*apenas nomes da músicas*) de acordo com a temperatura atual.



## Requisitos

1. Se a temperatura (*celsius*) estiver acima de 30 graus, as músicas sugeridas serão para festas
2. Caso a temperatura esteja entre 15 e 30 graus, sugira faixas de música pop
3. Se estiver um pouco frio (entre 10 e 14 graus), sugira faixas de rock
4. Caso contrário, se estiver frio lá fora, proponha músicas clássicas

## Requisitos não funcionais

- Como este serviço será um sucesso mundial, ele deve estar preparado para ser tolerante a falhas, responsivo e resiliente.

- Utilize a linguagem C# .Net. Use qualquer ferramenta e estrutura com as quais se sinta confortável e elabore brevemente sua solução, detalhes de arquitetura, escolha de padrões e estruturas.

- Além disso, facilite a implantação/execução de seus serviços localmente (*considere usar alguma solução de container/VM para isso*). 

## Dicas

Você pode usar a API do *[OpenWeatherMaps](https://openweathermap.org)* para buscar dados de temperatura e o *[Spotify](https://developer.spotify.com/)* para sugerir as músicas da playlist.


## Recomendações

* Utilize C#;
* Utilize .NET Core 3.1;
* Utilize docker;
* Utilize boas práticas de codificação, isso será avaliado;
* Mostre que você manja dos paranauê do C#;
* Código limpo, organizado e documentado (quando necessário);
* Use e abuse de:
  * SOLID;
  * Criatividade;
  * Performance;
  * Manutenabilidade;
  * Testes Unitários (quando necessário)
  * ... pois avaliaremos tudo isso!


# Resolução do Desafio

### Containerização 

Projeto usando sistema de container do Docker. Para levantar o ambiente, executar no prompt:
- Dentro do diretório do projeto (ApiPlaylistOnLocation), executar no prompt: 
  1.1. `docker build -t api:1.0`
  1.2. `docker container run -p 5000:80 api:1.0`
  1.3. Rodar no navegador: `localhost:5000/api/v1/recife` 
- [Imagem Docker da aplicação no Docker Hub](https://hub.docker.com/r/marcelinoborges/api-playlist-by-location-temperature)

# Documentação da API

**ENDPOINTS**:

1. GET (Por nome de cidade):
   - Caminho: `/api/v1/{nome-da-cidade}`
   - Retornos:
	 - 500: Erro interno do servidor + JSON com detalhes da mensagem de erro.
	 - 400: Cidade ou coordenada não encontrada + JSON com detalhes da mensagem de erro.
	 - 200: Sucesso na requisição + JSON com playlist.

2. GET (Por coordenadas): 
   - Caminho: `/api/v1/lat={latitude}&lon={longitude}`
   - Retornos:
	 - 500: Erro interno do servidor + JSON com detalhes da mensagem de erro.
	 - 400: Cidade ou coordenada não encontrada + JSON com detalhes da mensagem de erro.
	 - 200: Sucesso na requisição + JSON com playlist.