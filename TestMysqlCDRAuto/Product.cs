using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;

namespace TestMysqlCDRAuto 
{
    public class Product : IComparable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public const string templatesPath = "C:\\Users\\Reginaldo\\Desktop\\templates";
        public bool FileLock = true;
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
        private string model;
        public string Model
        {
            get { return model; }
            set { 
                model = value;
                this.safeFileName = $"{Product.templatesPath}\\{model}.cdr";
            }
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
        private string safeFileName;
        public string SafeFileName
        {
            get { return safeFileName; }
           
        }
        public BitmapImage ProductThumb
        {
            get
            {
                return null;
                GetCdrThumb getCdrThumb = new GetCdrThumb();
                if(!File.Exists(SafeFileName))
                {
                    this.Status = ProductStatus.NoTemplateFound;
                    return null;
                }
                BitmapImage img = getCdrThumb.GetThumb(SafeFileName);
                FileLock = false;
                return img;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Product other = obj as Product;
            if (other != null)
            {
                if (other.Id == this.id)
                    return 0;
                else
                    return 1;
            }
            else
                throw new ArgumentException("Object is not a Product");
        }

        private ProductStatus status = ProductStatus.NotInitialize;

        public ProductStatus Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged(); }
        }
    }
        public enum ProductStatus
        {
            Complete,
            Busy,
            NotInitialize,
            Skipped,
            NoTemplateFound

        }
}