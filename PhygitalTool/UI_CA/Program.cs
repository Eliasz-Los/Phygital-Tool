// See https://aka.ms/new-console-template for more information

using BL;
using Microsoft.EntityFrameworkCore;
using Phygital.DAL.EF;
using Phygital.UI_CA;

Console.WriteLine("Hello, World!");

var builder = new DbContextOptionsBuilder();

var dbContext = new PhygitalDbContext(builder.Options);

var repository = new FlowRepository(dbContext);
var manager = new FlowManager(repository);

var consoleUi = new ConsoleUi(manager, repository);
consoleUi.Run();