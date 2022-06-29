using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

    namespace GetDataFromMySql
    {
        public class MysqlDB
        {
            //private readonly string connStr = "server=localhost;user=root;database=vasaboden;port=3306;password=";
            private readonly string connStr = $"server={DockerUI.settings.server};user={DockerUI.settings.user};database={DockerUI.settings.database};port={DockerUI.settings.port};password={DockerUI.settings.password}";
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
            public bool UpdateOrderStatus(int orderId)
            {
                try
                {
                    string sql = string.Format("update oc_order set order_status_id = 2 where order_id=@orderId;");
                    conn.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = conn;
                    comand.CommandText = sql;
                    comand.Prepare();
                    comand.Parameters.AddWithValue("@orderId", orderId);

                    int result = comand.ExecuteNonQuery();
                    if (result == 1)
                        return true;
                    return false;
                }
                catch(Exception erro)
                {
                    throw erro;
                }
                finally
                {
                    this.closeConnection();
                }
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


