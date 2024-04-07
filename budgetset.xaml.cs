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
using System.Data;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;

namespace FFERP
{
    /// <summary>
    /// budgetset.xaml 的交互逻辑
    /// </summary>
    public partial class budgetset : Page
    {
        public budgetset()
        {
            InitializeComponent();
            mupdate();
            dataset();
            DataContext = this;
            chartdata();
        }



        private void chartdata()//个人支出总额
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select 衣金额 as y,食金额 as s,住金额 as z,行金额 as x,其他金额 as q from budget where 用户名='{0}'", acc.Username);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cloth2.Values = new ChartValues<int> { sdr.GetInt32("y") };
                tran2.Values = new ChartValues<int> { sdr.GetInt32("x") };
                food2.Values = new ChartValues<int> { sdr.GetInt32("s") };
                live2.Values = new ChartValues<int> { sdr.GetInt32("z") };
                other2.Values = new ChartValues<int> { sdr.GetInt32("q") };
            }
            sdr.Close();
            sqlconn.Close();          
        }

      static int s = 0, s1 = 0, c = 0, f = 0, l = 0, t = 0, o = 0, c1 = 0, f1 = 0, l1 = 0, t1 = 0, o1 = 0;
        private void dataset()
        {
           
            SqlConnection sqlconn = null;
            sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select 衣金额,食金额,住金额,行金额,其他金额,SUM( 衣金额+食金额+住金额+行金额+其他金额) as 预算总额 " +
                "from budget where 用户名='{0}' group by 衣金额,食金额,住金额,行金额,其他金额",acc.Username);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                cloth.Text = reader.GetInt32(0).ToString();
                food.Text = reader.GetInt32(1).ToString();
                live.Text = reader.GetInt32(2).ToString();
                tran.Text = reader.GetInt32(3).ToString();
                other.Text = reader.GetInt32(4).ToString();
                sum.Text = String.Format("{0:C0}", reader.GetInt32(5));
                s= reader.GetInt32(5);
                c= reader.GetInt32(0);
                f= reader.GetInt32(1);
                l= reader.GetInt32(2);
                t= reader.GetInt32(3);
                o= reader.GetInt32(4);
            }
            reader.Close();


            string sql1 = string.Format(@"SELECT 
             SUM(CASE
            WHEN 用途类型 = '衣' AND 用户名 = '{0}' AND 收支类型 = '支出' AND 日期 LIKE '%{1}%' THEN 金额 ELSE 0 END) ,
             SUM(CASE
            WHEN 用途类型 = '食' AND 用户名 = '{2}' AND 收支类型 = '支出' AND 日期 LIKE '%{3}%' THEN 金额 ELSE 0 END) ,
            SUM(CASE
            WHEN 用途类型 = '住' AND 用户名 = '{4}' AND 收支类型 = '支出' AND 日期 LIKE '%{5}%' THEN 金额 ELSE 0 END) ,
             SUM(CASE
            WHEN 用途类型 = '行' AND 用户名 = '{6}' AND 收支类型 = '支出' AND 日期 LIKE '%{7}%' THEN 金额 ELSE 0 END) ,
             SUM(CASE
            WHEN 用途类型 = '其他' AND 用户名 = '{8}' AND 收支类型 = '支出' AND 日期 LIKE '%{9}%' THEN 金额 ELSE 0 END) ,
            SUM(case when 用户名='{10}' AND 收支类型='支出' AND 日期 LIKE '%{11}%' THEN 金额 ELSE 0 END)
            FROM ie", acc.Username,DateTime.Now.ToString("yyyy/MM"), acc.Username, DateTime.Now.ToString("yyyy/MM"), acc.Username, DateTime.Now.ToString("yyyy/MM"),
            acc.Username, DateTime.Now.ToString("yyyy/MM"), acc.Username, DateTime.Now.ToString("yyyy/MM"), acc.Username, DateTime.Now.ToString("yyyy/MM"));
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {
                cloth1.Text = reader1.GetInt32(0).ToString();
                food1.Text = reader1.GetInt32(1).ToString();
                live1.Text = reader1.GetInt32(2).ToString();
                tran1.Text = reader1.GetInt32(3).ToString();
                other1.Text = reader1.GetInt32(4).ToString();
                sum1.Text = String.Format("{0:C0}", reader1.GetInt32(5));
                s1= reader1.GetInt32(5);
                c1= reader1.GetInt32(0);
                f1= reader1.GetInt32(1);
                l1 = reader1.GetInt32(2);
                t1 = reader1.GetInt32(3);
                o1 = reader1.GetInt32(4);

                cloth3.Values = new ChartValues<int> { reader1.GetInt32(0) };
                tran3.Values = new ChartValues<int> { reader1.GetInt32(1) };
                food3.Values = new ChartValues<int> { reader1.GetInt32(2) };
                live3.Values = new ChartValues<int> { reader1.GetInt32(3) };
                other3.Values = new ChartValues<int> { reader1.GetInt32(4) };
            }
            reader1.Close();

            string sql2 = string.Format(@"SELECT ie.用户名, 家庭, 日期, 收支类型, 金额, 用途类型, 
                        CASE WHEN 用途类型 = '衣' THEN 衣金额
                        WHEN 用途类型 = '食' THEN 食金额
                        WHEN 用途类型 = '住' THEN 住金额
                        WHEN 用途类型 = '行' THEN 行金额
                        WHEN 用途类型 = '其他' THEN 其他金额
                        END AS '该预算总金额' FROM ie, budget WHERE ie.用户名 = budget.用户名 and 收支类型='支出' and ie.用户名='{0}'", acc.Username);
            SqlDataAdapter sda=new SqlDataAdapter(sql2,sqlconn);
           DataTable dt = new DataTable();
            sda.Fill(dt);
            data1.ItemsSource = dt.DefaultView;
            sqlconn.Close();

           if(s>s1)
            {
                sum1.Foreground = Brushes.Green;
            }
           else
            {
                sum1.Foreground = Brushes.Red;
            }

        }

        private void mupdate()
        {
            string date = DateTime.Now.ToString("dd");
            if(date=="01")
            {
                SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();             
                switch (c1)
                {
                    case 1 when c1 > c:
                        cloth.Text = (c1+c1*0.1).ToString();
                        break;
                    case 2 when f1 > f:
                       food.Text = (f1+f1*0.1).ToString();
                        break;
                    case 3 when l1 > l:
                       live.Text = (l1+l1*0.1).ToString();
                        break;
                    case 4 when t1 > t:
                      tran.Text = (t1+t1*0.1).ToString();
                        break;
                    case 5 when o1 > o:
                        other.Text = (o1+o1*0.1).ToString();
                        break;
                    default:
                        // 其他情况的操作
                        break;
                }
                string sql = string.Format("update budget set 衣金额='{0}',食金额='{1}',住金额='{2}',行金额='{3}',其他金额='{4}' where 用户名='{5}'",
                     Convert.ToInt32(cloth.Text.Trim()), Convert.ToInt32(food.Text.Trim()), Convert.ToInt32(live.Text.Trim()), Convert.ToInt32(tran.Text.Trim()), Convert.ToInt32(other.Text.Trim()), acc.Username);
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                int jg = cmd.ExecuteNonQuery();
                sqlconn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            budgetset p=new budgetset();
            budset.Content = p;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            budgetrecommend p=new budgetrecommend();
            budset.Content=p;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            financialnews p=new financialnews();
            budset.Content= p;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            feedback p=new feedback();
            budset.Content = p;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("update budget set 衣金额='{0}',食金额='{1}',住金额='{2}',行金额='{3}',其他金额='{4}' where 用户名='{5}'",
                   Convert.ToInt32(cloth.Text.Trim()), Convert.ToInt32(food.Text.Trim()), Convert.ToInt32(live.Text.Trim()), Convert.ToInt32(tran.Text.Trim()), Convert.ToInt32(other.Text.Trim()), acc.Username);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            int jg = cmd.ExecuteNonQuery();
            dataset();
            DataContext = this;
            chartdata();
            sqlconn.Close();
        }
    }
}
