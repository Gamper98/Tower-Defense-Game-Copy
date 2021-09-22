namespace TDGame.View
{
    partial class GameForm
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
            this.TimeLabel = new System.Windows.Forms.Label();
            this.GoldLabel = new System.Windows.Forms.Label();
            this.TowerLabel = new System.Windows.Forms.Label();
            this.NormalTowerButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDifficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentTimeLabel = new System.Windows.Forms.Label();
            this.CurrentGoldLabel = new System.Windows.Forms.Label();
            this.MineLabel = new System.Windows.Forms.Label();
            this.MineButton = new System.Windows.Forms.Button();
            this.TurretButton = new System.Windows.Forms.Button();
            this.BomberButton = new System.Windows.Forms.Button();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.HPLabel = new System.Windows.Forms.Label();
            this.LevelLabel = new System.Windows.Forms.Label();
            this.UpgradeButton = new System.Windows.Forms.Button();
            this.DestroyButton = new System.Windows.Forms.Button();
            this.BaseLifeButton = new System.Windows.Forms.Button();
            this.BaseTurretButton = new System.Windows.Forms.Button();
            this.CountdownLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.TimeLabel.Location = new System.Drawing.Point(12, 540);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(119, 29);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "Time left: ";
            // 
            // GoldLabel
            // 
            this.GoldLabel.AutoSize = true;
            this.GoldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.GoldLabel.Location = new System.Drawing.Point(331, 540);
            this.GoldLabel.Name = "GoldLabel";
            this.GoldLabel.Size = new System.Drawing.Size(77, 29);
            this.GoldLabel.TabIndex = 1;
            this.GoldLabel.Text = "Gold: ";
            // 
            // TowerLabel
            // 
            this.TowerLabel.AutoSize = true;
            this.TowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.TowerLabel.Location = new System.Drawing.Point(606, 55);
            this.TowerLabel.Name = "TowerLabel";
            this.TowerLabel.Size = new System.Drawing.Size(157, 37);
            this.TowerLabel.TabIndex = 2;
            this.TowerLabel.Text = "TOWERS";
            // 
            // NormalTowerButton
            //
            this.NormalTowerButton.BackgroundImage = _normaltowerImage;
            this.NormalTowerButton.Location = new System.Drawing.Point(613, 115);
            this.NormalTowerButton.Name = "NormalTowerButton";
            this.NormalTowerButton.Size = new System.Drawing.Size(45, 45);
            this.NormalTowerButton.TabIndex = 3;
            this.NormalTowerButton.UseVisualStyleBackColor = false;
            this.NormalTowerButton.Click += new System.EventHandler(this.NormalTowerButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.setDifficultyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(963, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitGameToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // exitGameToolStripMenuItem
            // 
            this.exitGameToolStripMenuItem.Name = "exitGameToolStripMenuItem";
            this.exitGameToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitGameToolStripMenuItem.Text = "Exit Game";
            this.exitGameToolStripMenuItem.Click += new System.EventHandler(this.exitGameToolStripMenuItem_Click);
            // 
            // setDifficultyToolStripMenuItem
            // 
            this.setDifficultyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.hardToolStripMenuItem});
            this.setDifficultyToolStripMenuItem.Name = "setDifficultyToolStripMenuItem";
            this.setDifficultyToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.setDifficultyToolStripMenuItem.Text = "Set Difficulty";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.easyToolStripMenuItem.Text = "Easy";
            this.easyToolStripMenuItem.Click += new System.EventHandler(this.easyToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            this.hardToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.hardToolStripMenuItem.Text = "Hard";
            this.hardToolStripMenuItem.Click += new System.EventHandler(this.hardToolStripMenuItem_Click);
            // 
            // CurrentTimeLabel
            // 
            this.CurrentTimeLabel.AutoSize = true;
            this.CurrentTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.CurrentTimeLabel.Location = new System.Drawing.Point(177, 540);
            this.CurrentTimeLabel.Name = "label4";
            this.CurrentTimeLabel.Size = new System.Drawing.Size(0, 29);
            this.CurrentTimeLabel.TabIndex = 5;
            // 
            // CurrentGoldLabel
            // 
            this.CurrentGoldLabel.AutoSize = true;
            this.CurrentGoldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.CurrentGoldLabel.Location = new System.Drawing.Point(430, 540);
            this.CurrentGoldLabel.Name = "label5";
            this.CurrentGoldLabel.Size = new System.Drawing.Size(0, 29);
            this.CurrentGoldLabel.TabIndex = 6;
            // 
            // MineLabel
            // 
            this.MineLabel.AutoSize = true;
            this.MineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.MineLabel.Location = new System.Drawing.Point(606, 180);
            this.MineLabel.Name = "MineLabel";
            this.MineLabel.Size = new System.Drawing.Size(117, 37);
            this.MineLabel.TabIndex = 7;
            this.MineLabel.Text = "MINES";
            // 
            // MineButton
            // 
            this.MineButton.BackgroundImage = _mineImage;
            this.MineButton.Location = new System.Drawing.Point(613, 234);
            this.MineButton.Name = "MineButton";
            this.MineButton.Size = new System.Drawing.Size(45, 45);
            this.MineButton.TabIndex = 8;
            this.MineButton.UseVisualStyleBackColor = false;
            this.MineButton.Click += new System.EventHandler(this.MineButton_Click);
            // 
            // TurretButton
            // 
            this.TurretButton.BackgroundImage = _turretImage;
            this.TurretButton.Location = new System.Drawing.Point(678, 115);
            this.TurretButton.Name = "TurretButton";
            this.TurretButton.Size = new System.Drawing.Size(45, 45);
            this.TurretButton.TabIndex = 9;
            this.TurretButton.UseVisualStyleBackColor = false;
            this.TurretButton.Click += new System.EventHandler(this.TurretButton_Click);
            // 
            // BomberButton
            // 
            this.BomberButton.BackgroundImage = _bomberImage;
            this.BomberButton.Location = new System.Drawing.Point(746, 115);
            this.BomberButton.Name = "BomberButton";
            this.BomberButton.Size = new System.Drawing.Size(45, 45);
            this.BomberButton.TabIndex = 10;
            this.BomberButton.UseVisualStyleBackColor = false;
            this.BomberButton.Click += new System.EventHandler(this.BomberButton_Click);
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.TypeLabel.Location = new System.Drawing.Point(603, 310);
            this.TypeLabel.Name = "label7";
            this.TypeLabel.Size = new System.Drawing.Size(0, 31);
            this.TypeLabel.TabIndex = 11;
            // 
            // HPLabel
            // 
            this.HPLabel.AutoSize = true;
            this.HPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.HPLabel.Location = new System.Drawing.Point(605, 380);
            this.HPLabel.Name = "label8";
            this.HPLabel.Size = new System.Drawing.Size(0, 31);
            this.HPLabel.TabIndex = 12;
            // 
            // LevelLabel
            // 
            this.LevelLabel.AutoSize = true;
            this.LevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.LevelLabel.Location = new System.Drawing.Point(605, 450);
            this.LevelLabel.Name = "label9";
            this.LevelLabel.Size = new System.Drawing.Size(0, 31);
            this.LevelLabel.TabIndex = 13;
            // 
            // UpgradeButton
            // 
            this.UpgradeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.UpgradeButton.Location = new System.Drawing.Point(597, 522);
            this.UpgradeButton.Name = "UpgradeButton";
            this.UpgradeButton.Size = new System.Drawing.Size(135, 67);
            this.UpgradeButton.TabIndex = 14;
            this.UpgradeButton.Text = "UPGRADE";
            this.UpgradeButton.UseVisualStyleBackColor = true;
            this.UpgradeButton.Visible = false;
            this.UpgradeButton.Click += new System.EventHandler(this.UpgradeButton_Click);
            // 
            // DestroyButton
            // 
            this.DestroyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.DestroyButton.Location = new System.Drawing.Point(773, 522);
            this.DestroyButton.Name = "DestroyButton";
            this.DestroyButton.Size = new System.Drawing.Size(136, 67);
            this.DestroyButton.TabIndex = 15;
            this.DestroyButton.Text = "DESTROY";
            this.DestroyButton.UseVisualStyleBackColor = true;
            this.DestroyButton.Visible = false;
            this.DestroyButton.Click += new System.EventHandler(this.DestroyButton_Click);
            // 
            // BaseLifeButton
            // 
            this.BaseLifeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BaseLifeButton.Location = new System.Drawing.Point(797, 522);
            this.BaseLifeButton.Name = "BaseLifeButton";
            this.BaseLifeButton.Size = new System.Drawing.Size(87, 35);
            this.BaseLifeButton.TabIndex = 16;
            this.BaseLifeButton.Text = "LIFE";
            this.BaseLifeButton.UseVisualStyleBackColor = true;
            this.BaseLifeButton.Visible = false;
            this.BaseLifeButton.Click += new System.EventHandler(this.BaseLifeButton_Click);
            // 
            // BaseTurretButton
            // 
            this.BaseTurretButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BaseTurretButton.Location = new System.Drawing.Point(797, 554);
            this.BaseTurretButton.Name = "BaseTurretButton";
            this.BaseTurretButton.Size = new System.Drawing.Size(87, 35);
            this.BaseTurretButton.TabIndex = 17;
            this.BaseTurretButton.Text = "TURRET";
            this.BaseTurretButton.UseVisualStyleBackColor = true;
            this.BaseTurretButton.Visible = false;
            this.BaseTurretButton.Click += new System.EventHandler(this.BaseTurretButton_Click);
            // 
            // CountdownLabel
            // 
            this.CountdownLabel.AutoSize = true;
            this.CountdownLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.CountdownLabel.Location = new System.Drawing.Point(17, 607);
            this.CountdownLabel.Name = "label10";
            this.CountdownLabel.Size = new System.Drawing.Size(0, 31);
            this.CountdownLabel.TabIndex = 18;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 675);
            this.Controls.Add(this.CountdownLabel);
            this.Controls.Add(this.BaseTurretButton);
            this.Controls.Add(this.BaseLifeButton);
            this.Controls.Add(this.DestroyButton);
            this.Controls.Add(this.UpgradeButton);
            this.Controls.Add(this.LevelLabel);
            this.Controls.Add(this.HPLabel);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.BomberButton);
            this.Controls.Add(this.TurretButton);
            this.Controls.Add(this.MineButton);
            this.Controls.Add(this.MineLabel);
            this.Controls.Add(this.CurrentGoldLabel);
            this.Controls.Add(this.CurrentTimeLabel);
            this.Controls.Add(this.NormalTowerButton);
            this.Controls.Add(this.TowerLabel);
            this.Controls.Add(this.GoldLabel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label GoldLabel;
        private System.Windows.Forms.Label TowerLabel;
        private System.Windows.Forms.Button NormalTowerButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDifficultyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem;
        private System.Windows.Forms.Label CurrentTimeLabel;
        private System.Windows.Forms.Label CurrentGoldLabel;
        private System.Windows.Forms.Label MineLabel;
        private System.Windows.Forms.Button MineButton;
        private System.Windows.Forms.Button TurretButton;
        private System.Windows.Forms.Button BomberButton;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Label HPLabel;
        private System.Windows.Forms.Label LevelLabel;
        private System.Windows.Forms.Button UpgradeButton;
        private System.Windows.Forms.Button DestroyButton;
        private System.Windows.Forms.Button BaseLifeButton;
        private System.Windows.Forms.Button BaseTurretButton;
        private System.Windows.Forms.Label CountdownLabel;
    }
}

