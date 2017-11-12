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
        private Image playerImage;

        private Point p1Spawn;
        private Point p2Spawn;
        private Point p3Spawn;
        private Point p4Spawn;
        private int mapNumber = 1;

        enum ClickAction { SetP1Spawn, SetP2Spawn, SetP3Spawn, SetP4Spawn, }

        private ClickAction currentClickAction;

        public MapMaker()
        {
            InitializeComponent();
            playerImage = Image.FromFile("playerMonk.png");
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {

        }

        // click the clear button to reset everything in the application
        private void ClearButton_Click(object sender, EventArgs e)
        {
            /*
             * Changed so that spawn points are all set to 0
             * There are issues regarding the currentMapDomain though...
             * Sometimes, you can suddenly go back to previous values you had if you created new files
             * It doesn't happen every time I try it, though
             * Been trying to find ways around this but I can't find anything
             * Tried setting it equal to null, or doing ResetText()
             * -Sophia
             */

            // reset the current map domain info. sets back to map 1.
            // sometimes runs into an error where you can suddenly go back to random numbers? doesnt always happen for me though.
            // not sure how to fix it
            currentMapDomain.ResetText();
            currentMapDomain.Text = null;
            currentMapDomain.Text = "Map 1";
            
            // clear background image textbox
            BackgroundImageTextBox.Clear();

            playerImage = null;
            MapPanel = null;

            // reset spawn points
            p1Spawn.X = 0;
            p1Spawn.Y = 0;
            p2Spawn.X = 0;
            p2Spawn.Y = 0;
            p3Spawn.X = 0;
            p3Spawn.Y = 0;
            p4Spawn.X = 0;
            p4Spawn.Y = 0;
        }

        /*
         * 10/20 UPDATE:
         * Two different types of ways to save a file, wasn't sure which would necessarily work better.
         */

        // Click the save button to save the file. Also depends on the array/list/whatever you feel is best.
        // Just edit the information to what you end up naming the array.
        private void SaveButton_Click(object sender, EventArgs e)
        {

            /*
             * New way
             * Save all of the data to a textfile in whatever location the user picks
             * Default name is "map", but it can be changed
             * -Sophia
             */

            SaveFileDialog file = new SaveFileDialog();
            file.FileName = "map.txt";
            file.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            file.ShowDialog();

            using(StreamWriter filewrite = new StreamWriter(file.FileName))
            {
                // save the player locations
                filewrite.WriteLine(String.Format("Player 1 X pos: {0}", p1Spawn.X));
                filewrite.WriteLine(String.Format("Player 1 Y pos: {0}", p1Spawn.Y));
                filewrite.WriteLine(String.Format("Player 2 X pos: {0}", p2Spawn.X));
                filewrite.WriteLine(String.Format("Player 2 Y pos: {0}", p2Spawn.Y));
                filewrite.WriteLine(String.Format("Player 3 X pos: {0}", p3Spawn.X));
                filewrite.WriteLine(String.Format("Player 3 Y pos: {0}", p3Spawn.Y));
                filewrite.WriteLine(String.Format("Player 4 X pos: {0}", p4Spawn.X));
                filewrite.WriteLine(String.Format("Player 4 Y pos: {0}", p4Spawn.Y));

                // save the background image
                filewrite.WriteLine(String.Format("Background image: {0}", BackgroundImageTextBox.Text));

                // save the map name
                filewrite.WriteLine(String.Format("Map name: {0}", currentMapDomain.Text));
            }

            // previous way of doing it here: 
            // (didn't want to delete full just in case)
            // save the file
            //Stream str;
            //SaveFileDialog saveFile = new SaveFileDialog();

            //saveFile.Filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
            //saveFile.FilterIndex = 2;
            //saveFile.RestoreDirectory = true;

            //if(saveFile.ShowDialog() == DialogResult.OK)
            //{
            //    if ((str = saveFile.OpenFile()) != null)
            //    {
            //        //str.Write("{0:0.0} ", /* ARRAY NAME.LENGTH*/);
            //        //str.Write("{0:0.0}"[], 1, 1);
            //        using (StreamWriter saveFileWrite = new StreamWriter(saveFile.FileName))
            //        {
            //            saveFileWrite.WriteLine(String.Format("First player pos is {0}", p1Spawn.X));
            //        }
            //            str.Close();
            //    }
            //}
        }

        private void CurrentMapTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        // When the user clicks the create new button, they make a new map
        private void CreateNewButton_Click(object sender, EventArgs e)
        {
            /*
             * Changed the function of the create new button to work better
             * No longer only adds an "11" to the list that you need to manually scroll to
             * Once you hit create new, it's the right map number and you are automatically sent to that name in the domainupdown
             * Can still scroll back
             * -Sophia
             */

            // increment mapNumber
            mapNumber++;

            // adds in the next value to the map
            currentMapDomain.Items.Add("Map " + mapNumber);
            currentMapDomain.DownButton();

            // empty each point
            p1Spawn = Point.Empty;
            p2Spawn = Point.Empty;
            p3Spawn = Point.Empty;
            p4Spawn = Point.Empty;

            // reset spawn values
            p1Spawn.X = 0;
            p1Spawn.Y = 0;
            p2Spawn.X = 0;
            p2Spawn.Y = 0;
            p3Spawn.X = 0;
            p3Spawn.Y = 0;
            p4Spawn.X = 0;
            p4Spawn.Y = 0;
        }

        private void MapPanel_MouseClick(object sender, MouseEventArgs e)
        {
            switch (currentClickAction)
            {
                case ClickAction.SetP1Spawn:
                    p1Spawn = e.Location;
                    break;
                case ClickAction.SetP2Spawn:
                    p2Spawn = e.Location;
                    break;
                case ClickAction.SetP3Spawn:
                    p3Spawn = e.Location;
                    break;
                case ClickAction.SetP4Spawn:
                    p4Spawn = e.Location;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            MapPanel.Invalidate();
        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(playerImage, p1Spawn);
            g.DrawImage(playerImage, p2Spawn);
            g.DrawImage(playerImage, p3Spawn);
            g.DrawImage(playerImage, p4Spawn);
        }

        private void Player1SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetP1Spawn;
        }

        private void MapMaker_Load(object sender, EventArgs e)
        {

        }
    }
}
