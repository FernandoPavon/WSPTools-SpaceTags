#region Copyright

/*
 *  Copyright 2021 Autodesk, Inc. All rights reserved.
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:

 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.

 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.

 *  Autodesk Consulting Services
 *  fernando.pavon@autodesk.com
 *  
 */

#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;
using System.Reflection;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace WSPTools
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            string addinPath = Path.GetDirectoryName(assemblyPath);
            string assembly = "WSPTools.dll";
            string dllpath = Path.Combine(addinPath, assembly);
            string starterTab = "WSP Tools";

            //Create Tab
            //----------
            application.CreateRibbonTab(starterTab);

           
            // About WSP Tools 
            RibbonPanel panel = application.CreateRibbonPanel(starterTab, "About WSP Tools");

            PushButton button = panel.AddItem(new PushButtonData("About",
            "Information", dllpath, "WSPTools.About")) as PushButton;
            Uri uriImage = new Uri($"pack://application:,,,/WSPTools;component/Resources/Info_32x32.jpg", UriKind.Absolute);
            BitmapImage image = new BitmapImage(uriImage);
            button.LargeImage = image;
            button.ToolTip = "About WSP Tools";

            //Space Tags Data
            //----------------------------
            panel = application.CreateRibbonPanel(starterTab, "Space Tags");

            button = panel.AddItem(new PushButtonData("ID_SpaceData",
            "Space Data", dllpath, "WSPTools.SpaceTagData")) as PushButton;
            uriImage = new Uri($"pack://application:,,,/WSPTools;component/Resources/SpaceData.png", UriKind.Absolute);
            image = new BitmapImage(uriImage);
            button.LargeImage = image;
            button.ToolTip = "Browse Space Data";

            button = panel.AddItem(new PushButtonData("ID_UpdateTags",
           "Update Tags", dllpath, "WSPTools.UpdateSpaceTags")) as PushButton;
            uriImage = new Uri($"pack://application:,,,/WSPTools;component/Resources/Flow2.png", UriKind.Absolute);
            image = new BitmapImage(uriImage);
            button.LargeImage = image;
            button.ToolTip = "Update Space Tags";

            button = panel.AddItem(new PushButtonData("ID_CreateTags",
           "Create Tags", dllpath, "WSPTools.CreateSpaceTags")) as PushButton;
            uriImage = new Uri($"pack://application:,,,/WSPTools;component/Resources/CreateTag-2.png", UriKind.Absolute);
            image = new BitmapImage(uriImage);
            button.LargeImage = image;
            button.ToolTip = "Create Space Tags";

            /*
            //long description
            string longDesc = "Create area views in a consistent manner by selection scale, scopebox, template and name pattern.";
            button.LongDescription = longDesc;

            //help image
            Uri uriHelpImage = new Uri($"pack://application:,,,/CRTKL_Sheets;component/Resources/CreateViews.png", UriKind.Absolute);
            BitmapImage helpImage = new BitmapImage(uriHelpImage);
            button.ToolTipImage = helpImage;

            //link to web help
            string helpURL = $"https://www.pavonleo.com/CRTKL_UserGuide.pdf";
            ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url, helpURL);
            button.SetContextualHelp(contextHelp);
            */

            //Document Opening and Closing Events
            //-----------------------------------
            application.ControlledApplication.DocumentOpened
                   += new EventHandler<Autodesk.Revit.DB.Events.DocumentOpenedEventArgs>(Application_DocumentOpened);

            application.ControlledApplication.DocumentClosing
                   += new EventHandler<Autodesk.Revit.DB.Events.DocumentClosingEventArgs>(Application_DocumentClosing);

            //Space Tag Updater for Duct Terminals
            //------------------------------------
            SpaceTagUpdater terminalUpdater = new SpaceTagUpdater(application.ActiveAddInId);

            UpdaterRegistry.RegisterUpdater(terminalUpdater);

            ElementCategoryFilter catFilter = new ElementCategoryFilter(BuiltInCategory.OST_DuctTerminal);
            ElementClassFilter classFilter = new ElementClassFilter(typeof(FamilyInstance));
            LogicalAndFilter terFilter = new LogicalAndFilter(catFilter, classFilter);

            //ChangeType changeAny = Element.GetChangeTypeAny();
            ChangeType changeAddition = Element.GetChangeTypeElementAddition();
            ChangeType changeDeletion = Element.GetChangeTypeElementDeletion();
            ChangeType changeGeometry = Element.GetChangeTypeGeometry();
            ChangeType changeParameter = Element.GetChangeTypeParameter(new ElementId(-1));

            ChangeType changeAddDel = ChangeType.ConcatenateChangeTypes(changeAddition, changeDeletion);
            ChangeType changeGeoPar = ChangeType.ConcatenateChangeTypes(changeGeometry, changeParameter);
            ChangeType changeType = ChangeType.ConcatenateChangeTypes(changeAddDel, changeGeoPar);

            UpdaterRegistry.AddTrigger(terminalUpdater.GetUpdaterId(), terFilter,  changeType);

            //Space Tag Updater for Mechanical Equipment
            //------------------------------------------
            SpaceTagMecUpdater mechanicalUpdater = new SpaceTagMecUpdater(application.ActiveAddInId);

            UpdaterRegistry.RegisterUpdater(mechanicalUpdater);

            ElementCategoryFilter mecFilter = new ElementCategoryFilter(BuiltInCategory.OST_MechanicalEquipment);
            ElementClassFilter mecClassFilter = new ElementClassFilter(typeof(FamilyInstance));
            LogicalAndFilter mecLogicalFilter = new LogicalAndFilter(mecFilter, mecClassFilter);

            //ChangeType changeAny = Element.GetChangeTypeAny();
            ChangeType mecChangeAddition = Element.GetChangeTypeElementAddition();
            ChangeType mecChangeDeletion = Element.GetChangeTypeElementDeletion();
            ChangeType mecChangeGeometry = Element.GetChangeTypeGeometry();
            ChangeType mecChangeParameter = Element.GetChangeTypeParameter(new ElementId(-1));

            ChangeType mecChangeAddDel = ChangeType.ConcatenateChangeTypes(mecChangeAddition, mecChangeDeletion);
            ChangeType mecChangeGeoPar = ChangeType.ConcatenateChangeTypes(mecChangeGeometry, mecChangeParameter);
            ChangeType mecChangeType = ChangeType.ConcatenateChangeTypes(mecChangeAddDel, mecChangeGeoPar);

            UpdaterRegistry.AddTrigger(mechanicalUpdater.GetUpdaterId(), mecLogicalFilter, mecChangeType);



            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            application.ControlledApplication.DocumentOpened -= Application_DocumentOpened;
            application.ControlledApplication.DocumentClosing -= Application_DocumentClosing;

            return Result.Succeeded;
        }

        private void Application_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs args)
        {
            Document doc = args.Document;

            Transaction transaction = new Transaction(doc, "Update Space Tags");
            if (transaction.Start() == TransactionStatus.Started)
            {
                if (Utils.UpdateSpaceTags(doc))
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.RollBack();
                }
            }
        }

        private void Application_DocumentClosing(object sender, Autodesk.Revit.DB.Events.DocumentClosingEventArgs args)
        {
            Document doc = args.Document;

            Transaction transaction = new Transaction(doc, "Update Space Tags");
            if (transaction.Start() == TransactionStatus.Started)
            {
                if (Utils.UpdateSpaceTags(doc))
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.RollBack();
                }
            }
        }
    }

   

    public class SpaceTagUpdater : IUpdater
    {
        public static bool m_updateActive = true;
        public static int m_count = 0;
        public static int m_id = 0;
        readonly AddInId addinID = null;
        readonly UpdaterId updaterID = null;

        public SpaceTagUpdater(AddInId id)
        {
            addinID = id;
            updaterID = new UpdaterId(addinID, new Guid("ADAA53F1-DBDB-458D-AC59-746352518B8B"));
        }
        public void Execute(UpdaterData data)
        {
            try
            {
                Document doc = data.GetDocument();

                //ICollection<ElementId> listEle = data.GetModifiedElementIds();

                Utils.UpdateSpaceTags(doc);
               
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception in Space Tag Duct Terminals Update", ex.Message);
            }
        }

        public string GetAdditionalInformation()
        {
            return "Space Tag Update Automation";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.FreeStandingComponents;
        }

        public UpdaterId GetUpdaterId()
        {
            return updaterID;
        }

        public string GetUpdaterName()
        {
            return "Space Tag updater";
        }
    }

    public class SpaceTagMecUpdater : IUpdater
    {
        public static bool m_updateActive = true;
        public static int m_count = 0;
        public static int m_id = 0;
        readonly AddInId addinID = null;
        readonly UpdaterId updaterID = null;

        public SpaceTagMecUpdater(AddInId id)
        {
            addinID = id;
            updaterID = new UpdaterId(addinID, new Guid("830B197E-A151-43FA-ADFD-6AEB079E77DE"));
        }
        public void Execute(UpdaterData data)
        {
            try
            {
                Document doc = data.GetDocument();

                Utils.UpdateSpaceTags(doc);

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception Space Tag Mechanical Equipment Update", ex.Message);
            }
        }

        public string GetAdditionalInformation()
        {
            return "Space Tag Mec Update Automation";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.FreeStandingComponents;
        }

        public UpdaterId GetUpdaterId()
        {
            return updaterID;
        }

        public string GetUpdaterName()
        {
            return "Space Tag Mec updater";
        }
    }
}
