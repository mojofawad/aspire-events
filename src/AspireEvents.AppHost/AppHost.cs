var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AspireEvents_Gateway>("api-gateway");

builder.AddProject<Projects.AspireEvents_Bookings>("bookings-api");
builder.AddProject<Projects.AspireEvents_Events>("events-api");
builder.AddProject<Projects.AspireEvents_Notifications>("notifications-api");
builder.AddProject<Projects.AspireEvents_Payments>("payments-api");
builder.AddProject<Projects.AspireEvents_Users>("users-api");

builder.AddNpmApp("web-app", "../aspire-events-web", "dev")
    .WithExternalHttpEndpoints()
    .WithHttpEndpoint(env: "PORT");

builder.Build().Run();