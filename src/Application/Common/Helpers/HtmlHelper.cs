namespace Synopsis.Helpers;

public static class HtmlHelper
{
    public static string GetFeedbackHtmlMessage(string email, string body)
    {
        return $"New order {email} <br>Message: <br>\"{body}\"";
    }
}