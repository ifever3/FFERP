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
                MessageBox.Show("家庭名不能为空");
               fname.Focus();
                return false;
            }
            if (username.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户名不能为空");
                username.Focus();
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

                        string query = "SELECT COUNT(*) FROM account WHERE 用户名 = @input";
                        SqlCommand command = new SqlCommand(query, sqlconn);
                        command.Parameters.AddWithValue("@input", username.Text.Trim());
                        int count = (int)command.ExecuteScalar(); // 执行查询并返回结果集中的第一行的第一列                      
                        if (count > 0)
                        {
                            MessageBox.Show("用户名已存在");
                           command.Dispose();
                        }
                     
                        else
                        {
                        sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                        sqlconn.Open();
                        string query1 = "SELECT COUNT(*) FROM faccount WHERE 家庭 = @input1";
                        SqlCommand command1 = new SqlCommand(query1, sqlconn);
                        command1.Parameters.AddWithValue("@input1", fname.Text.Trim());
                        int count1 = (int)command1.ExecuteScalar(); // 执行查询并返回结果集中的第一行的第一列
                        if (count1 > 0)//查询已有该家庭，则不用注册家庭直接注册用户
                        {
                            string sqlc = string.Format("select * from faccount where 家庭='{0}' and 家庭密码='{1}'",
                    fname.Text.Trim(), passwordbox2.Password.Trim());
                            SqlCommand cmdd = new SqlCommand(sqlc, sqlconn);                          
                            SqlDataReader sdr = cmdd.ExecuteReader();
                            if (sdr.Read())
                            {
                                sdr.Close();
                                string sql = string.Format("insert into account values ('{0}','{1}','{2}');",
                                                       username.Text.Trim(), passwordbox1.Password.Trim(), fname.Text.Trim());
                                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                                string sql1 = string.Format("insert into [user] values ('{0}','{1}','{2}','{3}','{4}','{5}');",
                               username.Text.Trim(), fname.Text.Trim(), null, null, null, @"D:\VS实验\FFERP\image\tp3.jpeg");
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
                            else
                            {
                                MessageBox.Show("已有家庭密码错误");
                                passwordbox2.Focus();
                            }
                           
                        }
                        else //无家庭则注册家庭及用户
                        {
                            string sql = string.Format("insert into account values ('{0}','{1}','{2}');",
                           username.Text.Trim(), passwordbox1.Password.Trim(), fname.Text.Trim());
                            SqlCommand cmd = new SqlCommand(sql, sqlconn);
                            string sql1 = string.Format("insert into [user] values ('{0}','{1}','{2}','{3}','{4}','{5}');",
                           username.Text.Trim(), fname.Text.Trim(), null, null, null, @"D:\VS实验\FFERP\image\tp3.jpeg");
                            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
                            string sql2 = string.Format("insert into faccount values ('{0}','{1}');",
                           fname.Text.Trim(), passwordbox2.Password.Trim());
                            SqlCommand cmd2 = new SqlCommand(sql2, sqlconn);
                            int jg = cmd.ExecuteNonQuery();
                            int jg1 = cmd1.ExecuteNonQuery();
                            int jg2 = cmd2.ExecuteNonQuery();
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
