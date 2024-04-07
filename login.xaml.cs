using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json.Nodes;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ServiceModel;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace FFERP
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
            username.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            signup p=new signup();
            s.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (username.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户名不能为空");
                username.Focus();
                return;
            }
            if (password.Password.Trim().Length==0)
            {
                MessageBox.Show("密码不能为空");
                password.Focus();
                return;
            }
            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                string sql = string.Format("select * from account where 用户名='{0}' and 密码='{1}'",
                    username.Text.Trim(), password.Password.Trim());
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    acc.Username = sdr["用户名"].ToString();
                    acc.Userpwd = password.Password.Trim();
                    acc.Userfamily=sdr["家庭"].ToString();
                    MainWindow p = new MainWindow();
                    p.Show();
                    var win = Window.GetWindow(this);
                    win.Close();
                    cmd.Dispose();                 
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                    password.Focus();
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sqlconn.Close();
            
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            string p = null;
          SqlConnection  sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select 密码 from account where 用户名=(select 用户名 from [user] where 电话='{0}')",
               phonenum.Text);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                p=sdr[0].ToString();
            }
                if (textnum.Text == code)
            {
                MessageBox.Show("你的密码为：" + p);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string p = null;
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select * from [user] where 用户名='{0}' and 电话='{1}'",
            u.Text,phonenum.Text);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);            
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                message();
                MessageBox.Show("已发送");
            }
            else
            {
                MessageBox.Show("用户名或电话错误！");
            }
        }

        private const String host = "https://gyytz.market.alicloudapi.com";
        private const String path = "/sms/smsSend";
        private const String method = "POST";
        private const String appcode = "299131ae6dfa40789d372b42882fcfde";
        static Random rd = new Random();
        static int num = rd.Next(100000, 1000000);
        static string code=num.ToString();
        
        private void message()
        {
            string phone = phonenum.Text.Trim();
            String querys = $"mobile={phone}&param=**code**:{code}&smsSignId=2e65b1bb3d054466b82f0c9d125465e2&templateId=11085f0550b54280a2d999e1fcadc03b";
            String bodys = "";
            String url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            Console.WriteLine(httpResponse.StatusCode);
            Console.WriteLine(httpResponse.Method);
            Console.WriteLine(httpResponse.Headers);
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            Console.WriteLine(reader.ReadToEnd());
            Console.WriteLine("\n");

        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }



    }
}
