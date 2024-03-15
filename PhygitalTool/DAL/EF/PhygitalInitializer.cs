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
        
        // Filling answers
        Option o1 = new Option { OptionText = "CD&V" };
        Option o2 = new Option { OptionText = "Vooruit" };
        Option o3 = new Option { OptionText = "NV-A" };
        Option o4 = new Option { OptionText = "Groen" };
        Option o5 = new Option { OptionText = "PVDA" };
        Option o6 = new Option { OptionText = "Open-VLD" };
        Option o7 = new Option { OptionText = "Vlaams Belang" };
        
        Option o8 = new Option { OptionText = "Voor" };
        Option o9 = new Option { OptionText = "Tegen" };
        Option o10 = new Option { OptionText = " " };
        
        // Kan brol zijn maar is effe nodig voor testdate
        Answer a1 = new Answer { Text = "CD&V" };                
        Answer a2 = new Answer { Text = "Vooruit" };             
        Answer a3 = new Answer { Text = "NV-A" };                
        Answer a4 = new Answer { Text = "Groen" };               
        Answer a5 = new Answer { Text = "PVDA" };                
        Answer a6 = new Answer { Text = "Open-VLD" };            
        Answer a7 = new Answer { Text = "Vlaams Belang" };       
                                                                       
        Answer a8 = new Answer { Text = "Voor" };                
        Answer a9 = new Answer { Text = "Tegen" };               
        Answer a10 = new Answer { Text = "Geen mening " };                  

        // Vragen opvullen
        var q1 = new SingleChoiceQuestion
        {
            Text = "Wat is je favoriete partij?", 
            Active = true, SequenceNumber = 1, 
            Options = new List<Option>()
        };
        var q2 = new OpenQuestion()
        {
            Text = "Waarom kiest u voor deze partij?", 
            Active = true, SequenceNumber = 3,
            Answer = new Answer()
        };
        var q3 = new SingleChoiceQuestion
        {
            Text = "Bent u voor of tegen: BTW van 6 procent op elektriciteit?", 
            Active = true, SequenceNumber = 2, 
            Options = new List<Option>()
        };
        
        // Flows opmaken
        var f1 = new Flow
        {
            FlowType = Flowtype.linear,
            IsOpen = true,
            Theme = th1,
            SingleChoiceQuestions = new List<SingleChoiceQuestion>(),
            Answers = new List<Answer>()
        };
        
        var f2 = new Flow
        {
            FlowType = Flowtype.circular,
            IsOpen = true,
            Theme = th2
        };

        // In the second part of the seed method we create the relations between the different classes
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // flow 1 relations
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
        
       
        
        q1.Options.Add(o1);
        q1.Options.Add(o2);
        q1.Options.Add(o3);
        q1.Options.Add(o4);
        q1.Options.Add(o5);
        q1.Options.Add(o6);
        q1.Options.Add(o7);
        q2.Answer = a10;
        q3.Options.Add(o8);
        q3.Options.Add(o9);
        
        a1.Question = q1;
        a2.Question = q1;
        a3.Question = q1;
        a4.Question = q1;
        a5.Question = q1;
        a6.Question = q1;
        a7.Question = q1;
        a8.Question = q3;
        a9.Question = q3;
        a10.Question = q2;
        
        f1.SingleChoiceQuestions.Add(q1);
        f1.OpenQuestions.Add(q2);
        f1.SingleChoiceQuestions.Add(q3);
        q1.Flow = f1;
        q2.Flow = f1;
        q3.Flow = f1;
        
        i1.SubTheme = th1;
        i2.SubTheme = th2;
        
        // context => database
        context.Flows.Add(f1);
        context.Flows.Add(f2);
        
        context.Themas.Add(th1);
        context.Themas.Add(th2);
        
        context.Infos.Add(i1);
        context.Infos.Add(i2);
        
        context.Options.Add(o1);
        context.Options.Add(o2);
        context.Options.Add(o3);
        context.Options.Add(o4);
        context.Options.Add(o5);
        context.Options.Add(o6);
        context.Options.Add(o7);
        context.Options.Add(o8);
        context.Options.Add(o9);
        context.Options.Add(o10);
        
        context.SingleChoiceQuestions.Add(q1);
        context.OpenQuestions.Add(q2);
        context.SingleChoiceQuestions.Add(q3);
        
        context.Answers.Add(a1);
        context.Answers.Add(a2);
        context.Answers.Add(a3);
        context.Answers.Add(a4);
        context.Answers.Add(a5);
        context.Answers.Add(a6);
        context.Answers.Add(a7);
        context.Answers.Add(a8);
        context.Answers.Add(a9);
        context.Answers.Add(a10);
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}