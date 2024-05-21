using Phygital.BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.DAL;
using Phygital.DAL.EF;
using Phygital.Domain.User;
using FlowManager = Phygital.BL.Managers.FlowManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PhygitalDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Phygital.db")));

builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PhygitalDbContext>();
// repositories
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFlowElementRepository, FlowElementRepository>();
builder.Services.AddScoped<IFlowRepository, FlowRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// managers
builder.Services.AddScoped<IAnswerManager, AnswerManager>();
builder.Services.AddScoped<IFeedbackManager, FeedbackManager>();
builder.Services.AddScoped<IFlowElementManager, FlowElementManager>();
builder.Services.AddScoped<IFlowManager, FlowManager>();
builder.Services.AddScoped<IPlatformManager, PlatformManager>();
builder.Services.AddScoped<ISessionManager, SessionManager>();
builder.Services.AddScoped<IThemeManager, ThemeManager>();
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IStatisticsManager, StatisticsManager>();
builder.Services.AddScoped<IUserManager, UserManager>();

// cookies
// builder.Services.ConfigureApplicationCookie(cfg =>
// {
//     cfg.Events.OnRedirectToLogin += ctx =>
//     {
//         if (ctx.Request.Path.StartsWithSegments("/api"))
//         {
//             ctx.Response.StatusCode = 401;
//         }
//
//         return Task.CompletedTask;
//     };
//
//     cfg.Events.OnRedirectToAccessDenied += ctx =>
//     {
//         if (ctx.Request.Path.StartsWithSegments("/api"))
//         {
//             ctx.Response.StatusCode = 403;
//         }
//
//         return Task.CompletedTask;
//     };
// });

var app = builder.Build();

PhygitalInitializer.InitializeDatabaseAndSeedData(app.Services);

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
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
public partial class Program{}