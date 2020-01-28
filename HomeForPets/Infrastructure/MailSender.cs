using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace HomeForPets.Infrastructure
{
    public enum MailType
    {
        Accept,
        Deny
    }

    public static class MailSender
    {
        public static void Send(string fromEmail, string fromSenderName, string toEmail, string[] comment, MailType type)
        {
            MailAddress from = new MailAddress(fromEmail, fromSenderName);
            MailAddress to = new MailAddress(toEmail);

            MailMessage message = new MailMessage(from, to)
            {
                Subject = "Администрация"
            };

            if (type == MailType.Deny)
            {
                message.Body = "К сожалению, вынуждены сообщить вам, что в регистрации было отказано по причине: " + comment[0];
            }
            else if(type == MailType.Accept)
            {
                message.Body = "Поздравляем! Ваша заявка одобрена. Ваш логин: " 
                    + comment[0] + ", ваш пароль: " + comment[1];
            }

            SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
            client.Credentials = new NetworkCredential("elizavetaaleshkevitch@yandex.ru", "rtvthjdj0227");
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}