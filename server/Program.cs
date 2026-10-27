using CommBank.Configurations;

DotEnvLoader.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json", optional: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsPolicy();
builder.Services.AddDatabaseSetup(builder.Configuration);
builder.Services.AddDependencyInjection();

var app = builder.Build();

app.UseMiddlewarePipeline();

app.Run();
