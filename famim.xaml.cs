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
using System.Data;
using System.Data.SqlClient;

namespace FFERP
{
    /// <summary>
    /// famim.xaml 的交互逻辑
    /// </summary>
    public partial class famim : Page
    {
        public famim()
        {
            InitializeComponent();
            setdata();
        }

        private void setdata()
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("select * from userview where 家庭='{0}' ",acc.Userfamily);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            // DataSet ds = new DataSet();//用SqlDataAdapter来获得数据库中的数据，填充至DataSet中
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中
            sqlconn.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userinformation p = new userinformation();
            fam.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pwdchange p = new pwdchange();
           fam.Content = p;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            famim p =new famim();
            fam.Content=p;
        }
    }
}
