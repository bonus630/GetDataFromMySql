using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestMysqlCDRAuto
{
    public class Order : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products { get { return products; } set { products = value;NotifyPropertyChanged(); } }

        public void AddProduct(Product product)
        {
            if (!products.Contains(product))
                products.Add(product);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Product product { get; set; }
        private OrderStatus status = OrderStatus.NotInitialize;

        public OrderStatus Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged(); }
        }
    }
    public enum OrderStatus
    {
        Complete,
        Busy,
        NotInitialize,
        Skipped

    }
}
