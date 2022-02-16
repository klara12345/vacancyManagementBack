using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using VacancyManagment.DTO;
using VacancyManagment.Models;
using VacancyManagment.Services;


var builder = WebApplication.CreateBuilder(args);
//var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    });

builder.Services.AddDbContext<VacancyCadidatesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VacancyCadidates"));



});
builder.Services.AddCors();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Token"])),
        ValidateIssuer =false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");

app.MapGet("/register", 
async ([FromServices] VacancyCadidatesContext context) =>
    await context.VacancyUsers.Select(x => new RegisterDTO(x)).ToListAsync());

app.MapGet("/vacancies", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    async ([FromServices] VacancyCadidatesContext context) =>
    await context.Vacancies.Select(x => new VacanciesDTO(x)).ToListAsync());


app.MapPost("/vacancies", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
async ([FromBody] VacanciesDTO vacancydto,[FromServices] VacancyCadidatesContext context, HttpResponse response) =>
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

app.MapPut("/vacancies/{id}",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

app.MapDelete("/vacancies/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
app.UseCors(options =>
 options.WithOrigins("http://localhost:4200/")
 .AllowAnyHeader()
 .AllowAnyOrigin()
);
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();


