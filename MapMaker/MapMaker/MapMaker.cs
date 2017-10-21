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
            //CurrentMapTextBox.Clear();
            currentMapDomain.Text = "Map 1";
            MapArrayGrid.ClearSelection(); // MapArrayGrid.Clear? MapArrayGrid.CancelEdit?
            BackgroundImageTextBox.Clear(); // there's a red line here because of the hanging .Clear(); later
            /* ARRAY NAME */.Clear();
        }

        /*
         * 10/20 UPDATE:
         * Two different types of ways to save a file, wasn't sure which would necessarily work better.
         */

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

            Stream str;
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                if((str = saveFile.OpenFile()) != null)
                {
                    str.Write("{0:0.0} ", /* ARRAY NAME.LENGTH*/);
                    str.Close();
                }
            }
        }

        private void CurrentMapTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        // When the user clicks the create new button, they make a new map
        private void CreateNewButton_Click(object sender, EventArgs e)
        {
            /*
             * Not sure if we should clear everything first. Doing it the same way as "clear" might just delete the previous map info
             * 
             * //CurrentMapTextBox.Clear();
            currentMapDomain.Text = "Map 1";
            MapArrayGrid.ClearSelection(); // MapArrayGrid.Clear? MapArrayGrid.CancelEdit?
            BackgroundImageTextBox.Clear(); // there's a red line here because of the hanging .Clear(); later
            /* ARRAY NAME.CLEAR*/

            int mapNumber = 1;
            currentMapDomain.Items.Add("Map " + mapNumber + 1);
        }
    }
}
