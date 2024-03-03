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
       
    private void setdata()
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'", acc.Userfamily);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中
            sqlconn.Close();
        }

        private void setdate()
        {
            if(bill.IsChecked == false)
            {
                
            }
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

        private void bill_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if(bill!=null)
            {
                if (bill.IsChecked == true)
                {
                    if (calendar.DisplayMode == CalendarMode.Month || calendar.DisplayMode == CalendarMode.Decade)
                    {
                        calendar.DisplayMode = CalendarMode.Year;
                    }
                }
                if (bill.IsChecked == false)
                {
                    if (calendar.DisplayMode == CalendarMode.Month || calendar.DisplayMode == CalendarMode.Year)
                    {
                        calendar.DisplayMode = CalendarMode.Decade;
                    }
                }
            }
              
        }
        private void calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            if(bill!=null)
            {
                if (bill.IsChecked == false)
                {
                    tb_calendar.Text = calendar.DisplayDate.Date.ToString("yyyy");
                    Pop.IsOpen = false;
                }
                if (bill.IsChecked == true)
                {
                    tb_calendar.Text = calendar.DisplayDate.Date.ToString("yyyy/MM");
                    Pop.IsOpen = false;
                }
            }    
           
        }

        private void dateicon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Pop.IsOpen)
            {
                Pop.IsOpen = false;
            }
            else
            {
                Pop.IsOpen = true;
            }
        }
    }
}
