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

app.MapPut("/vacancies/{id}",
    async (long id, VacanciesDTO vacancydto, VacancyCadidatesContext context) =>
    {
        var vacancy = await context.Vacancies.FindAsync(id);
        if (vacancy == null) return Results.NotFound();
        vacancy.Vacancy1 = vacancydto.Vacancy1;
        vacancy.Description = vacancydto.Description;
        vacancy.IsActive = vacancydto.IsActive;
        vacancy.DateCreated = vacancydto.DateCreated;
        vacancy.DateSaved = vacancydto.DateSaved;
        await context.SaveChangesAsync();
        return Results.NoContent();
    });

app.MapDelete("/vacancies/{id}",
    async (long id, VacancyCadidatesContext context) =>
    {
        if(await context.Vacancies.FindAsync(id) is Vacancy vacancy)
        {
            context.Vacancies.Remove(vacancy);
            await context.SaveChangesAsync();
            return Results.Ok(new VacanciesDTO(vacancy));
        }
        return Results.NotFound();
    });



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();


