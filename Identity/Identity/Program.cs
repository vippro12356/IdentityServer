using Identity;
using Identity.Interface.Repositories;
using Identity.Models;
using Identity.Repository;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityContext>(op =>
{
    op.UseSqlServer("Data Source=LAPTOP-CVUOF421;Initial Catalog=Data;Integrated Security=True;TrustServerCertificate=True");
});
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
builder.Services.AddIdentityServer(op =>
{
    op.Events.RaiseErrorEvents = true;
    op.Events.RaiseInformationEvents = true;
    op.Events.RaiseFailureEvents = true;
    op.Events.RaiseSuccessEvents = true;
})
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = b => b.UseSqlServer("Data Source=LAPTOP-CVUOF421;Initial Catalog=Data;Integrated Security=True;TrustServerCertificate=True",
            sql => sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
    })
       .AddOperationalStore(options =>
       {
           options.ConfigureDbContext = b => b.UseSqlServer("Data Source=LAPTOP-CVUOF421;Initial Catalog=Data;Integrated Security=True;TrustServerCertificate=True",
               sql => sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
       })
       .AddAspNetIdentity<AppUser>()
       .AddDeveloperSigningCredential()
;
//.AddInMemoryIdentityResources(Config.IdentityResources)
//.AddInMemoryApiResources(Config.GetApiResource())
//.AddInMemoryClients(Config.GetClient())
//.AddAspNetIdentity<AppUser>()
//.AddInMemoryApiScopes(Config.GetApiScope())
//.AddDeveloperSigningCredential();
builder.Services.AddAuthentication();
builder.Services.AddScoped<IUserRepository, UserRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIdentityServer();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDeveloperExceptionPage();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
    using (var context = scope.ServiceProvider.GetService<ConfigurationDbContext>())
    {
        if (context != null)
        {
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClient())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.GetApiResource())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in Config.GetApiScope())
                {
                    context.ApiScopes.Add(apiScope.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.IdentityResources.Any())
            {
                foreach (var identityResource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(identityResource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
app.Run();
