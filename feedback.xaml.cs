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

namespace FFERP
{
    /// <summary>
    /// feedback.xaml 的交互逻辑
    /// </summary>
    public partial class feedback : Page
    {
        public feedback()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            budgetset p = new budgetset();
            fb.Content = p;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            budgetrecommend p = new budgetrecommend();
            fb.Content = p;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            financialnews p = new financialnews();
            fb.Content = p;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            feedback p = new feedback();
            fb.Content = p;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            fdtext.Text = "  ";
        }
    }
}
