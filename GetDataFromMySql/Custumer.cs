using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GetDataFromMySql
{
    public class Custumer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
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
        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private string line1;

        public string Line1
        {
            get { return line1; }
            set { line1 = value; }
        }

        private string line2;

        

        public string Line2
        {
            get { return line2; }
            set { line2 = value; }
        }

        public Product product { get; set; }
        private CustumerStatus status = CustumerStatus.NotInitialize;

        public CustumerStatus Status
        {
            get { return status; }
            set { status = value;NotifyPropertyChanged(); }
        }



    }    
    public enum CustumerStatus
    {
            Complete,
            Busy,
            NotInitialize,
            Skipped

    }
}
