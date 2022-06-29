using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Corel.Interop.VGCore;

namespace TestMysqlCDRAuto
{
    public class DocumentEditor
    {
        private Application app;
        public Product product;
        public DocumentEditor(Application app)
        {
            this.app = app;
       
            this.app.Visible = false;
            this.app.Optimization = true;
            this.app.EventsEnabled = false;

        }
        public DocumentEditor(object app)
        {
           
            this.app = app as Application;
            this.app.Visible = false;
            this.app.Optimization = true;
            this.app.EventsEnabled = false;
        }

        public bool Open()
        {
            try
            {
                Document doc = this.app.OpenDocument(product.SafeFileName);
                if (doc != null)
                    return true;
                return false;
            }
            catch { return false; }
        }
        public void EditSave()
        {
            try
            {
                
                Shape line1 = this.app.ActiveDocument.ActiveLayer.FindShape("Line1");
                line1.Text.Contents = product.Line1;
                Shape line2 = this.app.ActiveDocument.ActiveLayer.FindShape("Line2");
                line2.Text.Contents = product.Line2;

                this.app.ActiveDocument.SaveAs($"C:\\Users\\Reginaldo\\Desktop\\results\\{product.Name}-{product.Name}.cdr");
                
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                this.app.ActiveDocument.Close();
                
                //app.Optimization = false;
                //app.Refresh();
            }
        }

        public void Close()
        {
            try
            {
                this.app.Quit();

            }
            catch { }
        }
    }
}
