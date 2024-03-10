using Phygital.Domain.Questionsprocess;

namespace Phygital.UI_CA.Extension;

public static class FlowExtension
{
    public static String StringRepresentation(this Flow flow)
    {
        var text = "";
        text += $"{flow.Id} - {flow.FlowType,-13} - {flow.IsOpen,-10}";
        return text;
    }
}