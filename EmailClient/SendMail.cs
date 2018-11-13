using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace EmailClient
{
    public class SendMail
    {
        int numberOfMessages;
        DateTime measureMessageTime = Convert.ToDateTime("1900-1-1");

        public SendMail()
        {
        }
        public void SendToPhone(string phoneNumber, string subject, string body)
        {
            //brute force method... this is a hack and needs to be fixed
            Send(subject, body, $"{phoneNumber}@vtext.com");//try sending it to verizon
            Send(subject, body, $"{phoneNumber}@mms.att.net");//ATT
            Send(subject, body, $"{phoneNumber}@pm.sprint.com");//sprint
            Send(subject, body, $"{phoneNumber}@tmomail.net");//tmoble
        }
        public void Send(string subject, string body)
        {
            Send(subject, body, "YourPhoneNumber@vtext.com");
        }
        public void Send(string subject, string body, string toAddress)
        {
            TimeSpan timespan = DateTime.Now - measureMessageTime;
            if (timespan.Hours > 1)
            {
                numberOfMessages = 0;
                measureMessageTime = DateTime.Now;
            }
            // failsafe the number of messages that can go out with a single hour
            if (numberOfMessages > 35)
            {
                return;
            }
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            MailAddress from = new MailAddress("YOURgMAILADDRESS");
            char[] delimiterChars = { ';' };
            string[] addresses = toAddress.Split(delimiterChars);
            foreach (string address in addresses)
            {
                MailAddress to = new MailAddress(address);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                NetworkCredential credential = new NetworkCredential("USERNAME", "PASSWORD"); //
                client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.EnableSsl = true;
                try
                {
                    client.Send(message);
                    numberOfMessages++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
