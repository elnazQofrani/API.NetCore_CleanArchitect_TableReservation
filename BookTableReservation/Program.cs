using Microsoft.EntityFrameworkCore;
using Application.Mappings;
using Application.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using Application.Services;
using Infrastructure.Repositories;
using BookTableReservation.validation;
using Application.DTOs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<CustomerAddDtoValidation>());

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<SeatDtoValidation>());

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<BookingDtoValidation>());


builder.Services.AddDbContext<DbProjectContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectConnectionString")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerRepository , CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<ISeatRepository, SeatRespositoey>();
builder.Services.AddScoped<ISeatService, SeatService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
//builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
