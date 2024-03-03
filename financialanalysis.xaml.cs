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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using System.Data;
namespace FFERP
{
    /// <summary>
    /// financialanalysis.xaml 的交互逻辑
    /// </summary>
    public partial class financialanalysis : Page
    {
        public financialanalysis()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            setdata();
        }
       
    private void setdata()//个人支出
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 用户名='{0}'and 日期 like '%{1}%' and 收支类型='支出'", acc.Username,tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 用户名='{0}' and 收支类型='支出'", acc.Username);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();     
                num.Text=sdr1["msum"].ToString();
            }
            sqlconn.Close();
        }
       
        private void setdata1()//家庭支出
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'and 日期 like '%{1}%' and 收支类型='支出'", acc.Userfamily, tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 家庭='{0}' and 收支类型='支出'", acc.Userfamily);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sqlconn.Close();
        }
        private void setdata2()//个人收入
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 用户名='{0}'and 日期 like '%{1}%' and 收支类型='收入'", acc.Username,tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 用户名='{0}' and 收支类型='收入'", acc.Username);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sqlconn.Close();
        }
        private void setdata3()//家庭收入
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'and 日期 like '%{1}%' and 收支类型='收入'", acc.Userfamily, tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 家庭='{0}' and 收支类型='收入'", acc.Userfamily);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sqlconn.Close();
        }

        public class MainViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }

        public MainViewModel()
        {
            SeriesCollection = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "金额",
                Values = new ChartValues<double> { 3, 5, 2, 7 }
            }
           
        };
        }
    }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            debt p = new debt();
            fa.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bankaccount p = new bankaccount();
           fa.Content = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dailyfinance p = new dailyfinance();
            fa.Content = p;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            financialanalysis p = new financialanalysis();
            fa.Content = p;
        }


        private void calendar_img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bill.IsChecked is false)
            {
                calendar.Visibility = Visibility.Visible;
            }
            if (bill.IsChecked is true)
            {
                calendar1.Visibility = Visibility.Visible;
            }
        }
        private void calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {      
                if (calendar.DisplayMode == CalendarMode.Month || calendar.DisplayMode == CalendarMode.Year)
                {
                    calendar.DisplayMode = CalendarMode.Decade;
                }                                                                
        }
        private void calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
                tb_calendar.Text = calendar.DisplayDate.Date.ToString("yyyy");
                calendar.Visibility = Visibility.Hidden;
        }     
        private void calendar1_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            tb_calendar.Text = calendar1.DisplayDate.Date.ToString("yyyy/MM");
            calendar1.Visibility = Visibility.Hidden;         
        }
        private void calendar1_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (calendar1.DisplayMode == CalendarMode.Month || calendar1.DisplayMode == CalendarMode.Decade)
            {
                calendar1.DisplayMode = CalendarMode.Year;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(uof.IsChecked is false )//个人
            {
                if(type.IsChecked == false)
                {
                    setdata();
                }
                if(type.IsChecked == true)
                {
                    setdata2();
                }
            }           
            if (uof.IsChecked is true )//家庭
            {
                if(type.IsChecked == false)
                {
                    setdata1();
                }
                if(type.IsChecked == true)
                {
                    setdata3();
                }
            }
            
        }

        
    }
}
