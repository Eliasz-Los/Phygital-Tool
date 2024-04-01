using BL;
using Microsoft.AspNetCore.Identity;
using Phygital.BL;
using Phygital.DAL;
using Phygital.DAL.EF;
using Phygital.Domain.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<PhygitalDbContext>();
builder.Services.AddScoped<IFlowRepository, FlowRepository>();
builder.Services.AddScoped<IFlowManager, FlowManager>();
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//AddRoles() methods
// builder.Services.AddDefaultIdentity<IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddRoles<IdentityRole>()
//     .AddEntityFrameworkStores<PhygitalDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PhygitalDbContext>();
    //TODO continue here
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var userRole = new IdentityRole
    {
        Name = CustomIdentity.UserRole
    };
    roleManager.CreateAsync(userRole).Wait();
    
    var adminRole = new IdentityRole
    {
        Name = CustomIdentity.AdminRole
    };
    roleManager.CreateAsync(adminRole).Wait();
    
    var subAdminRole = new IdentityRole
    {
        Name = CustomIdentity.SubAdminRole
    };
    roleManager.CreateAsync(subAdminRole).Wait();
    
    var supervisionRole = new IdentityRole
    {
        Name = CustomIdentity.SupervisionRole
    };
    roleManager.CreateAsync(supervisionRole).Wait();

    var admin1 = new IdentityUser
    {
        Email = "admin1@phyticaltool.be",
        UserName = "admin1",
        EmailConfirmed = true
    };
    userManager.CreateAsync(admin1, "admin1").Wait();
    
    var subAdmin1 = new IdentityUser
    {
        Email = "subAdmin1@phyticaltool.be",
        UserName = "subAdmin1",
        EmailConfirmed = true
    };
    userManager.CreateAsync(subAdmin1, "subAdmin1").Wait();
    
    var subAdmin2 = new IdentityUser
    {
        Email = "subAdmin2@phyticaltool.be",
        UserName = "subAdmin2",
        EmailConfirmed = true
    };
    userManager.CreateAsync(subAdmin2, "subAdmin2").Wait();

    var supervisor1 = new IdentityUser
    {
        Email = "supervisor1@phyticaltool.be",
        UserName = "supervisor1",
        EmailConfirmed = true
    };
    userManager.CreateAsync(supervisor1, "supervisor1").Wait();

    var user1 = new IdentityUser
    {
        Email = "user1@phyticaltool.be",
        UserName = "user1",
        EmailConfirmed = true
    };
    userManager.CreateAsync(user1, "user1").Wait();
    
    userManager.AddToRoleAsync(admin1, CustomIdentity.AdminRole).Wait();
    userManager.AddToRoleAsync(subAdmin1, CustomIdentity.SubAdminRole).Wait();
    userManager.AddToRoleAsync(subAdmin2, CustomIdentity.SubAdminRole).Wait();
    userManager.AddToRoleAsync(supervisor1, CustomIdentity.SupervisionRole).Wait();
    userManager.AddToRoleAsync(user1, CustomIdentity.UserRole).Wait();
}