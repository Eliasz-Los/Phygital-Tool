using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Session;
using Phygital.Domain.Themas;

namespace Phygital.DAL.EF;

public class PhyticalDbContext : DbContext
{
    // Questionsprocess package
    public DbSet<Flow> Flows { get; set; }
    public DbSet<FlowElement> FlowElements { get; set; }
    
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Text> Texts { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Info> Infos { get; set; }

    // Session package
    public DbSet<Participation> Participations { get; set; }

    // Themas package
    public DbSet<Thema> Themas { get; set; }

    public PhyticalDbContext(DbContextOptions options) : base(options)
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
        modelBuilder.Entity<Flow>().ToTable("Flow");
        modelBuilder.Entity<Flow>().HasIndex(flow => flow.Id).IsUnique();
        
        modelBuilder.Entity<Info>().ToTable("Infos").HasIndex(info => info.Id).IsUnique();
        modelBuilder.Entity<Question>().ToTable("Questions").HasIndex(questions => questions.Id).IsUnique();
        modelBuilder.Entity<Answer>().ToTable("Answers").HasIndex(answer => answer.Id).IsUnique();
        
        // Session package
        modelBuilder.Entity<Participation>().ToTable("Participation");
        modelBuilder.Entity<Participation>().HasIndex(participation => participation.Id).IsUnique();
        
        // Themas package
        modelBuilder.Entity<Thema>().ToTable("Thema");
        modelBuilder.Entity<Thema>().HasIndex(thema => thema.Id).IsUnique();
        
        // Relations
        //one flow has many flowelements 
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Answers)
            .WithOne(answer => answer.Flow)
            //not sure about this one 
            .HasForeignKey("flowId");
        
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Questions)
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

        // one on one Question - Answer
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Answer)
            .WithOne(a => a.Question)
            //We need to specify the foreign key because the Answer class does not have a QuestionId property
            .HasForeignKey<Question>("questionId");
        
        // one flow has many participations
        modelBuilder.Entity<Flow>()
            .HasMany(f => f.Participations)
            .WithOne(p => p.flow);
        
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.flow)
            .WithMany(f => f.Participations);
        
        // one thema can be used in multiple flows
        modelBuilder.Entity<Thema>()
            .HasMany(t => t.Flows)
            .WithOne(f => f.Thema);
        
        modelBuilder.Entity<Flow>()
            .HasOne(f => f.Thema)
            .WithMany(t => t.Flows);
        
        // one subthema can be used in multiple flow elements 
        modelBuilder.Entity<Thema>()
            .HasMany(t => t.FlowElements)
            .WithOne(fe => fe.SubThema);
        
        modelBuilder.Entity<FlowElement>()
            .HasOne(t => t.SubThema)
            .WithMany(fe => fe.FlowElements);
    }
}