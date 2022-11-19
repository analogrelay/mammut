using Auth0.AspNetCore.Authentication;
using Mammut.Web;
using Mammut.Web.Entities;
using Mammut.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = Auth0Constants.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/account/forbidden";
        options.LoginPath = "/account/welcome";
    })
    .AddAuth0WebAppAuthentication(options =>
    {
        options.CallbackPath = "/account/callback";
        options.Domain = builder.Configuration["Auth0:Domain"].Require();
        options.ClientId = builder.Configuration["Auth0:ClientId"].Require();
        options.SkipCookieMiddleware = true;
    });

builder.Services.AddDbContext<MammutDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("MammutDb"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.Add(new LowercasePageRoutingConvention("handler"));
    
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Account/Welcome");
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
