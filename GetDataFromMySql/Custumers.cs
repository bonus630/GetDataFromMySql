using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromMySql
{
    public class Custumers
    {
        ObservableCollection<Custumer> custumers = new ObservableCollection<Custumer>();
        public ObservableCollection<Custumer> CustumersList { get { return custumers; } }

        public Custumers(ObservableCollection<Custumer> custumers)
        {
            this.custumers = custumers;
        }
        public void SetProductToCustumer()
        {
            MysqlDB mysqlDB = new MysqlDB();
            for (int i = 0; i < custumers.Count; i++)
            {
                custumers[i].product = mysqlDB.GetProduct(custumers[i].ProductID);
            }
        }
    }
}
