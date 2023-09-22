using api.auth.Data;
using api.auth.jwt;
using api.auth.Services;
using api.Models;
using api.Models.auth.Data;
using api.Models.auth.Services;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
// using api.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

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

// builder.Services.AddEntityFrameworkSqlServer().AddDbContext<UserRegisteredDbContext>(
//     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
// );

// builder.Services.AddEntityFrameworkSqlServer().Add

builder.Services.AddScoped<IContactsRepository, ContactsServices>();

builder.Services.AddScoped<IUsersAuthRepository, UsersAuthService>();

builder.Services.AddScoped<IAuthUserEmailRepository, AuthUserEmailService>();

// builder.Services.AddScoped<IUserRegisteredRepository, UserRegisteredService>();

builder.Services.AddScoped<JwtService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// builder.Services.AddHttpContextAccessor();

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

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UsersAuthDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       

    })
    .AddJwtBearer(options =>
    
    {
       
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            // ValidIssuer = "http://localhost:7087", // Replace with your issuer
            ValidateAudience = false,
            // ValidAudience = "http://localhost:7087", // Replace with your audience
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])), // Replace with your secret key
            //When time gonna zero the users get out home
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddDistributedMemoryCache();

// builder.Services.AddAuthentication(CookieAuthenticationDefaults).AddCookie(option => {
//     option.L
// })

builder.Services.AddSession();

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
    
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    //TODO - Change router to login
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
