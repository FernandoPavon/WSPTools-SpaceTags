using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSPTools.Properties;

namespace WSPTools
{
    public partial class About_Form : Form
    {
        public About_Form()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void About_Form_Load(object sender, EventArgs e)
        {
            AboutTextBox.Text = "Copyright 2021 Autodesk, Inc. All rights reserved. " +

               "\r\n\r\nTHE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR" +
               "\r\nIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY," +
               "\r\nFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE" +
               "\r\nAUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER" +
               "\r\nLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM," +
               "\r\nOUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE." +

               "\r\n\r\nAutodesk" +
               "\r\nCustomer Services Organisation" +
               "\r\nFarnborough, UK" +
               "\r\nFernando Pavon - Senior Technology Consultant - fernando.pavon@autodesk.com" +
               "\r\n\r\nTested for Revit 2020" +
               "\r\n\r\nVersion " + Resources.VersionNumber +
               "\r\n" + Resources.VersionDate;
        }
    }
}
