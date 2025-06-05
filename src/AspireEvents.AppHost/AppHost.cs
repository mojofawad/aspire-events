var builder = DistributedApplication.CreateBuilder(args);

var localDbPassword = builder.AddParameter("local-db-password", "l0cal-Dev-p@ssword");

var sqlServer = builder.AddSqlServer("sql-server", password: localDbPassword);

var bookingsDb = sqlServer.AddDatabase("bookings-db", "bookings-db");
var eventsDb = sqlServer.AddDatabase("events-db", "events-db");
var notificationsDb = sqlServer.AddDatabase("notifications-db", "notifications-db");
var paymentsDb = sqlServer.AddDatabase("payments-db", "payments-db");
var usersDb = sqlServer.AddDatabase("users-db", "users-db");

var gateway = builder.AddProject<Projects.AspireEvents_Gateway>("api-gateway");

builder.AddProject<Projects.AspireEvents_Bookings>("bookings-api")
    .WithReference(bookingsDb);

builder.AddProject<Projects.AspireEvents_Events>("events-api")
    .WithReference(eventsDb);

builder.AddProject<Projects.AspireEvents_Notifications>("notifications-api")
    .WithReference(notificationsDb);

builder.AddProject<Projects.AspireEvents_Payments>("payments-api")
    .WithReference(paymentsDb);

builder.AddProject<Projects.AspireEvents_Users>("users-api")
    .WithReference(usersDb);

builder.AddNpmApp("web-app", "../aspire-events-web", "dev")
    .WithExternalHttpEndpoints()
    .WithHttpEndpoint(env: "PORT")
    .WithReference(gateway);

builder.Build().Run();