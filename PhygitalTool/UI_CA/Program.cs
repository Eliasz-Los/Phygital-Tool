// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Phygital.BL.Managers;
using Phygital.DAL.EF;
using Phygital.UI_CA;

Console.WriteLine("Hello, World!");

/*var builder = new DbContextOptionsBuilder();

var dbContext = new PhygitalDbContext(builder.Options);

var flowRepository = new FlowRepository(dbContext);
var flowElementRepository = new FlowElementRepository(dbContext);
var themeRepository = new ThemeRepository(dbContext);
var themeManager = new ThemeManager(themeRepository);
var flowManager = new FlowManager(flowRepository, themeManager);
var flowElementManager = new FlowElementManager(flowElementRepository);

var consoleUi = new ConsoleUi(flowManager, flowElementManager);
consoleUi.Run();*/