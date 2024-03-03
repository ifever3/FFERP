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
namespace FFERP
{
    /// <summary>
    /// debt.xaml 的交互逻辑
    /// </summary>
    public partial class debt : Page
    {
        public debt()
        {
            InitializeComponent();
            dataset();
        }

        private void dataset()
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select * from debtview where 家庭='{0}' ", acc.Userfamily);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中
            sqlconn.Close();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            debt p = new debt();
            debtm.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bankaccount p = new bankaccount();
            debtm.Content = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dailyfinance p = new dailyfinance();
            debtm.Content = p;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            financialanalysis p = new financialanalysis();
            debtm.Content = p;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = null;
            if (datepicker.Text.ToString() == "" && usernamebox.Text.ToString() == "")
            {
                sql = string.Format("select * from debtview where 家庭='{0}'", acc.Userfamily);
            }
            if (datepicker.Text.ToString() == "" && usernamebox.Text.ToString() != "")
            {
                sql = string.Format("select * from debtview where 家庭='{0}'and 用户名='{1}'", acc.Userfamily, usernamebox.Text.ToString());
            }
            if (usernamebox.Text.ToString() == "" && datepicker.Text.ToString() != "")
            {
                sql = string.Format("select * from debtview where 家庭='{0}'and 日期='{1}'", acc.Userfamily, Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"));//构造一个SQL语句字符串   
            }
            if (usernamebox.Text.ToString() != "" && datepicker.Text.ToString() != "")
            {
                sql = string.Format("select * from debtview where  日期='{0}'and 用户名='{1}'", Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"), acc.Username);
            }

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中
            sqlconn.Close();
        }
    }
}
