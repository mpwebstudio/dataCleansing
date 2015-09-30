namespace data_validation.net.Web.Controllers.Helpers
{
    using ARSoft.Tools.Net.Dns;
    using data_validation.net.Web.Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    public class Email
    {
        public List<EmailValidationViewModel> Validate(string id)
        {
            var result = new List<EmailValidationViewModel>();
            try
            {
                System.Net.Mail.MailAddress email = new System.Net.Mail.MailAddress(id);
            }
            catch
            {
                result.Add(new EmailValidationViewModel
                {
                    Email = id,
                    IsValid = "Email sintaxis is invalid",
                });
                return result;
            }

            string mxRecord = string.Empty;
            var host = id.Substring(id.IndexOf("@") + 1, id.Length - id.IndexOf("@") - 1);
            var response = DnsClient.Default.Resolve(host, RecordType.Mx);
            var records = response.AnswerRecords.OfType<MxRecord>();
            foreach (var record in records)
            {
                mxRecord = record.ExchangeDomainName;
                break;
            }

            if(mxRecord == "")
            {
                result.Add(new EmailValidationViewModel
                {
                    Email = id,
                    IsValid = "MxRecord is invalid",
                    MxRecord = mxRecord
                });

                return result;
            }
            var isValidEmail = Click(id, mxRecord);

            if (isValidEmail != null)
            {
                if (isValidEmail.Substring(0, 1) == "2")
                {
                    isValidEmail = "Yes";
                }
                else if (isValidEmail.Substring(0, 1) == "5")
                {
                    isValidEmail = "Email sintaxis and MxRecords are valid.";
                }
                else
                {
                    isValidEmail = "No";
                }
            }
            else
            {
                isValidEmail = "No";
            }

            result.Add(new EmailValidationViewModel
            {
                Email = id,
                IsValid = isValidEmail,
                MxRecord = mxRecord
            });

            return result;
        }

        private string Click(string email, string mxRecord)
        {
            string message = string.Empty;
            TcpClient tClient = new TcpClient(mxRecord, 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO " + mxRecord + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            try
            {
                ResponseString = reader.ReadLine();
            }
            catch
            {
                message = "000";
                return message;
            }
            Delay(500);
            dataBuffer = BytesFromString("MAIL FROM: <mike@data-cleansing.net>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            if (ResponseString.Substring(0, 3) == "550")
            {
                message = ResponseString;
                return message;
            }
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            Delay(500);
            dataBuffer = BytesFromString("RCPT TO:<" + email.Trim() + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            try
            {
                ResponseString = reader.ReadLine();
            }
            catch
            {
                message = ResponseString;
                return message;
            }
            message = ResponseString;
            //if (GetResponseCode(ResponseString) == 550)
            //{
            //    message = ResponseString;
            //    //Response.Write("Mail Address Does not Exist !<br/><br/>");
            //    //Response.Write("<B><font color='red'>Original Error from Smtp Server :</font></b>" + ResponseString);
            //}
            //else
            //{
            //    message = ResponseString;
            //}
            /* QUITE CONNECTION */
            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();

            return message;
        }
        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        private static Task Delay(double milliseconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) =>
            {
                tcs.TrySetResult(true);
            };
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }
    }
}