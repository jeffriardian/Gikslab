using Gikslab.API.Extensions;
using Gikslab.Repository.Data;
using Gikslab.Service.Services;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
LoggerManager logger = new LoggerManager();

builder.Services.ConfigureResponseCaching();

// Add services to the container.
builder.Services.RegisterDependencies();
builder.Services.ConfigureMapping();
builder.Services.ConfigureLoggerService();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);


builder.Services.ConfigureControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    UserRoleData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();