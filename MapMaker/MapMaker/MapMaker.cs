using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapMaker
{
    public partial class MapMaker : Form
    {
        public MapMaker()
        {
            InitializeComponent();
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {

        }

        // click the clear button to reset everything in the application
        private void ClearButton_Click(object sender, EventArgs e)
        {
            CurrentMapTextBox.Clear();
            MapArrayGrid.ClearSelection(); // MapArrayGrid.Clear? MapArrayGrid.CancelEdit?
            BackgroundImageTextBox.Clear(); // there's a red line here because of the hanging .Clear(); later
            /* ARRAY NAME */.Clear();
        }

        // Click the save button to save the file. Also depends on the array/list/whatever you feel is best.
        // Just edit the information to what you end up naming the array.
        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(/* ARRAY NAME*/))
            {
                // loop through array size
                for (int i = 0; i < /* ARRAY NAME .*/Length; i++)
                {
                    // Write to a file
                    sw.Write("{0:0.0} ", i);
                }
            }
        }
    }
}
