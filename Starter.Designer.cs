namespace Sudoku
{
    partial class Starter
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Starter));
            StartButton = new Button();
            LoadGame = new Button();
            QuitButton = new Button();
            pictureBox1 = new PictureBox();
            cmbDifficulty = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // StartButton
            // 
            StartButton.BackColor = Color.LightSteelBlue;
            StartButton.BackgroundImageLayout = ImageLayout.None;
            StartButton.Font = new Font("Yu Gothic", 14F);
            StartButton.ForeColor = SystemColors.ActiveCaptionText;
            StartButton.Location = new Point(332, 405);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(212, 70);
            StartButton.TabIndex = 0;
            StartButton.Text = "Нова гра";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // LoadGame
            // 
            LoadGame.AutoSize = true;
            LoadGame.BackColor = Color.LightSteelBlue;
            LoadGame.BackgroundImageLayout = ImageLayout.None;
            LoadGame.Font = new Font("Yu Gothic", 14F);
            LoadGame.ForeColor = SystemColors.ActiveCaptionText;
            LoadGame.Location = new Point(332, 511);
            LoadGame.Name = "LoadGame";
            LoadGame.Size = new Size(212, 70);
            LoadGame.TabIndex = 1;
            LoadGame.Text = "Продовжити";
            LoadGame.UseVisualStyleBackColor = false;
            LoadGame.Click += LoadGame_Click;
            // 
            // QuitButton
            // 
            QuitButton.AutoSize = true;
            QuitButton.BackColor = Color.LightSteelBlue;
            QuitButton.BackgroundImageLayout = ImageLayout.None;
            QuitButton.Font = new Font("Yu Gothic", 14F);
            QuitButton.ForeColor = SystemColors.ActiveCaptionText;
            QuitButton.Location = new Point(332, 587);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(212, 70);
            QuitButton.TabIndex = 3;
            QuitButton.Text = "Вийти";
            QuitButton.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.SUDOKU__3___1_;
            pictureBox1.Location = new Point(190, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(497, 362);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.BackColor = Color.Lavender;
            cmbDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDifficulty.ForeColor = SystemColors.ControlText;
            cmbDifficulty.FormattingEnabled = true;
            cmbDifficulty.Items.AddRange(new object[] { "Легка", "Середня", "Складна" });
            cmbDifficulty.SelectedIndex = 1;
            cmbDifficulty.Location = new Point(378, 481);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(122, 24);
            cmbDifficulty.TabIndex = 5;
            // 
            // Starter
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(872, 707);
            Controls.Add(cmbDifficulty);
            Controls.Add(pictureBox1);
            Controls.Add(QuitButton);
            Controls.Add(LoadGame);
            Controls.Add(StartButton);
            Font = new Font("Yu Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Starter";
            Text = "Starter";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartButton;
        private Button LoadGame;
        private Button QuitButton;
        private PictureBox pictureBox1;
        private ComboBox cmbDifficulty;
    }
}