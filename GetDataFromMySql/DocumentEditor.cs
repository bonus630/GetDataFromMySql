using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Corel.Interop.VGCore;

namespace GetDataFromMySql
{
    public class DocumentEditor
    {
        private Application app;
        public Product product;
        public DocumentEditor(Application app)
        {
            this.app = app;
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
                Document doc = this.app.OpenDocument($"{DockerUI.settings.templatesFolder}\\{product.Model}.cdr");
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
                this.app.Optimization = true;
                this.app.EventsEnabled = false;

                Shape line1 = this.app.ActiveDocument.ActiveLayer.FindShape("Line1");
                line1.Text.Contents = product.Line1;
                Shape line2 = this.app.ActiveDocument.ActiveLayer.FindShape("Line2");
                line2.Text.Contents = product.Line2;

                this.app.ActiveDocument.SaveAs($"{DockerUI.settings.resultsFolder}\\{product.Name}-{product.Id}.cdr");
                
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                this.app.ActiveDocument.Close();

                this.app.Optimization = false;
                this.app.EventsEnabled = true;
                this.app.Refresh();
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
