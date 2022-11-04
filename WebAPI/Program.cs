using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers;
using Business.DependencyResolvers.Autofac;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Security.Token;
using Core.CrossCuttingConcerns.Security.Token.Encryption;
using Core.DependencyResolvers;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPI.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddBusinessServices();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
    builder => builder.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
    new BusinessCoreModule()
});

TokenOptions tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = tokenOptions.Issuer,
               ValidAudience = tokenOptions.Audience,
               IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
           };
       });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your access token in text input that below."
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});
builder.Services.AddCors(
//    opt => opt.AddDefaultPolicy(corsPolicyBuilder =>
//{
//    corsPolicyBuilder.AllowAnyHeader();
//    corsPolicyBuilder.AllowAnyMethod();
//    corsPolicyBuilder.AllowCredentials();
//})
);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//if(app.Environment.IsProduction())
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(opt =>
{
    var corsOriginConfiguration = app.Configuration.GetSection("CorsOriginsConfiguration").Get<CorsOriginsConfiguration>();
    opt.WithOrigins(corsOriginConfiguration.AngularUI)
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials();
});

app.Run();