using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10MinuteMail
{
    class Program
    {
        static void Main(string[] args)
        {
            var mail = new TenMinuteMail();
            mail.Initialize();
            mail.Interval = 2000;
            mail.OnReceiveMail += Mail_OnReceiveMail;
            Console.WriteLine(mail.GetEmailAddress());
            mail.Start();

            Console.Read();
        }

        private static void Mail_OnReceiveMail(object sender, MailEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\New message !");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Attachment Count: {e.Mail.AttachmentCount}");
            Console.WriteLine("Attachments: ");

            foreach(var item in e.Mail.Attachments)
            {
                Console.WriteLine($"-{item}");
            }

            Console.WriteLine($"BodyPlainText: {e.Mail.BodyPlainText}");
            Console.WriteLine($"BodyPreview: {e.Mail.BodyPreview}");
            Console.WriteLine($"BodyText: {e.Mail.BodyText}");
            Console.WriteLine($"FormattedDate: {e.Mail.FormattedDate}");
            Console.WriteLine($"Forwarded: {e.Mail.Forwarded}");
            Console.WriteLine("FromList: ");

            foreach(var item in e.Mail.FromList)
            {
                Console.WriteLine($"-{item}");
            }

            Console.WriteLine($"ID: {e.Mail.ID}");
            Console.WriteLine($"PrimaryFromAddress: {e.Mail.PrimaryFromAddress}");
            Console.WriteLine($"Read: {e.Mail.Read}");
            Console.WriteLine("RecipientList: ");

            foreach(var item in e.Mail.RecipientList)
            {
                Console.WriteLine($"-{item}");
            }

            Console.WriteLine($"RepliedTo: {e.Mail.RepliedTo}");
            Console.WriteLine($"SentDate: {e.Mail.SentDate}");
            Console.WriteLine($"Subject: {e.Mail.Subject}");
        }
    }
}
