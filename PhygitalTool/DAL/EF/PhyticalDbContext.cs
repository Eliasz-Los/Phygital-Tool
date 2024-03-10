using Domain;
using Domain.Questionsprocess;
using Domain.Session;
using Domain.Themas;
using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;

namespace Phygital.DAL.EF;

public class PhyticalDbContext : DbContext
{
    // Questionsprocess package
    public DbSet<Flow> Flows { get; set; }
    public DbSet<FlowElement> FlowElements { get; set; }
    // public DbSet<Info> Infos { get; set; }
    // public DbSet<Question> Questions { get; set; }
    // public DbSet<Answer> Answers { get; set; }

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
            optionsBuilder.UseSqlite("Data Source=Phygital.db");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Questionsprocess package
        modelBuilder.Entity<Flow>().ToTable("Flow");
        modelBuilder.Entity<Flow>().HasIndex(flow => flow.Id).IsUnique();
        
        modelBuilder.Entity<FlowElement>().ToTable("FlowElement");
        modelBuilder.Entity<FlowElement>().HasIndex(flowElement => flowElement.Id).IsUnique();
        
        // modelBuilder.Entity<Info>().ToTable("Info");
        // modelBuilder.Entity<Info>().HasIndex(info => info.Id).IsUnique();
        //
        // modelBuilder.Entity<Question>().ToTable("Question");
        // modelBuilder.Entity<Question>().HasIndex(question => question.Id).IsUnique();
        //
        // modelBuilder.Entity<Answer>().ToTable("Answer");
        // modelBuilder.Entity<Answer>().HasIndex(answer => answer.Id).IsUnique();
        
        // Session package
        modelBuilder.Entity<Participation>().ToTable("Participation");
        modelBuilder.Entity<Participation>().HasIndex(participation => participation.Id).IsUnique();
        
        // Themas package
        modelBuilder.Entity<Thema>().ToTable("Thema");
        modelBuilder.Entity<Thema>().HasIndex(thema => thema.Id).IsUnique();
        
        // Relations
        // one flow has many flow elements
        modelBuilder.Entity<Flow>()
            .HasMany(f => f.FlowElements)
            .WithOne(fe => fe.Flow);
        
        modelBuilder.Entity<FlowElement>()
            .HasOne(fe => fe.Flow)
            .WithMany(f => f.FlowElements);
        
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