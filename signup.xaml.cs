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
    /// signup.xaml 的交互逻辑
    /// </summary>
    public partial class signup : Page
    {
        public signup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            login p=new login();
            l.Content = p;
        }
        private bool ischeck()
        {
            if (fname.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户昵称不能为空");
               fname.Focus();
                return false;
            }
            if (userid.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户账号不能为空");
                userid.Focus();
                return false;
            }
            if (passwordbox1.Password.Trim().Length == 0)
            {
                MessageBox.Show("密码不能为空");
                passwordbox1.Focus();
                return false;
            }
            if (passwordbox2.Password.Trim().Length == 0)
            {
                MessageBox.Show("密码不能为空");
                passwordbox2.Focus();
                return false;
            }
            return true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ischeck())
            { 
                    SqlConnection sqlconn = null;
                    try
                    {
                        sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                        sqlconn.Open();

                        string query = "SELECT COUNT(*) FROM account WHERE 账号 = @input";
                        SqlCommand command = new SqlCommand(query, sqlconn);
                        command.Parameters.AddWithValue("@input", userid.Text.Trim());
                        int count = (int)command.ExecuteScalar(); // 执行查询并返回结果集中的第一行的第一列

                        
                        if (count > 0)
                        {
                            MessageBox.Show("id已存在");
                        }
                      
                        else
                        {
                            string sql = string.Format("insert into account values ('{0}','{1}','{2}','{3}');",
                           "admin", userid.Text.Trim(), passwordbox1.Password.Trim(), fname.Text.Trim());
                            SqlCommand cmd = new SqlCommand(sql, sqlconn);
                            string sql1 = string.Format("insert into [user] values ('{0}','{1}','{2}','{3}','{4}','{5}');",
                             "admin",fname.Text.Trim(), null, null, null, @"D:\VS实验\FFERP\image\tp3.jpeg");
                            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
                            int jg = cmd.ExecuteNonQuery();
                            int jg1 = cmd1.ExecuteNonQuery();
                            if (jg > 0 && jg1 > 0)
                            {
                                MessageBox.Show("注册成功");
                            }
                            else
                            {
                                MessageBox.Show("注册失败");
                            }

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
        }
    }
}
