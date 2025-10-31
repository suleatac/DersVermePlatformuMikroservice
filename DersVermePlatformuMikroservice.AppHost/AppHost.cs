
var builder = DistributedApplication.CreateBuilder(args);

#region Keycloak-PostgreSQL
// Parametreler (CI/.env üzerinden beslenebilir)
var keycloakAdmin = builder.AddParameter("KEYCLOAK-ADMIN");
var keycloakAdminPassword = builder.AddParameter("KEYCLOAK-ADMIN-PASSWORD");
var postgresDbName = builder.AddParameter("POSTGRES-DB");
var postgresUser = builder.AddParameter("POSTGRES-USER");
var postgresPassword = builder.AddParameter("POSTGRES-PASSWORD");

// PostgreSQL instance used by Keycloak
var keycloakPostgres = builder
    .AddPostgres("postgres-db-keycloak", postgresUser, postgresPassword, 5432)
    .WithImage("postgres", "16.2")
    .WithDataVolume("postgres.db.keycloak.volume")
    .WithEnvironment("POSTGRES_DB", postgresDbName)
    .WithEnvironment("POSTGRES_USER", postgresUser)
    .WithEnvironment("POSTGRES_PASSWORD", postgresPassword)
    .AddDatabase("keycloak-db");


// Keycloak container'ýný ekleyin ve PostgreSQL ile uyumlu hale getirin
// Keycloak container
var keycloak = builder
    .AddContainer("keycloak", "quay.io/keycloak/keycloak:25.0")
    .WithEnvironment("KC_HOSTNAME_PORT", "8080")
    .WithEnvironment("KC_HOSTNAME_STRICT_BACKCHANNEL", "false")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_HOSTNAME_STRICT_HTTPS", "false")
    .WithEnvironment("KC_HOSTNAME_STRICT_HTTP", "false")
    .WithEnvironment("KC_HEALTH_ENABLED", "true")
    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KEYCLOAK_ADMIN", keycloakAdmin)
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", keycloakAdminPassword)

    // point Keycloak to the Postgres container (use service name from keycloakPostgres)
    .WithEnvironment("KC_DB_URL", $"jdbc:postgresql://postgres-db-keycloak/{postgresDbName}")
    .WithEnvironment("KC_DB_USERNAME", postgresUser)
    .WithEnvironment("KC_DB_PASSWORD", postgresPassword)
    .WithArgs("start-dev")
    .WaitFor(keycloakPostgres)
    .WithHttpEndpoint(8080, 8080, "keycloak-http-endpoint")
    ;

// A lightweight endpoint reference to use from APIs (used below with WithReference)
var keycloakEndpoint = keycloak.GetEndpoint("keycloak-http-endpoint");
#endregion

#region RabbitMQ
var rabbitMqUsername = builder.AddParameter("RABBITMQ-USERNAME");
var rabbitMqPassword = builder.AddParameter("RABBITMQ-PASSWORD");
var rabbitMq = builder.AddRabbitMQ("rabbitMQ", rabbitMqUsername, rabbitMqPassword, 5672)
   .WithManagementPlugin(15672);
#endregion

#region Catalog-API
var mongoUsername = builder.AddParameter("MONGO-USERNAME");
var mongoPassword = builder.AddParameter("MONGO-PASSWORD");

var catalogMongoDb = builder.AddMongoDB("mongo-db-catalog", 27030, mongoUsername, mongoPassword)
    .WithImage("mongo:8.0-rc")
    .WithDataVolume("mongo.db.catalog.volume")
    .AddDatabase("catalog-db");
var catalogApi = builder.AddProject<Projects.Microservice_Catalog_Api>("microservice-catalog-api");

catalogApi.WithReference(catalogMongoDb).WaitFor(catalogMongoDb).WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Basket-API
var basketRedisPassword = builder.AddParameter("REDIS-PASSWORD");

var basketRedisDb = builder.AddRedis("redis-db-basket", 6379)
    .WithImage("redis:7.0-alpine")
    .WithDataVolume("redis.db.basket.volume")
    .WithPassword(basketRedisPassword);
var redisBasketApi = builder.AddProject<Projects.Microservice_Basket_Api>("microservice-basket-api");

redisBasketApi.WithReference(basketRedisDb).WaitFor(basketRedisDb).WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Discount-API

var discountMongoDb = builder.AddMongoDB("mongo-db-discount", 27034, mongoUsername, mongoPassword)
    .WithImage("mongo:8.0-rc")
    .WithDataVolume("mongo.db.discount.volume")
    .AddDatabase("discount-db");
var discountApi = builder.AddProject<Projects.Mikroservice_Discount_Api>("mikroservice-discount-api");

discountApi.WithReference(discountMongoDb).WaitFor(discountMongoDb).WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region File-API
var fileApi = builder.AddProject<Projects.Microservice_File_Api>("microservice-file-api").WithReference(rabbitMq).WaitFor(rabbitMq);
#endregion

#region Payment-API
var paymentApi = builder.AddProject<Projects.Microservice_Payment_Api>("microservice-payment-api")
    .WithReference(rabbitMq).WaitFor(rabbitMq).WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Order-API

var orderSQLServerPassword = builder.AddParameter("SQLSERVER-SA-PASSWORD");

var sqlserverOrderDb = builder.AddSqlServer("sqlserver-db-order")
    .WithPassword(orderSQLServerPassword)
    .WithEnvironment("ACCEPT_EULA", "Y") // EULA'yý kabul et
    .WithEnvironment("MSSQL_PID", "Developer")
    //.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
    .WithDataVolume("sqlserver.db.order.volume")
    .AddDatabase("order-db-aspire");



    var orderApi = builder.AddProject<Projects.Mikroservice_Order_Api>("mikroservice-order-api");
orderApi.WithReference(sqlserverOrderDb).WaitFor(sqlserverOrderDb)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Gateway-API
builder.AddProject<Projects.Microservice_Gateway>("microservice-gateway").WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Web-API
var web = builder.AddProject<Projects.Mikroservice_web>("microservice-web");
web.WithReference(catalogApi).WithReference(redisBasketApi).WithReference(discountApi)
    .WithReference(orderApi).WithReference(fileApi).WithReference(paymentApi)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion



builder.Build().Run();
