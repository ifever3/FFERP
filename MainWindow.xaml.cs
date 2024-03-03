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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           home p = new home();
            main.Content = p;
            set();
        }

        public void set()
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select 头像 from [user] where 用户名='{0}'", acc.Username);
            string url = null;
            SqlCommand cmd6 = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr6 = cmd6.ExecuteReader();
            if (sdr6.Read())
            {
                url = Convert.ToString(sdr6[0].ToString());
                sdr6.Close();
            }
            tx.Source = new BitmapImage(new Uri(url));
            username.Content = acc.Username;
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            home p = new home();
            main.Content = p;
            set();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            userinformation p =new userinformation();
            main.Content = p;
            set();
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            dailyfinance p = new dailyfinance();
            main.Content = p;
            set();

        }

        private void RadioButton_Click_2(object sender, RoutedEventArgs e)
        {
            budgetset p = new budgetset();
            main.Content = p;
            set();

        }
    }
}
