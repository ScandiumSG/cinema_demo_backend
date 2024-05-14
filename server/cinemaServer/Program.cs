using cinemaServer.Data;
using cinemaServer.Endpoints;
using cinemaServer.Models.PureModels;
using cinemaServer.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>
    (
        opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("dockerDB"))
    );

builder.Services.AddScoped<IRepository<Screening>, Repository<Screening>>();
builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configure controllers/endpoints
app.ScreeningEndpointConfiguration();
app.MovieEndpointConfiguration();

app.Run();