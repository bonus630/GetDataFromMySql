using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMysqlCDRAuto
{
    public class Orders
    {
        ObservableCollection<Order> orders = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrdersList { get { return orders; } }

        public Orders(ObservableCollection<Order> orders)
        {
            this.orders = orders;
        }
        public void SetProductToCustumer()
        {
            MysqlDB mysqlDB = new MysqlDB();
            for (int i = 0; i < orders.Count; i++)
            {
                for (int r = 0; r < orders.Count; r++)
                {

                }
                orders[i].Products = mysqlDB.GetProducts(orders[i].Id);
            }
        }
    }
}
