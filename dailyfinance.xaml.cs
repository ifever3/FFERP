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
    /// dailyfinance.xaml 的交互逻辑
    /// </summary>
    public partial class dailyfinance : Page
    {
        public dailyfinance()
        {
            InitializeComponent();
            i = 0;
            dataset();
        }
        static int i = 0;
        private void dataset()
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            //string sql = string.Format("select * from ie where 用户名='{0}' ", acc.Username);
            string sql = null;
            if (datepicker.Text.ToString() == "" && waybox.Text.ToString() != "")//10
            {
                sql = string.Format("select * from ie where  用户名='{0}' and 用途 like '%{1}%'",acc.Username, waybox.Text.ToString());
            }
            if (waybox.Text.ToString() != "" && datepicker.Text.ToString() != "")//11
            {
                sql = string.Format("select * from ie where  日期='{0}'and 用户名='{1}' and 用途 like '%{2}%'", Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"), acc.Username, waybox.Text.ToString());
            }
            if (waybox.Text.ToString() == "" && datepicker.Text.ToString() != "")//01
            {
                sql = string.Format("select * from ie where 用户名='{0}'and 日期='{1}'", acc.Username, Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"));//构造一个SQL语句字符串   
            }
            if (datepicker.Text.ToString() == "" && waybox.Text.ToString() == "")//00
            {
                sql = string.Format("select * from ie where 用户名='{0}'", acc.Username);
            }
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds); // 填充表

            if (i==0)
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
            data1.ItemsSource = ds.DefaultView; // 填充到系统的视图中
            
            sqlconn.Close();
            add.Visibility = Visibility.Visible;
        }
        private void dataset1()
        {
            if(i==1)
            {
                add.Visibility = Visibility.Hidden;
                data1.Columns.RemoveAt(0);
                i= 0;
            }
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = null;
            if (datepicker.Text.ToString() == "" && waybox.Text.ToString() != "")//10
            {
                sql = string.Format("select * from ie where  家庭='{0}'  and 用途 like '%{1}%'",acc.Userfamily, waybox.Text.ToString());
            }
            if (waybox.Text.ToString() != "" && datepicker.Text.ToString() != "")//11
            {
                sql = string.Format("select * from ie where  日期='{0}'and 家庭='{1}' and 用途 like '%{2}%'", Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"), acc.Userfamily,waybox.Text.ToString());
            }
            if (waybox.Text.ToString() == "" && datepicker.Text.ToString() != "")//01
            {
                sql = string.Format("select * from ie where 家庭='{0}'and 日期='{1}'", acc.Userfamily, Convert.ToDateTime(datepicker.Text).ToString("yyyy/MM/dd"));//构造一个SQL语句字符串   
            }
            if (datepicker.Text.ToString() == "" && waybox.Text.ToString() == "")//00
            {
                sql = string.Format("select * from ie where 家庭='{0}'", acc.Userfamily);
            }
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds); // 填充表            
            data1.ItemsSource = ds.DefaultView; // 填充到系统的视图中

            sqlconn.Close();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)data1.SelectedItem;
            
            if (selectedRow != null)
            { 
                int num = Convert.ToInt32(selectedRow["num"]);
                SqlConnection sqlconn = null;
                sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                 string sql = string.Format("update ie set 用户名='{0}',家庭='{1}',日期='{2}',收支类型='{3}',金额='{4}',途径类型='{5}',用途类型='{6}', 用途='{7}' where num='{8}'", acc.Username,
                                    acc.Userfamily, Convert.ToDateTime(selectedRow["日期"]).ToString("yyyy/MM/dd"),Convert.ToString(selectedRow["收支类型"]),
                                    Convert.ToString(selectedRow["金额"]), Convert.ToString(selectedRow["途径类型"]),
                                    Convert.ToString(selectedRow["用途类型"]), Convert.ToString(selectedRow["用途"]),num);
                        SqlCommand cmd = new SqlCommand(sql, sqlconn);
                        int jg = cmd.ExecuteNonQuery();
                if(jg>0)
                {
                    MessageBox.Show("yes");
                }
                else
                {
                    MessageBox.Show("no");
                }
            }
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
                string sql = string.Format("delete from ie where num={0}", num);

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
            dailyf.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bankaccount p = new bankaccount();
            dailyf.Content = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dailyfinance p = new dailyfinance();
            dailyf.Content = p;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            financialanalysis p = new financialanalysis();
            dailyf.Content = p;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {          
            if(uof.IsChecked==false)
            {
                dataset();
            }
            if(uof.IsChecked ==true)
            {
                dataset1();
            }
        }

        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }

        private bool ischeck()
        {
            if (adddate.Text.Trim().Length == 0 || addmoney.Text.Trim().Length == 0 || addu.Text.Trim().Length == 0 || addtype.Text.Trim().Length == 0 || addoi.Text.Trim().Length == 0||addway.Text.Trim().Length==0)
            {
                MessageBox.Show("不能为空");
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
                        string sql = string.Format("insert into ie values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');", acc.Username,
                                        acc.Userfamily, Convert.ToDateTime(adddate.Text).ToString("yyyy/MM/dd"), addoi.Text, addmoney.Text, addway.Text, addtype.Text, addu.Text);
                        SqlCommand cmd = new SqlCommand(sql, sqlconn);
                        int jg = cmd.ExecuteNonQuery();
                        if (jg > 0)
                        {
                            adddate.Text = "";
                            addmoney.Text = "";
                            addoi.Text = "";
                            addtype.Text = "";
                            addu.Text = "";
                            addway.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("记录失败");
                            adddate.Text = "";
                            addmoney.Text = "";
                            addoi.Text = "";
                            addtype.Text = "";
                            addu.Text = "";
                            addway.Text = "";
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
            addoi.Text = "";
            addtype.Text = "";
            addu.Text = "";
            addway.Text = "";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
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
                        string sql = string.Format("update ie set {0}='{1}' where num='{2}'", cn, input, num);
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

        private void uof_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
