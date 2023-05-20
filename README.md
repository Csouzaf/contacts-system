# contacts-system

1) Create connection to SQL Server Database

   - Download the packages: EntityFrameworkCore 6.04, SqlServer 6.04 and Cors 2.2.0;
   - Create a class in the model, like UsersModel and classify the strings as empty ( = string.Empty; );
   - Then create the context of this model as UsersDbContextModel;
  
   - Configure Program.cs for VScode to avoid Cors and configure with Sql Server using:
  
   builder.Services.AddEntityFrameworkSqlServer().AddDbContext<UsersDbContextModel>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
     options.AddDefaultPolicy(builder =>
     {
         options.AddPolicy("AllowAnyOrigin", builder =>
             builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
     });

});
  
  
   //After app.UseStaticFiles();
   app.UseCors("AllowAnyOrigin");
  
   In appsettings.json, finish building the connection with:
  
     "ConnectionStrings":{
       "DefaultConnection":"Server=YourServerName;Database=ContactsSystem;Trusted_Connection=True;Integrated Security=True"
     },
  
   Finally, use dotnet ef migrations add InitialCreate and dotnet ef database update to create and update the tables.
  
  2) Inject dependencies between the services and the repository in the controller
 
  builder.Services.AddScoped<IContactsRepository, ContactsServices>();
    
    
