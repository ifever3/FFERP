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
using LiveCharts;
using LiveCharts.Wpf;

namespace FFERP
{
    /// <summary>
    /// financialnews.xaml 的交互逻辑
    /// </summary>
    public partial class financialnews : Page
    {
        public financialnews()
        {
            InitializeComponent();
            LoadNews();
            LoadStock();
        }


       static string sc = "sh601009";
        private async void LoadStock()
        {
            string p = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiKey = "04509a18799a89673da5c04d5dfeeb05";
                    string apiUrl = "http://web.juhe.cn/finance/stock/hs";
                    
                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}?gid={sc}&key={apiKey}");
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(data);
                        JToken result = json["result"][0]["data"];

                        string stockname = result["name"].ToString();
                        string nowprice = result["nowPri"].ToString();
                        string increasePercent = result["increPer"].ToString();
                        string increaseAmount = result["increase"].ToString();
                        string tosp= result["nowPri"].ToString();
                        string yeep = result["yestodEndPri"].ToString();
                        string tomax = result["todayMax"].ToString();
                        string tomin = result["todayMin"].ToString();
                        string d=result["date"].ToString();
                        string time=result["time"].ToString();
                        sname.Content ="股票名称："+ stockname;
                        nowp.Content = "当前价格：" + nowprice;
                        increper.Content = "跌涨百分比：" + increasePercent;
                        increase.Content = "跌涨额：" + increaseAmount;
                        tsp.Content = "今日开盘价：" + tosp;
                        yep.Content = "昨日收盘价：" + yeep;
                        tmax.Content = "今日最高价：" + tomax;
                        tmin.Content = "今日最低价：" + tomin;
                        date.Content = "时期时间：" + d + time;



                        JToken kresult = json["result"][0]["gopicture"];
                        string kimage = kresult["dayurl"].ToString();
                        BitmapImage bitmapImage = new BitmapImage(new Uri( kimage));
                        k.Source = bitmapImage;
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
                            int count = 0;
                            Expander[] a = new Expander[5]; // 创建一个长度为3的 Expander 数组
                            a[0] = news1;
                            a[1] = news2;
                            a[2] = news3;
                            a[3] = news4;
                            a[4] = news5;
                            TextBlock[] b = new TextBlock[5]; // 创建一个长度为3的 Expander 数组
                            b[0] = text1;
                            b[1] = text2;
                            b[2] = text3;
                            b[3] = text4;
                            b[4] = text5;
                            TextBlock[] c = new TextBlock[5]; // 创建一个长度为3的 Expander 数组
                            c[0] = title1;
                            c[1] = title2;
                            c[2] = title3;
                            c[3] = title4;
                            c[4] = title5;
                            Image[] d = new Image[5]; // 创建一个长度为3的 Expander 数组
                            d[0] = image1;
                            d[1] = image2;
                            d[2] = image3;
                            d[3] = image4;
                            d[4] = image5;
                            foreach (JObject newsItem in newsList)
                            {
                              if(count<5)
                                {
                                    string title = newsItem["title"].ToString();
                                    string text = newsItem["description"].ToString();
                                    string time = newsItem["ctime"].ToString();
                                    string picurl = newsItem["picUrl"].ToString();
                                    b[count].Text = "内容："+text;
                                    a[count].Header = title;
                                    c[count].Text = "标题："+title;                                  
                                    
                                    BitmapImage bitmapImage = new BitmapImage(new Uri("https:" + picurl));
                                    d[count].Source = bitmapImage;
                                    count++;
                                }
                                else
                                {
                                    break;
                                }                                                                     
                            }
                          
                        }
                        else
                        {
                            MessageBox.Show(json["error_msg"].ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            budgetset p = new budgetset();
            news.Content = p;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            budgetrecommend p = new budgetrecommend();
            news.Content = p;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            financialnews p= new financialnews();
            news.Content = p;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            feedback p= new feedback();
            news.Content= p;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(stockcode.Text!=null)
            {
                sc=stockcode.Text.Trim();
                LoadStock();
            }
        }
    }
}
