using System;
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
    class UpdateSpaceTags : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document m_doc = uidoc.Document;

                Transaction tx = new Transaction(m_doc);
                tx.Start("Update Space Tags");

                if (Utils.UpdateSpaceTags(m_doc)) 
                {
                    tx.Commit();
                    return Result.Succeeded;
                }
                else
                {
                    tx.RollBack();
                    return Result.Failed;
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception in UpdateSpaceTags command", "Exception\r\n" + ex.GetType().ToString() + "\r\n" + ex.Message);
                return Result.Failed;
            }
        }
    }
}
