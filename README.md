# contacts-system

1) Criar conexão ao Banco de Dados SQL Server

  - Fazer o donwload dos pacotes: EntityFrameworkCore 6.04, SqlServer 6.04 e Cors 2.2.0;
  - Crie uma classe no model, como o UsersModel e classificar as strings como vazias ( = string.Empty; );
  - Depois cria o contexto desse modelo como o UsersDbContextModel;
  
  - Configurar o Program.cs para o VScode para evitar o Cors e fazer a configuração com Sql Server utilizando: 
  
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
  
  //Após o app.UseStaticFiles();
  app.UseCors("AllowAnyOrigin");
  
  No appsettings.json, finaliza a construção da conexão com:
  
    "ConnectionStrings":{
      "DefaultConnection":"Server=NomedoSeuServidor;Database=ContactsSystem;Trusted_Connection=True;Integrated Security=True"
    },
  
  Por fim utiliza o dotnet ef migrations add InitialCreate e dotnet ef database update para criar e atualizar as tabelas.
    
    
