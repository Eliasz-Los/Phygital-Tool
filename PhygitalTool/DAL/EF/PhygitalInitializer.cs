using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Feedback;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Session;
using Phygital.Domain.Themas;
using Phygital.Domain.User;

namespace Phygital.DAL.EF;

public class PhygitalInitializer
{
    public static void InitializeDatabaseAndSeedData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PhygitalDbContext>();
            if (context.CreateDatabase(dropExisting: true))
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                Seed(context);
                SeedIdentity(userManager, roleManager, context).GetAwaiter().GetResult();
            }
        }
    }

    private static async Task SeedIdentity(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        PhygitalDbContext context)
    {
        // all role types
        var adminRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.AdminRole
        };
        await roleManager.CreateAsync(adminRole);

        var subAdminRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.SubAdminRole
        };
        await roleManager.CreateAsync(subAdminRole);

        var supervisorRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.SupervisorRole
        };
        await roleManager.CreateAsync(supervisorRole);

        var userRole = new IdentityRole
        {
            Name = CustomIdentityConstraints.UserRole
        };
        await roleManager.CreateAsync(userRole);

        // hardcoded users implentation and assignment of a role
        var adminPhygital = new Account()
        {
            Email = "admin@phygital.be",
            UserName = "admin@phygital.be", EmailConfirmed = true
        };
        await userManager.CreateAsync(adminPhygital, "Admin@01");
        await userManager.AddToRoleAsync(adminPhygital, CustomIdentityConstraints.AdminRole);

        var subAdmin = new Account()
        {
            Email = "subadmin@phygital.be",
            UserName = "subadmin@phygital.be", EmailConfirmed = true
        };
        await userManager.CreateAsync(subAdmin, "Subadmin@01");
        await userManager.AddToRoleAsync(subAdmin, CustomIdentityConstraints.SubAdminRole);

        var supervisor = new Account()
        {
            Email = "supervisor@phygital.be",
            UserName = "supervisor@phygital.be", EmailConfirmed = true
        };
        await userManager.CreateAsync(supervisor, "Supervisor@01");
        await userManager.AddToRoleAsync(supervisor, CustomIdentityConstraints.SupervisorRole);

        var user = new Account()
        {
            Email = "user@phygital.be",
            UserName = "user@phygital.be", EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "User@01");
        await userManager.AddToRoleAsync(user, CustomIdentityConstraints.UserRole);

        var Arthur = new Account()
        {
            Email = "arthur.linsen@student.kdg.be",
            UserName = "arthur.linsen@student.kdg.be",
            EmailConfirmed = true,
            Name = "Arthur",
            LastName = "Linsen"
        };
        await userManager.CreateAsync(Arthur, "Arthur@01");
        await userManager.AddToRoleAsync(Arthur, CustomIdentityConstraints.UserRole);

        var Eliasz = new Account()
        {
            Email = "eliasz.los@student.kdg.be",
            UserName = "eliasz.los@student.kdg.be",
            EmailConfirmed = true,
            Name = "Eliasz",
            LastName = "Los"
        };
        await userManager.CreateAsync(Eliasz, "Eliasz@01");
        await userManager.AddToRoleAsync(Eliasz, CustomIdentityConstraints.SubAdminRole);

        var Josse = new Account()
        {
            Email = "josse.dresselaers@phygital.be",
            UserName = "josse.dresselaers@phygital.be",
            EmailConfirmed = true,
            Name = "Josse",
            LastName = "Dresselaers"
        };
        await userManager.CreateAsync(Josse, "Josse@01");
        await userManager.AddToRoleAsync(Josse, CustomIdentityConstraints.SubAdminRole);

        var Jonas = new Account()
        {
            Email = "jonas.wuyten@phygital.be",
            UserName = "jonas.wuyten@phygital.be",
            EmailConfirmed = true,
            Name = "Jonas",
            LastName = "Wuyten",
        };
        await userManager.CreateAsync(Jonas, "Jonas@01");
        await userManager.AddToRoleAsync(Jonas, CustomIdentityConstraints.SubAdminRole);

        var Willem = new Account()
        {
            Email = "willem.kuijpers@phygital.be",
            UserName = "willem.kuijpers@phygital.be",
            EmailConfirmed = true,
            Name = "Willem",
            LastName = "Kuijpers"
        };
        await userManager.CreateAsync(Willem, "Willem@01");
        await userManager.AddToRoleAsync(Willem, CustomIdentityConstraints.SubAdminRole);


        var TestUser = new Account()
        {
            Email = "tester.kdg@student.kdg.be",
            UserName = "tester.kdg@student.kdg.be",
            EmailConfirmed = true,
            Name = "Kdg",
            LastName = "Tester"
        };
        await userManager.CreateAsync(TestUser, "Test@01");
        await userManager.AddToRoleAsync(TestUser, CustomIdentityConstraints.UserRole);

        var Organisation1 = new Organisation
        {
            Name = "KdG",
            Description = "Karel de Grote Hogeschool",
            Accounts = new List<Account>()
        };

        var Organisation2 = new Organisation
        {
            Name = "Treecompany",
            Description = "opdracht gever",
            Accounts = new List<Account>()
        };

        //Adding account to organisations
        Organisation1.Accounts = new List<Account> { Arthur, Eliasz, Josse, Jonas, Willem, TestUser };
        Organisation2.Accounts = new List<Account> { adminPhygital, subAdmin, supervisor, user };
        
        context.Organisations.AddRange(Organisation1, Organisation2);
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }

    private static void Seed(PhygitalDbContext context)
    {
        // In the first part of the seed method we create data to be put into the database
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        // Filling themes
        var th1 = new Theme { Title = "Politiek", Description = "Simpele vragen rond politiek" };
        var th2 = new Theme { Title = "Vakantie", Description = "Simpele vragen rond vakantie" };

        // Filling info
        var i1 = new Text
        {
            Title = "Verschillende Partijen",
            Content =
                "Sinds de laatste verkiezingen zijn er 7 partijen in de kamer vertegenwoordigd. Dit zijn: CD&V, Vooruit, NV-A, Groen, PVDA, Open-VLD en Vlaams Belang."
        };
        var i2 = new Text
        {
            Title = "LGBT+",
            Content =
                "Met rechten wordt het bedoeldd dat ze zelf mogen kiezen met wie ze willen trouwen en dat ze niet gediscrimineerd mogen worden."
        };
        var i3 = new Text
        {
            Title = "PFAS",
            Content =
                "PFAS zijn door de mens gemaakte stoffen die worden gevonden in de leefomgeving en in mensen. Ze komen in kleine hoeveelheden voor in onder andere de bodem, het oppervlaktewater en bloed van mensen. Dit komt bijvoorbeeld doordat fabrieken ze uitstoten bij productieprocessen waar deze stoffen worden gebruikt."
        };
        var i4 = new Text
        {
            Title = "Belastingen",
            Content =
                "De belasting op het gebruik van aardgas gaat omhoog. Dat komt door de inflatiecorrectie. Maar ook door een extra verhoging in de laagste verbruiksschijven van 2,5 cent exclusief btw per m3. Door de belasting op energie te verlagen en die op aardgas te verhogen wil de regering verduurzaming stimuleren."
        };

        var i5 = new Image
        {
            Title = "Belastingschijf",
            Url = "~/images/belastingschijf.png",
            AltText = "Belastingschijf van 2024"
        };

        var i6 = new Video
        {
            Title = "Partijen in België",
            Url = "Fq7LErJRTyo", //"https://www.youtube.com/watch?v=Fq7LErJRTyo&t=24s" 
            Description =
                "Dit video zal een korte uitleg geven over de verschillende partijen in België zodat u een beter geïnformeerde keuze kan maken."
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
            Answer = new Answer(),
            SubTheme = th1,
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
            Answer = new Answer(),
            SubTheme = th1
        };
        var q10 = new OpenQuestion()
        {
            Text =
                "Hoe zou u het beleid op het gebied van gendergelijkheid en LGBTQ+-rechten willen zien veranderen of verbeteren?",
            Active = true,
            SequenceNumber = 10,
            Answer = new Answer(),
            SubTheme = th1
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


        var installation1 = new Installation
        {
            Name = "UAntwerpen",
            PostalCode = 2000,
            Street = "Prinsstraat",
            StreetNumber = 13,
            Sessions = new List<Session>()
        };

        var installation2 = new Installation
        {
            Name = "KdG Groenplaats",
            PostalCode = 2000,
            Street = "Nationalestraat",
            StreetNumber = 5,
            Sessions = new List<Session>()
        };

        var session1 = new Session
        {
            StartDate = new DateTime(2024, 5, 6).ToUniversalTime(),
            EndDate = new DateTime(2024, 7, 30).ToUniversalTime(),
            SessionType = SessionType.prive,
            Installation = new Installation()
        };

        var session2 = new Session
        {
            StartDate = new DateTime(2024, 5, 6).ToUniversalTime(),
            EndDate = new DateTime(2024, 7, 30).ToUniversalTime(),
            SessionType = SessionType.semipubliek,
            Installation = new Installation(),
            Participations = new List<Participation>()
        };

        var participation1 = new Participation
        {
            Duration = new TimeSpan(8, 10, 0),
            AmountOfParticipants = 1,
            Session = new Session(),
            Flow = new Flow()
        };

        var participation2 = new Participation
        {
            Duration = new TimeSpan(10, 15, 10),
            AmountOfParticipants = 1,
            Session = new Session(),
            Flow = new Flow()
        };

        var participation3 = new Participation
        {
            Duration = new TimeSpan(12, 11, 30),
            AmountOfParticipants = 1,
            Session = new Session(),
            Flow = new Flow()
        };

        var participation4 = new Participation
        {
            Duration = new TimeSpan(12, 22, 45),
            AmountOfParticipants = 1,
            Session = new Session(),
            Flow = new Flow()
        };

        // Create some Posts
        var post1 = new Post
        {
            Title = "Nieuwe thema: Sport",
            Text = "Ik denk dat thema rond sport een interessante onderwerp zou maken om aan jongeren te vragen.",
            Theme = th1
        };
        var post2 = new Post
            { Title = "Uitgave", Text = "Wanneer zal de phygital tool uitkomen in Brussel?", Theme = th2 };

        // Create some Reactions
        var reaction1 = new Reaction { Content = "Klinkt als een goed idee eigenlijk!" };
        var reaction2 = new Reaction { Content = "Waarschijnlijk eind juli" };

        // Create some Likes
        var like1 = new Like { Reaction = reaction1, Timestamp = DateTime.UtcNow, LikeType = LikeType.ThumbsUp };
        var like2 = new Like { Reaction = reaction2, Timestamp = DateTime.UtcNow, LikeType = LikeType.ThumbsUp };

        // Create some PostReactions
        var postReaction1 = new PostReaction { Post = post1, Reaction = reaction1, Timestamp = DateTime.UtcNow };
        var postReaction2 = new PostReaction { Post = post2, Reaction = reaction2, Timestamp = DateTime.UtcNow };

        // Create some PostLikes
        var postLike1 = new PostLike { Post = post1, Like = like1, Timestamp = DateTime.UtcNow, IsLiked = true };
        var postLike2 = new PostLike { Post = post2, Like = like2, Timestamp = DateTime.UtcNow, IsLiked = true };


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
        f1.FlowElements.Add(i5);
        f1.FlowElements.Add(i6);

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

        //Adding sessions to the installation
        installation1.Sessions = new List<Session> { session1, session2 };

        //Adding participations to the session
        session1.Participations = new List<Participation> { participation1, participation2 };
        session2.Participations = new List<Participation> { participation3, participation4 };

        //Adding flows to the participation
        f1.Participations = new List<Participation> { participation1, participation2 };
        f2.Participations = new List<Participation> { participation3, participation4 };


        /////////////////////////////////////////
        // Third part: adding to the Database //
        ////////////////////////////////////////

        // Adding flows
        context.Flows.Add(f1);
        context.Flows.Add(f2);

        th1.FlowElements = new List<FlowElement>
            { q1, q2, q3, q4, q6, q7, q8, q9, q10, q11, q12, q13, q14, i1, i2, i3, i4, i5, i6 };

        // Adding themes
        context.Themas.Add(th1);
        context.Themas.Add(th2);

        // adding FlowElements
        context.FlowElements.AddRange(new FlowElement[]
            { q1, q2, q3, q4, q6, q7, q8, q9, q10, q11, q12, q13, q14, i1, i2, i3, i4, i5, i6 });
        // adding options
        context.AddRange(new Option[]
        {
            o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, o16, o17, o18, o19, o20, o21, o22, o23,
            o24, o25, o26, o27, o28, o29
        });

        // Adding answers
        context.AddRange(new Answer[] { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12 });

        // Adding installations
        context.Installations.Add(installation1);
        context.Installations.Add(installation2);

        // Adding sessions
        context.Sessions.Add(session1);
        context.Sessions.Add(session2);

        // Adding participations
        context.Participations.Add(participation1);
        context.Participations.Add(participation2);

        // Add to the Posts collection
        context.Posts.AddRange(new Post[] { post1, post2 });

        // Add to the Reactions collection
        context.Reactions.AddRange(new Reaction[] { reaction1, reaction2 });

        // Add to the Likes collection
        context.Likes.AddRange(new Like[] { like1, like2 });

        // Add to the PostReactions collection
        context.PostReactions.AddRange(new PostReaction[] { postReaction1, postReaction2 });

        // Add to the PostLikes collection
        context.PostLikes.AddRange(new PostLike[] { postLike1, postLike2 });


        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}