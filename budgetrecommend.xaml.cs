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
    /// budgetrecommend.xaml 的交互逻辑
    /// </summary>
    public partial class budgetrecommend : Page
    {
        public budgetrecommend()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            budgetset p=new budgetset();
            budrec.Content = p;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            budgetrecommend p=  new budgetrecommend();  
            budrec.Content=p;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            financialnews p=new financialnews();
            budrec.Content= p;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            feedback p=new feedback();
            budrec.Content = p;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void setmoney()
        {
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            double l = 8, p = 15, i = 13, s = 12;
            if (life.IsChecked == true)
            {
                l = l + 5;
            }
            if (play.IsChecked == true)
            {
                p = p + 5;
            }
            if (invest.IsChecked == true)
            {
                i = i + 5;
            }
            if (save.IsChecked == true)
            {
                s = s + 5;
            }
          double sl = l / (l + p + i + s);
          double sp = p / (l + p + i + s);
          double si = i / (l + p + i + s);
          double ss = s / (l + p + i + s);

            double sum = Convert.ToInt32(budgetsum.Text.Trim());

            life1.Values = new ChartValues<int> { Convert.ToInt32(sum*sl) };
            play1.Values = new ChartValues<int> { Convert.ToInt32(sum * sp) };
            invest1.Values = new ChartValues<int> { Convert.ToInt32(sum * si) };
            save1.Values = new ChartValues<int> { Convert.ToInt32(sum * ss) };

            life2.Content = Convert.ToInt32(sum * sl);
            play2.Content = Convert.ToInt32(sum * sp);
            invest2.Content = Convert.ToInt32(sum * si);
            save2.Content = Convert.ToInt32(sum * ss);
        }
    }
}
