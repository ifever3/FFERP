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
using System.Windows.Shapes;

namespace FFERP
{
    /// <summary>
    /// start.xaml 的交互逻辑
    /// </summary>
    public partial class start : Window
    {
        public start()
        {
            InitializeComponent();
            login p=new login();
            login.Content = p;
        }
    }
}
