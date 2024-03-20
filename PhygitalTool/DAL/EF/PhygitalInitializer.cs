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
        var th1 = new Theme { Title = "Politiek" , Description = "Simpele vragen rond politiek"};
        var th2 = new Theme { Title = "Vakantie" , Description = "Simpele vragen rond vakantie"};
        
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
        var q1 = new SingleChoiceQuestion
        {
            Text = "Wat is je favoriete partij?", 
            Active = true, SequenceNumber = 1, 
            Options = new List<Option>()
        };
        var q2 = new OpenQuestion
        {
            Text = "Waarom kiest u voor deze partij?", 
            Active = true, SequenceNumber = 2,
            Answer = new Answer()
        };
        var q3 = new SingleChoiceQuestion
        {
            Text = "Bent u voor of tegen: BTW van 6 procent op elektriciteit?", 
            Active = true, SequenceNumber = 3, 
            Options = new List<Option>()
        };

        var q4 = new RangeQuestion
        {
            Text = "Wat is uw mening over deze uitspraak: 'De regering moet meer investeren in de zorgsector'",
            Active = true, SequenceNumber = 4,
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
        
        //Option o10 = new Option { OptionText = "Geen mening" };
        
        // Kan brol zijn maar is effe nodig voor testdate
        var a1 = new Answer { Text = "CD&V"};                
        var a2 = new Answer { Text = "Vooruit" };             
        var a3 = new Answer { Text = "NV-A" };                
        var a4 = new Answer { Text = "Groen" };               
        var a5 = new Answer { Text = "PVDA" };                
        var a6 = new Answer { Text = "Open-VLD" };            
        var a7 = new Answer { Text = "Vlaams Belang" };       
        
        var a8 = new Answer { Text = "Voor" };                
        var a9 = new Answer { Text = "Tegen" };               
        var a10 = new Answer { Text = "Geen mening"};          
        
        

        // In the second part of the seed method we create the relations between the different classes
        /////////////////////////////////////////////////////////////////////////////////////////////////////
       
        // Adding questions & info to the flow
        f1.FlowElements.Add(q1);
        f1.FlowElements.Add(q2);
        f1.FlowElements.Add(q3);
        f1.FlowElements.Add(q4);
        
        f1.FlowElements.Add(i1);
        f1.FlowElements.Add(i2);
        
        // Adding options to the questions
        q1.Options.Add(o1);
        q1.Options.Add(o2);
        q1.Options.Add(o3);
        q1.Options.Add(o4);
        q1.Options.Add(o5);
        q1.Options.Add(o6);
        q1.Options.Add(o7);
        q3.Options.Add(o8);
        q3.Options.Add(o9);
        q2.Answer = a10;
        q4.Options.Add(o10); //zwaar tegen
        q4.Options.Add(o11); //tegen
        q4.Options.Add(o12); //neutraal
        q4.Options.Add(o13); //voor
        q4.Options.Add(o14); //zwaar voor
       
        
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
        context.FlowElements.AddRange( new FlowElement[] {q1,q2,q3,i1,i2});
       
        // adding options
        context.AddRange(new Option[] {o1,o2,o3,o4,o5,o6,o7,o8,o9,o10,o11,o12,o13,o14});
        
        // Adding answers
        context.AddRange(new Answer[] {a1,a2,a3,a4,a5,a6,a7,a8,a9,a10});
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}