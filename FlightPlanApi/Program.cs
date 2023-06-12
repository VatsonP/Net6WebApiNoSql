using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FlightPlanApi.Authentication;
using FlightPlanApi.Data;
using System.ComponentModel;
using Amazon.Runtime.Internal.Transform;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("flightplan", new OpenApiInfo
    {
        Title = "Flight Plan API",
        Version = "v3",
        Description = "My Web Demo FlightPlan Project"
    });

    options.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using Bearer scheme (for My Web Demo FlightPlan Project)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            { 
                Reference = new OpenApiReference
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "basicAuth"
                }
            },
            new string[] {}
        }
    });

    options.EnableAnnotations();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
    ("BasicAuthentication", null);

builder.Services.AddScoped<IDatabaseAdapter, MongoDbDatabase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/flightplan/swagger.json",
                                                       "Flight Plan API"));
}
app.UseCors(config =>
{
    config
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
