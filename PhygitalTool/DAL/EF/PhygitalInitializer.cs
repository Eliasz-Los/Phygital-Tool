using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class PhygitalInitializer
{
    private static bool _hasBeenInitialized = false;

    // Initializing database
    public static void Initialize(PhygitalDbContext context, bool dropDatabase = false)
    {
        if (!_hasBeenInitialized)
        {
            if (dropDatabase)
                context.Database.EnsureDeleted();

            if (context.Database.EnsureCreated())
                Seed(context);

            _hasBeenInitialized = true;
        }
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
            Content = "Dit is een tekst"
        };
        var i2 = new Text
        {
            Content = "Dit is een andere tekst"
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

        var q5 = new MultipleChoice
        {
            Text = "Zijn er partijen waar je absoluut niet op zou stemmen?",
            Active = true,
            SequenceNumber = 5,
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
            Text = "Welke neutrale partij?",
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

        var a1 = new Answer { Text = "CD&V" };
        var a2 = new Answer { Text = "Vooruit" };
        var a3 = new Answer { Text = "NV-A" };
        var a4 = new Answer { Text = "Groen" };
        var a5 = new Answer { Text = "PVDA" };
        var a6 = new Answer { Text = "Open-VLD" };
        var a7 = new Answer { Text = "Vlaams Belang" };

        var a8 = new Answer { Text = "Voor" };
        var a9 = new Answer { Text = "Tegen" };
        var a10 = new Answer { Text = "Geen mening" };


// In the second part of the seed method we create the relations between the different classes
/////////////////////////////////////////////////////////////////////////////////////////////////////

// Adding questions & info to the flow
        f1.FlowElements.Add(q1);
        f1.FlowElements.Add(q2);
        f1.FlowElements.Add(q3);
        f1.FlowElements.Add(q4);
        f1.FlowElements.Add(q5);
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
        q5.Options.Add(o1);
        q5.Options.Add(o2);
        q5.Options.Add(o3);
        q5.Options.Add(o4);
        q5.Options.Add(o5);
        q5.Options.Add(o6);
        q5.Options.Add(o7);
        q6.Options.Add(o10); //zwaar tegen
        q6.Options.Add(o11); //tegen
        q6.Options.Add(o12); //neutraal
        q6.Options.Add(o13); //voor
        q6.Options.Add(o14);
        q7.Options.Add(o15); //te hoog
        q7.Options.Add(o16); //te laag
        q7.Options.Add(o17); //passend
        q8.Options.Add(o8);
        q8.Options.Add(o9);
        q9.Answer = a10;
        q10.Answer = a10;
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
        context.FlowElements.AddRange(new FlowElement[] { q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12, q13, q14, i1, i2 });

        // adding options
        context.AddRange(new Option[]
            { o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, o16, o17, o18, o19, o20, o21, o22, o23, o24});

        // Adding answers
        context.AddRange(new Answer[] { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 });

        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}