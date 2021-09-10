using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;

namespace WSPTools
{
    [Transaction(TransactionMode.Manual)]
    class SpaceTagData : IExternalCommand
    {
        private Document m_doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                m_doc = uidoc.Document;

                SpaceTag_Form form = new SpaceTag_Form
                {
                    ActiveDocument = m_doc,
                    ActiveUIDocument = uidoc
                };

                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception in SpaceTagData command", "Exception\r\n" + ex.GetType().ToString() + "\r\n" + ex.Message);
                return Result.Failed;
            }
        }

    }
}
