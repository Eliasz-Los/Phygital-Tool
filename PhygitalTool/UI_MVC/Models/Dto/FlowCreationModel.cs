namespace Phygital.UI_MVC.Models.Dto;

public class FlowCreationModel
{
        public string FlowType { get; set; }
        public bool IsOpen { get; set; }
        public long ThemeId { get; set; } //zo verwacht het geen hele Theme objecten maar enkel de id en dan in controller verder verbinden
}