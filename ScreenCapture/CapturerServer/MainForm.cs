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
using System.Runtime.InteropServices;
using System.IO;

namespace CapturerServer
{
    public partial class MainForm : Form
    {
        public static MainForm staticInstance;

        public Thread captureThread;
        private int currentScreenID = 0;
        private bool isCapturing = false;
        public  bool abort = false;
        private Action updateDelegate;

        private Bitmap currentCapture;
        public byte[] jpegDisplay;
        public int imgID = 0;

        public MainForm()
        {
            staticInstance = this;
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
                    Rectangle captureRect = current.Bounds;
                    Bitmap captureBitmap = new Bitmap(captureRect.Width, captureRect.Height, PixelFormat.Format32bppArgb);
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(captureRect.Left, captureRect.Top, 0, 0, captureRect.Size);

                    if (currentCapture == null || currentCapture.Size != captureBitmap.Size || !CompareMemCmp(currentCapture, captureBitmap))
                    {
                        //Manipulate ID
                        imgID++;
                        if (imgID > 100000) imgID = imgID % 100000;

                        //Store
                        lock (currentCapture)
                        {
                            if (currentCapture != null) currentCapture.Dispose();
                            currentCapture = captureBitmap;
                        }

                        //Transform
                        using (var memStream = new MemoryStream())
                        {
                            captureBitmap.Save(memStream, ImageFormat.Jpeg);
                            jpegDisplay = memStream.ToArray();
                        }

                        //Display
                        Invoke(updateDelegate);
                    }
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
            ControlLabel.Text = imgID.ToString();
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
        
        //Comparing Bitmaps

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        private static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
    }
}
