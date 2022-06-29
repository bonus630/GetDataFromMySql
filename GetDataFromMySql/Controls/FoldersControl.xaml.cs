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

namespace GetDataFromMySql.Controls
{
    /// <summary>
    /// Interaction logic for FoldersControl.xaml
    /// </summary>
    public partial class FoldersControl : UserControl
    {
        public FoldersControl()
        {
            InitializeComponent();
            txt_results.Content = DockerUI.settings.resultsFolder;
            txt_templates.Content = DockerUI.settings.templatesFolder;
        }

        private void btn_templateFolder_Click(object sender, RoutedEventArgs e)
        {
            txt_templates.Content = GetFolderPath();
        }

        private void btn_resultFolder_Click(object sender, RoutedEventArgs e)
        {
            txt_results.Content = GetFolderPath();
        }
        private string GetFolderPath()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return fbd.SelectedPath;
            return "";
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadConfig sl = new SaveLoadConfig();
            sl.SaveFolders(txt_templates.Content.ToString(), txt_results.Content.ToString());
        }
    }
}
