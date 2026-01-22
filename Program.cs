using Ticket.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ticket.Data;
using Ticket.Repositories;
using Ticket.Services.Auth;
using Ticket.Repositories.IRepositories;
using Ticket.Services.Hash;
using Ticket.Services.State;
using Ticket.Services.User;
using System.Text;
using Blazored.SessionStorage;
using Ticket.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEntryTicketRepository, EntryTicketRepository>();

builder.Services.AddScoped<IPasswordHash, PasswordHash>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddBlazoredSessionStorage();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHash>();

    await using var db = await dbFactory.CreateDbContextAsync();

    if (!db.Users.Any(u => u.Username == "admin"))
    {
        db.Users.Add(new Users
        {
            Username = "admin",
            Name = "Administrator",
            PasswordHash = hasher.Hash("password"),
            Role = "Admin",
            IsActive = true,
            CreatedAt = DateTime.Now
        });

        await db.SaveChangesAsync();
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
