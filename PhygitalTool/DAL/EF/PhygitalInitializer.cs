using DAL.EF;
using Domain;
using Domain.Datatypes;
using Domain.Questionsprocess;
using Domain.Themas;
using Phygital.Domain.Questionsprocess;

namespace Phygital.DAL.EF;

public class PhygitalInitializer
{
    private static bool _hasBeenInitialized = false;
    
    public static void Initialize(PhyticalDbContext context, bool dropDatabase = false)
    {
        if (!_hasBeenInitialized)
        {
            if(dropDatabase)
                context.Database.EnsureDeleted();
            
            if (context.Database.EnsureCreated())
                Seed(context);
            
            _hasBeenInitialized = true;
        }
    }
    
    
    private static void Seed(PhyticalDbContext context)
    {
        // Info opvullen
        Text t1 = new Text { Content = "Dit is een tekst" };
        Text t2 = new Text { Content = "Dit is een andere tekst" };
        
        Info i1 = new Info { Title = "Jeugdverkiezingen" };
        Info i2 = new Info { Title = "Vakantie" };
        
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
         Flow f1 = new Flow { FlowType = Flowtype.linear, IsOpen = true };
    
    
        // Relaties leggen

        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
    
}