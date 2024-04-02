using BL;
using Microsoft.AspNetCore.Identity;
using Phygital.BL;
using Phygital.DAL;
using Phygital.DAL.EF;
using Phygital.Domain.User;
using Phygital.UI_MVC;
using Microsoft.AspNetCore.Identity.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PhygitalDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PhygitalDbContext>();

builder.Services.AddScoped<IFlowRepository, FlowRepository>();
builder.Services.AddScoped<IFlowManager, FlowManager>();
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// TODO cookies has to come under here

var app = builder.Build();

//TODO error here needs fix !!!
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PhygitalDbContext>();
    if (context.CreateDatabase(dropExisting :true))
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        SeedIdentity(userManager, roleManager);
        // DataSeeder.Seed(context);
        PhygitalInitializer.Initialize(context);
    }
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

app.MapRazorPages();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // all role types
    var adminRole = new IdentityRole
    {
        Name = CustomIdentityConstraints.AdminRole
    };
    roleManager.CreateAsync(adminRole).Wait();
    var subAdminRole = new IdentityRole
    {
        Name = CustomIdentityConstraints.SubAdminRole
    };
    roleManager.CreateAsync(subAdminRole).Wait();
    var supervisorRole = new IdentityRole
    {
        Name = CustomIdentityConstraints.SupervisorRole
    };
    roleManager.CreateAsync(supervisorRole).Wait();
    var userRole = new IdentityRole
    {
        Name = CustomIdentityConstraints.UserRole
    };
    roleManager.CreateAsync(userRole).Wait();
    
    // hardcoded users
    var adminPhytical1 = new IdentityUser
    {
        Email = "admin1@phytical.be",
        UserName = "admin1", EmailConfirmed = true
    };
    userManager.CreateAsync(adminPhytical1,"admin1@phytical").Wait();
    
    //TODO proposition om emails voor subadmins te maken met @[bedrijf] voor meer overzicht ipv @phytical
    var subAdmin1 = new IdentityUser
    {
        Email = "subadmin1@phytical.be",
        UserName = "subadmin1", EmailConfirmed = true
    };
    userManager.CreateAsync(subAdmin1,"subAdmin1@phytical").Wait();
    
    //TODO  idem voor subadmin
    var supervisor1 = new IdentityUser
    {
        Email = "supervisor1@phytical.be",
        UserName = "supervisor1", EmailConfirmed = true
    };
    userManager.CreateAsync(supervisor1,"supervisor1@phytical").Wait();    
    
    //TODO i dont think it would need a role necessary (considering a machine would start thanks to the supervisor in linear flow)
    var user1 = new IdentityUser
    {
        Email = "sam@kdg.be",
        UserName = "sam@kdg.be", EmailConfirmed = true
    };
    userManager.CreateAsync(user1,"user1@phytical").Wait();    
    
    // assign hardcoded users to a role
    userManager.AddToRoleAsync(adminPhytical1, CustomIdentityConstraints.AdminRole).Wait();
    userManager.AddToRoleAsync(subAdmin1, CustomIdentityConstraints.AdminRole).Wait();
    userManager.AddToRoleAsync(supervisor1, CustomIdentityConstraints.UserRole).Wait();
    userManager.AddToRoleAsync(user1, CustomIdentityConstraints.UserRole).Wait();
}