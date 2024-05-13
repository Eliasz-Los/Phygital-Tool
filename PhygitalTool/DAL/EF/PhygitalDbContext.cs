using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Session;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;
using Phygital.Domain.User;
using Version = Phygital.Domain.Subplatform.Version;

namespace Phygital.DAL.EF;

public class PhygitalDbContext : IdentityDbContext<IdentityUser> // DbContext
{
    // Accounts package
    // public DbSet<Account> Accounts { get; set; }
    
    // Questionsprocess package
    public DbSet<Flow> Flows { get; set; }
    public DbSet<FlowElement> FlowElements { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<SingleChoiceQuestion> SingleChoiceQuestions { get; set; }
    public DbSet<RangeQuestion> RangeQuestions { get; set; }
    public DbSet<OpenQuestion> OpenQuestions { get; set; }
    public DbSet<MultipleChoice> MultipleChoices { get; set; }
    public DbSet<Option> Options { get; set; }
    
    public DbSet<Image> Images { get; set; }
    public DbSet<Text> Texts { get; set; }
    public DbSet<Video> Videos { get; set; }

    // Session package
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Installation> Installations { get; set; }
    public DbSet<Note> Notes { get; set; }
    
    // Thema package
    public DbSet<Theme> Themas { get; set; }
    
    // Subplatform package
    public DbSet<Project> Projects { get; set; }
    public DbSet<Version> Versions { get; set; }

    public PhygitalDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(connectionString:"Phygital.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
        
        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //////////////////////
        // Accounts package //
        //////////////////////
        // modelBuilder.Entity<IdentityUser>().ToTable("Accounts").HasIndex(user => user.Id).IsUnique();
        
        ///////////////////////////////
        // Questionsprocess package //
        //////////////////////////////
        modelBuilder.Entity<Flow>().ToTable("Flow").HasIndex(flow => flow.Id).IsUnique();
        modelBuilder.Entity<Answer>().ToTable("Answers").HasIndex(answer => answer.Id).IsUnique();
        modelBuilder.Entity<Option>().ToTable("Options").HasIndex(option => option.Id).IsUnique();
        
        //flowelement, info and Question is abstract
        modelBuilder.Entity<FlowElement>().ToTable("FlowElement").HasKey(fe => fe.Id);
        modelBuilder.Entity<Info>().ToTable("Infos").HasBaseType<FlowElement>();
        modelBuilder.Entity<Video>().ToTable("Videos").HasBaseType<Info>();
        modelBuilder.Entity<Image>().ToTable("Images").HasBaseType<Info>();
        modelBuilder.Entity<Text>().ToTable("Texts").HasBaseType<Info>();
        
        modelBuilder.Entity<Question>().ToTable("Questions").HasBaseType<FlowElement>();
        modelBuilder.Entity<MultipleChoice>().ToTable("MultipleChoices").HasBaseType<Question>();
        modelBuilder.Entity<SingleChoiceQuestion>().ToTable("SingleChoiceQuestions").HasBaseType<Question>();
        modelBuilder.Entity<RangeQuestion>().ToTable("RangeQuestions").HasBaseType<Question>();
        modelBuilder.Entity<OpenQuestion>().ToTable("OpenQuestions").HasBaseType<Question>();
        
        /////////////////////
        // Session package //
        /////////////////////
        modelBuilder.Entity<Participation>().ToTable("Participation").HasIndex(participation => participation.Id).IsUnique();
        modelBuilder.Entity<Note>().ToTable("Note").HasIndex(note => note.Id).IsUnique();
        modelBuilder.Entity<Session>().ToTable("Session").HasIndex(session => session.Id).IsUnique();
        modelBuilder.Entity<Installation>().ToTable("Installation").HasIndex(installation => installation.Id).IsUnique();
        
        // one particpation has many notes
        modelBuilder.Entity<Participation>()
            .HasMany(n => n.Notes)
            .WithOne(p => p.Participation)
            .HasForeignKey("participationId");
        modelBuilder.Entity<Note>()
            .HasOne(n => n.Participation)
            .WithMany(f => f.Notes)
            .HasForeignKey("participationId");
        
        // one session has many participations
        modelBuilder.Entity<Session>()
            .HasMany(s => s.Participations)
            .WithOne(p => p.Session)
            .HasForeignKey("sessionId");
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Session)
            .WithMany(s => s.Participations)
            .HasForeignKey("sessionId");
        
        // one installation has many sessions
        modelBuilder.Entity<Installation>()
            .HasMany(i => i.Sessions)
            .WithOne(s => s.Installation)
            .HasForeignKey("installationId");
        modelBuilder.Entity<Session>()
            .HasOne(s => s.Installation)
            .WithMany(i => i.Sessions)
            .HasForeignKey("installationId");
        
        ////////////////////
        // Themes package //
        ////////////////////
        modelBuilder.Entity<Theme>().ToTable("Theme").HasIndex(thema => thema.Id).IsUnique();
        
        /////////////////////////
        // Subplatform package //
        /////////////////////////
        modelBuilder.Entity<Project>().ToTable("Projects").HasIndex(project => project.Id).IsUnique();
        modelBuilder.Entity<Version>().ToTable("Versions").HasIndex(version => version.Id).IsUnique();
        
        // Relations
        //one flow has many flowelements 
        modelBuilder.Entity<Flow>()
            .HasMany(f => f.FlowElements)
            .WithOne(fe => fe.Flow)
            .HasForeignKey("flowId");
        modelBuilder.Entity<FlowElement>()
            .HasOne(fe => fe.Flow)
            .WithMany(f => f.FlowElements)
            .HasForeignKey("flowId");
        //theme linking
        modelBuilder.Entity<FlowElement>()
            .HasOne(q => q.SubTheme)
            .WithMany(s => s.FlowElements);
        
        // one flow has many answers
        modelBuilder.Entity<Flow>()
            .HasMany(flow => flow.Answers)
            .WithOne(answer => answer.Flow)
            .HasForeignKey("flowId");
        modelBuilder.Entity<Answer>()
            .HasOne(answer => answer.Flow)
            .WithMany(flow => flow.Answers)
            .HasForeignKey("flowId");
        
        // one flow has many participations
        modelBuilder.Entity<Flow>()
            .HasMany(f => f.Participations)
            .WithOne(p => p.Flow)
            .HasForeignKey("flowId");
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Flow)
            .WithMany(f => f.Participations)
            .HasForeignKey("flowId");
        
        // one theme can be used in multiple flows
        modelBuilder.Entity<Theme>()
            .HasMany(t => t.Flows)
            .WithOne(f => f.Theme);
        modelBuilder.Entity<Flow>()
            .HasOne(f => f.Theme)
            .WithMany(t => t.Flows);
        
        // one subtheme can be used in multiple flow elements 
        modelBuilder.Entity<Theme>()
            .HasMany(t => t.FlowElements)
            .WithOne(fe => fe.SubTheme);
        
        modelBuilder.Entity<FlowElement>()
            .HasOne(fe => fe.SubTheme)
            .WithMany(t => t.FlowElements);
        
        //  Questions //
        // SingleChoiceQuestion has many options
        modelBuilder.Entity<SingleChoiceQuestion>()
            .HasMany(scq => scq.Options)
            .WithOne(o => o.SingleChoiceQuestion);
        modelBuilder.Entity<Option>()
            .HasOne(o => o.SingleChoiceQuestion)
            .WithMany(scq => scq.Options);
        // RangeQuestion has many options
        modelBuilder.Entity<RangeQuestion>()
            .HasMany(rq => rq.Options)
            .WithOne(o => o.RangeQuestion);
        modelBuilder.Entity<Option>()
            .HasOne(o => o.RangeQuestion)
            .WithMany(r => r.Options);
        // MultipleChoice has many options
        modelBuilder.Entity<MultipleChoice>()
            .HasMany(mc => mc.Options)
            .WithOne(o => o.MultipleChoice);
        modelBuilder.Entity<Option>()
            .HasOne(o => o.MultipleChoice)
            .WithMany(mc => mc.Options);
        // OpenQuestion has one answer
        modelBuilder.Entity<OpenQuestion>()
            .HasOne(oq => oq.Answer)
            .WithOne(a => a.OpenQuestion)
            .HasForeignKey<Answer>("openQuestionId");
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.OpenQuestion)
            .WithOne(oq => oq.Answer)
            .HasForeignKey<OpenQuestion>("answerId");
        
        // Answers has many options chosen by user
        modelBuilder.Entity<Answer>()
            .HasMany(a => a.ChosenOptions)
            .WithOne(o => o.Answer);
        modelBuilder.Entity<Option>()
            .HasOne(o => o.Answer)
            .WithMany(a => a.ChosenOptions);

        
        // Subplatform
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Versions)
            .WithOne(v => v.Project);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Flows)
            .WithOne(f => f.Project);

        modelBuilder.Entity<Flow>()
            .HasOne(f => f.Project)
            .WithMany(p => p.Flows);

        modelBuilder.Entity<Version>()
            .HasOne(v => v.Project)
            .WithMany(p => p.Versions);
    }
    public bool CreateDatabase(bool dropExisting = true)
    {
        if (dropExisting)
        {
            Database.EnsureDeleted();
        }

        return Database.EnsureCreated();
    }
}