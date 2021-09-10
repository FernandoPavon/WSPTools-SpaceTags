using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;

namespace WSPTools
{
    public partial class SpaceTag_Form : System.Windows.Forms.Form
    {
        private Document m_doc;
        private const string noTag = "<No Tag>"; 
        public Document ActiveDocument { set { m_doc = value; } }
        public UIDocument ActiveUIDocument;

        public SpaceTag_Form()
        {
            InitializeComponent();
        }

        private void SpaceTag_Form_Load(object sender, EventArgs e)
        {
            Text = string.Format(Text, Properties.Resources.VersionNumber, Properties.Resources.VersionDate);

            IList<Element> tagTypes = Utils.GetTagTypes(m_doc);

            foreach(Element tag in tagTypes)
            {
                SpaceTagType type = tag as SpaceTagType;
                Family family = type.Family;
                TagsComboBox.Items.Add(family.Name + " : " + type.Name);
            }

            if(TagsComboBox.Items.Count > 0)
            {
                TagsComboBox.SelectedIndex = 0;
            }

            IList<Element> spaces = Utils.GetSpaces(m_doc);

            foreach(Element ele in spaces)
            {
                Space space = ele as Space;

                string[] items = new string[6];

                items[0] = space.Id.IntegerValue.ToString();
                items[1] = space.Number;
                items[2] = space.Name;
                items[3] = space.Level.Name;

                ElementId tagId = FindTagId(space);

                if(null == tagId)
                {
                    items[4] = noTag;
                }
                else
                {
                    items[4] = tagId.IntegerValue.ToString();
                }

                Level level = space.Level;
                ElementId vId = level.FindAssociatedPlanViewId();
                items[5] = vId.IntegerValue.ToString();

                ListViewItem lvi = new ListViewItem(items);

                SpaceListView.Items.Add(lvi);
            }
        }

        private ElementId FindTagId(Space space)
        {
            ElementId eId = null;

            string selTag = TagsComboBox.SelectedItem as string;
            string[] tagSplit = selTag.Split(':');
            string tagName = tagSplit[1].Trim();

            IList<Element> tags = Utils.GetTags_ByTypeName(m_doc, tagName);

            foreach(Element tag in tags)
            {
                SpaceTag spaceTag = tag as SpaceTag;

                if(spaceTag.Space.Id.IntegerValue.ToString().Equals(space.Id.IntegerValue.ToString()))
                {
                    eId = tag.Id;
                    break;
                }
            }

            return eId;
        }

        private void SpaceListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = SpaceListView.SelectedItems;

            if (selectedItems.Count == 0) return;

            ListViewItem selItem = selectedItems[0];

            string id = selItem.Text;
            int iid = int.Parse(id);

            IList<FamilyInstance> terminals = Utils.GetDuctTerminals_InSpaceId(m_doc, new ElementId(iid));

            EquipmentListView.Items.Clear();

            foreach (FamilyInstance term in terminals)
            {
                string[] items = new string[4];

                items[0] = term.Id.IntegerValue.ToString();
                items[1] = term.Symbol.FamilyName;
                items[2] = term.Symbol.Name;

                Parameter parFlow = term.LookupParameter("Flow");
                items[3] = "";
                if (parFlow != null)
                {
                    items[3] = parFlow.AsValueString();
                }

                ListViewItem lvi = new ListViewItem(items);

                EquipmentListView.Items.Add(lvi);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem lvi in SpaceListView.Items)
            {
                string strId = lvi.SubItems[0].Text;
                int intId = int.Parse(strId);

                string tagPlaced = lvi.SubItems[5].Text;

                string vId = lvi.SubItems[4].Text;
                int intvId = int.Parse(vId);
                Autodesk.Revit.DB.View view = m_doc.GetElement(new ElementId(intvId)) as Autodesk.Revit.DB.View;

                Space space = m_doc.GetElement(new ElementId(intId)) as Space;

                Transaction tx = new Transaction(m_doc);
                tx.Start("Create Space Tags");

                if (tagPlaced.Equals(noTag))
                {
                    XYZ cen = GetSpaceCenter(space);
                    UV uvcen = new UV(cen.X, cen.Y);
                    m_doc.Create.NewSpaceTag(space, uvcen, view);
                }
               
                Parameter par = space.LookupParameter("AirSupply");
                if(null != par)
                {
                    par.Set(GetAirFlow(space));
                }
                
                tx.Commit();
            }
        }

        public XYZ GetSpaceCenter(Space space)
        {
            BoundingBoxXYZ bounding = space.get_BoundingBox(null);
            XYZ center = (bounding.Max + bounding.Min) * 0.5;
            LocationPoint locPt = (LocationPoint)space.Location;
            XYZ spaceCenter = new XYZ(center.X, center.Y, locPt.Point.Z);
            return spaceCenter;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private string GetAirFlow(Space space)
        {
            IList<FamilyInstance> terminals = Utils.GetDuctTerminals_InSpaceId(m_doc, space.Id);
            double dbFlow = 0;

            foreach (FamilyInstance term in terminals)
            {
                Parameter parFlow = term.LookupParameter("Flow");

                if (parFlow != null)
                {
                    string flow = parFlow.AsValueString();
                    double flo = 0;
                    try
                    {
                        flo = double.Parse(GetNumber(flow));
                    }
                    catch
                    {
                        continue;
                    }
                    dbFlow += flo;
                }
            }

            return string.Format("{0:0.00}", dbFlow) + " L/s";
        }

        private string GetNumber(string original)
        {
            return new string(original.Where(c => Char.IsDigit(c) || c=='.').ToArray());
        }
    }
}
