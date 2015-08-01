namespace XmlBinderTest
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls.Ribbon;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
       
        public MainWindow()
        {
            InitializeComponent();
            this.ReadXml();
            _ribbon.IsMinimized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadXml()
        {
            var xmlPath = Path.Combine(Environment.CurrentDirectory, "XmlData", "ProductPortofolio.xml");
           // this.provider.Source = new Uri(xmlPath);
           // this.provider.XPath = "root/portofolio/product";
        }
    }
}
