using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using corel = Corel.Interop.VGCore;

namespace TestMysqlCDRAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
       
        ProcessOrder pc;
        DocumentEditor editor;
        object app;
       
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private object corelApp()
        {
            try
            {
                Type pia_type = Type.GetTypeFromProgID("CorelDRAW.Application");
                app = Activator.CreateInstance(pia_type);
                return app;
            }
            catch
            {
                return null;
            }
        }
        public void ReleaseCdrCom()
        {
            editor.Close();
            int c = Marshal.ReleaseComObject(app);

            System.Windows.MessageBox.Show(c.ToString());
        }



        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
         
        }
      

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            switch ((sender as ListViewItem).Tag)
            {
                case "0":
                    MainContent.Content = new Controls.OrdersControl(pc);
                    break;
                case "1":
                    MainContent.Content = new Controls.MySqlConfigControl();
                    break;
                case "2":
                    MainContent.Content = new Controls.FoldersControl();
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ReleaseCdrCom();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartObjs();
        }
        private void StartObjs()
        {
            app = corelApp();
            if (app == null)
                return;
            pc = new ProcessOrder();
            editor = new DocumentEditor(app);
            
            pc.Editor = editor;
            MainContent.Content = new Controls.OrdersControl(pc);
           
        }
    }
}
