using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromMySql
{
    public  class SaveLoadConfig
    {
        public void SaveFolders(string templatesFolder, string resultsFolder)
        {
            Properties.Settings.Default.templatesFolder = templatesFolder;
            Properties.Settings.Default.resultsFolder = resultsFolder;
            Properties.Settings.Default.Save();
        }
        public void SaveDbData(string server, string user,string password,string database,string port)
        {
            Properties.Settings.Default.server = server;
            Properties.Settings.Default.user=user;
            Properties.Settings.Default.password=password;
            Properties.Settings.Default.database=database;
            Properties.Settings.Default.port= port;



            Properties.Settings.Default.Save();
        }
        public Settings LoadSettings()
        {
            string templatesFolder = Properties.Settings.Default.templatesFolder;
            string resultsFolder= Properties.Settings.Default.resultsFolder;
            string server = Properties.Settings.Default.server;
            string user = Properties.Settings.Default.user;
            string password = Properties.Settings.Default.password;
            string database = Properties.Settings.Default.database;
            string port = Properties.Settings.Default.port;
            Settings setting = new Settings(templatesFolder, resultsFolder,server,user,password,database,port);
            return setting;
        }
    }
    public struct Settings
    {
        public Settings(string templatesFolder,string resultsFolder,string server,string user, string password,string database,string port)
        {
            this.templatesFolder= templatesFolder;
            this.resultsFolder=  resultsFolder;
            this.server=         server;
            this.user=           user;
            this.password=       password;
            this.database=       database;
            this.port=           port;
        }
       public string templatesFolder;
       public string resultsFolder;
       public string server;
       public string user;
       public string password;
       public string database;
       public string port;
    }
}
