using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace WSPTools
{
    [Transaction(TransactionMode.Manual)]
    class About : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var aboutForm = new About_Form();

            aboutForm.ShowDialog();

            return Result.Succeeded;

        }
    }
}
