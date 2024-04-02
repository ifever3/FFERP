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
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stockService;
using System.ServiceModel;
using System.Data;
using System.Data.SqlClient;
using System;
using System.IO;
using CsvHelper;
using OfficeOpenXml;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FFERP
{
    /// <summary>
    /// home.xaml 的交互逻辑
    /// </summary>
    public partial class home : Page
    {
        public home()
        {
            InitializeComponent();
            c1.Text = "USD";
            c2.Text = "CNY";
           LoadEX();
            barload();
            LoadNews();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            recommendload();
        }
        public class wechatBill
        {
            public string date { get; set; }
            public string tran { get; set; }
            public string ic { get; set; }
            public int money { get; set; }
            public string way { get; set; }

            // 其他你需要的字段...
        }

        public List<wechatBill> ParseCsvFile(string filePath)
        {
            var bills = new List<wechatBill>();
            try
            {
                string number = null;
                var lines = File.ReadAllLines(filePath);
                SqlConnection sqlconn = null;
                sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                foreach (var line in lines.Skip(25)) // 假设第一行是标题行，需要跳过
                {
                    var values = line.Split(',');
                    string input = values[5];
                    string pattern = @"[0-9.]+"; // 正则表达式，匹配一个或多个数字或小数点
                    Match match = Regex.Match(input, pattern);
                    number = match.Value; // 提取的数字部分                    
                    string numberStr = number;
                    decimal numberDecimal = decimal.Parse(numberStr);
                    int numberInt = (int)Math.Round(numberDecimal);

                    var bill = new wechatBill
                    {
                        date = DateTime.Parse(values[0]).ToString("yyyy/MM/dd"),
                        tran = values[2],
                        ic = values[4],
                        money = numberInt,
                        way = values[6],
                        // 映射其他字段...
                    };
                    bills.Add(bill);
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlconn;
           
                for (int i = 0; i < bills.Count; i++)
                {

                    // 检查数据库中是否已经存在相同的数据
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM ie WHERE 用户名=@Username AND 家庭=@Userfamily AND 日期 = @Date AND 收支类型 = @IC AND 金额 = @Money AND 途径类型 = @Way AND 用途类型=@Other AND 用途 = @Tran", sqlconn);
                    checkCmd.Parameters.AddWithValue("@Username", acc.Username);
                    checkCmd.Parameters.AddWithValue("@Userfamily", acc.Userfamily);
                    checkCmd.Parameters.AddWithValue("@Date", bills[i].date);
                    checkCmd.Parameters.AddWithValue("@IC", bills[i].ic);
                    checkCmd.Parameters.AddWithValue("@Money", bills[i].money);
                    checkCmd.Parameters.AddWithValue("@Way", bills[i].way);
                    checkCmd.Parameters.AddWithValue("@Other", "其他");
                    checkCmd.Parameters.AddWithValue("@Tran", bills[i].tran);

                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount == 0)
                    {
                        // 数据库中不存在相同数据，执行插入操作
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO ie VALUES (@Username, @Userfamily, @Date, @IC, @Money, @Way, @Other, @Tran)";
                        cmd.Parameters.AddWithValue("@Username", acc.Username);
                        cmd.Parameters.AddWithValue("@Userfamily", acc.Userfamily);
                        cmd.Parameters.AddWithValue("@Date", bills[i].date);
                        cmd.Parameters.AddWithValue("@IC", bills[i].ic);
                        cmd.Parameters.AddWithValue("@Money", bills[i].money);
                        cmd.Parameters.AddWithValue("@Way", bills[i].way);
                        cmd.Parameters.AddWithValue("@Other", "其他");
                        cmd.Parameters.AddWithValue("@Tran", bills[i].tran);

                        int jg = cmd.ExecuteNonQuery();
                    }
                }
                    sqlconn.Close();
                }
            
            catch (Exception ex)
            {
                // 处理异常
            }
            return bills;
        }

        public class AlipayBill
        {
            public string date { get; set; }
            public string tran { get; set; }
            public string ic { get; set; }
            public int money { get; set; }
            public string way { get; set; }
            // 其他你需要的字段...
        }

        public List<AlipayBill> ParseCsvFile1(string filePath)
        {
            var bills = new List<AlipayBill>();
            try
            {
                string number = null;
                var lines = File.ReadAllLines(filePath, Encoding.GetEncoding("GB2312"));
                SqlConnection sqlconn = null;
                sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                foreach (var line in lines.Skip(25)) // 假设第一行是标题行，需要跳过
                {
                    var values = line.Split(',');

                    string input = values[6];

                    decimal numberDecimal = decimal.Parse(input);
                    int numberInt = (int)Math.Round(numberDecimal);


                    var bill = new AlipayBill
                    {
                        date = DateTime.Parse(values[0]).ToString("yyyy/MM/dd"),
                        tran = values[2],
                        ic = values[5],
                        money = numberInt,
                        way = values[7],
                        // 映射其他字段...
                    };
                    bills.Add(bill);
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlconn;

                for (int i = 0; i < bills.Count; i++)
                {

                    // 检查数据库中是否已经存在相同的数据
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM ie WHERE 用户名=@Username AND 家庭=@Userfamily AND 日期 = @Date AND 收支类型 = @IC AND 金额 = @Money AND 途径类型 = @Way AND 用途类型=@Other AND 用途 = @Tran", sqlconn);
                    checkCmd.Parameters.AddWithValue("@Username", acc.Username);
                    checkCmd.Parameters.AddWithValue("@Userfamily", acc.Userfamily);
                    checkCmd.Parameters.AddWithValue("@Date", bills[i].date);
                    checkCmd.Parameters.AddWithValue("@IC", bills[i].ic);
                    checkCmd.Parameters.AddWithValue("@Money", bills[i].money);
                    checkCmd.Parameters.AddWithValue("@Way", bills[i].way);
                    checkCmd.Parameters.AddWithValue("@Other", "其他");
                    checkCmd.Parameters.AddWithValue("@Tran", bills[i].tran);

                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount == 0)
                    {
                        // 数据库中不存在相同数据，执行插入操作
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO ie VALUES (@Username, @Userfamily, @Date, @IC, @Money, @Way, @Other, @Tran)";
                        cmd.Parameters.AddWithValue("@Username", acc.Username);
                        cmd.Parameters.AddWithValue("@Userfamily", acc.Userfamily);
                        cmd.Parameters.AddWithValue("@Date", bills[i].date);
                        cmd.Parameters.AddWithValue("@IC", bills[i].ic);
                        cmd.Parameters.AddWithValue("@Money", bills[i].money);
                        cmd.Parameters.AddWithValue("@Way", bills[i].way);
                        cmd.Parameters.AddWithValue("@Other", "其他");
                        cmd.Parameters.AddWithValue("@Tran", bills[i].tran);

                        int jg = cmd.ExecuteNonQuery();
                    }
                }
                    sqlconn.Close();
                
            }
            catch (Exception ex)
            {
                // 处理异常
            }
            return bills;
        }
        private void barload()
        {
            string cm = DateTime.Now.ToString("yyyy/MM");
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format(@"select 
    SUM(CASE WHEN 用户名 = '{0}' AND 用途类型 = '衣' AND 收支类型 = '支出' and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS y,
    SUM(CASE WHEN 用户名 = '{2}' AND 用途类型 = '食' AND 收支类型 = '支出' and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS s,
    SUM(CASE WHEN 用户名 = '{4}' AND 用途类型 = '住' AND 收支类型 = '支出' and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS z,
    SUM(CASE WHEN 用户名 = '{6}' AND 用途类型 = '行' AND 收支类型 = '支出' and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS x,
    SUM(CASE WHEN 用户名 = '{8}' AND 用途类型 = '其他' AND 收支类型 = '支出' and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS q
    from ie", acc.Username,cm, acc.Username, cm, acc.Username, cm, acc.Username, cm, acc.Username, cm);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                int y = sdr.GetInt32("y");
                int s = sdr.GetInt32("s");
                int z= sdr.GetInt32("z");
                int x = sdr.GetInt32("x");
                int q= sdr.GetInt32("q");
                bar1.Value = 100 * y / (y + s + z + x + q+1);
                bar2.Value = 100 * s / (y + s + z + x + q+1);
                bar3.Value = 100 * z / (y + s + z + x + q+1);
                bar4.Value = 100 * x / (y + s + z + x + q+1);
                bar5.Value = 100 * q / (y + s + z + x + q+1);
            }
            sdr.Close();
            sqlconn.Close();
        }
        private async void LoadEX()
        {
            string p = null;
         
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiKey = "ec47c5279113bb6ded89c68f27df9207";
                    string apiUrl = "http://op.juhe.cn/onebox/exchange/currency";
                    string a = null, b = null;                   
                        a = c1.Text.Trim();
                        b = c2.Text.Trim();
                    string c = "2";
                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}?key={apiKey}&from={a}&to={b}&version={c}");

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(data);
                        JToken result = json["result"][0];
                        string cf=result["currencyF"].ToString();
                        string ct = result["currencyT"].ToString();
                        string exchangeRate = result["result"].ToString();
                        string cfn=result["currencyF_Name"].ToString();
                        string ctn = result["currencyT_Name"].ToString();
                        string cfd = result["currencyFD"].ToString();
                        string time = result["updateTime"].ToString();
                        cname1.Content = cf+cfn;
                        cname2.Content = ct+ctn;
                        currencyFD.Content = cfd;
                        resultrate.Content = exchangeRate;
                        datetime.Content = "查询时间："+time;
                    }                       
                   }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadNews()
        {
            string p = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiKey = "9aa438ac3f2b46fc5e68750d731ffbb2";
                    string apiUrl = "http://apis.juhe.cn/fapigx/caijing/query";

                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}?key={apiKey}");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(content);
                        if (json["error_code"].ToString() == "0")
                        {
                            JObject resultObject = (JObject)json["result"];
                            JArray newsList = (JArray)resultObject["newslist"];
                            string newsContent = string.Empty;
                            JToken result = json["result"]["newslist"][0];
                           
                                    string title = result["title"].ToString();
                                    string text = result["description"].ToString();
                                    string time = result["ctime"].ToString();
                                    string picurl = result["picUrl"].ToString();
                                    string source = result["source"].ToString();
                                      title1.Text = title + "-----------" + source;
                                    BitmapImage bitmapImage = new BitmapImage(new Uri("https:" + picurl));
                                   image1.Source = bitmapImage;
                                }
                            }                      
                    }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void recommendload()
        {
            adddate.Text = DateTime.Today.ToString("yyyy/MM/dd");
            int time = DateTime.Now.Hour;
            switch (time)
            {
                case 7:
                    addoi.Text = "支出";
                    addtype.Text = "食";
                    addu.Text = "早餐";
                    break;
                case 12:
                    addoi.Text = "支出";
                    addtype.Text = "食";
                    addu.Text = "午餐";
                    break;
                case 19:
                    addoi.Text = "支出";
                    addtype.Text = "食";
                    addu.Text = "晚餐";
                    break;
                default:     
                    string a=null,b=null;
                    SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                    sqlconn.Open();
                    string sql = string.Format(@"SELECT TOP 1 用途类型, 收支类型, 途径类型 FROM ie WHERE num IN(SELECT TOP 20 num FROM ie where 用户名='{0}' ORDER BY num DESC) GROUP BY 用途类型, 收支类型, 途径类型 ORDER BY COUNT(*) DESC; ",
                        acc.Username);
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        a = sdr["收支类型"].ToString();
                        b= sdr["用途类型"].ToString();
                        // Assuming addtype and addoi are ComboBox components
                       addoi.Items.Add(sdr["收支类型"].ToString()); // Add item to ComboBox
                       addtype.Items.Add(sdr["用途类型"].ToString()); // Add item to ComboBox
                       
                        // Get the value of the last item added to ComboBox
                         addoi.Text = addoi.Items[addtype.Items.Count - 1].ToString();
                         addtype.Text = addtype.Items[addtype.Items.Count - 1].ToString();
                       
                    }
                    sdr.Close();
                    sqlconn.Close();
                    break;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
                    
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadEX();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }
            string path = openFileDialog.FileName;
            if(wa.Text == "微信账单")
            {
                ParseCsvFile(path);
            }
            if(wa.Text == "支付宝账单")
            {
                ParseCsvFile1(path);
            }

            dailyfinance p = new dailyfinance();
            h.Content = p;
        }

        private void wora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool ischeck()
        {
            if (adddate.Text.Trim().Length == 0 || addmoney.Text.Trim().Length == 0 || addu.Text.Trim().Length == 0 || addtype.Text.Trim().Length == 0 || addoi.Text.Trim().Length == 0 || addway.Text.Trim().Length == 0)
            {
                MessageBox.Show("不能有空");
                return false;
            }
            return true;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
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

        private void Card_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            financialanalysis p=new financialanalysis();
            h.Content = p;
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            financialnews p=new financialnews();
            h.Content = p;
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            financialanalysis p= new financialanalysis();
            h.Content= p;
        }
    }
}
