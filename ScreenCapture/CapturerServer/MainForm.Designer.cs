namespace CapturerServer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StreamPicture = new System.Windows.Forms.PictureBox();
            this.CaptureButton = new System.Windows.Forms.Button();
            this.ScreenLabel = new System.Windows.Forms.Label();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.ControlLabel = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.StreamPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // StreamPicture
            // 
            this.StreamPicture.Enabled = false;
            this.StreamPicture.Location = new System.Drawing.Point(54, 87);
            this.StreamPicture.Name = "StreamPicture";
            this.StreamPicture.Size = new System.Drawing.Size(768, 432);
            this.StreamPicture.TabIndex = 0;
            this.StreamPicture.TabStop = false;
            // 
            // CaptureButton
            // 
            this.CaptureButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.CaptureButton.Location = new System.Drawing.Point(568, 567);
            this.CaptureButton.Name = "CaptureButton";
            this.CaptureButton.Size = new System.Drawing.Size(146, 36);
            this.CaptureButton.TabIndex = 1;
            this.CaptureButton.Text = "Start Capture";
            this.CaptureButton.UseVisualStyleBackColor = true;
            this.CaptureButton.Click += new System.EventHandler(this.CaptureButton_Click);
            // 
            // ScreenLabel
            // 
            this.ScreenLabel.AutoSize = true;
            this.ScreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ScreenLabel.Location = new System.Drawing.Point(368, 529);
            this.ScreenLabel.Name = "ScreenLabel";
            this.ScreenLabel.Size = new System.Drawing.Size(145, 25);
            this.ScreenLabel.TabIndex = 2;
            this.ScreenLabel.Text = "Current Screen";
            this.ScreenLabel.Click += new System.EventHandler(this.ScreenLabel_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.PreviousButton.Location = new System.Drawing.Point(54, 525);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(102, 36);
            this.PreviousButton.TabIndex = 3;
            this.PreviousButton.Text = "Previous";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.NextButton.Location = new System.Drawing.Point(720, 525);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(102, 36);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // ControlLabel
            // 
            this.ControlLabel.AutoSize = true;
            this.ControlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ControlLabel.Location = new System.Drawing.Point(54, 584);
            this.ControlLabel.Name = "ControlLabel";
            this.ControlLabel.Size = new System.Drawing.Size(142, 25);
            this.ControlLabel.TabIndex = 5;
            this.ControlLabel.Text = "Control Status:";
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.HeaderLabel.Location = new System.Drawing.Point(47, 30);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(156, 25);
            this.HeaderLabel.TabIndex = 6;
            this.HeaderLabel.Text = "Screen Capturer";
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.ExitButton.Location = new System.Drawing.Point(720, 567);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(102, 36);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 619);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.ControlLabel);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PreviousButton);
            this.Controls.Add(this.ScreenLabel);
            this.Controls.Add(this.CaptureButton);
            this.Controls.Add(this.StreamPicture);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StreamPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox StreamPicture;
        private System.Windows.Forms.Button CaptureButton;
        private System.Windows.Forms.Label ScreenLabel;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label ControlLabel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.Button ExitButton;
    }
}

