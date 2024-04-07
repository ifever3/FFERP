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
            i=0;
            dataset();
        }
        static int i = 0;
        private void dataset()
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = null;
            if (uof.IsChecked == false)
            {
                if (datepicker.Text.ToString() == "" && usernamebox.Text.ToString() != "")
                {
                    sql = string.Format("select * from debt where  用户名='{0}'", usernamebox.Text.ToString());
                }
                if (usernamebox.Text.ToString() != "" && datepicker.Text.ToString() != "")
                {
                    sql = string.Format("select * from debt where  日期='{0}'and 用户名='{1}'", Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"), acc.Username);
                }
                if (usernamebox.Text.ToString() == "" && datepicker.Text.ToString() != "")
                {
                    sql = string.Format("select * from debt where 用户名='{0}'and 日期='{1}'", acc.Username, Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"));//构造一个SQL语句字符串   
                }
                if (datepicker.Text.ToString() == "" && usernamebox.Text.ToString() == "")
                {
                    sql = string.Format("select * from debt where 用户名='{0}'", acc.Username);
                }
            }
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            if (i == 0)
            {
                DataGridTemplateColumn deleteColumn = new DataGridTemplateColumn();
                deleteColumn.Header = "Delete";

                FrameworkElementFactory deleteButtonFactory = new FrameworkElementFactory(typeof(Button));
                deleteButtonFactory.SetValue(Button.ContentProperty, "Del");
                deleteButtonFactory.SetValue(Button.FontSizeProperty, 8.0);
                deleteButtonFactory.SetValue(Button.WidthProperty, 45.0);
                deleteButtonFactory.SetValue(Button.HeightProperty, 20.0);
                deleteButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click)); // 添加删除按钮的点击事件处理程序

                DataTemplate deleteButtonTemplate = new DataTemplate();
                deleteButtonTemplate.VisualTree = deleteButtonFactory;
                deleteColumn.CellTemplate = deleteButtonTemplate;

                data1.Columns.Add(deleteColumn);
                i = 1;
            }
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中
            sqlconn.Close();
            add.Visibility = Visibility.Visible;
        }
        private void dataset1()
        {
            if (i == 1)
            {
                add.Visibility = Visibility.Hidden;
                data1.Columns.RemoveAt(0);
                i = 0;
            }
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = null;
            if (uof.IsChecked == true)
            {
                if (datepicker.Text.ToString() == "" && usernamebox.Text.ToString() != "")
                {
                    sql = string.Format("select * from debt where  用户名='{0}'", usernamebox.Text.ToString());
                }
                if (usernamebox.Text.ToString() != "" && datepicker.Text.ToString() != "")
                {
                    sql = string.Format("select * from debt where  日期='{0}'and 用户名='{1}'", Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"), acc.Username);
                }
                if (usernamebox.Text.ToString() == "" && datepicker.Text.ToString() != "")
                {
                    sql = string.Format("select * from debt where 家庭='{0}'and 日期='{1}'", acc.Userfamily, Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"));//构造一个SQL语句字符串   
                }
                if (datepicker.Text.ToString() == "" && usernamebox.Text.ToString() == "")
                {
                    sql = string.Format("select * from debt where 家庭='{0}'", acc.Userfamily);
                }
            }
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中
            sqlconn.Close();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)data1.SelectedItem;

            if (selectedRow != null)
            {
                int num = Convert.ToInt32(selectedRow["num"]);
                SqlConnection sqlconn = null;

                sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                string sql = string.Format("delete from debt where num={0}", num);

                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                int jg = cmd.ExecuteNonQuery();
                if (jg > 0)
                {
                    // 删除数据库记录成功后再删除DataGrid中的行
                    selectedRow.Delete();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
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
            if (uof.IsChecked == false)
            {
                dataset();
            }
            if (uof.IsChecked == true)
            {
                dataset1();
            }
        }

        private bool ischeck()
        {
           if( adddate.Text.Trim().Length == 0 || addmoney.Text.Trim().Length==0 || addob.Text.Trim().Length==0 ||addtype.Text.Trim().Length==0)
            {
                MessageBox.Show("不能有空");
                return false;
            }
           return true;
        }
        private void accept_Click(object sender, RoutedEventArgs e)
        {
            if (ischeck())
            {
                SqlConnection sqlconn = null;
                try
                {
                     sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                    sqlconn.Open();
                    string sql = string.Format("insert into debt values ('{0}','{1}','{2}','{3}','{4}','{5}');", acc.Username,
                                    acc.Userfamily, Convert.ToDateTime(adddate.Text).ToString("yyyy/MM/dd"), addtype.Text, addob.Text, addmoney.Text);
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    int jg = cmd.ExecuteNonQuery();
                    if (jg > 0)
                    {
                        adddate.Text = "";
                        addmoney.Text = "";
                        addtype.Text = "";
                        addob.Text = "";

                    }
                    else
                    {
                      
                        adddate.Text = "";
                        addmoney.Text = "";
                        addtype.Text = "";
                        addob.Text = "";
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

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            adddate.Text = "";
            addmoney.Text = "";
            addtype.Text = "";
            addob.Text = "";
        }

        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }

        private void data1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (i == 1)
            {
                string input = (e.EditingElement as TextBox).Text;
                //如果修改后的值和修改前的值不一样
                if (preValue != input)
                {
                    SqlConnection sqlconn = null;
                    try
                    {
                        DataRowView selectedRow = (DataRowView)data1.SelectedItem;
                        int num = Convert.ToInt32(selectedRow["num"]);

                        var cells = this.data1.SelectedCells;
                        int RowIndex = 0;
                        int ColumnIndex = 0;
                        string cn = null;

                        ColumnIndex = this.data1.CurrentCell.Column.DisplayIndex;
                        cn = data1.Columns[ColumnIndex].Header.ToString();

                        sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                        sqlconn.Open();
                        string sql = string.Format("update debt set {0}='{1}' where num='{2}'", cn, input, num);
                        SqlCommand cmd = new SqlCommand(sql, sqlconn);
                        int jg = cmd.ExecuteNonQuery();
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

        string preValue = "";

        private void data1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            preValue = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
        }
    }
}
