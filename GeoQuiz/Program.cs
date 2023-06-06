using GeoQuiz;
using GeoQuiz.Services;
using GeoQuiz.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Cors;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddDbContext<GeoQuizContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GeoQuizContext")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILandmarkService, LandmarkService>();
builder.Services.AddScoped<IWinnerChecker, WinnerChecker>();
builder.Services.AddSignalR();



var app = builder.Build();
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/landmarks"
});


app.MapControllers();

app.MapHub<LobbyHub>("/lobby");

app.Run();