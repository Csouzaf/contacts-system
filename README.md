# contacts-system

What you need to run the project about registration of people:

1) Create connection to SQL Server Database

   - Download the packages: EntityFrameworkCore 6.04, SqlServer 6.04 and Cors 2.2.0;
   - Create a class in the model like UsersModel and classify the strings as empty ( = string.Empty; );
   - Then create the context of this model as UsersDbContextModel;
   - Configure Program.cs for VSCode to avoid Cors and configure with Sql Server using the code bellow:
  
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
      });


      //After app.UseStaticFiles();
      app.UseCors("AllowAnyOrigin");

  
 2) In your appsettings.json, you need the paste the code to finish building the connection with SQL Server:

        "ConnectionStrings":{
          "DefaultConnection":"Server=YourServerName;Database=ContactsSystem;Trusted_Connection=True;Integrated Security=True"
        },

    Finally, use in terminal: dotnet ef migrations add <Name of your Migration> 
    and after this, use: dotnet ef database update to create and update the tables in your SQL Server.
   
  
  3) Inject dependencies between the services and the repository in the controller
  
  When your download of this project, you will need inject the dependencies about the service and repository. Paste the code bellow in your Program.Cs:
  
  builder.Services.AddScoped<IContactsRepository, ContactsServices>();
   
  
  4) Run the Angular and Dotnet at VsCode
  
  Run the Angular 13 in the path frontend/angular with: ng serve 
  Run the Dotnet 6 in the path /api with: dotnet run
  
  5) In the router contacts, your click in "Novo Contato" and create the contact with name, email and tel. After this you can to put or delete the user. 
