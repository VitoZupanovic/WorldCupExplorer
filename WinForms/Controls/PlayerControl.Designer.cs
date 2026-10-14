namespace WinForms
{
    partial class PlayerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblName = new Label();
            lblNumber = new Label();
            lblPosition = new Label();
            lblCaptain = new Label();
            lblFavorite = new Label();
            picPlayer = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picPlayer).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(142, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(90, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Player name";
            // 
            // lblNumber
            // 
            lblNumber.AutoSize = true;
            lblNumber.Location = new Point(87, 12);
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(34, 20);
            lblNumber.TabIndex = 1;
            lblNumber.Text = "#10";
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(142, 32);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(61, 20);
            lblPosition.TabIndex = 2;
            lblPosition.Text = "Position";
            // 
            // lblCaptain
            // 
            lblCaptain.AutoSize = true;
            lblCaptain.Location = new Point(142, 52);
            lblCaptain.Name = "lblCaptain";
            lblCaptain.Size = new Size(87, 20);
            lblCaptain.TabIndex = 3;
            lblCaptain.Text = "Captain: No";
            // 
            // lblFavorite
            // 
            lblFavorite.AutoSize = true;
            lblFavorite.Location = new Point(278, 12);
            lblFavorite.Name = "lblFavorite";
            lblFavorite.Size = new Size(22, 20);
            lblFavorite.TabIndex = 4;
            lblFavorite.Text = "★";
            lblFavorite.Visible = false;
            // 
            // picPlayer
            // 
            picPlayer.BorderStyle = BorderStyle.FixedSingle;
            picPlayer.Location = new Point(15, 17);
            picPlayer.Name = "picPlayer";
            picPlayer.Size = new Size(55, 55);
            picPlayer.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayer.TabIndex = 5;
            picPlayer.TabStop = false;
            // 
            // PlayerControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(picPlayer);
            Controls.Add(lblFavorite);
            Controls.Add(lblCaptain);
            Controls.Add(lblPosition);
            Controls.Add(lblNumber);
            Controls.Add(lblName);
            Name = "PlayerControl";
            Size = new Size(300, 90);
            ((System.ComponentModel.ISupportInitialize)picPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblNumber;
        private Label lblPosition;
        private Label lblCaptain;
        private Label lblFavorite;
        private PictureBox picPlayer;
    }
}
