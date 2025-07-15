using Serilog;
using UserPortal.Api;
using UserPortal.Api.Middlewares;


var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    // Register application and infrastructure dependencies
    builder.Services.AddApiDI(builder.Configuration);
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    // Serilog Configuration
    builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .ReadFrom.Configuration(context.Configuration)  // read settings from appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()                               // logs to console window
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // logs to daily rolling file
);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler(_ => { });

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}


