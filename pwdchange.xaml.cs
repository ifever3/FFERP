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
using System.Windows.Forms;

namespace FFERP
{
    /// <summary>
    /// pwdchange.xaml 的交互逻辑
    /// </summary>
    public partial class pwdchange : Page
    {
        public pwdchange()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            famim p = new famim();
            pwdc.Content = p;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userinformation p=new userinformation();
            pwdc.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pwdchange p=new pwdchange();
            pwdc.Content= p;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (FloatingPasswordBox1.Password.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("原密码不能为空");
                FloatingPasswordBox1.Focus();
                return;
            }
            if (FloatingPasswordBox1.Password.Trim() != acc.Userpwd)
            {
                System.Windows.Forms.MessageBox.Show("原密码不正确，重新输入");
                FloatingPasswordBox1.Focus();
                return;
            }
            if (FloatingPasswordBox2.Password.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("新密码不能为空");
                FloatingPasswordBox2.Focus();
                return;
            }
            if (FloatingPasswordBox2.Password.Trim() == acc.Userpwd)
            {
                System.Windows.Forms.MessageBox.Show("密码不能与原密码一致");
                FloatingPasswordBox2.Focus();
                return;
            }
            if (FloatingPasswordBox3.Password.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("确认密码不能为空");
                FloatingPasswordBox3.Focus();
                return;
            }
            if (FloatingPasswordBox3.Password.Trim() != FloatingPasswordBox2.Password)
            {
                System.Windows.Forms.MessageBox.Show("密码不一致");
                FloatingPasswordBox3.Focus();
                return;
            }

            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("update account set 密码 ='{0}' where 用户名='{1}'",
                FloatingPasswordBox3.Password.Trim(), acc.Username);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            int jg = cmd.ExecuteNonQuery();
            if (jg > 0)
            {
                acc.Userpwd = FloatingPasswordBox3.Password.Trim();
                FloatingPasswordBox1.Password = "";
                FloatingPasswordBox2.Password = "";
                FloatingPasswordBox3.Password = "";
                System.Windows.Forms.MessageBox.Show("密码修改成功，请重新登录");
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
            sqlconn.Close();
        }
    
    }
}
