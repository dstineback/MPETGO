using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
//using System.Net.Mail;
using System.Text;
using EASendMail;

namespace MPETGO.Pages
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            txtName.Focus();
        }

        

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress(txtEmail.Text.ToString());
            //    mail.To.Add("stinebackd@fwginc.com");
            //    mail.Subject = txtSubject.Text;
            //    mail.Body = "Name: " + txtName.Text + "<br /><br />Email: " + txtEmail.Text + "<br />" + txtBody.Text;
            //    mail.IsBodyHtml = true;

            //    if (FileUpload1.HasFile)
            //    {
            //        string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            //        mail.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileName));
            //    }


            //    using (SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587))
            //    {
            //        smtp.UseDefaultCredentials = false;
            //        smtp.Credentials = new NetworkCredential("stinebackd@fwginc.com", "Dman3411");
            //        smtp.EnableSsl = true;
            //        smtp.Send(mail);
            //    }
            //}
            SmtpMail oMail = new SmtpMail("Tryit");
            SmtpClient oSmtp = new SmtpClient();
            //MailMessage mm = new MailMessage(txtEmail.Text.ToString(), "developerFWG@gmail.com");

            oMail.To = "developerFWG@gmail.com";
            oMail.From = txtEmail.Text.ToString();
            oMail.Subject = txtSubject.Text.ToString();
            var body = "Name: " + txtName.Text + "<br /><br />Email: " + txtEmail.Text + "<br />" + txtBody.Text;
            oMail.TextBody = body.ToString();
            
            //mm.Subject = txtSubject.Text;
            //mm.Body = "Name: " + txtName.Text + "<br /><br />Email: " + txtEmail.Text + "<br />" + txtBody.Text;
            if (FileUpload1.HasFiles)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                //mm.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileName)); 
                oMail.AddAttachment(FileName);
            }
            //mm.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            oServer.Port = 465;
            oServer.ConnectType = SmtpConnectType.ConnectDirectSSL;
            oServer.User = "developerFWG@gmail.com";
            oServer.Password = "FWGinc3411";
            oSmtp.SendMail(oServer, oMail);
            //smtp.EnableSsl = true;
            //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            //NetworkCred.UserName = "developerFWG@gmail.com";
            //NetworkCred.Password = "FWGinc3411";
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = NetworkCred;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            //smtp.Port = 587;
            //smtp.Port = 465;
            
            //smtp.Port = 25;
            //smtp.Send(mm);
            lblMessage.Text = "Email Sent SucessFully.";
            //Clear();
            Response.Write("<script language='javascript'>window.alert('Email was sucessfully sent');window.location.href='https://m-pet.net/mpetgo-CKR/index.aspx';</script>");
        }

        protected void Clear()
        {
            txtName.Value = "";
            txtSubject.Value = "";
            txtEmail.Value = "";
            txtBody.Value = "";         
        }


        protected void EmailPopUp_Load(object sender, EventArgs e)
        {
            EmailPopUp.Enabled = true;
        }
    }
}