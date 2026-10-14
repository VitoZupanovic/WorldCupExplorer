namespace WinForms.Forms
{
    partial class ConfirmForm
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
            lblMessage = new Label();
            btnYes = new Button();
            btnNo = new Button();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Location = new Point(21, 17);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(364, 50);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "Are you sure?";
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnYes
            // 
            btnYes.DialogResult = DialogResult.OK;
            btnYes.Location = new Point(186, 94);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(90, 35);
            btnYes.TabIndex = 1;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            // 
            // btnNo
            // 
            btnNo.DialogResult = DialogResult.Cancel;
            btnNo.Location = new Point(295, 94);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(90, 35);
            btnNo.TabIndex = 2;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = true;
            // 
            // ConfirmForm
            // 
            AcceptButton = btnYes;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnNo;
            ClientSize = new Size(397, 155);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfirmForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Confirm";
            ResumeLayout(false);
        }

        #endregion

        private Label lblMessage;
        private Button btnYes;
        private Button btnNo;
    }
}