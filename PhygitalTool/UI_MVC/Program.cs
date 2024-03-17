using BL;
using Phygital.BL;
using Phygital.DAL;
using Phygital.DAL.EF;

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