using System.ComponentModel.DataAnnotations;

namespace Domain.Thema;

public class Thema
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Thema> SubThemas { get; set; }

    public Thema(string title, string description, ICollection<Thema> subThemas)
    {
        Title = title;
        Description = description;
        SubThemas = subThemas;
    }
}