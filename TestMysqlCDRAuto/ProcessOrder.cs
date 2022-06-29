using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace TestMysqlCDRAuto
{
    public class ProcessOrder
    {
        private Orders orders;
        private DocumentEditor editor;
        public DocumentEditor Editor { set { editor = value; } }
        object app;
        public ProcessOrder()
        {
           
        }
        public void ProcessCustumers(ObservableCollection<Order> orders)
        {
            Thread th = new Thread(new ParameterizedThreadStart(processOrders));
            th.IsBackground= true;
            th.Start(orders);
        }
        private void processOrders(object param)
        {
            ObservableCollection<Order> orders = param as ObservableCollection<Order>;
            for (int i = 0; i < orders.Count; i++)
            {
                processOrder(orders[i]);
            }
        }
        private void processOrder(Order order)
        {
            order.Status = OrderStatus.Busy;
            bool erro = false;
            try
            {
                for (int i = 0; i < order.Products.Count; i++)
                {
                    try
                    {
                      
                        editor.product = order.Products[i];
                        if (!editor.Open())
                        {
                            order.Products[i].Status = ProductStatus.NoTemplateFound;
                            continue;
                        }
                        editor.EditSave();
                    }
                    catch
                    {
                        erro = true;
                        order.Products[i].Status = ProductStatus.Skipped;
                    }
                    order.Products[i].Status = ProductStatus.Complete;
                }
                
                order.Status = OrderStatus.Complete;
            }
            catch
            {
                order.Status = OrderStatus.Skipped;
            }
        }
     
    }
}
