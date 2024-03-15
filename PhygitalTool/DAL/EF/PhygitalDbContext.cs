using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Session;
using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class PhygitalDbContext : DbContext
{
    // Questionsprocess package
    public DbSet<Flow> Flows { get; set; }
    public DbSet<FlowElement> FlowElements { get; set; }
    public DbSet<Answer> Answers { get; set; }
    
    // Misschien questions niet nodig omdat abstract
    public DbSet<Question> Questions { get; set; }
    public DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    public DbSet<RangeQuestion> RangeQuestions { get; set; }
    public DbSet<OpenQuestion> OpenQuestions { get; set; }
    public DbSet<MultipleChoice> MultipleChoices { get; set; }
    public DbSet<Option> Options { get; set; }
    
    public DbSet<Image> Images { get; set; }
    public DbSet<Text> Texts { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Info> Infos { get; set; }

    // Session package
    public DbSet<Participation> Participations { get; set; }

    // Themas package
    public DbSet<Theme> Themas { get; set; }

    public PhygitalDbContext(DbContextOptions options) : base(options)
    {
        PhygitalInitializer.Initialize(this, true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // optionsBuilder.UseNpgsql("Data Source=Phygital.db");
            optionsBuilder.UseSqlite("Data Source=Phygital.db");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Questionsprocess package
        modelBuilder.Entity<Flow>().ToTable("Flow").HasIndex(flow => flow.Id).IsUnique();
        modelBuilder.Entity<Info>().ToTable("Infos").HasIndex(info => info.Id).IsUnique();
        // Misschien questions niet nodig omdat abstract
        modelBuilder.Entity<Question>().ToTable("Questions").HasIndex(questions => questions.Id).IsUnique();
        modelBuilder.Entity<Answer>().ToTable("Answers").HasIndex(answer => answer.Id).IsUnique();
        modelBuilder.Entity<Option>().ToTable("Options").HasIndex(option => option.Id).IsUnique();
        
        // Session package
        modelBuilder.Entity<Participation>().ToTable("Participation").HasIndex(participation => participation.Id).IsUnique();
        
        // Themas package
        modelBuilder.Entity<Theme>().ToTable("Theme").HasIndex(thema => thema.Id).IsUnique();
        
        // Relations
        //one flow has many flowelements 
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Answers)
            .WithOne(answer => answer.Flow)
            .HasForeignKey("flowId");
        
        // Question elements
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.SingleChoiceQuestions)
            .WithOne(question => question.Flow)
            .HasForeignKey("flowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.RangeQuestions)
            .WithOne(question => question.Flow)
            .HasForeignKey("flowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.OpenQuestions)
            .WithOne(question => question.Flow)
            .HasForeignKey("flowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.MultipleChoices)
            .WithOne(question => question.Flow)
            .HasForeignKey("flowId");
        
        // Info elementen
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Images)
            .WithOne(image => image.Flow)
            .HasForeignKey("flowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Videos)
            .WithOne(video => video.Flow)
            .HasForeignKey("flowId");

        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Texts)
            .WithOne(text => text.Flow)
            .HasForeignKey("flowId");

        // one question has one or many options
        modelBuilder.Entity<SingleChoiceQuestion>()
            .HasMany(q => q.Options)
            .WithOne(a => (SingleChoiceQuestion)a.Question)
            .HasForeignKey("optionId");

        modelBuilder.Entity<MultipleChoice>()
            .HasMany(m => m.Options)
            .WithOne(q => (MultipleChoice)q.Question)
            .HasForeignKey("optionId");

        modelBuilder.Entity<OpenQuestion>()
            .HasOne(q => q.Answer)
            .WithOne(a => (OpenQuestion)a.Question)
            .HasForeignKey<Answer>(a => a.Id);
        
        modelBuilder.Entity<RangeQuestion>()
            .HasMany( q => q.Options)
            .WithOne( a => (RangeQuestion)a.Question)
            .HasForeignKey("optionId");
        
        
        // one flow has many participations
        modelBuilder.Entity<Flow>()
            .HasMany(f => f.Participations)
            .WithOne(p => p.flow);
        
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.flow)
            .WithMany(f => f.Participations);
        
        // one thema can be used in multiple flows
        modelBuilder.Entity<Theme>()
            .HasMany(t => t.Flows)
            .WithOne(f => f.Theme);
        
        modelBuilder.Entity<Flow>()
            .HasOne(f => f.Theme)
            .WithMany(t => t.Flows);
        
        // one subthema can be used in multiple flow elements 
        modelBuilder.Entity<Theme>()
            .HasMany(t => t.FlowElements)
            .WithOne(fe => fe.SubTheme);
        
        // modelBuilder.Entity<FlowElement>()
        //     .HasOne(t => t.SubTheme)
        //     .WithMany(fe => fe.FlowElements);
    }
}