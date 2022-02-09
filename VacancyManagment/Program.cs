using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using VacancyManagment.DTO;
using VacancyManagment.Models;

var builder = WebApplication.CreateBuilder(args);
//var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VacancyCadidatesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VacancyCadidates"));



});

var app = builder.Build();


app.MapGet("/vacancies", async ([FromServices] VacancyCadidatesContext context) =>
    await context.Vacancies.Select(x => new VacanciesDTO(x)).ToListAsync());


app.MapPost("/vacancies",
async([FromBody] VacanciesDTO vacancydto,[FromServices] VacancyCadidatesContext context, HttpResponse response) =>
{
    var vacancy = new Vacancy
    {
        Vacancy1 = vacancydto.Vacancy1,
        Description = vacancydto.Description,
        IsActive = vacancydto.IsActive,
        DateCreated = vacancydto.DateCreated,
        DateSaved = vacancydto.DateSaved
    };
    context.Vacancies.Add(vacancy);
    await context.SaveChangesAsync();
    return Results.Created($"/GetVacancy/{vacancy.IdVacancy}", new VacanciesDTO(vacancy));
});

     

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();


