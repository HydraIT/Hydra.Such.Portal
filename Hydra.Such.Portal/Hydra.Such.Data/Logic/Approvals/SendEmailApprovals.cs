﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
    using System.Net.Mail;
using System.Text;

namespace Hydra.Such.Data.Logic.Approvals
{
    public class SendEmailApprovals : EmailAutomation
    {
        public EmailsAprovações EmailApproval { get; set; }
        private bool MailSent = false;

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                MailSent = false;
                string Token = (string)e.UserState;
                if (!e.Cancelled && e.Error == null)
                {
                    MailSent = true;
                    if (MailSent)
                    {
                        EmailApproval.ObservaçõesEnvio = "Mensagem enviada com Sucesso";
                        EmailApproval.Enviado = true;
                        DBApprovalEmails.Update(EmailApproval);
                    }
                }

                if (e.Error != null)
                {
                    EmailApproval.ObservaçõesEnvio = "ERRO: " + e.Error.Message.ToString() +  "Não foi possível enviar a mensagem " + DateTime.Now.ToString();
                    DBApprovalEmails.Update(EmailApproval);
                }
            }
            catch { }
        }

        public void SendEmail()
        {
            if (EmailApproval == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(From) && !IsValidEmail(From))
            {
                EmailApproval.ObservaçõesEnvio = "Email do remetente inválido!";

                try { DBApprovalEmails.Update(EmailApproval); } catch { }
                return;
            }

            if (To == null || To.Count <= 0)
            {
                EmailApproval.ObservaçõesEnvio = "Não há destinatários";
                return;
            }

            try {DBApprovalEmails.Update(EmailApproval);} catch { }

            SmtpClient Client = new SmtpClient(Config.Host, Config.Port);
            NetworkCredential Credentials = new NetworkCredential(Config.Username, Config.Password);
            Client.UseDefaultCredentials = true;
            Client.Credentials = Credentials;
            Client.EnableSsl = Config.SSL;

            MailMessage MMessage = new MailMessage
            {
                From = new MailAddress(From, DisplayName)
            };

            foreach (var t in To)
            {
                MMessage.To.Add(new MailAddress(t));
            }

            foreach (var cc in CC)
            {
                if (IsValidEmail(cc))
                    MMessage.CC.Add(cc);
            }

            foreach (var bcc in BCC)
            {
                if (IsValidEmail(bcc))
                    MMessage.Bcc.Add(bcc);
            }

            MMessage.Subject = Subject;
            MMessage.Body = Body;
            MMessage.IsBodyHtml = IsBodyHtml;

            Client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string UserState = "EmailAprovações";
            Client.SendAsync(MMessage, UserState);


            //MMessage.Dispose();
        }

        public void SendEmail_Simple()
        {
            SmtpClient Client = new SmtpClient(Config.Host, Config.Port);
            NetworkCredential Credentials = new NetworkCredential(Config.Username, Config.Password);
            Client.UseDefaultCredentials = true;
            Client.Credentials = Credentials;
            Client.EnableSsl = Config.SSL;

            MailMessage MMessage = new MailMessage
            {
                From = new MailAddress(From, DisplayName)
            };

            foreach (var t in To)
            {
                MMessage.To.Add(new MailAddress(t));
            }

            foreach (var cc in CC)
            {
                if (IsValidEmail(cc))
                    MMessage.CC.Add(cc);
            }

            foreach (var bcc in BCC)
            {
                if (IsValidEmail(bcc))
                    MMessage.Bcc.Add(bcc);
            }

            MMessage.Subject = Subject;
            MMessage.Body = Body;
            MMessage.IsBodyHtml = IsBodyHtml;

            if (!string.IsNullOrEmpty(Anexo))
            {
                Attachment anex = new Attachment(Anexo);
                MMessage.Attachments.Add(anex);
            }

            Client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string UserState = "EmailAprovações";
            Client.SendAsync(MMessage, UserState);
        }
    }
}
