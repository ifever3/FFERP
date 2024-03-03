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
            userid.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            signup p=new signup();
            s.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (userid.Text.Trim().Length == 0)
            {
                MessageBox.Show("账号不能为空");
                userid.Focus();
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
                string sql = string.Format("select account.用户名,[user].家庭 from account,[user] where account.用户名=[user].用户名 and 账号='{0}'and 密码='{1}'",
                    userid.Text.Trim(), password.Password.Trim());
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    acc.Username = sdr["用户名"].ToString();
                    acc.Userid = userid.Text.Trim();
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
    }
}
