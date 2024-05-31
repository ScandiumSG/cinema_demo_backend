using cinemaServer.Data;
using cinemaServer.Endpoints;
using cinemaServer.Models.PureModels;
using cinemaServer.Models.User;
using cinemaServer.Repository;
using cinemaServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema API", Version = "v1" });

    // Define the Bearer scheme that uses JWT as the security scheme
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lowercase
        BearerFormat = "JWT"
    });

    // Apply the security to all Swagger API operations
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<DataContext>
    (
        opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("elephantDB"))
    );

builder.Services.AddScoped<ICompUpcomingRepository<Screening>, ScreeningRepository>();
builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();
builder.Services.AddScoped<IRepository<Theater>, Repository<Theater>>();
builder.Services.AddScoped<IRepository<Ticket>, Repository<Ticket>>();
builder.Services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.SignIn.RequireConfirmedAccount = false;
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 5;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtTokenSettings:ValidIssuer"],
        ValidAudience = builder.Configuration["JwtTokenSettings:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenSettings:SymmetricSecurityKey"]!))
    };
});

// Add authorization services
builder.Services.AddAuthorization();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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
app.TheaterEndpointConfiguration();
app.UserEndpointConfiguration();
app.TicketEndpointConfiguration();

//Configure auth endpoint
app.AuthenticationEndpointConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

public partial class Program { } // needed for testing - please ignore