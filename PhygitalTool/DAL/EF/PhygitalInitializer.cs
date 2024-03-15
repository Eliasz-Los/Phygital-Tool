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
        // Info opvullen
        Info i1 = new Text { Content = "Dit is een tekst" };
        Info i2 = new Text { Content = "Dit is een andere tekst" };

        // Vragen opvullen
        SingleChoiceQuestion q1 = new SingleChoiceQuestion { Text = "Wat is je favoriete partij?", Active = true, SequenceNumber = 1 };
        SingleChoiceQuestion q2 = new SingleChoiceQuestion() { Text = "Bent u voor of tegen: BTW van 6 procent op elektriciteit?", Active = true, SequenceNumber = 2 };
        SingleChoiceQuestion q3 = new SingleChoiceQuestion() { Text = "Bent u voor of tegen: Gratis anticonceptie voor vrouwen tot 25 jaar?", Active = true, SequenceNumber = 3 };
        
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

        // Theme's opvullen
        Theme th1 = new Theme { Title = "Politiek" , Description = "Simpele vragen rond politiek"};
        Theme th2 = new Theme { Title = "Vakantie" , Description = "Simpele vragen rond vakantie"};

        // Flows opmaken
        var f1 = new Flow
        {
            FlowType = Flowtype.linear,
            IsOpen = true,
        };
        
        var f2 = new Flow
        {
            FlowType = Flowtype.circular,
            IsOpen = true,
        };

        // Relaties leggen
        i1.SubTheme = th1;
        i2.SubTheme = th2;
        
        // Test flow
        f1.Theme = th1;
        f1.SingleChoiceQuestions = new List<SingleChoiceQuestion> { q1, q2, q3 };
        f1.Answers = new List<Answer> { a1, a2, a3, a4, a5, a6, a7, a8, a9 };
        f1.Texts = new List<Text> { (Text)i1, (Text)i2 };
        
        q1.Answers = new List<Answer> { a1, a2, a3, a4, a5, a6, a7 };
        q2.Answers = new List<Answer> { a8, a9 };
        q3.Answers = new List<Answer> { a8, a9 };
        
        // context => database
        context.Flows.Add(f1);
        context.Flows.Add(f2);
        
        context.Themas.Add(th1);
        context.Themas.Add(th2);
        
        context.Infos.Add(i1);
        context.Infos.Add(i2);
        
        context.Questions.Add(q1);
        context.Questions.Add(q2);
        context.Questions.Add(q3);
        
        context.Answers.Add(a1);
        context.Answers.Add(a2);
        context.Answers.Add(a3);
        context.Answers.Add(a4);
        context.Answers.Add(a5);
        context.Answers.Add(a6);
        context.Answers.Add(a7);
        context.Answers.Add(a8);
        context.Answers.Add(a9);
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}