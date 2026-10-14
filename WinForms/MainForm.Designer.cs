namespace WinForms
{
    partial class WorldCupStats
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldCupStats));
            lblFavoriteTeam = new Label();
            lblCurrentSettings = new Label();
            cmbFavoriteTeam = new ComboBox();
            btnSaveFavorites = new Button();
            pnlFavoritePlayers = new Panel();
            pnlOtherPlayers = new Panel();
            lblFavoritePlayers = new Label();
            lblOtherPlayers = new Label();
            btnAddSelectedToFavorites = new Button();
            btnRemoveSelectedFromFavorites = new Button();
            lblRankings = new Label();
            btnGoalRanking = new Button();
            btnYellowCardRanking = new Button();
            btnAttendanceRanking = new Button();
            dgvRankings = new DataGridView();
            btnPrintRanking = new Button();
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDialog = new PrintDialog();
            printPreviewDialog = new PrintPreviewDialog();
            pageSetupDialog = new PageSetupDialog();
            btnPageSetup = new Button();
            btnPreviewRanking = new Button();
            btnSettings = new Button();
            lblLoading = new Label();
            btnClearRanking = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRankings).BeginInit();
            SuspendLayout();
            // 
            // lblFavoriteTeam
            // 
            lblFavoriteTeam.AutoSize = true;
            lblFavoriteTeam.Location = new Point(42, 62);
            lblFavoriteTeam.Name = "lblFavoriteTeam";
            lblFavoriteTeam.Size = new Size(160, 20);
            lblFavoriteTeam.TabIndex = 0;
            lblFavoriteTeam.Text = "Favorite national team:";
            // 
            // lblCurrentSettings
            // 
            lblCurrentSettings.AutoSize = true;
            lblCurrentSettings.Location = new Point(42, 16);
            lblCurrentSettings.Name = "lblCurrentSettings";
            lblCurrentSettings.Size = new Size(115, 20);
            lblCurrentSettings.TabIndex = 1;
            lblCurrentSettings.Text = "Current settings:";
            // 
            // cmbFavoriteTeam
            // 
            cmbFavoriteTeam.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFavoriteTeam.FormattingEnabled = true;
            cmbFavoriteTeam.Location = new Point(229, 62);
            cmbFavoriteTeam.Name = "cmbFavoriteTeam";
            cmbFavoriteTeam.Size = new Size(218, 28);
            cmbFavoriteTeam.TabIndex = 2;
            // 
            // btnSaveFavorites
            // 
            btnSaveFavorites.Location = new Point(42, 99);
            btnSaveFavorites.Name = "btnSaveFavorites";
            btnSaveFavorites.Size = new Size(160, 42);
            btnSaveFavorites.TabIndex = 3;
            btnSaveFavorites.Text = "Save favorites";
            btnSaveFavorites.UseVisualStyleBackColor = true;
            btnSaveFavorites.Click += btnSaveFavorites_Click;
            // 
            // pnlFavoritePlayers
            // 
            pnlFavoritePlayers.AllowDrop = true;
            pnlFavoritePlayers.AutoScroll = true;
            pnlFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
            pnlFavoritePlayers.Location = new Point(42, 179);
            pnlFavoritePlayers.Name = "pnlFavoritePlayers";
            pnlFavoritePlayers.Size = new Size(340, 270);
            pnlFavoritePlayers.TabIndex = 4;
            pnlFavoritePlayers.DragDrop += pnlFavoritePlayers_DragDrop;
            pnlFavoritePlayers.DragEnter += pnlPlayers_DragEnter;
            // 
            // pnlOtherPlayers
            // 
            pnlOtherPlayers.AllowDrop = true;
            pnlOtherPlayers.AutoScroll = true;
            pnlOtherPlayers.BorderStyle = BorderStyle.FixedSingle;
            pnlOtherPlayers.Location = new Point(581, 179);
            pnlOtherPlayers.Name = "pnlOtherPlayers";
            pnlOtherPlayers.Size = new Size(340, 270);
            pnlOtherPlayers.TabIndex = 5;
            pnlOtherPlayers.DragDrop += pnlOtherPlayers_DragDrop;
            pnlOtherPlayers.DragEnter += pnlPlayers_DragEnter;
            // 
            // lblFavoritePlayers
            // 
            lblFavoritePlayers.AutoSize = true;
            lblFavoritePlayers.Location = new Point(42, 156);
            lblFavoritePlayers.Name = "lblFavoritePlayers";
            lblFavoritePlayers.Size = new Size(112, 20);
            lblFavoritePlayers.TabIndex = 6;
            lblFavoritePlayers.Text = "Favorite players";
            // 
            // lblOtherPlayers
            // 
            lblOtherPlayers.AutoSize = true;
            lblOtherPlayers.Location = new Point(581, 156);
            lblOtherPlayers.Name = "lblOtherPlayers";
            lblOtherPlayers.Size = new Size(97, 20);
            lblOtherPlayers.TabIndex = 7;
            lblOtherPlayers.Text = "Other players";
            // 
            // btnAddSelectedToFavorites
            // 
            btnAddSelectedToFavorites.Location = new Point(402, 229);
            btnAddSelectedToFavorites.Name = "btnAddSelectedToFavorites";
            btnAddSelectedToFavorites.Size = new Size(160, 35);
            btnAddSelectedToFavorites.TabIndex = 8;
            btnAddSelectedToFavorites.Text = "Add selected ->";
            btnAddSelectedToFavorites.UseVisualStyleBackColor = true;
            btnAddSelectedToFavorites.Click += btnAddSelectedToFavorites_Click;
            // 
            // btnRemoveSelectedFromFavorites
            // 
            btnRemoveSelectedFromFavorites.Location = new Point(402, 270);
            btnRemoveSelectedFromFavorites.Name = "btnRemoveSelectedFromFavorites";
            btnRemoveSelectedFromFavorites.Size = new Size(160, 35);
            btnRemoveSelectedFromFavorites.TabIndex = 9;
            btnRemoveSelectedFromFavorites.Text = "<- Remove selected";
            btnRemoveSelectedFromFavorites.UseVisualStyleBackColor = true;
            btnRemoveSelectedFromFavorites.Click += btnRemoveSelectedFromFavorites_Click;
            // 
            // lblRankings
            // 
            lblRankings.AutoSize = true;
            lblRankings.Location = new Point(42, 483);
            lblRankings.Name = "lblRankings";
            lblRankings.Size = new Size(71, 20);
            lblRankings.TabIndex = 10;
            lblRankings.Text = "Rankings:";
            // 
            // btnGoalRanking
            // 
            btnGoalRanking.Location = new Point(131, 473);
            btnGoalRanking.Name = "btnGoalRanking";
            btnGoalRanking.Size = new Size(150, 40);
            btnGoalRanking.TabIndex = 11;
            btnGoalRanking.Text = "Goal ranking";
            btnGoalRanking.UseVisualStyleBackColor = true;
            btnGoalRanking.Click += btnGoalRanking_Click;
            // 
            // btnYellowCardRanking
            // 
            btnYellowCardRanking.Location = new Point(314, 473);
            btnYellowCardRanking.Name = "btnYellowCardRanking";
            btnYellowCardRanking.Size = new Size(150, 40);
            btnYellowCardRanking.TabIndex = 12;
            btnYellowCardRanking.Text = "Yellow card ranking";
            btnYellowCardRanking.UseVisualStyleBackColor = true;
            btnYellowCardRanking.Click += btnYellowCardRanking_Click;
            // 
            // btnAttendanceRanking
            // 
            btnAttendanceRanking.Location = new Point(497, 473);
            btnAttendanceRanking.Name = "btnAttendanceRanking";
            btnAttendanceRanking.Size = new Size(150, 40);
            btnAttendanceRanking.TabIndex = 13;
            btnAttendanceRanking.Text = "Attendance ranking";
            btnAttendanceRanking.UseVisualStyleBackColor = true;
            btnAttendanceRanking.Click += btnAttendanceRanking_Click;
            // 
            // dgvRankings
            // 
            dgvRankings.AllowUserToAddRows = false;
            dgvRankings.AllowUserToDeleteRows = false;
            dgvRankings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRankings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRankings.Location = new Point(131, 534);
            dgvRankings.Name = "dgvRankings";
            dgvRankings.ReadOnly = true;
            dgvRankings.RowHeadersWidth = 51;
            dgvRankings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRankings.Size = new Size(700, 172);
            dgvRankings.TabIndex = 14;
            // 
            // btnPrintRanking
            // 
            btnPrintRanking.Location = new Point(354, 721);
            btnPrintRanking.Name = "btnPrintRanking";
            btnPrintRanking.Size = new Size(150, 40);
            btnPrintRanking.TabIndex = 15;
            btnPrintRanking.Text = "Print / Export PDF";
            btnPrintRanking.UseVisualStyleBackColor = true;
            btnPrintRanking.Click += btnPrintRanking_Click;
            // 
            // printDocument
            // 
            printDocument.EndPrint += printDocument_EndPrint;
            printDocument.PrintPage += printDocument_PrintPage;
            // 
            // printDialog
            // 
            printDialog.Document = printDocument;
            printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            printPreviewDialog.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.Enabled = true;
            printPreviewDialog.Icon = (Icon)resources.GetObject("printPreviewDialog.Icon");
            printPreviewDialog.Name = "printPreviewDialog";
            printPreviewDialog.Visible = false;
            // 
            // pageSetupDialog
            // 
            pageSetupDialog.Document = printDocument;
            // 
            // btnPageSetup
            // 
            btnPageSetup.Location = new Point(42, 721);
            btnPageSetup.Name = "btnPageSetup";
            btnPageSetup.Size = new Size(150, 40);
            btnPageSetup.TabIndex = 16;
            btnPageSetup.Text = "Page Setup";
            btnPageSetup.UseVisualStyleBackColor = true;
            btnPageSetup.Click += btnPageSetup_Click;
            // 
            // btnPreviewRanking
            // 
            btnPreviewRanking.Location = new Point(198, 721);
            btnPreviewRanking.Name = "btnPreviewRanking";
            btnPreviewRanking.Size = new Size(150, 40);
            btnPreviewRanking.TabIndex = 17;
            btnPreviewRanking.Text = "Preview";
            btnPreviewRanking.UseVisualStyleBackColor = true;
            btnPreviewRanking.Click += btnPreviewRanking_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(464, 9);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(130, 35);
            btnSettings.TabIndex = 18;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // lblLoading
            // 
            lblLoading.AutoSize = true;
            lblLoading.Location = new Point(617, 16);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(72, 20);
            lblLoading.TabIndex = 19;
            lblLoading.Text = "Loading...";
            lblLoading.Visible = false;
            // 
            // btnClearRanking
            // 
            btnClearRanking.Location = new Point(680, 473);
            btnClearRanking.Name = "btnClearRanking";
            btnClearRanking.Size = new Size(150, 40);
            btnClearRanking.TabIndex = 20;
            btnClearRanking.Text = "Clear ranking";
            btnClearRanking.UseVisualStyleBackColor = true;
            btnClearRanking.Click += btnClearRanking_Click;
            // 
            // WorldCupStats
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 783);
            Controls.Add(btnClearRanking);
            Controls.Add(lblLoading);
            Controls.Add(btnSettings);
            Controls.Add(btnPreviewRanking);
            Controls.Add(btnPageSetup);
            Controls.Add(btnPrintRanking);
            Controls.Add(dgvRankings);
            Controls.Add(btnAttendanceRanking);
            Controls.Add(btnYellowCardRanking);
            Controls.Add(btnGoalRanking);
            Controls.Add(lblRankings);
            Controls.Add(btnRemoveSelectedFromFavorites);
            Controls.Add(btnAddSelectedToFavorites);
            Controls.Add(lblOtherPlayers);
            Controls.Add(lblFavoritePlayers);
            Controls.Add(pnlOtherPlayers);
            Controls.Add(pnlFavoritePlayers);
            Controls.Add(btnSaveFavorites);
            Controls.Add(cmbFavoriteTeam);
            Controls.Add(lblCurrentSettings);
            Controls.Add(lblFavoriteTeam);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "WorldCupStats";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "World Cup Stats";
            FormClosing += WorldCupStats_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dgvRankings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFavoriteTeam;
        private Label lblCurrentSettings;
        private ComboBox cmbFavoriteTeam;
        private Button btnSaveFavorites;
        private Panel pnlFavoritePlayers;
        private Panel pnlOtherPlayers;
        private Label lblFavoritePlayers;
        private Label lblOtherPlayers;
        private Button btnAddSelectedToFavorites;
        private Button btnRemoveSelectedFromFavorites;
        private Label lblRankings;
        private Button btnGoalRanking;
        private Button btnYellowCardRanking;
        private Button btnAttendanceRanking;
        private DataGridView dgvRankings;
        private Button btnPrintRanking;
        private System.Drawing.Printing.PrintDocument printDocument;
        private PrintDialog printDialog;
        private PrintPreviewDialog printPreviewDialog;
        private PageSetupDialog pageSetupDialog;
        private Button btnPageSetup;
        private Button btnPreviewRanking;
        private Button btnSettings;
        private Label lblLoading;
        private Button btnClearRanking;
    }
}
