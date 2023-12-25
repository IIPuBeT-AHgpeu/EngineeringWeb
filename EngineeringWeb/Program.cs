using EngineeringWeb.Services;
using EngineeringWeb.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mydocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/EngineringWebSettings";

builder.Services.AddTransient<ISettingsManager, SettingsManager>(sm => new SettingsManager(mydocumentsPath));
builder.Services.AddTransient<IDataManager, DataManager>();
builder.Services.AddTransient<ISimpleLogger, SimpleLogger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
