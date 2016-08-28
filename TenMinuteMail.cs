using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace _10MinuteMail
{
    public class TenMinuteMail
    {

        private string Email { get; set; }
        private CookieContainer cookies;
        private int messageCount;
        private List<Mail> mails;
        public int Interval { get; set; }

        public delegate void ReceiveMailHandler(object sender, MailEventArgs e);
        public event ReceiveMailHandler OnReceiveMail;

        private bool started = false;

        public string GetEmailAddress()
        {
            return Email;
        }

        public void Initialize()
        {
            var req = FastHttpRequest.Create("https://10minutemail.com/10MinuteMail/index.html");
            cookies = new CookieContainer();
            messageCount = 0;

            if (Interval == 0)
                Interval = 1000;

            mails = new List<Mail>();

            req.CookieContainer = cookies;
            using (var response = (HttpWebResponse)req.GetResponse())
            {
                cookies.Add(response.Cookies);
            }

            req = FastHttpRequest.Create("https://10minutemail.com/10MinuteMail/resources/session/address");
            req.CookieContainer = cookies;

            using (var response = (HttpWebResponse)req.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
                Email = reader.ReadToEnd();
            }
        }

        public int GetMessageCount()
        {
            var req = FastHttpRequest.Create("https://10minutemail.com/10MinuteMail/resources/messages/messageCount");
            req.CookieContainer = cookies;

            int i;

            using (var response = (HttpWebResponse)req.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
                i = Convert.ToInt32(reader.ReadToEnd());
            }

            return i;
        }

        public bool CheckMail()
        {
            if (GetMessageCount() != messageCount)
                return true;

            return false;
        }

        public List<Mail> GetMails()
        {
            var req = FastHttpRequest.Create("https://10minutemail.com/10MinuteMail/resources/messages/messagesAfter/0");
            req.CookieContainer = cookies;

            string json;
            using (var response = (HttpWebResponse)req.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Mail>>(json);
        }

        public Mail GetFirstMail()
        {
            return mails[0];
        }

        public Mail GetLastMail()
        {
            return mails[mails.Count - 1];
        }

        public Mail GetMail()
        {
            int i = messageCount;
            if (i != 0)
                i--;

            var req = FastHttpRequest.Create($"https://10minutemail.com/10MinuteMail/resources/messages/messagesAfter/{i}");
            req.CookieContainer = cookies;

            string json;
            using (var response = (HttpWebResponse)req.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Mail>>(json)[0];
        }

        public void Start()
        {
            started = true;
            Task.Run(() =>
            {
                while (started)
                {
                    if (CheckMail())
                    {
                        messageCount = GetMessageCount();
                        var mail = GetMail();
                        OnReceiveMail(this, new MailEventArgs(mail));
                    }

                    System.Threading.Thread.Sleep(Interval);
                }
            });
        }

        public void Stop()
        {
            started = false;
        }

    }

    public class Mail
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("repliedTo")]
        public bool RepliedTo { get; set; }
        [JsonProperty("forwarded")]
        public bool Forwarded { get; set; }
        [JsonProperty("sentDate")]
        public long SentDate { get; set; }
        [JsonProperty("recipientList")]
        public string[] RecipientList { get; set; }
        [JsonProperty("fromList")]
        public string[] FromList { get; set; }
        [JsonProperty("bodyText")]
        public string BodyText { get; set; }
        [JsonProperty("bodyPlainText")]
        public string BodyPlainText { get; set; }
        [JsonProperty("formattedDate")]
        public string FormattedDate { get; set; }
        [JsonProperty("bodyPreview")]
        public string BodyPreview { get; set; }
        [JsonProperty("attachmentCount")]
        public int AttachmentCount { get; set; }
        [JsonProperty("attachments")]
        public string[] Attachments { get; set; }
        [JsonProperty("read")]
        public bool Read { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("primaryFromAddress")]
        public string PrimaryFromAddress { get; set; }
    }

    public class MailEventArgs : EventArgs
    {
        public Mail Mail { get; set; }

        public MailEventArgs(Mail mail)
        {
            this.Mail = mail;
        }
    }
}
