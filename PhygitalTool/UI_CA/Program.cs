// See https://aka.ms/new-console-template for more information

using BL;
using Microsoft.EntityFrameworkCore;
using Phygital.DAL.EF;
using Phygital.UI_CA;

Console.WriteLine("Hello, World!");

var builder = new DbContextOptionsBuilder();
builder.UseNpgsql("Data Source=Phygital.db");
var dbContext = new PhyticalDbContext(builder.Options);
var repository = new Repository(dbContext);
var manager = new Manager(repository);
var consoleUi = new ConsoleUi(manager);
consoleUi.Run();