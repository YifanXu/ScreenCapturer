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
        private Thread captureThread;
        private int currentScreenID = 0;
        private bool isCapturing = false;
        private Bitmap currentCapture;
        private bool abort = false;
        private Action updateDelegate;

        public MainForm()
        {
            InitializeComponent();
            updateDelegate = UpdateImage;
            currentCapture = new Bitmap(1, 1);

            //Screens Setup
            ScreenLabel.Text = GetScreenText(Screen.AllScreens[currentScreenID]);
            if(Screen.AllScreens.Length == 1)
            {
                PreviousButton.Enabled = false;
                NextButton.Enabled = false;
            }

            //Start CaptureThread
            captureThread = new Thread(CaptureScreen);
            captureThread.Start();
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
            ScreenLabel.Text = GetScreenText(Screen.AllScreens[currentScreenID]);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            currentScreenID++;
            if(currentScreenID >= Screen.AllScreens.Length)
            {
                currentScreenID = 0;
            }
            Screen s = Screen.AllScreens[currentScreenID];
            ScreenLabel.Text = GetScreenText(Screen.AllScreens[currentScreenID]);
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            isCapturing = !isCapturing;
            StreamPicture.Enabled = isCapturing;
            CaptureButton.Text = isCapturing ? "Stop Capture" : "Start Capture";
        }

        private void CaptureScreen()
        {
            while(!abort)
            {
                if (isCapturing)
                {
                    //Capture
                    Screen current = Screen.AllScreens[currentScreenID];
                    //Rectangle captureRect = new Rectangle(0, 0, 1920, 1080);
                    Rectangle captureRect = current.Bounds;
                    Bitmap captureBitmap = new Bitmap(captureRect.Width, captureRect.Height, PixelFormat.Format32bppArgb);
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(captureRect.Left, captureRect.Top, 0, 0, captureRect.Size);

                    //Store
                    lock(currentCapture)
                    {
                        if (currentCapture != null) currentCapture.Dispose();
                        currentCapture = captureBitmap;
                    }

                    //Display
                    Invoke(updateDelegate);

                    captureGraphics.Dispose();
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
            captureThread.Abort();
            Close();
        }

        private void UpdateImage()
        {
            if (StreamPicture.Image != null) StreamPicture.Image.Dispose();
            if (currentCapture != null)
            {
                Bitmap newImage = new Bitmap(StreamPicture.Width, StreamPicture.Height);
                Graphics captureGraphics = Graphics.FromImage(newImage);
                captureGraphics.DrawImage(currentCapture, new Rectangle(0, 0, StreamPicture.Width, StreamPicture.Height));
                StreamPicture.Image = newImage;
                captureGraphics.Dispose();
            }
        }

        private string GetScreenText(Screen s)
        {
            return $"\"{s.DeviceName}\"[{currentScreenID}]({s.Bounds.Width}x{s.Bounds.Height})";
        }
    }
}
