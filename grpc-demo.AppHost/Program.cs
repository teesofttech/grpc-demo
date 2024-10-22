var builder = DistributedApplication.CreateBuilder(args);

var shoppingcartDb = builder.AddPostgres("shoppingcart-db")
    .WithDataVolume()
    .WithPgAdmin();

builder.AddProject<Projects.Discount_Grpc>("discount-grpc");

builder.AddProject<Projects.ShoppingCart_Api>("shoppingcart-api")
    .WithReference(shoppingcartDb);

builder.Build().Run();
