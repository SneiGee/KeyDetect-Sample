using System.Globalization;
using System.Web;

namespace Application.Common.Helpers;

public static class URLBuilder
{
    public enum MessageType
    {
        ConfirmEmail = 0,
        ResetPassword = 1,
        WelcomeMail = 2
    };

    public static string BuildUrl(string urlPath, string token, string userId)
    {
        var uriBuilder = new UriBuilder(urlPath);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["token"] = token;
        query["userid"] = userId;
        uriBuilder.Query = query.ToString();
        return uriBuilder.ToString();
    }

    public static string BuildContext(MessageType messageType, string link, string firstName)
    {
        string templatePath = string.Empty;
        string templateSubject = string.Empty;

        switch (messageType)
        {
            case MessageType.ConfirmEmail:
                templatePath = "ConfirmEmail.cshtml";
                templateSubject = "Confirm your email address";
                break;
            case MessageType.ResetPassword:
                templatePath = "ResetPassword.cshtml";
                templateSubject = "Reset your password";
                break;
            case MessageType.WelcomeMail:
                templatePath = "WelcomeMail.cshtml";
                templateSubject = "Welcome to our website!";
                break;
            default:
                throw new ArgumentException("Invalid message type.");
        }

        string templateFullPath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", templatePath);
        string templateBody = File.ReadAllText(templateFullPath);

        templateBody = templateBody.Replace("{{FirstName}}", CultureInfo.InvariantCulture.TextInfo.ToTitleCase(firstName));
        templateBody = templateBody.Replace("{{Link}}", link);

        return templateBody;
    }
}