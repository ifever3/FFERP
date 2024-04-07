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
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;

namespace FFERP
{
    /// <summary>
    /// financialanalysis.xaml 的交互逻辑
    /// </summary>
    public partial class financialanalysis : Page
    {
        List<int> a = new List<int>();
        List<int> b = new List<int>();

        ColumnSeries outcome = new ColumnSeries { Title = "outcome" };      
        //新建一条温度的柱状图
        ColumnSeries income = new ColumnSeries { Title = "income" };

        public financialanalysis()
        {
            InitializeComponent();            
            setdata();
            SeriesCollection = new SeriesCollection { };
            SeriesCollection.Add(outcome);
            SeriesCollection.Add(income);
            DataContext = this;
            cartesianchart();
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;         
            pa.Text = selectedSeries.Title + "排行榜";

            if (uof.IsChecked is false)//个人
            {
                if (type.IsChecked == false)
                {
                    pchange1(selectedSeries.Title.ToString());
                }
                if (type.IsChecked == true)
                {
                    pchange3(selectedSeries.Title.ToString());
                }
            }
            if (uof.IsChecked is true)//家庭
            {
                if (type.IsChecked == false)
                {
                    pchange2(selectedSeries.Title.ToString());
                }
                if (type.IsChecked == true)
                {
                    pchange4(selectedSeries.Title.ToString());
                }
            }
           
        }

        private void pchange1(string p)
        {
                SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
                sqlconn.Open();
                string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 用户名='{0}'and 日期 like '%{1}%' and 收支类型='支出' and 用途类型='{2}'", acc.Username, tb_calendar.Text,p);
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable ds = new DataTable();
                sda.Fill(ds);//填充表
                data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

                string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 用户名='{0}' and 收支类型='支出' and 日期 like '%{1}%' and 用途类型='{2}'", acc.Username, tb_calendar.Text,p);
                SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    count.Text = sdr1["count"].ToString();
                    num.Text = sdr1["msum"].ToString();
                }
                sdr1.Close();
                sqlconn.Close();
        }
        private void pchange2(string p)//家庭支出
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'and 日期 like '%{1}%' and 收支类型='支出' and 用途类型='{2}'", acc.Userfamily, tb_calendar.Text,p);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 家庭='{0}' and 收支类型='支出' and 日期 like '%{1}%' and 用途类型='{2}'", acc.Userfamily, tb_calendar.Text,p);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sdr1.Close();
            sqlconn.Close();
        }
        private void pchange3(string p)//个人收入
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 用户名='{0}'and 日期 like '%{1}%' and 收支类型='收入' and 用途类型='{2}'", acc.Username, tb_calendar.Text,p);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 用户名='{0}' and 收支类型='收入' and 日期 like '%{1}%' and 用途类型='{2}'", acc.Username, tb_calendar.Text,p);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sdr1.Close();
            sqlconn.Close();
        }
        private void pchange4(string p)//家庭收入
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'and 日期 like '%{1}%' and 收支类型='收入' and 用途类型='{2}'", acc.Userfamily, tb_calendar.Text,p);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 家庭='{0}' and 收支类型='收入' and 日期 like '%{1}%'", acc.Userfamily, tb_calendar.Text,p);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sdr1.Close();
            sqlconn.Close();
        }
        public void cartesianchart()
        {
            
            string y = calendar.DisplayDate.Date.ToString("yyyy");
            a.Clear();
            b.Clear();

            string[] m = new string[] { y+"/01", y + "/02", y + "/03", y + "/04", y + "/05", y + "/06", y + "/07", y + "/08", y + "/09", y + "/10", y + "/11", y +"/12" };
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();           
            string sql1 = string.Format(@"select 
      SUM(CASE WHEN 用户名 = '{0}'  AND 收支类型 = '支出'and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS jan,
    SUM(CASE WHEN 用户名 = '{2}'  AND 收支类型 = '支出'and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS feb,
    SUM(CASE WHEN 用户名 = '{4}'  AND 收支类型 = '支出'and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS mar,
    SUM(CASE WHEN 用户名 = '{6}'  AND 收支类型 = '支出'and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS apr,
    SUM(CASE WHEN 用户名 = '{8}'  AND 收支类型 = '支出'and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS may,
    SUM(CASE WHEN 用户名 = '{10}'  AND 收支类型 = '支出'and 日期 like '%{11}%' THEN 金额 ELSE 0 END) AS jun,
    SUM(CASE WHEN 用户名 = '{12}'  AND 收支类型 = '支出'and 日期 like '%{13}%' THEN 金额 ELSE 0 END) AS jul,
    SUM(CASE WHEN 用户名 = '{14}'  AND 收支类型 = '支出'and 日期 like '%{15}%' THEN 金额 ELSE 0 END) AS aug,
    SUM(CASE WHEN 用户名 = '{16}'  AND 收支类型 = '支出'and 日期 like '%{17}%' THEN 金额 ELSE 0 END) AS sep,
    SUM(CASE WHEN 用户名 = '{18}'  AND 收支类型 = '支出'and 日期 like '%{19}%' THEN 金额 ELSE 0 END) AS oct,
    SUM(CASE WHEN 用户名 = '{20}'  AND 收支类型 = '支出'and 日期 like '%{21}%' THEN 金额 ELSE 0 END) AS nov,
    SUM(CASE WHEN 用户名 = '{22}'  AND 收支类型 = '支出'and 日期 like '%{23}%' THEN 金额 ELSE 0 END) AS dece
    from ie",acc.Username,m[0], acc.Username, m[1], acc.Username, m[2], acc.Username, m[3], acc.Username, m[4], acc.Username, m[5], acc.Username, m[6], acc.Username, m[7], acc.Username, m[8], acc.Username, m[9], acc.Username, m[10], acc.Username, m[11]);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                for(int i = 0; i <=11; i++)
                {
                    a.Add(sdr1.GetInt32(i));                    
                }
            }
            sdr1.Close();

            string sql2 = string.Format(@"select 
    SUM(CASE WHEN 用户名 = '{0}'  AND 收支类型 = '收入'and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS jan,
    SUM(CASE WHEN 用户名 = '{2}'  AND 收支类型 = '收入'and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS feb,
    SUM(CASE WHEN 用户名 = '{4}'  AND 收支类型 = '收入'and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS mar,
    SUM(CASE WHEN 用户名 = '{6}'  AND 收支类型 = '收入'and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS apr,
    SUM(CASE WHEN 用户名 = '{8}'  AND 收支类型 = '收入'and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS may,
    SUM(CASE WHEN 用户名 = '{10}'  AND 收支类型 = '收入'and 日期 like '%{11}%' THEN 金额 ELSE 0 END) AS jun,
    SUM(CASE WHEN 用户名 = '{12}'  AND 收支类型 = '收入'and 日期 like '%{13}%' THEN 金额 ELSE 0 END) AS jul,
    SUM(CASE WHEN 用户名 = '{14}'  AND 收支类型 = '收入'and 日期 like '%{15}%' THEN 金额 ELSE 0 END) AS aug,
    SUM(CASE WHEN 用户名 = '{16}'  AND 收支类型 = '收入'and 日期 like '%{17}%' THEN 金额 ELSE 0 END) AS sep,
    SUM(CASE WHEN 用户名 = '{18}'  AND 收支类型 = '收入'and 日期 like '%{19}%' THEN 金额 ELSE 0 END) AS oct,
    SUM(CASE WHEN 用户名 = '{20}'  AND 收支类型 = '收入'and 日期 like '%{21}%' THEN 金额 ELSE 0 END) AS nov,
    SUM(CASE WHEN 用户名 = '{22}'  AND 收支类型 = '收入'and 日期 like '%{23}%' THEN 金额 ELSE 0 END) AS dece
    from ie", acc.Username, m[0], acc.Username, m[1], acc.Username, m[2], acc.Username, m[3], acc.Username, m[4], acc.Username, m[5], acc.Username, m[6], acc.Username, m[7], acc.Username, m[8], acc.Username, m[9], acc.Username, m[10], acc.Username, m[11]);
            SqlCommand cmd2 = new SqlCommand(sql2, sqlconn);
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            if (sdr2.Read())
            {
                for (int i = 0; i < 12; i++)
                {
                    b.Add( sdr2.GetInt32(i));
                }
            }
            sdr2.Close();

            sqlconn.Close();
                
            outcome.Values = new ChartValues<int>(a);
            income.Values = new ChartValues<int>(b);
            month.Labels=new ChartValues<string>(m);                    
            Formatter = value => value.ToString("N");   
        }

        public void cartesianchart1()
        {

            string y = calendar.DisplayDate.Date.ToString("yyyy");
            a.Clear();
            b.Clear();

            string[] m = new string[] { y + "/01", y + "/02", y + "/03", y + "/04", y + "/05", y + "/06", y + "/07", y + "/08", y + "/09", y + "/10", y + "/11", y + "/12" };
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql1 = string.Format(@"select 
      SUM(CASE WHEN 家庭 = '{0}'  AND 收支类型 = '支出'and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS jan,
    SUM(CASE WHEN 家庭 = '{2}'  AND 收支类型 = '支出'and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS feb,
    SUM(CASE WHEN 家庭 = '{4}'  AND 收支类型 = '支出'and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS mar,
    SUM(CASE WHEN 家庭 = '{6}'  AND 收支类型 = '支出'and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS apr,
    SUM(CASE WHEN 家庭 = '{8}'  AND 收支类型 = '支出'and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS may,
    SUM(CASE WHEN 家庭 = '{10}'  AND 收支类型 = '支出'and 日期 like '%{11}%' THEN 金额 ELSE 0 END) AS jun,
    SUM(CASE WHEN 家庭 = '{12}'  AND 收支类型 = '支出'and 日期 like '%{13}%' THEN 金额 ELSE 0 END) AS jul,
    SUM(CASE WHEN 家庭 = '{14}'  AND 收支类型 = '支出'and 日期 like '%{15}%' THEN 金额 ELSE 0 END) AS aug,
    SUM(CASE WHEN 家庭 = '{16}'  AND 收支类型 = '支出'and 日期 like '%{17}%' THEN 金额 ELSE 0 END) AS sep,
    SUM(CASE WHEN 家庭 = '{18}'  AND 收支类型 = '支出'and 日期 like '%{19}%' THEN 金额 ELSE 0 END) AS oct,
    SUM(CASE WHEN 家庭 = '{20}'  AND 收支类型 = '支出'and 日期 like '%{21}%' THEN 金额 ELSE 0 END) AS nov,
    SUM(CASE WHEN 家庭 = '{22}'  AND 收支类型 = '支出'and 日期 like '%{23}%' THEN 金额 ELSE 0 END) AS dece
    from ie", acc.Userfamily, m[0], acc.Userfamily, m[1], acc.Userfamily, m[2], acc.Userfamily, m[3], acc.Userfamily, m[4], acc.Userfamily, m[5], acc.Userfamily, m[6], acc.Userfamily, m[7], acc.Userfamily, m[8], acc.Userfamily, m[9], acc.Userfamily, m[10], acc.Userfamily, m[11]);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                for (int i = 0; i <= 11; i++)
                {
                    a.Add(sdr1.GetInt32(i));
                }
            }
            sdr1.Close();

            string sql2 = string.Format(@"select 
    SUM(CASE WHEN 家庭 = '{0}'  AND 收支类型 = '收入'and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS jan,
    SUM(CASE WHEN 家庭 = '{2}'  AND 收支类型 = '收入'and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS feb,
    SUM(CASE WHEN 家庭 = '{4}'  AND 收支类型 = '收入'and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS mar,
    SUM(CASE WHEN 家庭 = '{6}'  AND 收支类型 = '收入'and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS apr,
    SUM(CASE WHEN 家庭 = '{8}'  AND 收支类型 = '收入'and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS may,
    SUM(CASE WHEN 家庭 = '{10}'  AND 收支类型 = '收入'and 日期 like '%{11}%' THEN 金额 ELSE 0 END) AS jun,
    SUM(CASE WHEN 家庭 = '{12}'  AND 收支类型 = '收入'and 日期 like '%{13}%' THEN 金额 ELSE 0 END) AS jul,
    SUM(CASE WHEN 家庭 = '{14}'  AND 收支类型 = '收入'and 日期 like '%{15}%' THEN 金额 ELSE 0 END) AS aug,
    SUM(CASE WHEN 家庭 = '{16}'  AND 收支类型 = '收入'and 日期 like '%{17}%' THEN 金额 ELSE 0 END) AS sep,
    SUM(CASE WHEN 家庭 = '{18}'  AND 收支类型 = '收入'and 日期 like '%{19}%' THEN 金额 ELSE 0 END) AS oct,
    SUM(CASE WHEN 家庭 = '{20}'  AND 收支类型 = '收入'and 日期 like '%{21}%' THEN 金额 ELSE 0 END) AS nov,
    SUM(CASE WHEN 家庭 = '{22}'  AND 收支类型 = '收入'and 日期 like '%{23}%' THEN 金额 ELSE 0 END) AS dece
    from ie", acc.Userfamily, m[0], acc.Userfamily, m[1], acc.Userfamily, m[2], acc.Userfamily, m[3], acc.Userfamily, m[4], acc.Userfamily, m[5], acc.Userfamily, m[6], acc.Userfamily, m[7], acc.Userfamily, m[8], acc.Userfamily, m[9], acc.Userfamily, m[10], acc.Userfamily, m[11]);
            SqlCommand cmd2 = new SqlCommand(sql2, sqlconn);
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            if (sdr2.Read())
            {
                for (int i = 0; i < 12; i++)
                {
                    b.Add(sdr2.GetInt32(i));
                }
            }
            sdr2.Close();

            sqlconn.Close();

            outcome.Values = new ChartValues<int>(a);
            income.Values = new ChartValues<int>(b);
            month.Labels = new ChartValues<string>(m);
            Formatter = value => value.ToString("N");
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

       
        private void setdata()//个人支出
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 用户名='{0}'and 日期 like '%{1}%' and 收支类型='支出'", acc.Username,tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 用户名='{0}' and 收支类型='支出' and 日期 like '%{1}%'", acc.Username, tb_calendar.Text);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();     
                num.Text=sdr1["msum"].ToString();
            }
            sdr1.Close();           
            sqlconn.Close();
            chartdata();
        }
       
        private void setdata1()//家庭支出
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'and 日期 like '%{1}%' and 收支类型='支出'", acc.Userfamily, tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 家庭='{0}' and 收支类型='支出' and 日期 like '%{1}%'", acc.Userfamily,tb_calendar.Text);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sdr1.Close();
            sqlconn.Close();

            chartdata1();
        }
        private void setdata2()//个人收入
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 用户名='{0}'and 日期 like '%{1}%' and 收支类型='收入'", acc.Username,tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 用户名='{0}' and 收支类型='收入' and 日期 like '%{1}%'", acc.Username,tb_calendar.Text);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sdr1.Close();
            sqlconn.Close();

            chartdata2();
        }
        private void setdata3()//家庭收入
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY 金额 DESC) AS 排行, * FROM ieview where 家庭='{0}'and 日期 like '%{1}%' and 收支类型='收入'", acc.Userfamily, tb_calendar.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable ds = new DataTable();
            sda.Fill(ds);//填充表
            data1.ItemsSource = ds.DefaultView;//填充到系统的视图中

            string sql1 = string.Format("select count(*) as count,SUM(金额) as msum from ie where 家庭='{0}' and 收支类型='收入' and 日期 like '%{1}%'", acc.Userfamily,tb_calendar.Text);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                count.Text = sdr1["count"].ToString();
                num.Text = sdr1["msum"].ToString();
            }
            sdr1.Close();
            sqlconn.Close();

            chartdata3();
        }


        private void chartdata()//个人支出总额
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();           
            string sql = string.Format(@"select 
    SUM(CASE WHEN 用户名 = '{0}' AND 用途类型 = '衣' AND 收支类型 = '支出' and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS y,
    SUM(CASE WHEN 用户名 = '{2}' AND 用途类型 = '食' AND 收支类型 = '支出' and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS s,
    SUM(CASE WHEN 用户名 = '{4}' AND 用途类型 = '住' AND 收支类型 = '支出' and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS z,
    SUM(CASE WHEN 用户名 = '{6}' AND 用途类型 = '行' AND 收支类型 = '支出' and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS x,
    SUM(CASE WHEN 用户名 = '{8}' AND 用途类型 = '其他' AND 收支类型 = '支出' and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS q
    from ie", acc.Username,tb_calendar.Text, acc.Username, tb_calendar.Text, acc.Username, tb_calendar.Text, acc.Username, tb_calendar.Text, acc.Username, tb_calendar.Text);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cloth.Values = new ChartValues<int> {sdr.GetInt32("y")};
                tran.Values = new ChartValues<int> { sdr.GetInt32("x") };
                eat.Values = new ChartValues<int> { sdr.GetInt32("s") };
                live.Values = new ChartValues<int> { sdr.GetInt32("z") };
                other.Values = new ChartValues<int> { sdr.GetInt32("q") };
            }
            sdr.Close();
            sqlconn.Close();
      
            PointLabel = chartPoint => string.Format("{0} {1:p1}", cloth.Title.ToString(), chartPoint.Participation);
            PointLabel1 = chartPoint => string.Format("{0} {1:p1}", eat.Title.ToString(), chartPoint.Participation);
            PointLabel2 = chartPoint => string.Format("{0} {1:p1}", live.Title.ToString(), chartPoint.Participation);
            PointLabel3 = chartPoint => string.Format("{0} {1:p1}", tran.Title.ToString(), chartPoint.Participation);
            PointLabel4 = chartPoint => string.Format("{0} {1:p1}", other.Title.ToString(), chartPoint.Participation);
        }
        private void chartdata1()//家庭支出总额
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format(@"select 
    SUM(CASE WHEN 家庭 = '{0}' AND 用途类型 = '衣' AND 收支类型 = '支出' and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS y,
    SUM(CASE WHEN 家庭 = '{2}' AND 用途类型 = '食' AND 收支类型 = '支出' and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS s,
    SUM(CASE WHEN 家庭 = '{4}' AND 用途类型 = '住' AND 收支类型 = '支出' and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS z,
    SUM(CASE WHEN 家庭 = '{6}' AND 用途类型 = '行' AND 收支类型 = '支出' and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS x,
    SUM(CASE WHEN 家庭 = '{8}' AND 用途类型 = '其他' AND 收支类型 = '支出' and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS q
    from ie", acc.Userfamily,tb_calendar.Text, acc.Userfamily, tb_calendar.Text, acc.Userfamily, tb_calendar.Text, acc.Userfamily, tb_calendar.Text, acc.Userfamily, tb_calendar.Text);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cloth.Values = new ChartValues<int> { sdr.GetInt32("y") };
                tran.Values = new ChartValues<int> { sdr.GetInt32("x") };
                eat.Values = new ChartValues<int> { sdr.GetInt32("s") };
                live.Values = new ChartValues<int> { sdr.GetInt32("z") };
                other.Values = new ChartValues<int> { sdr.GetInt32("q") };
            }
            sdr.Close();
            sqlconn.Close();

            PointLabel = chartPoint => string.Format("{0} {1:p1}", cloth.Title.ToString(), chartPoint.Participation);
            PointLabel1 = chartPoint => string.Format("{0} {1:p1}", eat.Title.ToString(), chartPoint.Participation);
            PointLabel2 = chartPoint => string.Format("{0} {1:p1}", live.Title.ToString(), chartPoint.Participation);
            PointLabel3 = chartPoint => string.Format("{0} {1:p1}", tran.Title.ToString(), chartPoint.Participation);
            PointLabel4 = chartPoint => string.Format("{0} {1:p1}", other.Title.ToString(), chartPoint.Participation);
        }
        private void chartdata2()//个人收入总额
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format(@"select 
    SUM(CASE WHEN 用户名 = '{0}' AND 用途类型 = '衣' AND 收支类型 = '收入' and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS y,
    SUM(CASE WHEN 用户名 = '{2}' AND 用途类型 = '食' AND 收支类型 = '收入' and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS s,
    SUM(CASE WHEN 用户名 = '{4}' AND 用途类型 = '住' AND 收支类型 = '收入' and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS z,
    SUM(CASE WHEN 用户名 = '{6}' AND 用途类型 = '行' AND 收支类型 = '收入' and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS x,
    SUM(CASE WHEN 用户名 = '{8}' AND 用途类型 = '其他' AND 收支类型 = '收入' and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS q
    from ie", acc.Username,tb_calendar.Text, acc.Username, tb_calendar.Text, acc.Username, tb_calendar.Text, acc.Username, tb_calendar.Text, acc.Username, tb_calendar.Text);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cloth.Values = new ChartValues<int> { sdr.GetInt32("y") };
                tran.Values = new ChartValues<int> { sdr.GetInt32("x") };
                eat.Values = new ChartValues<int> { sdr.GetInt32("s") };
                live.Values = new ChartValues<int> { sdr.GetInt32("z") };
                other.Values = new ChartValues<int> { sdr.GetInt32("q") };
            }
            sdr.Close();
            sqlconn.Close();

            PointLabel = chartPoint => string.Format("{0} {1:p1}", cloth.Title.ToString(), chartPoint.Participation);
            PointLabel1 = chartPoint => string.Format("{0} {1:p1}", eat.Title.ToString(), chartPoint.Participation);
            PointLabel2 = chartPoint => string.Format("{0} {1:p1}", live.Title.ToString(), chartPoint.Participation);
            PointLabel3 = chartPoint => string.Format("{0} {1:p1}", tran.Title.ToString(), chartPoint.Participation);
            PointLabel4 = chartPoint => string.Format("{0} {1:p1}", other.Title.ToString(), chartPoint.Participation);
        }
        private void chartdata3()//家庭收入总额
        {
            SqlConnection sqlconn = new SqlConnection("server=LAPTOP-LJQH2OK2;uid=sa;pwd=123;database=FF ERP");
            sqlconn.Open();
            string sql = string.Format(@"select 
    SUM(CASE WHEN 家庭 = '{0}' AND 用途类型 = '衣' AND 收支类型 = '收入' and 日期 like '%{1}%' THEN 金额 ELSE 0 END) AS y,
    SUM(CASE WHEN 家庭 = '{2}' AND 用途类型 = '食' AND 收支类型 = '收入' and 日期 like '%{3}%' THEN 金额 ELSE 0 END) AS s,
    SUM(CASE WHEN 家庭 = '{4}' AND 用途类型 = '住' AND 收支类型 = '收入' and 日期 like '%{5}%' THEN 金额 ELSE 0 END) AS z,
    SUM(CASE WHEN 家庭 = '{6}' AND 用途类型 = '行' AND 收支类型 = '收入' and 日期 like '%{7}%' THEN 金额 ELSE 0 END) AS x,
    SUM(CASE WHEN 家庭 = '{8}' AND 用途类型 = '其他' AND 收支类型 = '收入' and 日期 like '%{9}%' THEN 金额 ELSE 0 END) AS q
    from ie", acc.Userfamily,tb_calendar.Text, acc.Userfamily, tb_calendar.Text, acc.Userfamily, tb_calendar.Text, acc.Userfamily, tb_calendar.Text, acc.Userfamily, tb_calendar.Text);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cloth.Values = new ChartValues<int> { sdr.GetInt32("y") };
                tran.Values = new ChartValues<int> { sdr.GetInt32("x") };
                eat.Values = new ChartValues<int> { sdr.GetInt32("s") };
                live.Values = new ChartValues<int> { sdr.GetInt32("z") };
                other.Values = new ChartValues<int> { sdr.GetInt32("q") };
            }
            sdr.Close();
            sqlconn.Close();

            PointLabel = chartPoint => string.Format("{0} {1:p1}", cloth.Title.ToString(), chartPoint.Participation);
            PointLabel1 = chartPoint => string.Format("{0} {1:p1}", eat.Title.ToString(), chartPoint.Participation);
            PointLabel2 = chartPoint => string.Format("{0} {1:p1}", live.Title.ToString(), chartPoint.Participation);
            PointLabel3 = chartPoint => string.Format("{0} {1:p1}", tran.Title.ToString(), chartPoint.Participation);
            PointLabel4 = chartPoint => string.Format("{0} {1:p1}", other.Title.ToString(), chartPoint.Participation);
        }


        public Func<ChartPoint, string> PointLabel { get; set; }
        public Func<ChartPoint, string> PointLabel1 { get; set; }
        public Func<ChartPoint, string> PointLabel2 { get; set; }
        public Func<ChartPoint, string> PointLabel3 { get; set; }
        public Func<ChartPoint, string> PointLabel4 { get; set; }




        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            debt p = new debt();
            fa.Content = p;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bankaccount p = new bankaccount();
           fa.Content = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dailyfinance p = new dailyfinance();
            fa.Content = p;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            financialanalysis p = new financialanalysis();
            fa.Content = p;
        }


        private void calendar_img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bill.IsChecked is false)
            {
                calendar.Visibility = Visibility.Visible;
            }
            if (bill.IsChecked is true)
            {
                calendar1.Visibility = Visibility.Visible;
            }
        }
        private void calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {      
                if (calendar.DisplayMode == CalendarMode.Month || calendar.DisplayMode == CalendarMode.Year)
                {
                    calendar.DisplayMode = CalendarMode.Decade;
                }                                                                
        }
        private void calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            tb_calendar.Text = calendar.DisplayDate.Date.ToString("yyyy");
            calendar.Visibility = Visibility.Hidden;
        }     
        private void calendar1_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            tb_calendar.Text = calendar1.DisplayDate.Date.ToString("yyyy/MM");
            calendar1.Visibility = Visibility.Hidden;         
        }
        private void calendar1_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (calendar1.DisplayMode == CalendarMode.Month || calendar1.DisplayMode == CalendarMode.Decade)
            {
                calendar1.DisplayMode = CalendarMode.Year;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            pa.Text = "排行榜";
            if (uof.IsChecked is false )//个人
            {
                if(type.IsChecked == false)
                {
                    setdata();
                    cartesianchart();
                }
                if(type.IsChecked == true)
                {
                    setdata2();
                    cartesianchart();
                }
            }           
            if (uof.IsChecked is true )//家庭
            {
                if(type.IsChecked == false)
                {
                    setdata1();
                    cartesianchart1();
                }
                if(type.IsChecked == true)
                {
                    setdata3();
                    cartesianchart1();
                }
            }            
        }
               
    }
}
