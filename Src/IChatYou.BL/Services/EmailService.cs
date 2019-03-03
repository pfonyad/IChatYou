//using ServiceFlex.BI.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ServiceFlex.BI.Models;
//using System.Net.Mail;
//using ServiceFlex.Common.Exception;
//using ServiceFlex.Common.WordCodes;
//using System.Net;

namespace IChatYou.BL.Services
{
    //public class EmailService : IEmailService
    //{
    //    public void SendEmail(SmtpSetting smtpSettings, MailMessage mailMessage)
    //    {
    //        ValidateSmtpSetting(smtpSettings);

    //        try
    //        {
    //            using (SmtpClient client = new SmtpClient(smtpSettings.SmtpAddress, smtpSettings.SmtpPort))
    //            {
    //                client.DeliveryMethod = SmtpDeliveryMethod.Network;
    //                client.UseDefaultCredentials = false;
    //                client.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
    //                client.EnableSsl = smtpSettings.EnableSsl;

    //                client.Send(mailMessage);
    //            }
    //        }
    //        catch(Exception exc)
    //        {
    //            throw new ServiceFlexException(typeof(EmailServiceWordCodes.Error.ErrorDuringSendingEmail), new object[] { exc.Message }, null, exc);
    //        }
    //    }

    //    private void ValidateSmtpSetting(SmtpSetting smtpSettings)
    //    {
    //        if (String.IsNullOrWhiteSpace(smtpSettings.SmtpAddress))
    //            throw new ServiceFlexException(typeof(EmailServiceWordCodes.Error.NoSmtpAddress));

    //        if (smtpSettings.SmtpPort <= 0)
    //            throw new ServiceFlexException(typeof(EmailServiceWordCodes.Error.InvalidSmtpPort));

    //        if(String.IsNullOrWhiteSpace(smtpSettings.UserName))
    //            throw new ServiceFlexException(typeof(EmailServiceWordCodes.Error.NoSmtpAddress));

    //        if (String.IsNullOrWhiteSpace(smtpSettings.Password))
    //            throw new ServiceFlexException(typeof(EmailServiceWordCodes.Error.NoSmtpAddress));
    //    }
    //}
}
