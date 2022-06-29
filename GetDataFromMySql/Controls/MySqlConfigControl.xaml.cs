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
    /// Interaction logic for MySqlConfig.xaml
    /// </summary>
    public partial class MySqlConfigControl : UserControl
    {
        public MySqlConfigControl()
        {
            InitializeComponent();
            txt_dataBase.Text = DockerUI.settings.database;
            txt_password.Text = DockerUI.settings.password;
            txt_server.Text = DockerUI.settings.server;
            txt_user.Text = DockerUI.settings.user;
            txt_port.Text = DockerUI.settings.port;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadConfig sl = new SaveLoadConfig();
            sl.SaveDbData(txt_server.Text, txt_user.Text, txt_password.Text, txt_dataBase.Text, txt_port.Text);
        }
    }
}
