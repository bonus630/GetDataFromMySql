using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromMySql
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
        public void UpdateProductStatus(int orderId,int productId)
        {
            //Aqui vamos fazer a chamada no classe de banco para atualizar o order_status_id
            //To do a call in db class to update order_status_id
        }
        public void UpdateOrderStatus(Order order)
        {
            if (order.AllProductsComplete)
            {
                MysqlDB mysqlDB = new MysqlDB();
                mysqlDB.UpdateOrderStatus(order.Id);
            }
        }
        public bool FinishList
        {
            get
            {
                return checkFinishList();
            }
        }
        private bool checkFinishList()
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if(!orders[i].Processed)
                    return false;
            }
            return true;
        }
    }
}
