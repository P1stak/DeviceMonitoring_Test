using DeviceMonitoring.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls("http://localhost:5223", "https://localhost:7086");

// Контроллеры
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Device API",
        Version = "v1",
        Description = "API для управления устройствами"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {

        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



// Репозиторий и логирование
builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Запрос: {context.Request.Method} {context.Request.Path}");
    await next();
});


// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();