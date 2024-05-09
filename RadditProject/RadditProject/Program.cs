using Microsoft.Extensions.Configuration;
using RadditProject.Services.Classes;
using RadditProject.Services.Interfaces;
using RadditProject.Services.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Singleton
builder.Services.Add(new ServiceDescriptor(typeof(ITokenService), new TokenService()));
//new instance for each scope
builder.Services.Add(new ServiceDescriptor(typeof(ISubRedditService), typeof(SubRedditService), ServiceLifetime.Scoped));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
