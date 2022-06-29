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
    /// Interaction logic for CustumersControl.xaml
    /// </summary>
    public partial class OrdersControl : UserControl
    {
        CheckNewsCustumers newsCustumers = new CheckNewsCustumers();
        ProcessOrder pc;
        public OrdersControl(ProcessOrder pc)
        {
            InitializeComponent();
            this.pc = pc;
            newsCustumers.StartCheck();
            newsCustumers.NewList += NewsCustumers_NewList;
            newsCustumers.ConnectionOk += NewsCustumers_ConnectionOk;
        }

        private void NewsCustumers_ConnectionOk(bool obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (obj)
                    lba_connection.Content = "Connection Ok";
                else
                    lba_connection.Content = "Connection OFF";
            });

        }

        private void NewsCustumers_NewList(Orders obj)
        {
            UpdateCustumersList(obj);
        }

        private void UpdateCustumersList(Orders orders)
        {
            
            this.Dispatcher.Invoke(() =>
            {
                list.ItemsSource = orders.OrdersList;
               // pc = new ProcessCustumer();
                pc.ProcessCustumers(orders);
            });
        }

      
    }
}
