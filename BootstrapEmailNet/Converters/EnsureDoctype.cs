namespace BootstrapEmail.Net.Converters;

public class EnsureDoctype
{
    public static string Replace(string html)
    {
        return html.Replace("<html><head>", "\r\n<html>\r\n  <head>");

        // TODO : ensure the proper XHTML doctype which ensures best compatibility in email clients
        // https://github.com/bootstrap-email/bootstrap-email/discussions/168
        // return Regex.Replace(html, @"<!DOCTYPE.*(\[[\s\S]*?\])?>", @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");
    }
}