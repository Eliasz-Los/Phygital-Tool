namespace Phygital.UI_MVC.Models.Dto;

// This class is needed for a postrequest in the projectscontroller
public class ProjectCreationModel
{
    public string Name { get; set; }
    public SubThemasDto MainTheme { get; set; }
    public List<SubThemasDto> Themas { get; set; }
}
