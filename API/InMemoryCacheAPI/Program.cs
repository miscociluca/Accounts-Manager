using AutoMapper;
using AccountManagement.dto;
using AccountManagement.Models;
using AccountManagement.Services.Interfaces;
using AccountManagement.Services;
using System.Configuration;
using AccountManagement;
using AccountManagement.Helpers;
using Blue_Harvest_Redis.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAutoMapper(config => config.AddProfile(typeof(MappingProfiles)));
builder.Services.AddDataInfrastructure(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<InitializeCacheService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();
