using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.UI_CA.Extension;

public static class SCQExtension
{
    public static String StringRepresentation(this SingleChoiceQuestion scq)
    {
        var text = "";
        text += $"{scq.Id} - {scq.Text,-13} - {scq.Active,-10} - {scq.SequenceNumber} - {scq.Options}";
        return text;
    }
}