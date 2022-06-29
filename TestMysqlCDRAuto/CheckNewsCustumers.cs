
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

namespace TestMysqlCDRAuto
{
    public class CheckNewsCustumers
    {
        MysqlDB mysqlDB = new MysqlDB();
        int timer = 100000;
        public event Action<Orders> NewList;
        public void StartCheck()
        {
            Thread th = new Thread(new ThreadStart(process));
            th.IsBackground = true;
            th.Start();
        }
        private void process()
        {
            while(true)
            {
                Orders orders = new Orders(mysqlDB.GetOrders());
                orders.SetProductToCustumer();
                if (NewList != null)
                    NewList(orders);
                Thread.Sleep(timer);
            }
        }
    }
}
