using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;

namespace CapturerServer
{
    public partial class MainForm : Form
    {
        private Thread t;
        private int currentScreenID = 0;
        private bool isCapturing = false;
        private Bitmap currentCapture;
        private bool abort = false;

        public MainForm()
        {
            InitializeComponent();
            t = new Thread(CaptureScreen);
            t.Start();
        }

        private void ScreenLabel_Click(object sender, EventArgs e)
        {

        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            currentScreenID--;
            if(currentScreenID < 0)
            {
                currentScreenID = Screen.AllScreens.Length - 1;
            }
            ScreenLabel.Text = "Current Screen: Screen " + currentScreenID;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            currentScreenID++;
            if(currentScreenID >= Screen.AllScreens.Length)
            {
                currentScreenID = 0;
            }
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            isCapturing = !isCapturing;
            StreamPicture.Image = null;
        }

        private void CaptureScreen()
        {
            while(!abort)
            {
                if (isCapturing)
                {
                    //Capture
                    Screen current = Screen.AllScreens[currentScreenID];
                    Rectangle captureRect = current.Bounds;
                    Bitmap captureBitmap = new Bitmap(StreamPicture.Width, StreamPicture.Height, PixelFormat.Format32bppArgb);
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(captureRect.Left, captureRect.Top, 0, 0, captureRect.Size);

                    //Display/Store
                    StreamPicture.Image = captureBitmap;
                    currentCapture = captureBitmap;
                }

                Thread.Sleep(1000);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            abort = true;
            t.Join(5000);
        }
    }
}
