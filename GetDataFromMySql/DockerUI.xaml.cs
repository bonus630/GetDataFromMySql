using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
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
using System.Reflection;

namespace GetDataFromMySql
{
    public partial class DockerUI : UserControl
    {
        private corel.Application corelApp;
        private DocumentEditor documentEditor;
        private SaveLoadConfig config = new SaveLoadConfig();
        public static Settings settings;
        //public  const string templatesPath = "C:\\Users\\Reginaldo\\Desktop\\templates";
        private Styles.StylesController stylesController;
        ProcessOrder pc;
        
        public DockerUI(object app)
        {
            InitializeComponent();
            try
            {
                this.corelApp = app as corel.Application;
                documentEditor = new DocumentEditor(this.corelApp);
                stylesController = new Styles.StylesController(this.Resources, this.corelApp);
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                settings = config.LoadSettings();
            }
            catch
            {
                global::System.Windows.MessageBox.Show("VGCore Erro");
            }
            StartObjs();
        }

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("BouncyCastle.Crypto") || args.Name.Contains("Google.Protobuf") || args.Name.Contains("Renci.SshNet"))
            {
                var assemblyname = new AssemblyName(args.Name).Name;
                var assemblyFileName = $"{this.corelApp.AddonPath}GetDataFromMySql\\{assemblyname}.dll";
                var assembly = Assembly.LoadFrom(assemblyFileName);
                return assembly; 
            }
            return null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            stylesController.LoadThemeFromPreference();
        }

        private void StartObjs()
        {
         
            pc = new ProcessOrder();
            documentEditor = new DocumentEditor(this.corelApp);
            pc.Editor = documentEditor;
            pc.Running = false;
            MainContent.Children.Add(new Controls.OrdersControl(pc));

        }

        private void btn_list_Click(object sender, RoutedEventArgs e)
        {
            changeGrid(new Controls.OrdersControl(pc));
        }

        private void btn_folders_Click(object sender, RoutedEventArgs e)
        {
            changeGrid(new Controls.FoldersControl());
        }

        private void btn_mysql_Click(object sender, RoutedEventArgs e)
        {
            changeGrid(new Controls.MySqlConfigControl());
        }

        private void ck_isProcessing_Click(object sender, RoutedEventArgs e)
        {
            pc.Running = (bool)ck_isProcessing.IsChecked;
            if(pc.Running)
                changeGrid(new Controls.OrdersControl(pc));
        }
        private void changeGrid(UIElement element)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(element);
        }

    }
}
