namespace Domain.Questionsprocess;

public class Image : Info
{
    public long Id { get; set; }
    
    // TODO image zelf met een url?
    
    public string AltText { get; set; }

    public Image(string altText)
    {
        AltText = altText;
    }
}