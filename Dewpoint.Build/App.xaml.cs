using System;
using System.Windows;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

       System.Threading.Mutex mutex;
       string openFileName = "save.bin";
       string saveFileName = "save.bin";
       public App()
       {
           this.Startup += new StartupEventHandler(App_Startup);
       }

       void App_Startup(object sender, StartupEventArgs e)
       {

           bool ret;
           mutex = new System.Threading.Mutex(true, "ElectronicNeedleTherapySystem", out ret);

           if (!ret)
           {
               MessageBox.Show("已有一个程序实例运行");
               Environment.Exit(0);
           }


       }

        void App_Exit(object sender, ExitEventArgs e)
       {
          
       }
    }  
    }
    

