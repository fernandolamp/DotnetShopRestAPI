# REST Datadriven Shop

Simples API para estudo

## Configura��o 

Na execu��o local do projeto configure as vari�veis:

* DATABASE_PROVIDER : INMEMORY ou SQLSERVER

Se estiver usando o SQL Server, configure a vari�vel `connectionString` com os valores do banco de dados que seja utilizar.

## Docker

Se quiser montar a imagem docker e executar

Na pasta do dockerfile execute o comando:

`docker build -t shop:latest .`

Para subir o servi�o:

- Usando o inMemory provider:

`docker run -p 8080:80 -e DATABASE_PROVIDER='INMEMORY' --name shop shop:latest`

- Usando o SQLServer :

`docker run -p 8080:80 -e DATABASE_PROVIDER='SQLSERVER' -e ConnectionStrings__connectionString="SUA CONNECTION STRING" --name shop shop:latest`




