namespace Validadores;

public class ValidadorEmail
{
    private readonly string email;

    public ValidadorEmail(string email)
    {
        this.email = email;
    }
    public bool EsEmailValido()
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}