var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.Discount_Grpc>("discount-grpc");

builder.AddProject<Projects.ShoppingCart_Api>("shoppingcart-api");

builder.Build().Run();
