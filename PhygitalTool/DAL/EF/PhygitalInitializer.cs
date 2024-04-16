using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;
using Phygital.Domain.User;

namespace Phygital.DAL.EF;

public class PhygitalInitializer
{
    private static bool _hasBeenInitialized = false;
    
    public static void InitializeDatabaseAndSeedData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PhygitalDbContext>();
            if (context.CreateDatabase(dropExisting: true))
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                Seed(context);
                SeedIdentity(userManager, roleManager);
            }
        }
    }

    private static void Seed(PhygitalDbContext context)
    {
        // In the first part of the seed method we create data to be put into the database
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        // Account
        // var arthur = new Account
        // {
        //     Id = "361dc522-3f48-4666-a571-ca1437c2e7b7",
        //     Name = "Arthur",
        //     LastName = "Linsen",
        //     Mail = "arthur.linsen@phygital.be",
        //     RoleName = "ADMIN"
        // };
        //
        // var jonas = new Account
        // {
        //     Id = "56baeecc-8765-4b9a-9075-88157d6307f0",
        //     Name = "Jonas",
        //     LastName = "Wuyten",
        //     Mail = "jonas.wuyten@phygital.be",
        //     RoleName = "SUBADMIN"
        // };
        //
        // var eliasz = new Account
        // {
        //     Id = "8dd72827-87f8-46b5-af23-233fa24cc76e",
        //     Name = "Eliasz",
        //     LastName = "Los",
        //     Mail = "eliasz.los@phygital.be",
        //     RoleName = "SUPERVISOR"
        // };
        //
        // var josse = new Account
        // {
        //     Id = "dd448251-2c08-4150-af67-77cb1947bbd5",
        //     Name = "Josse",
        //     LastName = "Dresselaers",
        //     Mail = "josse.dresselaers@phygital.be",
        //     RoleName = "USER"
        // };
        //
        // var willem = new Account
        // {
        //     Id = "23784981-dda2-4ae0-8c42-5c49d026eff4",
        //     Name = "Willem",
        //     LastName = "Kuijpers",
        //     Mail = "willem.kuijpers@phygital.be",
        //     RoleName = "DEACTIVATED"
        // };
        //
        // context.Accounts.AddRange(new Account[] { arthur, jonas, eliasz, josse, willem });

        // Filling themes
        var th1 = new Theme { Title = "Politiek", Description = "Simpele vragen rond politiek" };
        var th2 = new Theme { Title = "Vakantie", Description = "Simpele vragen rond vakantie" };

        // Filling info
        var i1 = new Text
        {
            Content = "Deze vraag heeft betrekking op de politiek op gemeentelijk niveau"
        };
        var i2 = new Text
        {
            Content = "De volgende vragen gaan over de politieke voorkeur van mensen"
        };
        var i3 = new Text
        {
            Content =
                "PFAS zijn door de mens gemaakte stoffen die worden gevonden in de leefomgeving en in mensen. Ze komen in kleine hoeveelheden voor in onder andere de bodem, het oppervlaktewater en bloed van mensen. Dit komt bijvoorbeeld doordat fabrieken ze uitstoten bij productieprocessen waar deze stoffen worden gebruikt."
        };
        var i4 = new Text
        {
            Content =
                "De belasting op het gebruik van aardgas gaat omhoog. Dat komt door de inflatiecorrectie. Maar ook door een extra verhoging in de laagste verbruiksschijven van 2,5 cent exclusief btw per m3. Door de belasting op energie te verlagen en die op aardgas te verhogen wil de regering verduurzaming stimuleren."
        };

        var f1 = new Flow
        {
            FlowType = Flowtype.linear,
            IsOpen = true,
            FlowElements = new List<FlowElement>(),
            Answers = new List<Answer>()
        };

        var f2 = new Flow
        {
            FlowType = Flowtype.circular,
            IsOpen = true
        };

        // Vragen opvullen
        var q1 = new MultipleChoice
        {
            Text = "Wat is uw favoriete bron voor het verkrijgen van politieke informatie?",
            Active = true,
            SequenceNumber = 1,
            Options = new List<Option>()
        };

        var q2 = new OpenQuestion
        {
            Text = "Waarom kiest u voor deze partij?",
            Active = true,
            SequenceNumber = 2,
            Answer = new Answer()
        };
        var q3 = new SingleChoiceQuestion
        {
            Text = "Bent u voor of tegen: BTW van 6 procent op elektriciteit?",
            Active = true,
            SequenceNumber = 3,
            Options = new List<Option>()
        };

        var q4 = new RangeQuestion
        {
            Text = "Wat is uw mening over deze uitspraak: 'De regering moet meer investeren in de zorgsector'",
            Active = true,
            SequenceNumber = 4,
            Options = new List<Option>()
        };

        var q6 = new RangeQuestion()
        {
            Text = "Hoe tevreden bent u over de huidige regering?",
            Active = true,
            SequenceNumber = 6,
            Options = new List<Option>()
        };
        var q7 = new RangeQuestion()
        {
            Text = "Wat is uw mening over de huidige belastingtarieven voor bedrijven?",
            Active = true,
            SequenceNumber = 7,
            Options = new List<Option>()
        };
        var q8 = new SingleChoiceQuestion()
        {
            Text = "Bent u voorstander van een verplichte dienstplicht?",
            Active = true,
            SequenceNumber = 8,
            Options = new List<Option>()
        };
        var q9 = new OpenQuestion()
        {
            Text = "Waarom bent u voor of tegen de dienstplicht?",
            Active = true,
            SequenceNumber = 9,
            Answer = new Answer()
        };
        var q10 = new OpenQuestion()
        {
            Text =
                "Hoe zou u het beleid op het gebied van gendergelijkheid en LGBTQ+-rechten willen zien veranderen of verbeteren?",
            Active = true,
            SequenceNumber = 10,
            Answer = new Answer()
        };
        var q11 = new RangeQuestion()
        {
            Text = "Kies je eerder voor een linkse, neutrale of rechtse partij?",
            Active = true,
            SequenceNumber = 11,
            Options = new List<Option>()
        };
        var q12 = new SingleChoiceQuestion()
        {
            Text = "Welke linkse partij?",
            Active = true,
            SequenceNumber = 12,
            Options = new List<Option>()
        };
        var q13 = new SingleChoiceQuestion()
        {
            Text = "Welke rechtse partij?",
            Active = true,
            SequenceNumber = 13,
            Options = new List<Option>()
        };
        var q14 = new SingleChoiceQuestion()
        {
            Text = "Welke centrale partij?",
            Active = true,
            SequenceNumber = 14,
            Options = new List<Option>()
        };


        // Filling options & answers
        Option o1 = new Option { OptionText = "CD&V" };
        Option o2 = new Option { OptionText = "Vooruit" };
        Option o3 = new Option { OptionText = "NV-A" };
        Option o4 = new Option { OptionText = "Groen" };
        Option o5 = new Option { OptionText = "PVDA" };
        Option o6 = new Option { OptionText = "Open-VLD" };
        Option o7 = new Option { OptionText = "Vlaams Belang" };

        Option o8 = new Option { OptionText = "Voor" };
        Option o9 = new Option { OptionText = "Tegen" };

        Option o10 = new Option { OptionText = "Zwaar tegen" };
        Option o11 = new Option { OptionText = "Tegen" };
        Option o12 = new Option { OptionText = "Neutraal" };
        Option o13 = new Option { OptionText = "Voor" };
        Option o14 = new Option { OptionText = "Zwaar voor" };

        Option o15 = new Option { OptionText = "Te hoog" };
        Option o16 = new Option { OptionText = "Te laag" };
        Option o17 = new Option { OptionText = "Passend" };

        Option o18 = new Option { OptionText = "Sociale media" };
        Option o19 = new Option { OptionText = "Nieuwswebsites / kranten" };
        Option o20 = new Option { OptionText = "Televisie" };
        Option o21 = new Option { OptionText = "Radio" };

        Option o22 = new Option { OptionText = "Links" };
        Option o23 = new Option { OptionText = "Neutraal" };
        Option o24 = new Option { OptionText = "Rechts" };

        Option o25 = new Option { OptionText = "Tevreden" };
        Option o29 = new Option { OptionText = "Neutraal" };
        Option o26 = new Option { OptionText = "Ontevreden" };

        Option o27 = new Option { OptionText = "Voor" };
        Option o28 = new Option { OptionText = "Tegen" };


        var a1 = new Answer { ChosenAnswer = "CD&V" };
        var a2 = new Answer { ChosenAnswer = "Vooruit" };
        var a3 = new Answer { ChosenAnswer = "NV-A" };
        var a4 = new Answer { ChosenAnswer = "Groen" };
        var a5 = new Answer { ChosenAnswer = "PVDA" };
        var a6 = new Answer { ChosenAnswer = "Open-VLD" };
        var a7 = new Answer { ChosenAnswer = "Vlaams Belang" };

        var a8 = new Answer { ChosenAnswer = "Voor" };
        var a9 = new Answer { ChosenAnswer = "Tegen" };
        var a10 = new Answer { ChosenAnswer = "Geen mening" };
        var a11 = new Answer { ChosenAnswer = " " };
        var a12 = new Answer { ChosenAnswer = "geen interesse" };


        // In the second part of the seed method we create the relations between the different classes
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        // Adding questions & info to the flow
        f1.FlowElements.Add(q1);
        f1.FlowElements.Add(q2);
        f1.FlowElements.Add(q3);
        f1.FlowElements.Add(q4);
        f1.FlowElements.Add(q6);
        f1.FlowElements.Add(q7);
        f1.FlowElements.Add(q8);
        f1.FlowElements.Add(q9);
        f1.FlowElements.Add(q10);
        f1.FlowElements.Add(q11);
        f1.FlowElements.Add(q12);
        f1.FlowElements.Add(q13);
        f1.FlowElements.Add(q14);
        
        f1.FlowElements.Add(i1);
        f1.FlowElements.Add(i2);
        f1.FlowElements.Add(i3);
        f1.FlowElements.Add(i4);

        // Adding options to the questions
        q12.Options.Add(o5);
        q12.Options.Add(o2);
        q12.Options.Add(o4);
        q13.Options.Add(o3);
        q13.Options.Add(o7);
        q14.Options.Add(o1);
        q14.Options.Add(o6);
        q3.Options.Add(o8);
        q3.Options.Add(o9);
        q2.Answer = a10;
        q4.Options.Add(o10); //zwaar tegen
        q4.Options.Add(o11); //tegen
        q4.Options.Add(o12); //neutraal
        q4.Options.Add(o13); //voor
        q4.Options.Add(o14); //zwaar voor


        q6.Options.Add(o26);
        q6.Options.Add(o29);
        q6.Options.Add(o25);
        q7.Options.Add(o16); //te laag
        q7.Options.Add(o17); //passend
        q7.Options.Add(o15); //te hoog
        q8.Options.Add(o27);
        q8.Options.Add(o28);
        q9.Answer = a11;
        q10.Answer = a12;
        q1.Options.Add(o18);
        q1.Options.Add(o19);
        q1.Options.Add(o20);
        q1.Options.Add(o21);
        q11.Options.Add(o22);
        q11.Options.Add(o23);
        q11.Options.Add(o24);


        // Adding answers to the flow
        f1.Answers.Add(a1);
        f1.Answers.Add(a2);
        f1.Answers.Add(a3);
        f1.Answers.Add(a4);
        f1.Answers.Add(a5);
        f1.Answers.Add(a6);
        f1.Answers.Add(a7);
        f1.Answers.Add(a8);
        f1.Answers.Add(a9);
        f1.Answers.Add(a10);
        f1.Answers.Add(a11);
        f1.Answers.Add(a12);

        a1.Flow = f1;
        a2.Flow = f1;
        a3.Flow = f1;
        a4.Flow = f1;
        a5.Flow = f1;
        a6.Flow = f1;
        a7.Flow = f1;
        a8.Flow = f1;
        a9.Flow = f1;
        a10.Flow = f1;
        a11.Flow = f1;
        a12.Flow = f1;

        //Adding themes to the flow
        f1.Theme = th1;
        f2.Theme = th2;

        /////////////////////////////////////////
        // Third part: adding to the Database //
        ////////////////////////////////////////

        // Adding flows
        context.Flows.Add(f1);
        context.Flows.Add(f2);

        // Adding themes
        context.Themas.Add(th1);
        context.Themas.Add(th2);

        // adding FlowElements
        context.FlowElements.AddRange(new FlowElement[]
            { q1, q2, q3, q4, q6, q7, q8, q9, q10, q11, q12, q13, q14, i1, i2, i3, i4 });
        // adding options
        context.AddRange(new Option[]
        {
            o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, o16, o17, o18, o19, o20, o21, o22, o23,
            o24, o25, o26, o27, o28, o29
        });

        // Adding answers
        context.AddRange(new Answer[] { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12 });

        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
    
    public static void SeedIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // all role types
        var adminRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.AdminRole
        };
        roleManager.CreateAsync(adminRole);

        var subAdminRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.SubAdminRole
        };
        roleManager.CreateAsync(subAdminRole);

        var supervisorRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.SupervisorRole
        };
        roleManager.CreateAsync(supervisorRole);

        var userRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.UserRole
        };
        roleManager.CreateAsync(userRole);

        // hardcoded users implentation and assignment of a role
        var adminPhygital = new IdentityUser
        {
            Email = "admin@phygital.be",
            UserName = "admin", EmailConfirmed = true
        };
        userManager.CreateAsync(adminPhygital, "admin");
        userManager.AddToRoleAsync(adminPhygital, CustomIdentityConstraints.AdminRole);

        var subAdmin = new IdentityUser
        {
            Email = "subadmin@phygital.be",
            UserName = "subadmin", EmailConfirmed = true
        };
        userManager.CreateAsync(subAdmin, "subAdmin");
        userManager.AddToRoleAsync(subAdmin, CustomIdentityConstraints.SubAdminRole);

        var supervisor = new IdentityUser
        {
            Email = "supervisor@phygital.be",
            UserName = "supervisor", EmailConfirmed = true
        };
        userManager.CreateAsync(supervisor, "supervisor");
        userManager.AddToRoleAsync(supervisor, CustomIdentityConstraints.SupervisorRole);

        var user = new IdentityUser
        {
            Email = "user@phygital.be",
            UserName = "user", EmailConfirmed = true
        };
        userManager.CreateAsync(user, "user");
        userManager.AddToRoleAsync(user, CustomIdentityConstraints.UserRole);
    }
}