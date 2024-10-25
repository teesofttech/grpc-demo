var builder = DistributedApplication.CreateBuilder(args);

var shoppingcartDb = builder.AddPostgres("shoppingcart-db")
    .WithDataVolume()
    .WithPgAdmin();

var shoppingCartCache = builder.AddRedis("shoppingcart-cache")
    .WithDataVolume()
    .WithRedisCommander();

var disocuntGrpc = builder.AddProject<Projects.Discount_Grpc>("discount-grpc");

builder.AddProject<Projects.ShoppingCart_Api>("shoppingcart-api")
    .WithReference(shoppingcartDb)
    .WithReference(shoppingCartCache);

builder.Build().Run();
