using Mammut.Web.Entities;
using Mammut.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/forbidden";
        options.LogoutPath = "/account/logout";
    });

builder.Services.AddDbContext<MammutDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MammutDb"));
    options.UseSnakeCaseNamingConvention();
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.Add(new LowercasePageRoutingConvention("handler"));
    
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/account/login");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
