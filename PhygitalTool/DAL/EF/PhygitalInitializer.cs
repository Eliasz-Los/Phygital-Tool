using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class PhygitalInitializer
{
    private static bool _hasBeenInitialized = false;

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
        // Theme's opvullen
        var th1 = new Theme { Title = "Politiek" , Description = "Simpele vragen rond politiek"};
        var th2 = new Theme { Title = "Vakantie" , Description = "Simpele vragen rond vakantie"};
        
        // Info opvullen
        var i1 = new Text
        {
            Content = "Dit is een tekst"
        };
        var i2 = new Text
        {
            Content = "Dit is een andere tekst"
        };
        
        // Antwoorden opvullen
        Answer a1 = new Answer { Text = "CD&V" };
        Answer a2 = new Answer { Text = "Vooruit" };
        Answer a3 = new Answer { Text = "NV-A" };
        Answer a4 = new Answer { Text = "Groen" };
        Answer a5 = new Answer { Text = "PVDA" };
        Answer a6 = new Answer { Text = "Open-VLD" };
        Answer a7 = new Answer { Text = "Vlaams Belang" };
        
        Answer a8 = new Answer { Text = "Voor" };
        Answer a9 = new Answer { Text = "Tegen" };
        Answer a10 = new Answer { Text = " " };

        // Vragen opvullen
        var q1 = new SingleChoiceQuestion
        {
            Text = "Wat is je favoriete partij?", 
            Active = true, SequenceNumber = 1, 
            Answers = new List<Answer>()
        };
        var q2 = new OpenQuestion
        {
            Text = "Waarom kiest u voor deze partij?", 
            Active = true, SequenceNumber = 3,
            Answer = new Answer()
        };
        var q3 = new SingleChoiceQuestion
        {
            Text = "Bent u voor of tegen: BTW van 6 procent op elektriciteit?", 
            Active = true, SequenceNumber = 2, 
            Answers = new List<Answer>()
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

        
        //TODO: Relaties fixen
        
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
        
        f1.SingleChoiceQuestions.Add(q1);
        f1.OpenQuestions.Add(q2);
        f1.SingleChoiceQuestions.Add(q3);
        
        q1.Flow = f1;
        q2.Flow = f1;
        q3.Flow = f1;
        
        q1.Answers.Add(a1);
        q1.Answers.Add(a2);
        q1.Answers.Add(a3);
        q1.Answers.Add(a4);
        q1.Answers.Add(a5);
        q1.Answers.Add(a6);
        q1.Answers.Add(a7);
        q2.Answer = a10;
        q3.Answers.Add(a8);
        q3.Answers.Add(a9);
        a1.SingleChoiceQuestion = q1;
        a2.SingleChoiceQuestion = q1;
        a3.SingleChoiceQuestion = q1;
        a4.SingleChoiceQuestion = q1;
        a5.SingleChoiceQuestion = q1;
        a6.SingleChoiceQuestion = q1;
        a7.SingleChoiceQuestion = q1;
        a8.SingleChoiceQuestion = q3;
        a9.SingleChoiceQuestion = q3;
        a10.OpenQuestion = q2;
        
        i1.SubTheme = th1;
        i2.SubTheme = th2;
        
        // context => database
        context.Flows.Add(f1);
        context.Flows.Add(f2);
        
        context.Themas.Add(th1);
        context.Themas.Add(th2);
        
        context.Infos.Add(i1);
        context.Infos.Add(i2);
        
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