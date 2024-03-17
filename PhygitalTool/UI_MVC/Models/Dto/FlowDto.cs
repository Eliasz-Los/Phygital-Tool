using Phygital.Domain.Datatypes;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class FlowDto
{
    public long Id { get; set; }
    public Flowtype FlowType { get; set; }
    public Theme Theme { get; set; }
}