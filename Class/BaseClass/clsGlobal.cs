using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace NGCP.BaseClass
{
    public class clsGlobal
    {
        public static string APP_NAME = "myNGCP";
        private const string bytekey = "MYAPPLICATIONSYS"; //"RTASYSTEMAPPLICA";

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (Props == null)
            {
                return dataTable;
            }
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string Encrypt(string TextToEncrypt)
        {
            byte[] MyEncryptedArray = Encoding.UTF8
               .GetBytes(TextToEncrypt);

            byte[] MysecurityKeyArray = Encoding.UTF8.GetBytes(bytekey);

            var MyTripleDESCryptoService = new
               TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService
               .CreateEncryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyEncryptedArray, 0,
               MyEncryptedArray.Length);

            MyTripleDESCryptoService.Clear();
            string encrypted = Convert.ToBase64String(MyresultArray, 0,
               MyresultArray.Length);
            return BitConverter.ToString(Encoding.Default.GetBytes(encrypted)).Replace("-", "");
        }



        public static string Decrypt(string TextToDecrypt)
        {
            byte[] MyDecryptArray = Convert.FromBase64String
               (Encoding.ASCII.GetString(FromHex(TextToDecrypt)));

            byte[] MysecurityKeyArray = Encoding.UTF8.GetBytes(bytekey);

            var MyTripleDESCryptoService = new
               TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService
               .CreateDecryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyDecryptArray, 0,
               MyDecryptArray.Length);

            MyTripleDESCryptoService.Clear();
            return Encoding.UTF8.GetString(MyresultArray);
        }

        public static byte[] FromHex(string hex)
        {
            try
            {
                hex = hex.Replace("-", "");
                byte[] raw = new byte[hex.Length / 2];
                for (int i = 0; i < raw.Length; i++)
                {
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                return raw;
            }
            catch (NullReferenceException e)
            {
                return new byte[0];
            }
        }
        public static DateTime ParseString(string stringDate)
        {
            string[] data = stringDate.Split('/');
            return new DateTime(int.Parse(data[2]), int.Parse(data[0]), int.Parse(data[1]));
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    string lowerName = pro.Name.ToLower();
                    string lowerNameProp = column.ColumnName.ToLower();
                    if (lowerName == lowerNameProp)
                        if (lowerNameProp == "id" || lowerNameProp == "fileid")
                        {
                            pro.SetValue(obj, Encrypt(dr[column.ColumnName].ToString()), null);
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    else
                        continue;
                }
            }
            return obj;
        }

        /*public static string SendMail(__EmailModel model, IConfiguration config)
        {
            try
            {
                ConnectionStrings conn = new ConnectionStrings(config);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(conn.ConfigDetails("DefaultMailingAddress"));
                mailMessage.To.Add(model.ToEmail);
                mailMessage.Subject = model.Subject;
                mailMessage.Body = model.EmailBody;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient(conn.ConfigDetails("Client"));
                smtpClient.Port = Convert.ToInt32(conn.ConfigDetails("Port"));
                smtpClient.Send(mailMessage);
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }*/
    }
}