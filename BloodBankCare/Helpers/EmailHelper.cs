using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BloodBankCare.Helpers
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("nayeemsoftware2021@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("nayeemsoftware2021@gmail.com", "@nayeemsoftware2021@gmail.com#");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("nayeemsoftware2021@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("nayeemsoftware2021@gmail.com", "@nayeemsoftware2021@gmail.com#");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;


            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public bool SendEmailTwoFactorCode(string userEmail, string code)
        {

            var fromAddress = new MailAddress("nayeemsoftware2021@gmail.com", "Blood Bank Care");
            var toAddress = new MailAddress(userEmail, "Dear Client");
            const string fromPassword = "@nayeemsoftware2021@gmail.com#";
            const string subject = "Your Verification Code";
            string body = code;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000,

            };

            MailMessage mailMessage = new MailMessage();
            using (mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body

            })

                try
                {
                    smtp.Send(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            return false;
        }


        #region Helper Method
        int getSixDigitCode()
        {
            Random randomNumber = new Random();
            int randomOtp = randomNumber.Next(100001, 999999);
            return randomOtp;
        }
        #endregion

    }
}
