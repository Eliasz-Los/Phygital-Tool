using Phygital.Domain.Datatypes;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class FlowDto
{
    public long Id { get; set; }
    public Flowtype FlowType { get; set; }
    public bool IsOpen { get; set; }
    public long ThemeId { get; set; } //zo verwacht het geen hele Theme objecten maar enkel de id en dan in controller verder verbinden
}
