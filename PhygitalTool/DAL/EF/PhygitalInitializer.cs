using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class PhygitalInitializer
{
    private static bool _hasBeenInitialized = false;

    public static void Initialize(PhyticalDbContext context, bool dropDatabase = false)
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


    private static void Seed(PhyticalDbContext context)
    {
        // Info opvullen

        Info i1 = new Text { Content = "Dit is een tekst" };
        Info i2 = new Text { Content = "Dit is een andere tekst" };

        // Vragen opvullen
        Question q1 = new Question { Questiontype = Questiontype.singlechoice, Text = "Wat is je favoriete partij?", Active = true, SequenceNumber = 1 };
        Question q2 = new Question { Questiontype = Questiontype.singlechoice, Text = "Wat is je favoriete vakantiebestemming?", Active = true, SequenceNumber = 1 };
        Question q3 = new Question { Questiontype = Questiontype.singlechoice, Text = "Welke kleur maakt geel en blauw?", Active = true, SequenceNumber = 1 };
        // Antwoorden opvullen
        Answer a1 = new Answer { Text = "CD&V" };
        Answer a2 = new Answer { Text = "Spanje" };
        Answer a3 = new Answer { Text = "Groen" };

        // Thema's opvullen
        Thema th1 = new Thema { Title = "Politiek" , Description = "Simpele vragen rond politiek"};
        Thema th2 = new Thema { Title = "Vakantie" , Description = "Simpele vragen rond vakantie"};

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
        i1.SubThema = th1;
        i2.SubThema = th2;
        f1.Thema = th1;
        //f1.FlowElements = new List<FlowElement> { i1,i2, q1, q2, q3 , a1, a2, a3 };
        f1.Questions = new List<Question> { q1, q2, q3 };
        f1.Answers = new List<Answer> { a1, a2, a3 };
        f1.Texts = new List<Text> { (Text)i1, (Text)i2 };
        
        context.Flows.Add(f1);
        context.Flows.Add(f2);
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}