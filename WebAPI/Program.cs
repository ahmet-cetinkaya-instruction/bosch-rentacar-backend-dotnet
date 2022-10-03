using Business.Abstracts;
using Business.BusinessRules;
using Business.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes.InMemory;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Business IoC
builder.Services.AddSingleton<IBrandDal, InMemoryBrandDal>(); // 100
builder.Services.AddSingleton<BrandBusinessRules>(); // 101
builder.Services.AddSingleton<IBrandService, BrandManager>(); // 102
builder.Services.AddAutoMapper(assemblies: AppDomain.CurrentDomain.GetAssemblies()); // Extension
//

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();