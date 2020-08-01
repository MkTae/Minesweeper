using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public const int mapsize = 9;
        public const int mineCount = 10;
        public const int cellsize = 50;
        public bool isfirststep;
        public static Image spriteSet;
        private static int currentPictureToSet = 0;



        public int[,] map = new int[mapsize, mapsize];
        public Button[,] buttons = new Button[mapsize, mapsize];

        public Form1()
        {
            InitializeComponent();

            init();

        }

        public void init()
        {
            isfirststep = true;
            currentPictureToSet = 0;

            // spriteSet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "pictures//sprite.png"));
            initmap();
            initbuttons();
            configuratemapsize();
        }

        public static Image FindNeededImage(int xPos, int yPos)
        {
            Image image = new Bitmap(cellsize, cellsize);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(spriteSet, new Rectangle(new Point(0, 0), new Size(cellsize, cellsize)), 0 + 32 * xPos, 0 + 32 * yPos, 33, 33, GraphicsUnit.Pixel);


            return image;
        }
        private void configuratemapsize()
        {
            this.Width = mapsize * cellsize + 20;
            this.Height = (mapsize + 1) * cellsize;

        }

        private void initmap()
        {
            for (int i = 0; i < mapsize; i++)
            {
                for (int j = 0; j < mapsize; j++)
                {
                    map[i, j] = 0;
                }
            }
        }
        public void initbuttons()
        {
            for (int i = 0; i < mapsize; i++)
            {
                for (int j = 0; j < mapsize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellsize, i * cellsize);
                    button.Size = new Size(cellsize, cellsize);
                    button.MouseUp += new MouseEventHandler(OnButtonPressed); 
                    this.Controls.Add(button);


                    buttons[i, j] = button;
                }
            }
        }



        public void OnButtonPressed(object sender, MouseEventArgs e)
        {
            Button pressedbutton = sender as Button;
            if (e.Button==MouseButtons.Right)
            {                
                OnRightButtonPressed(pressedbutton);                                                         
            }
            else if(e.Button == MouseButtons.Left)
            {
                OnLeftButtonCilcked(pressedbutton);
            }

        }
        private static void OnRightButtonPressed(Button pressedButton)
        {
            currentPictureToSet++;
            currentPictureToSet %= 3;
            int posX = 0;
            int posY = 0;
            switch (currentPictureToSet)
            {
                case 0:
                    posX = 0;
                    posY = 0;
                    break;
                case 1:
                    posX = 0;
                    posY = 2;
                    break;
                case 2:
                    posX = 2;
                    posY = 2;
                    break;
            }
            pressedButton.Image = FindNeededImage(posX, posY);
        }
        private void OnLeftButtonCilcked(Button pressedbutton)
        {
            pressedbutton.Enabled = false;
            if (isfirststep)
            {
                seedmap();
                isfirststep = false;
            }
        }

        private void seedmap()
        {
            Random x = new Random();

            for (int k = 0; k < mineCount; k++)
            {
                int posi = x.Next(0, mapsize);
                int posj = x.Next(0, mapsize);
                while (map[posi, posj] == -1 || !buttons[posi, posj].Enabled)
                {
                    posi = x.Next(0, mapsize);
                    posj = x.Next(0, mapsize);
                }

                for (int i = posi - 1; i <= posi + 1; i++)
                {
                    for (int j = posj - 1; j <= posj + 1; j++)
                    {
                        if (i >= 0 && i < mapsize && j >= 0 && j < mapsize) { }
                        else
                        {
                            if (i < 0) { i++; }
                            if (j < 0) { j++; }
                            if (i >= mapsize) { break; }
                            if (j >= mapsize) { break; }
                        }
                        if (map[i, j] != -1)
                        {
                            map[i, j]++;
                        }
                    }
                }
                map[posi, posj] = -1;
            }
        }
        private void fillMap()
        {
            //to do: implement fill map logic
        }


    }
}
