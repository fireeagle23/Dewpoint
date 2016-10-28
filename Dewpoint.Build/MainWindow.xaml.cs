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
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [Serializable]
    public partial class MainWindow : Window
    {
       string openFileName = "save.bin";
       string saveFileName = "save.bin";
       public string label3content;
       public string label4content;
        public MainWindow()
        {

            InitializeComponent();
            IFormatter formatter = new BinaryFormatter();
            
            if (!File.Exists(openFileName))
            {
                File.Create(openFileName);
                RaidioButton1.IsChecked = true;
                RaidioButton1.Checked += new RoutedEventHandler(radio_Checked);
            }
                
            else 
            {
                Stream stream = new FileStream(this.openFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                RaidioButton1.IsChecked = (bool)formatter.Deserialize(stream);
                RaidioButton2.IsChecked = (bool)formatter.Deserialize(stream);
                TB1.Text = (string)formatter.Deserialize(stream);
                TB2.Text = (string)formatter.Deserialize(stream);            
                RaidioButton1.Checked += new RoutedEventHandler(radio_Checked);
                RaidioButton2.Checked += new RoutedEventHandler(radio_Checked);                                    
                label3content = (string)formatter.Deserialize(stream);
                label4content = (string)formatter.Deserialize(stream);
                stream.Close();
            }
            if (RaidioButton1.IsChecked == false && RaidioButton2.IsChecked == false)
                RaidioButton1.IsChecked = true;
            label3.Content = label3content;
            label4.Content = label4content;
          }
         public void Close_clickHandler(Object sender, RoutedEventArgs e)
        {
             this.Close();
         }
        double uncommon(string x){
             bool tmp = bolNum(x);
             if (string.IsNullOrEmpty(x)||bolNum(x)==false)
                return 0;
            else
                return Convert.ToDouble(x);
        }
         public bool bolNum(string x)
         {
             string regextext = @"^(-?\d+)(\.\d+)?$";
             Regex regex = new Regex(regextext, RegexOptions.None);
             return regex.IsMatch(x.Trim());
         }
 
        void radio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn != null)
            {
                label3.Content = (RaidioButton1.IsChecked == true ? "常压露点温度  =" : "压力露点温度  =");
                label4.Content = (RaidioButton1.IsChecked == true ? "压力露点温度  =" : "常压露点温度  =");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WpfApplication1.Methmetica methimetica = new WpfApplication1.Methmetica();
            double pdb = 0, p, tdb, pv = 0;
            string ptemp = TB1.Text;
            p = uncommon(ptemp);
            string tdbtemp = TB2.Text;
            tdb = uncommon(tdbtemp);
            double p1 = p + 0.101325;
            double Ttmp = tdb + 273.15;
            methimetica.tdb = tdb;
            methimetica.t = Ttmp;
            if (tdb > 90 || tdb < -100)
                MessageBox.Show("超出计算范围");
            else
                pdb = methimetica.PCount();
            if (RaidioButton1.IsChecked == true)
                pv = pdb * p1 / 0.101325;
            if (RaidioButton2.IsChecked == true)
                pv = pdb * 0.101325 / p1;
            methimetica.pv = pv;
            double Tf = methimetica.Tfcount();
            if (bolNum(ptemp) == false || bolNum(tdbtemp) == false)
            {
                MessageBox.Show("请输入数字");
                TB1.Text = null;
                TB2.Text = null;
                TB3.Text = null;
            }
            else
                TB3.Text = Convert.ToDouble(Tf).ToString("0.00");
            label3content = Convert.ToString(label3.Content);
            label4content = Convert.ToString(label4.Content);
            IFormatter formatter = new BinaryFormatter();   //定义类,主要用此类中的两个方法来实现功能
            Stream stream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, RaidioButton1.IsChecked);
            formatter.Serialize(stream, RaidioButton2.IsChecked);
            formatter.Serialize(stream, TB1.Text);
            formatter.Serialize(stream, TB2.Text);
            formatter.Serialize(stream, label3content);
            formatter.Serialize(stream, label4content);
            stream.Close();
        }

         
    }
}
