using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Infrastructure.Repositories;
using ExpenseTracker.Infrastructure.Sql;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
//builder.Services.AddDbContext<DataContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseTrackerCon")));
builder.Services.AddControllers().AddJsonOptions(options =>
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
   o.AddPolicy("AllowAll", new CorsPolicyBuilder()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed(origin => true)
                   .AllowCredentials()
                   .Build());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(configuration.GetConnectionString("ExpenseTrackerCon")));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAll");
//app.UseSerilogRequestLogging();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

