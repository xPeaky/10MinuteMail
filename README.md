# 10MinuteMail
API for https://10minutemail.com/

<h1>How to use ?</h1>

<pre><code>var mail = new TenMinuteMail();
mail.Initialize();
mail.Interval = 2000;
mail.OnReceiveMail += Mail_OnReceiveMail;
Console.WriteLine(mail.GetEmailAddress());
mail.Start();</code></pre>

Now you have to declare the event.
<pre><code>
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
</code></pre>

<h1>Functions</h1>
<h3>GetMessageCount();</h3>
Return message count.  
Return <code>int</code>

<h3>CheckMail()</h3>
Check if you receive an email.  
Return <code>bool</code>

<h3>GetMails()</h3>
Return a list of mails.  
Return <code>List<Mail></code>

<h3>GetFirstMail()</h3>
Return the first mail.  
Return <code>Mail</code>

<h3>GetLastMail()</h3>
Return the last mail.  
Return <code>Mail</code>

<h3>GetMail()</h3>
Return the mail you've just received.  
Return <code>Mail</code>

<h3>Start()</h3>
Start to check mails.

<h3>Stop()</h3>
Stop checking mails.

<h1>Mail</h1>
Subject <code>string</code>  
RepliedTo <code>bool</code>  
Forwarded <code>bool</code>  
SentDate <code>long</code> (convert it to date)  
RecipientList <code>string[]</code>  
FromList <code>string[]</code>  
BodyText <code>string</code>  
BodyPlainText <code>string</code>  
FormattedDate <code>string</code>  
BodyPreview <code>string</code>  
AttachmentCount <code>int</code>  
Attachments <code>string[]</code>  
Read <code>bool</code>  
ID <code>string</code>  
PrimaryFromAddress <code>string</code>  

<h1>Screenshot</h1>
<img src="https://puu.sh/qRczl/178bfc19ef.jpg" />
