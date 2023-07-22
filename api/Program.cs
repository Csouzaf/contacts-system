using api.auth.Data;
using api.auth.jwt;
using api.auth.Services;
using api.Models;
using api.Models.auth.Data;
using api.Models.auth.Services;
using api.Repository;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddEntityFrameworkSqlServer().AddDbContext<UsersDbContextModel>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<UsersAuthDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<AuthUserEmailDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// builder.Services.AddEntityFrameworkSqlServer().Add

builder.Services.AddScoped<IContactsRepository, ContactsServices>();

builder.Services.AddScoped<IUsersAuthRepository, UsersAuthService>();

builder.Services.AddScoped<IAuthUserEmailRepository, AuthUserEmailService>();

builder.Services.AddScoped<JwtService>();

// builder.Services.AddCors(options => 
// {
//     options.AddDefaultPolicy(builder => 
//     {
//         options.AddPolicy("AllowAnyOrigin", builder =>
//             builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader());
//     });

// });





var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(options => options.WithOrigins(new []{"http://localhost:7087","http://localhost:5075", "http://localhost:4200"})
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
