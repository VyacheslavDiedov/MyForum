using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace MyForum
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("My forum", "my_forum_register@ukr.net"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync ("smtp.ukr.net", 465, true);
                await client.AuthenticateAsync("my_forum_register@ukr.net", "f9nvhpPRcvaQZkE1");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }


    }
}
