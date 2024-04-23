// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Phygital.BL.Managers;
using Phygital.DAL.EF;
using Phygital.UI_CA;

Console.WriteLine("Hello, World!");

var builder = new DbContextOptionsBuilder();
// builder.UseNpgsql("Data Source=Phygital.db");
builder.UseSqlite("Data Source=Phygital.db");

var dbContext = new PhygitalDbContext(builder.Options);

var repository = new FlowRepository(dbContext);
var manager = new FlowManager(repository);

var consoleUi = new ConsoleUi(manager, repository);
consoleUi.Run();