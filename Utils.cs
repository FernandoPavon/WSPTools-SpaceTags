using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace WSPTools
{
	class Utils
	{
        private const string k_tagType_0 = "Space Data 0";
        private const string k_tagType_1 = "Space Data 1";
        private const string k_tagType_2 = "Space Data 2";
        private const string k_tagType_3 = "Space Data 3";
        private const string k_tagType_4 = "Space Data 4";

        public static bool UpdateSpaceTags(Document doc)
        {
            IList<Element> spaces = GetSpaces(doc);

            foreach (Space space in spaces)
            {
                SpaceTag spaceTag = FindSpaceTag_BySpace(doc, space);

                SubTransaction tx = new SubTransaction(doc);
                tx.Start();

                if (null != spaceTag)
                {
                    GetSpaceEquipment(doc, space, spaceTag);
                }

                tx.Commit();
            }

            return true;
        }

        public static bool CreateSpaceTags(Document doc)
        {
            IList<Element> spaces = GetSpaces(doc);

            foreach (Space space in spaces)
            {
                SpaceTag spaceTag = FindSpaceTag_BySpace(doc, space);

                Level level = space.Level;
                ElementId viewId = level.FindAssociatedPlanViewId();

                Autodesk.Revit.DB.View view = doc.GetElement(viewId) as Autodesk.Revit.DB.View;

                SubTransaction tx = new SubTransaction(doc);
                tx.Start();

                if (null == spaceTag)
                {
                    XYZ cen = GetSpaceCenter(space);
                    UV uvcen = new UV(cen.X, cen.Y);
                    spaceTag = doc.Create.NewSpaceTag(space, uvcen, view);
                }

                GetSpaceEquipment(doc, space, spaceTag);

                tx.Commit();
            }

            return true;
        }

        public static void ResetSpaceParameters(Space space)
        {
            Parameter par = space.LookupParameter("AirItem_1");
            if (null != par)
            {
                par.Set("");
            }
            par = space.LookupParameter("AirItem_2");
            if (null != par)
            {
                par.Set("");
            }
            par = space.LookupParameter("AirItem_3");
            if (null != par)
            {
                par.Set("");
            }
            par = space.LookupParameter("AirItem_4");
            if (null != par)
            {
                par.Set("");
            }
        }

        private static void GetSpaceEquipment(Document doc, Space space, SpaceTag spaceTag)
        {
            IList<string> result = GetEquipment_BySpace(doc, space);

            ResetSpaceParameters(space);

            if (result.Count == 0)
            {
                spaceTag.SpaceTagType = GetTagType_ByTypeName(doc, k_tagType_0);

            }
            else if (result.Count == 1)
            {
                spaceTag.SpaceTagType = GetTagType_ByTypeName(doc, k_tagType_1);
                Parameter par = space.LookupParameter("AirItem_1");
                if (null != par)
                {
                    par.Set(result[0]);
                }
            }
            else if (result.Count == 2)
            {
                spaceTag.SpaceTagType = GetTagType_ByTypeName(doc, k_tagType_2);
                Parameter par = space.LookupParameter("AirItem_1");
                if (null != par)
                {
                    par.Set(result[0]);
                }
                par = space.LookupParameter("AirItem_2");
                if (null != par)
                {
                    par.Set(result[1]);
                }
            }
            else if (result.Count == 3)
            {
                spaceTag.SpaceTagType = GetTagType_ByTypeName(doc, k_tagType_3);
                Parameter par = space.LookupParameter("AirItem_1");
                if (null != par)
                {
                    par.Set(result[0]);
                }
                par = space.LookupParameter("AirItem_2");
                if (null != par)
                {
                    par.Set(result[1]);
                }
                par = space.LookupParameter("AirItem_3");
                if (null != par)
                {
                    par.Set(result[2]);
                }

            }
            else if (result.Count == 4)
            {
                spaceTag.SpaceTagType = GetTagType_ByTypeName(doc, k_tagType_4);
                Parameter par = space.LookupParameter("AirItem_1");
                if (null != par)
                {
                    par.Set(result[0]);
                }
                par = space.LookupParameter("AirItem_2");
                if (null != par)
                {
                    par.Set(result[1]);
                }
                par = space.LookupParameter("AirItem_3");
                if (null != par)
                {
                    par.Set(result[2]);
                }
                par = space.LookupParameter("AirItem_4");
                if (null != par)
                {
                    par.Set(result[3]);
                }
            }
        }

       

        public static IList<Element> GetTagTypes(Document doc)
		{
			return new FilteredElementCollector(doc)
				.OfCategory(BuiltInCategory.OST_MEPSpaceTags)
				.WhereElementIsElementType()
				.ToElements();
		}

        public static IList<Element> GetSpaceTags(Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_MEPSpaceTags)
                .WhereElementIsNotElementType()
                .ToList();
        }

        public static SpaceTagType GetTagType_ByTypeName(Document doc, string tagTypeName)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_MEPSpaceTags)
                .WhereElementIsElementType()
                .Cast<SpaceTagType>()
                .Where(t => t.Name.Equals(tagTypeName))
                .ToList().First();
        }

        public static IList<Element> GetTags_ByTypeName(Document doc, string tagName)
		{
			return  new FilteredElementCollector(doc)
				.OfCategory(BuiltInCategory.OST_MEPSpaceTags)
				.WhereElementIsNotElementType()
				.Where(t => t.Name.Equals(tagName))
				.ToList();
		}

		public static IList<Element> GetSpaces(Document doc)
		{
			SpaceFilter spaceFilter = new SpaceFilter();

			return new FilteredElementCollector(doc)
				.WherePasses(spaceFilter)
				.ToElements();
		}

		public static List<FamilyInstance> GetDuctTerminals_InSpaceId(Document doc, ElementId spaceId)
		{
			List<FamilyInstance> terminals = new List<FamilyInstance>();

			IList<Element> equipment = new FilteredElementCollector(doc)
				.OfCategory(BuiltInCategory.OST_DuctTerminal)
				.WhereElementIsNotElementType()
				.ToElements();

			foreach(Element ele in equipment)
            {
				FamilyInstance inst = ele as FamilyInstance;
				Space space = inst.Space;
				
				if(space.Id.IntegerValue == spaceId.IntegerValue)
                {
					terminals.Add(inst);
                }
            }

			return terminals;
		}

        public static List<FamilyInstance> GetMechanicalEquipment_InSpaceId(Document doc, ElementId spaceId)
        {
            List<FamilyInstance> terminals = new List<FamilyInstance>();

            IList<Element> equipment = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_MechanicalEquipment)
                .WhereElementIsNotElementType()
                .ToElements();

            foreach (Element ele in equipment)
            {
                FamilyInstance inst = ele as FamilyInstance;
                Space space = inst.Space;

                if (space.Id.IntegerValue == spaceId.IntegerValue)
                {
                    Parameter par = inst.LookupParameter("MC Ventilation Flow");
                    if(null != par)
                    {
                        if (par.HasValue)
                        {
                            terminals.Add(inst);
                        }
                    }
                }
            }
            return terminals;
        }

        public static IList<FamilyInstance> GetMechanical_InSpaceId(Document doc, ElementId spaceId)
        {
            IList<FamilyInstance> terminals = new List<FamilyInstance>();

            IList<Element> equipment = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_MechanicalEquipment)
                .WhereElementIsNotElementType()
                .ToElements();

            foreach (Element ele in equipment)
            {
                FamilyInstance inst = ele as FamilyInstance;
                Space space = inst.Space;

                if (space.Id.IntegerValue == spaceId.IntegerValue)
                {
                    terminals.Add(inst);
                }
            }

            return terminals;
        }

        public static XYZ GetSpaceCenter(Space space)
        {
            BoundingBoxXYZ bounding = space.get_BoundingBox(null);
            XYZ center = (bounding.Max + bounding.Min) * 0.5;
            LocationPoint locPt = (LocationPoint)space.Location;
            XYZ spaceCenter = new XYZ(center.X, center.Y, locPt.Point.Z);

            return spaceCenter;
        }

        private static IList<string> GetEquipment_BySpace(Document doc, Space space)
        {
            List<FamilyInstance> terminals = GetDuctTerminals_InSpaceId(doc, space.Id);
            List<FamilyInstance> mechanical = GetMechanicalEquipment_InSpaceId(doc, space.Id);

            terminals.AddRange(mechanical);

            IList<string> equipList = new List<string>();
           
            foreach (FamilyInstance term in terminals)
            {
                string strItem = "NoMCUserCode";
                FamilySymbol symbol = term.Symbol;

                Parameter par = symbol.LookupParameter("MC User Code");
                if (null != par)
                {
                    if (par.HasValue)
                    {
                        strItem = par.AsString();
                    }
                }
                par = symbol.LookupParameter("MC Connection Size 1");
                if (null != par)
                {
                    if (par.HasValue)
                    {
                        double db = UnitUtils.ConvertFromInternalUnits(par.AsDouble(), DisplayUnitType.DUT_MILLIMETERS);
                        strItem = strItem + "-" + db.ToString("0");
                    }
                }
                equipList.Add(strItem);
            }

            string strEquipment;
            IList<string> done = new List<string>();
            IList<string> result = new List<string>();

            foreach (string eq in equipList)
            {
                if (done.Contains(eq)) continue;

                int count = CountEquip(equipList, eq);

                if (count == 0) continue;
                if (count == 1)
                {
                    strEquipment = eq + " ";
                }
                else
                {
                    strEquipment = count.ToString() + "x" + eq + " ";
                }

                result.Add(strEquipment);

                done.Add(eq);
            }

            return result;
        }

        private static string GetEquipment(Document doc, Space space)
        {
            IList<FamilyInstance> terminals = Utils.GetDuctTerminals_InSpaceId(doc, space.Id);
            IList<string> equipList = new List<string>();
            string strEquipment = "";

            foreach (FamilyInstance term in terminals)
            {
                FamilySymbol symbol = term.Symbol;

                Parameter parUserCode = symbol.LookupParameter("MC User Code");
                if (null != parUserCode)
                {
                    if (parUserCode.HasValue)
                    {
                        equipList.Add(parUserCode.AsString());
                    }
                }
                else
                {
                    equipList.Add("NoUserCode");
                }
            }

            IList<string> done = new List<string>();

            foreach (string eq in equipList)
            {
                if (done.Contains(eq)) continue;

                int count = CountEquip(equipList, eq);

                if (count == 0) continue;

                if (count == 1)
                {
                    strEquipment += eq + " ";
                }
                else
                {
                    strEquipment += count.ToString() + "x" + eq + " ";
                }

                done.Add(eq);
            }
            return strEquipment;
        }

        private static int CountEquip(IList<string> lst, string name)
        {
            int count = 0;

            foreach (string str in lst)
            {
                if (str.Equals(name))
                {
                    count++;
                }
            }

            return count;
        }

        private static string GetAirFlow(Document doc, Space space)
        {
            IList<FamilyInstance> terminals = Utils.GetDuctTerminals_InSpaceId(doc, space.Id);
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

        private static string GetNumber(string original)
        {
            return new string(original.Where(c => Char.IsDigit(c) || c == '.').ToArray());
        }

        private static SpaceTag FindSpaceTag_BySpace(Document doc, Space space)
        {
            SpaceTag spaceTag = null;

            IList<Element> tags = Utils.GetSpaceTags(doc);

            foreach (Element tag in tags)
            {
                spaceTag = tag as SpaceTag;

                if (spaceTag.Space.Id.IntegerValue.ToString().Equals(space.Id.IntegerValue.ToString()))
                {
                    return spaceTag;
                }
            }
            return null;
        }

        private static ElementId FindTagId(Document doc, Space space)
        {
            ElementId eId = null;

            IList<Element> tags = Utils.GetTags_ByTypeName(doc, k_tagType_1);

            foreach (Element tag in tags)
            {
                SpaceTag spaceTag = tag as SpaceTag;

                if (spaceTag.Space.Id.IntegerValue.ToString().Equals(space.Id.IntegerValue.ToString()))
                {
                    eId = tag.Id;
                    break;
                }
            }

            return eId;
        }
    }
}
