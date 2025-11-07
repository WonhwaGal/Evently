IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

/*
 evently.database:
    #image: mcr.microsoft.com/mssql/server:latest
    image: mcr.microsoft.com/mssql/server:2022-CU16-ubuntu-22.04
    container_name: Evently.Database
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=1234512345Aa$
      - MSSQL_PID=Express
    volumes:
      - ./.containers/database:/var/opt/mssql/data
    ports:
      - "1433:1433"
 */
IResourceBuilder<ParameterResource> eventlyDbPassword = builder.AddParameter(
    name: "evently-db-password",
    value: "1234512345Aa$",
    secret: true);

IResourceBuilder<SqlServerServerResource> eventlySql = builder.AddSqlServer("evently-dbms", eventlyDbPassword)
    .WithImage("mssql/server:2022-CU16-ubuntu-22.04")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithEnvironment("MSSQL_PID", "Express")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("evently-db");
    //.WithBindMount("./.containers/database", "/var/opt/mssql/data")

   IResourceBuilder<SqlServerDatabaseResource> eventlyDb = 
       eventlySql.AddDatabase("Database", "evently");

/*
  evently.redis:
    image: redis:latest
    container_name: Evently.Redis
    restart: no
    ports:
      - '6379:6379'
 */
   IResourceBuilder<RedisResource> eventlyCache =  builder.AddRedis(name:
           "Cache")
       .WithImage("redis:latest")
       .WithLifetime(ContainerLifetime.Persistent)
       .WithRedisInsight()
       .WithRedisCommander();

/*
  evently.seq:
    image: datalust/seq:latest
    container_name: Evently.Seq
    environment:
      - ACCEPT_EULA=Y
    ports:
      - "5341:5341"
      - "8180:80" # for monitoring
 */
   IResourceBuilder<SeqResource> eventlySeq = builder.AddSeq("evently-seq")
       .WithImage("datalust/seq:latest")
       .WithLifetime(ContainerLifetime.Persistent)
       .WithEnvironment("ACCEPT_EULA", "Y")
       .WithExternalHttpEndpoints();

/*
  evently.api:
    image: evently.api
    container_name: Evently.Api
    depends_on:
      - evently.database
    build:
      context: .
      dockerfile: src/API/Evently.Api/Dockerfile
    volumes:
      - ./.logs/Evently/Dev.Logs:/logs
    ports:
      - "5100:8080"
      - "5101:8081"
 */
   builder.AddProject<Projects.Evently_Api>(
           name: "evently-api")
       .WaitFor(eventlyDb)
       .WaitFor(eventlyCache)
       .WaitFor(eventlySeq)
       .WithReference(eventlyDb)
       .WithReference(eventlyCache)
       .WithReference(eventlySeq);

/*
  evently.ticketing.api:
    image: eventlyticketing.api
    container_name: Evently.Ticketing.Api
    build:
      context: .
      dockerfile: src/API/Evently.Ticketing.Api/Dockerfile
    ports:
      - "5200:8080"
      - "5201:8081"
 */
    builder.AddProject<Projects.Evently_Ticketing_Api>(
            name: "evently-ticketing-api")
        .WaitFor(eventlyDb)
        .WaitFor(eventlyCache)
        .WaitFor(eventlySeq)
        .WithReference(eventlyDb)
        .WithReference(eventlyCache)
        .WithReference(eventlySeq);

await builder.Build().RunAsync();
