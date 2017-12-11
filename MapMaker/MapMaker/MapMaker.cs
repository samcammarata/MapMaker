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
        private Image player1Image;
        private Image player2Image;
        private Image player3Image;
        private Image player4Image;
        private Image boss1Image;
        private Image boss2Image;
        private Image boss3Image;

        private Point p1Spawn;
        private Point p2Spawn;
        private Point p3Spawn;
        private Point p4Spawn;
        private Point b1Spawn;
        private Point b2Spawn;
        private Point b3Spawn;
        private int mapNumber = 1;

        enum ClickAction { SetP1Spawn, SetP2Spawn, SetP3Spawn, SetP4Spawn, SetB1Spawn, SetB2Spawn, SetB3Spawn, None}

        private ClickAction currentClickAction;

        private Image backgroundImage;

        public MapMaker()
        {
            InitializeComponent();
            player1Image = Image.FromFile("player1.png");
            player2Image = Image.FromFile("player2.png");
            player3Image = Image.FromFile("player3.png");
            player4Image = Image.FromFile("player4.png");

            boss1Image = Image.FromFile("TemporaryBoss.png");
            boss2Image = Image.FromFile("TemporaryBoss.png");
            boss3Image = Image.FromFile("TemporaryBoss.png");

            p1Spawn = Point.Empty;
            p2Spawn = Point.Empty;
            p3Spawn = Point.Empty;
            p4Spawn = Point.Empty;

            b1Spawn = Point.Empty;
            b2Spawn = Point.Empty;
            b3Spawn = Point.Empty;

            backgroundImage = null;
        }

        // click the clear button to reset everything in the application
        private void ClearButton_Click(object sender, EventArgs e)
        {
            /*
             * Fixed the clear button for currentMapDomain. 
             * Added in dialog to ask if user wants to go back and save first.
             * -Sophia
             */

            // ask user if they want to go back to save first
            DialogResult dialog = MessageBox.Show("Have you saved the map before clearing? (make sure to click the map after clearing)", "Exit", MessageBoxButtons.YesNo);

            // if they don't want to, it automatically clears for them
            if (dialog == DialogResult.Yes)
            {
                // reset the current map domain info. sets back to map 1
                // fixed so now it works properly
                /*
                currentMapTextBox.Items.Clear();
                currentMapDomain.ResetText();
                currentMapDomain.Text = null;
                currentMapDomain.Text = "Map 1";
                */
                CurrentMapTextBox.Text = "";

                // clear background image textbox
                BackgroundImageTextBox.Clear();

                //player1Image = null;
                //player2Image = null;
                //player3Image = null;
                //player4Image = null;
                //MapPanel = null;
                backgroundImage = null;
                p1Spawn = Point.Empty;
                p2Spawn = Point.Empty;
                p3Spawn = Point.Empty;
                p4Spawn = Point.Empty;
                b1Spawn = Point.Empty;
                b2Spawn = Point.Empty;
                b3Spawn = Point.Empty;

                // reset spawn points
                p1Spawn.X = 0;
                p1Spawn.Y = 0;
                p2Spawn.X = 0;
                p2Spawn.Y = 0;
                p3Spawn.X = 0;
                p3Spawn.Y = 0;
                p4Spawn.X = 0;
                p4Spawn.Y = 0;
                b1Spawn.X = 0;
                b2Spawn.X = 0;
                b3Spawn.X = 0;
                b1Spawn.Y = 0;
                b2Spawn.Y = 0;
                b3Spawn.Y = 0;
            }
            
        }

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
                filewrite.WriteLine(String.Format("Player 1 X pos:{0}", p1Spawn.X));
                filewrite.WriteLine(String.Format("Player 1 Y pos:{0}", p1Spawn.Y));
                filewrite.WriteLine(String.Format("Player 2 X pos:{0}", p2Spawn.X));
                filewrite.WriteLine(String.Format("Player 2 Y pos:{0}", p2Spawn.Y));
                filewrite.WriteLine(String.Format("Player 3 X pos:{0}", p3Spawn.X));
                filewrite.WriteLine(String.Format("Player 3 Y pos:{0}", p3Spawn.Y));
                filewrite.WriteLine(String.Format("Player 4 X pos:{0}", p4Spawn.X));
                filewrite.WriteLine(String.Format("Player 4 Y pos:{0}", p4Spawn.Y));

                // Save the boss locations
                filewrite.WriteLine(String.Format("Boss 1 X pos:{0}", b1Spawn.X));
                filewrite.WriteLine(String.Format("Boss 1 Y pos:{0}", b1Spawn.Y));
                filewrite.WriteLine(String.Format("Boss 2 X pos:{0}", b2Spawn.X));
                filewrite.WriteLine(String.Format("Boss 2 X pos:{0}", b2Spawn.Y));
                filewrite.WriteLine(String.Format("Boss 3 X pos:{0}", b3Spawn.X));
                filewrite.WriteLine(String.Format("Boss 3 X pos:{0}", b3Spawn.Y));

                // save the background image
                filewrite.WriteLine(String.Format("Background image: {0}", BackgroundImageTextBox.Text));

                // save the map name
                filewrite.WriteLine(String.Format("Map name: {0}", CurrentMapTextBox.Text));
            }
        }

        // 
        private void CurrentMapTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        // When the user clicks the create new button, they make a new map
        private void CreateNewButton_Click(object sender, EventArgs e)
        {
            /*
             * Dialog comes up asking if user wants to go back to save first before creating new.
             * -Sophia
             */

            // dialog asking user if they want to go back to save
            DialogResult dialog = MessageBox.Show("Have you saved the current map?", "Exit", MessageBoxButtons.YesNo);
            
            // user has already saved the map, programs creates a new file
            if(dialog == DialogResult.Yes)
            {
                // increment mapNumber
                mapNumber++;

                // adds in the next value to the map
                /*
                currentMapDomain.Items.Add("Map " + mapNumber);
                currentMapDomain.DownButton();
                */

                CurrentMapTextBox.Text = "Map" + mapNumber;

                // empty each point
                backgroundImage = null;
                p1Spawn = Point.Empty;
                p2Spawn = Point.Empty;
                p3Spawn = Point.Empty;
                p4Spawn = Point.Empty;
                b1Spawn = Point.Empty;
                b2Spawn = Point.Empty;
                b3Spawn = Point.Empty;

                // reset spawn values
                p1Spawn.X = 0;
                p1Spawn.Y = 0;
                p2Spawn.X = 0;
                p2Spawn.Y = 0;
                p3Spawn.X = 0;
                p3Spawn.Y = 0;
                p4Spawn.X = 0;
                p4Spawn.Y = 0;
                b1Spawn.X = 0;
                b1Spawn.Y = 0;
                b2Spawn.X = 0;
                b2Spawn.Y = 0;
                b3Spawn.X = 0;
                b3Spawn.Y = 0;
            }
        }

        private void MapPanel_MouseClick(object sender, MouseEventArgs e)
        {
            switch (currentClickAction)
            {
                case ClickAction.SetP1Spawn:
                    p1Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.SetP2Spawn:
                    p2Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.SetP3Spawn:
                    p3Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.SetP4Spawn:
                    p4Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.SetB1Spawn:
                    b1Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.SetB2Spawn:
                    b2Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.SetB3Spawn:
                    b3Spawn = e.Location;
                    currentClickAction = ClickAction.None;
                    break;
                case ClickAction.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            MapPanel.Invalidate();
        }

        // When a spawn button has been hit, the next subsequent click in the map panel will place that spawn point
        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (backgroundImage != null)
            {
                g.DrawImage(backgroundImage, 0, 0);
            }
            if (p1Spawn != Point.Empty)
            {
                g.DrawImage(player1Image, p1Spawn);
            }
            if (p2Spawn != Point.Empty)
            {
                g.DrawImage(player2Image, p2Spawn);
            }
            if (p3Spawn != Point.Empty)
            {
                g.DrawImage(player3Image, p3Spawn);
            }
            if (p4Spawn != Point.Empty)
            {
                g.DrawImage(player4Image, p4Spawn);
            }
            if (b1Spawn != Point.Empty)
            {
                g.DrawImage(boss1Image, b1Spawn);
            }
            if (b2Spawn != Point.Empty)
            {
                g.DrawImage(boss2Image, b2Spawn);
            }
            if (b3Spawn != Point.Empty)
            {
                g.DrawImage(boss3Image, b3Spawn);
            }

        }

        private void MapMaker_Load(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.None;
        }

        // clicking the upload button to upload in a background image
        private void BackgroundImageUploadButton_Click(object sender, EventArgs e)
        {
            /*
             * User can open various image file types, or any file type, to use as a background image.
             * The file location is stored in the text box.
             * -Sophia
             */

            // open file dialog
            OpenFileDialog dialog = new OpenFileDialog();

            // what type of files
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.JPEG) | *.BMP;*.JPG;*.PNG;*.JPEG | All Files (*.*) | *.*"; // this one is png files or all files

            // user can't open more than one file
            dialog.Multiselect = false; 

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                // get file name
                String path = dialog.FileName;
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding()));

                BackgroundImageTextBox.Text = path;

                backgroundImage = Image.FromFile(path);
            }
        }

        // When clicking on a player spawn button, it sets the current Click Action to the respective player's spawn click action
        private void Player1SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetP1Spawn;
        }

        private void Player2SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetP4Spawn;
        }

        private void Player3SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetP2Spawn;
        }

        private void Player4SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetP3Spawn;
        }

        private void Boss1SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetB1Spawn;
        }

        private void Boss2SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetB2Spawn;
        }

        private void Boss3SpawnButton_Click(object sender, EventArgs e)
        {
            currentClickAction = ClickAction.SetB3Spawn;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog dialog = new OpenFileDialog();

            // what type of files
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            // user can't open more than one file
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // get file name
                String path = dialog.FileName;
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) ;

                // Display the file name in the Current Map Domain Area
                CurrentMapTextBox.Text = path;

                // Read in the data and display the elements where they should be according to the text file
                // Read in player locations

                using (StreamReader reader = new StreamReader(path))
                {
                    // Read in the Player and Boss locations
                    for (int i = 0; i < 14; i++)
                    {
                        string input = reader.ReadLine().ToString();
                        string[] inputArray = input.Split(':');

                        if (i == 0)
                        {
                            p1Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 1)
                        {
                            p1Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 2)
                        {
                            p2Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 3)
                        {
                            p2Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 4)
                        {
                            p3Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 5)
                        {
                            p3Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 6)
                        {
                            p4Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 7)
                        {
                            p4Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 8)
                        {
                            b1Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 9)
                        {
                            b1Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 10)
                        {
                            b2Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 11)
                        {
                            b2Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 12)
                        {
                            b3Spawn.X = Convert.ToInt16(inputArray[1]);
                        }
                        else if (i == 13)
                        {
                            b3Spawn.Y = Convert.ToInt16(inputArray[1]);
                        }
                    }

                    // Read in the background image
                    string input2 = reader.ReadLine().ToString();
                    string[] input2Array = input2.Split(' ');
                    BackgroundImageTextBox.Text = input2Array[2];
                    backgroundImage = Image.FromFile(input2Array[2]);

                    // Read in the map name
                    string input3 = reader.ReadLine().ToString();
                    string[] input3Array = input3.Split(' ');
                    CurrentMapTextBox.Text = input3Array[2].ToString();
                }
            }
        }

    }
}
