# REST Datadriven Shop

Simples API para estudo

Essa API foi montada para estudo. 

O que foi abordado nos estudos dessa API:

* [Authorization](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/introduction?view=aspnetcore-5.0)
* [Role base Authorization](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-5.0)
* [EFCore Data Annotations](https://entityframeworkcore.com/model-data-annotations)
* [FluentMigrator](https://fluentmigrator.github.io/)

Ao executar a API pela primeira vez, se estiver usando o provider do SQLSERVER o sistema irá automáticamente
executar as migrations e o seed usando o fluentmigrator.

O swagger está no endpoint /swagger

Algumas urls estão protegidas, é necessário fazer login e usar o token nesse caso:


/users/login

```json
{
  "username": "adm",
  "password": "123",
  "role":"manager"
}
```

O login irá retornar um token, para usar no swagger clique no botão `Authorization` e coloque o "bearer  `token`" ´(substitua o token):

Exemplo:

`bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbSIsImlkIjoiMSIsInJvbGUiOiJtYW5hZ2VyIiwibmJmIjoxNjA5MTI1MDI4LCJleHAiOjE2MDkxMzIyMjgsImlhdCI6MTYwOTEyNTAyOH0.Xw8lI9EmfQCU5xyvjgWIhSlCJsbzaiTqBQ5xZetfAf0`

O admin padrão configurado é `admin`  senha `123`

Produtos:
  * `Torcida 200g`
  * `Detergente`


## Configuração 

Para execução local do projeto é necessário configurar as variáveis:

DATABASE_PROVIDER :
* INMEMORY ou 
* SQLSERVER

Exemplo:

appsettings.json

```json
{
  "DATABASE_PROVIDER": "SQLSERVER",
  "ConnectionStrings": {
    "connectionString": "Server=localhost;Database=master;User Id=SA;Password=1q2w3e4r@#$;"
  }
}
```


Se estiver usando o SQL Server, configure a variável `connectionString` com os valores do banco de dados que deseja utilizar.

## Docker Compose

Se preferir executar o docker compose:

`docker-compose up`

## Docker

Se quiser montar a imagem docker e executar

Na pasta do dockerfile execute o comando: 

`docker build -t shop:latest .`

Para subir o serviзo:

- Usando o inMemory provider:

`docker run -p 8080:80 -e DATABASE_PROVIDER='INMEMORY' --name shop shop:latest`

- Usando o SQLServer :

`docker run -p 8080:80 -e DATABASE_PROVIDER='SQLSERVER' -e ConnectionStrings__connectionString="SUA CONNECTION STRING" --name shop shop:latest`






