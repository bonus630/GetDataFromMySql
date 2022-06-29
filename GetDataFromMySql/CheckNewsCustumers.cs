
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

namespace GetDataFromMySql
{
    public class CheckNewsCustumers
    {
        MysqlDB mysqlDB = new MysqlDB();
        int timer = 5000;
        public event Action<Orders> NewList;
        public event Action<bool> ConnectionOk;
        private Orders orders; 
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
                try
                {
                    if (this.orders == null || this.orders.FinishList)
                    {
                        this.orders = new Orders(mysqlDB.GetOrders());
                        this.orders.SetProductToCustumer();
                        if (NewList != null)
                            NewList(orders);
                        if (ConnectionOk != null)
                            ConnectionOk(true);
                    }
                    Thread.Sleep(timer);
                }
                catch
                {
                    if (ConnectionOk != null)
                        ConnectionOk(false);
                }
            }
        }
    }
}
