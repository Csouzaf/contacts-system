# contacts-system

What you need to run the project about registration of people:

1. **Packages**:
>Install npm 16.6.6. Past in terminal: **npm install**<br>

>To Install Angular CLI 15.2 locally: Path cd frontend and after cd angular and Past **npm install @angular/cli@15.2** and **npm install @angular/core@15.2** <br>

>To Install Angular CLI 15.2 globally. Past in terminal: **npm install -g @angular/cli@15.2**<br><br>

2. **Run the Angular**:
>Path until cd frontend and cd angular<br>
>In your terminal locally, paste: **npx ng serve**<br>
>If the install was globlally: **ng serve**<br><br>

3. **Run .NET 6**:
>Configuration SQL Server In appsetting.json: <br>
>>"ConnectionStrings":{"DefaultConnection":"Server=NAME YOUR SQL SERVER ACCOUNT;Database=NAME YOUR DATABASE;Trusted_Connection=True;Integrated Security=True"} <br>

>**IMPORTANT: If you dowload this project with migration already create, delete it and create a new migration with step by step bellow:** <br>

>Create Relantionship between Authentication and Contacts Migrations: <br>

>>Path until cd api and create the migration for login, signup and contacts pasts: **dotnet ef migrations add AuthenticationUsers --context UsersAuthDbContext** <br>

>>After this you need update the database with: **dotnet ef database update --context UsersAuthDbContext** <br><br>

4. Create Account

> Create your account in signup <br><br>

5. Run the system

> With Angular and .NET running<br>

> Create your account in router signup and afther this do login to the page <br>

>In the router contacts, your click in "Novo Contato" and create the contact with name, email and tel. After this you can to put or delete the user. 
