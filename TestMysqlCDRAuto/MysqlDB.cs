using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

    namespace TestMysqlCDRAuto
    {
        public class MysqlDB
        {
            private readonly string connStr = "server=localhost;user=root;database=vasaboden;port=3306;password=";
            private MySqlConnection conn = null;
            public MySqlConnection Conn { get { return conn; } }
            public MysqlDB()
            {
                try
                {
                    conn = new MySqlConnection(connStr);
                }
                catch (MySqlException e)
                {
                    throw e;
                }
            }
            private void closeConnection()
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            public ObservableCollection<Order> GetOrders()
            {
                ObservableCollection<Order> orders = new ObservableCollection<Order>();
                try
                {
                    string sql = "select order_id,firstname from oc_order where order_status_id = 1";
                    conn.Open();
                    MySqlCommand comand = new MySqlCommand(sql, conn);
                    using (MySqlDataReader dataReader = comand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Order order = new Order();
                            order.Id = (int)dataReader[0];
                            order.Name = (string)dataReader[1];
                            orders.Add(order);
                        }
                    }
                }
                catch (Exception erro)
                {
                    throw erro;
                }
                finally
                {
                    this.closeConnection();
                }
                return orders;
            }
            public ObservableCollection<Product> GetProducts(int order_id)
            {
                ObservableCollection<Product> products = new ObservableCollection<Product>();

                try
                {
                    string sql = $"select product_id,name,model from oc_order_product where order_id={order_id}";
                    conn.Open();
                    MySqlCommand comand = new MySqlCommand(sql, conn);
                    using (MySqlDataReader dataReader = comand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Product product = new Product();
                            product.Id = (int)dataReader[0];
                            product.Name = (string)dataReader[1];
                            product.Model = (string)dataReader[2];

                            product.Line1 = "teste";
                            product.Line2 = "a";


                            products.Add(product);
                        }
                    }
                }
                catch (Exception erro)
                {
                    throw erro;
                }
                finally
                {
                    this.closeConnection();
                }
                return products;
            }
        }
    }


