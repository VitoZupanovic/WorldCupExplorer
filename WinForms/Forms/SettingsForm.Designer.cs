namespace WinForms
{
    partial class SettingsForm
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
            lblChampionship = new Label();
            lblDataSource = new Label();
            cmbChampionship = new ComboBox();
            cmbDataSource = new ComboBox();
            btnConfirm = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblChampionship
            // 
            lblChampionship.AutoSize = true;
            lblChampionship.Location = new Point(28, 28);
            lblChampionship.Name = "lblChampionship";
            lblChampionship.Size = new Size(107, 20);
            lblChampionship.TabIndex = 0;
            lblChampionship.Text = "Championship:";
            // 
            // lblDataSource
            // 
            lblDataSource.AutoSize = true;
            lblDataSource.Location = new Point(28, 65);
            lblDataSource.Name = "lblDataSource";
            lblDataSource.Size = new Size(91, 20);
            lblDataSource.TabIndex = 1;
            lblDataSource.Text = "Data source:";
            // 
            // cmbChampionship
            // 
            cmbChampionship.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChampionship.FormattingEnabled = true;
            cmbChampionship.Location = new Point(150, 25);
            cmbChampionship.Name = "cmbChampionship";
            cmbChampionship.Size = new Size(180, 28);
            cmbChampionship.TabIndex = 2;
            // 
            // cmbDataSource
            // 
            cmbDataSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDataSource.FormattingEnabled = true;
            cmbDataSource.Location = new Point(150, 62);
            cmbDataSource.Name = "cmbDataSource";
            cmbDataSource.Size = new Size(180, 28);
            cmbDataSource.TabIndex = 3;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(28, 127);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(100, 40);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(150, 127);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SettingsForm
            // 
            AcceptButton = btnConfirm;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(382, 203);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(cmbDataSource);
            Controls.Add(cmbChampionship);
            Controls.Add(lblDataSource);
            Controls.Add(lblChampionship);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblChampionship;
        private Label lblDataSource;
        private ComboBox cmbChampionship;
        private ComboBox cmbDataSource;
        private Button btnConfirm;
        private Button btnCancel;
    }
}