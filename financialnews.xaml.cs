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
        }

        private const string ApiUrl = "http://apis.juhe.cn/fapigx/caijing/query?key=9aa438ac3f2b46fc5e68750d731ffbb2&type=top&page=1"; // 替换YOUR_API_KEY为你在聚合数据上获得的API Key

        private async void LoadNews()
        {
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
                            Expander[] a = new Expander[3]; // 创建一个长度为3的 Expander 数组
                            a[0] = news1;
                            a[1] = news2;
                            a[2] = news3;
                            TextBlock[] b = new TextBlock[3]; // 创建一个长度为3的 Expander 数组
                            b[0] = text1;
                            b[1] = text2;
                            b[2] = text3;
                            TextBlock[] c = new TextBlock[3]; // 创建一个长度为3的 Expander 数组
                            c[0] = title1;
                            c[1] = title2;
                            c[2] = title3;
                            foreach (JObject newsItem in newsList)
                            {
                              if(count<3)
                                {
                                    string title = newsItem["title"].ToString();
                                    string text = newsItem["description"].ToString();
                                    string time = newsItem["ctime"].ToString();
                                    b[count].Text = "内容："+text;
                                     a[count].Header = title;
                                    c[count].Text = "标题："+title;
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

        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
