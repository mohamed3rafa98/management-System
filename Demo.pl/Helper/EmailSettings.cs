using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public class EmailSettings
    {
        public static void Email ( Email email)
        {
            var client = new SmtpClient(host: "smtp.gmail.com", port: 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(userName: "marafa086@gmail.com", password: "bozfoaglztxtmrwn");
            client.Send("marafa086@gmail.com",email.Recipient, email.Subject, email.Body);

        }
    }
}
