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
    /// userinformation.xaml 的交互逻辑
    /// </summary>
    public partial class userinformation : Page
    {
        public userinformation()
        {
            InitializeComponent();
            setid();
        }
        private void setid()
        {
            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                string sql = string.Format("select * from [user] where 用户名='{0}'", acc.Username);
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    usernamebox.Text = sdr["用户名"].ToString();
                    fnamebox.Text = sdr["家庭"].ToString();
                    sexbox.Text = sdr["性别"].ToString();
                    agebox.Text = sdr["年龄"].ToString();
                    phonebox.Text = sdr["电话"].ToString();
                    tx.Source=new BitmapImage(new Uri(sdr["头像"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlconn.Close();

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userinformation p=new userinformation();
            userim.Content = p;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            famim p = new famim();
            userim.Content = p;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pwdchange p = new pwdchange();  
            userim.Content= p;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconn = null;
            sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql1 = string.Format("update [user] set 用户名='{0}',家庭='{1}',性别='{2}',年龄='{3}',电话='{4}' where 用户名='{5}'",
                usernamebox.Text.Trim(), fnamebox.Text.Trim(), sexbox.Text.Trim(), agebox.Text.Trim(), phonebox.Text.Trim(), acc.Username);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            int jg1 = cmd1.ExecuteNonQuery();

            string sql2 = string.Format("update ie set 用户名='{0}' where 用户名='{1}'",
                           usernamebox.Text.Trim(), acc.Username);
            SqlCommand cmd2 = new SqlCommand(sql2, sqlconn);
            int jg2 = cmd2.ExecuteNonQuery();

            string sql3 = string.Format("update budget set 用户名='{0}' where 用户名='{1}'",
                           usernamebox.Text.Trim(), acc.Username);
            SqlCommand cmd3 = new SqlCommand(sql3, sqlconn);
            int jg3 = cmd3.ExecuteNonQuery();

            string sql4 = string.Format("update account set 用户名='{0}' where 用户名='{1}'",
                           usernamebox.Text.Trim(), acc.Username);
            SqlCommand cmd4 = new SqlCommand(sql4, sqlconn);
            int jg4 = cmd4.ExecuteNonQuery();


            if (jg1 > 0)
            {
                acc.Username = usernamebox.Text.Trim();
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败");
            }
            sqlconn.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }
            string url = openFileDialog.FileName;
            tx.Source = new BitmapImage(new Uri(url));

            string sql1 = string.Format("update [user] set 头像='{0}'where 用户名='{1}'", url, acc.Username);
            SqlCommand cmd = new SqlCommand(sql1, sqlconn);
            int jg = cmd.ExecuteNonQuery();
            if (jg > 0)
            {
                MessageBox.Show("更改成功");
            }
            else
            {
                MessageBox.Show("更改失败");
            }
            sqlconn.Close();
        }
    }
}
