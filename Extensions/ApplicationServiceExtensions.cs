﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NET6_JWT_Refresh_Token_WithOut_Identity.Helpers.AuthTools;
using NET6_JWT_Refresh_Token_WithOut_Identity.Services.Interfaces;
using NET6_JWT_Refresh_Token_WithOut_Identity.Services.Repository;
using NET6_JWT_Refresh_Token_WithOut_Identity.Entities.User;
using System.Text;

namespace NET6_JWT_Refresh_Token_WithOut_Identity.Extensions;

public static class ApplicationServiceExtensions
{
    //CORS
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder
            .WithOrigins("http://127.0.0.1:5173", "*")
            //.AllowAnyOrigin() //
            .AllowAnyMethod() // With Methods("Get,Post")
            .AllowAnyHeader()
            .AllowCredentials()
            ); //WithHeaders("accept", "content-type")

        });

    // Implementación de dependencias
    public static void AddAplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        //services.AddScoped<IMarcaRepository, MarcaRepository>();
        //services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    //JWT 
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        //configuración desde AppSettings y se lo asignamos a la clase rol
        services.Configure<JWT>(configuration.GetSection("JwtSettings"));

        //Se añade configuración Autentificación
        //método de extension que añade el método de autentificación
        services.AddAuthentication(options =>
        {
            //definimos el tipo de autentificación que necesitamos
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            //configuramos la informacion del token
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false; //definimos si necesitamos una conexión https //e prod se debe pasar a true
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"], //asignamos los valores desde al appsetting
                    ValidAudience = configuration["JwtSettings:Audience"],//asignamos los valores desde al appsetting
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])) //asignamos los valores desde al appsetting
                };
            });
    }


}
