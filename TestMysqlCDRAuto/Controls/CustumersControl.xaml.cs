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

namespace TestMysqlCDRAuto.Controls
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
                pc.ProcessCustumers(orders.OrdersList);
            });
        }

        private void list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
