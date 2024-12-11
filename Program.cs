using Microsoft.EntityFrameworkCore;
using TodoTaskApp.Repositories;
using TodoTaskData.Data;
using TodoTaskData.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=WARRIOR;Initial Catalog=TodoTask;Integrated Security=True;Trust Server Certificate=True"));
builder.Services.AddScoped<ITaskRepository, TaskRepository>(); 
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
