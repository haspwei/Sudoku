namespace Sudoku
{
    partial class SudokuGame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _isFormClosing = true;
                _gameStateManager?.StopAllProcesses();
                _gameStateManager?.UnsubscribeAllEvents();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuGame));
            panelSudoku = new Panel();
            panelControls = new Panel();
            label1 = new Label();
            HintButton = new Button();
            NotesButton = new Button();
            btnNormalMode = new Button();
            btnStepMode = new Button();
            btnAutoMode = new Button();
            CheckSolutionButton = new Button();
            SaveButton = new Button();
            ReturnButton = new Button();
            lblSpeedControl = new Label();
            trackBarSpeed = new TrackBar();
            panelNumbers = new Panel();
            Button1 = new Button();
            Button2 = new Button();
            Button3 = new Button();
            Button4 = new Button();
            Button5 = new Button();
            Button6 = new Button();
            Button7 = new Button();
            Button8 = new Button();
            Button9 = new Button();
            ClearButton = new Button();
            panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarSpeed).BeginInit();
            panelNumbers.SuspendLayout();
            SuspendLayout();
            // 
            // panelSudoku
            // 
            panelSudoku.BackColor = Color.MistyRose;
            panelSudoku.BorderStyle = BorderStyle.FixedSingle;
            panelSudoku.Location = new Point(80, 40);
            panelSudoku.Name = "panelSudoku";
            panelSudoku.Size = new Size(800, 800);
            panelSudoku.TabIndex = 0;
            // 
            // panelControls
            // 
            panelControls.BackColor = Color.LavenderBlush;
            panelControls.BorderStyle = BorderStyle.FixedSingle;
            panelControls.Controls.Add(label1);
            panelControls.Controls.Add(HintButton);
            panelControls.Controls.Add(NotesButton);
            panelControls.Controls.Add(btnNormalMode);
            panelControls.Controls.Add(btnStepMode);
            panelControls.Controls.Add(btnAutoMode);
            panelControls.Controls.Add(CheckSolutionButton);
            panelControls.Controls.Add(SaveButton);
            panelControls.Controls.Add(ReturnButton);
            panelControls.Controls.Add(lblSpeedControl);
            panelControls.Controls.Add(trackBarSpeed);
            panelControls.Location = new Point(900, 40);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(350, 800);
            panelControls.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(30, 16);
            label1.Name = "label1";
            label1.Size = new Size(126, 25);
            label1.TabIndex = 7;
            label1.Text = "Режими";
            // 
            // HintButton
            // 
            HintButton.BackColor = Color.LightSteelBlue;
            HintButton.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HintButton.Location = new Point(30, 455);
            HintButton.Name = "HintButton";
            HintButton.Size = new Size(290, 70);
            HintButton.TabIndex = 9;
            HintButton.Text = "Підказка";
            HintButton.UseVisualStyleBackColor = false;
            // 
            // NotesButton
            // 
            NotesButton.BackColor = Color.LightSteelBlue;
            NotesButton.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            NotesButton.Location = new Point(30, 370);
            NotesButton.Name = "NotesButton";
            NotesButton.Size = new Size(290, 70);
            NotesButton.TabIndex = 8;
            NotesButton.Text = "Здогадки";
            NotesButton.UseVisualStyleBackColor = false;
            // 
            // btnNormalMode
            // 
            btnNormalMode.BackColor = Color.LightSteelBlue;
            btnNormalMode.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnNormalMode.Location = new Point(30, 56);
            btnNormalMode.Name = "btnNormalMode";
            btnNormalMode.Size = new Size(290, 50);
            btnNormalMode.TabIndex = 0;
            btnNormalMode.Text = "Звичайний";
            btnNormalMode.UseVisualStyleBackColor = false;
            // 
            // btnStepMode
            // 
            btnStepMode.BackColor = Color.LightSteelBlue;
            btnStepMode.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnStepMode.Location = new Point(30, 122);
            btnStepMode.Name = "btnStepMode";
            btnStepMode.Size = new Size(290, 50);
            btnStepMode.TabIndex = 1;
            btnStepMode.Text = "Покроковий";
            btnStepMode.UseVisualStyleBackColor = false;
            // 
            // btnAutoMode
            // 
            btnAutoMode.BackColor = Color.LightSteelBlue;
            btnAutoMode.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnAutoMode.Location = new Point(30, 187);
            btnAutoMode.Name = "btnAutoMode";
            btnAutoMode.Size = new Size(290, 50);
            btnAutoMode.TabIndex = 2;
            btnAutoMode.Text = "Автоматичний";
            btnAutoMode.UseVisualStyleBackColor = false;
            // 
            // CheckSolutionButton
            // 
            CheckSolutionButton.BackColor = Color.LightSteelBlue;
            CheckSolutionButton.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CheckSolutionButton.Location = new Point(30, 540);
            CheckSolutionButton.Name = "CheckSolutionButton";
            CheckSolutionButton.Size = new Size(290, 70);
            CheckSolutionButton.TabIndex = 3;
            CheckSolutionButton.Text = "Перевірити розв'язок";
            CheckSolutionButton.UseVisualStyleBackColor = false;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = Color.LightSteelBlue;
            SaveButton.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            SaveButton.Location = new Point(30, 625);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(290, 70);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Зберегти гру";
            SaveButton.UseVisualStyleBackColor = false;
            // 
            // ReturnButton
            // 
            ReturnButton.BackColor = Color.LightSteelBlue;
            ReturnButton.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ReturnButton.Location = new Point(30, 710);
            ReturnButton.Name = "ReturnButton";
            ReturnButton.Size = new Size(290, 70);
            ReturnButton.TabIndex = 5;
            ReturnButton.Text = "Головне Меню";
            ReturnButton.UseVisualStyleBackColor = false;
            // 
            // lblSpeedControl
            // 
            lblSpeedControl.AutoSize = true;
            lblSpeedControl.Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblSpeedControl.Location = new Point(30, 270);
            lblSpeedControl.Name = "lblSpeedControl";
            lblSpeedControl.Size = new Size(162, 25);
            lblSpeedControl.TabIndex = 6;
            lblSpeedControl.Text = "Animation Speed";
            // 
            // trackBarSpeed
            // 
            trackBarSpeed.BackColor = Color.LavenderBlush;
            trackBarSpeed.Enabled = false;
            trackBarSpeed.Location = new Point(30, 309);
            trackBarSpeed.Minimum = 1;
            trackBarSpeed.Name = "trackBarSpeed";
            trackBarSpeed.Size = new Size(290, 45);
            trackBarSpeed.TabIndex = 7;
            trackBarSpeed.Value = 5;
            // 
            // panelNumbers
            // 
            panelNumbers.BackColor = Color.LavenderBlush;
            panelNumbers.BorderStyle = BorderStyle.FixedSingle;
            panelNumbers.Controls.Add(Button1);
            panelNumbers.Controls.Add(Button2);
            panelNumbers.Controls.Add(Button3);
            panelNumbers.Controls.Add(Button4);
            panelNumbers.Controls.Add(Button5);
            panelNumbers.Controls.Add(Button6);
            panelNumbers.Controls.Add(Button7);
            panelNumbers.Controls.Add(Button8);
            panelNumbers.Controls.Add(Button9);
            panelNumbers.Controls.Add(ClearButton);
            panelNumbers.Location = new Point(80, 850);
            panelNumbers.Name = "panelNumbers";
            panelNumbers.Size = new Size(1170, 100);
            panelNumbers.TabIndex = 2;
            // 
            // Button1
            // 
            Button1.BackColor = Color.LightSteelBlue;
            Button1.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button1.Location = new Point(30, 20);
            Button1.Name = "Button1";
            Button1.Size = new Size(80, 60);
            Button1.TabIndex = 0;
            Button1.Text = "1";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.BackColor = Color.LightSteelBlue;
            Button2.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button2.Location = new Point(130, 20);
            Button2.Name = "Button2";
            Button2.Size = new Size(80, 60);
            Button2.TabIndex = 1;
            Button2.Text = "2";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.BackColor = Color.LightSteelBlue;
            Button3.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button3.Location = new Point(230, 20);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 60);
            Button3.TabIndex = 2;
            Button3.Text = "3";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.BackColor = Color.LightSteelBlue;
            Button4.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button4.Location = new Point(330, 20);
            Button4.Name = "Button4";
            Button4.Size = new Size(80, 60);
            Button4.TabIndex = 3;
            Button4.Text = "4";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.BackColor = Color.LightSteelBlue;
            Button5.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button5.Location = new Point(430, 20);
            Button5.Name = "Button5";
            Button5.Size = new Size(80, 60);
            Button5.TabIndex = 4;
            Button5.Text = "5";
            Button5.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            Button6.BackColor = Color.LightSteelBlue;
            Button6.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button6.Location = new Point(530, 20);
            Button6.Name = "Button6";
            Button6.Size = new Size(80, 60);
            Button6.TabIndex = 5;
            Button6.Text = "6";
            Button6.UseVisualStyleBackColor = false;
            // 
            // Button7
            // 
            Button7.BackColor = Color.LightSteelBlue;
            Button7.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button7.Location = new Point(630, 20);
            Button7.Name = "Button7";
            Button7.Size = new Size(80, 60);
            Button7.TabIndex = 6;
            Button7.Text = "7";
            Button7.UseVisualStyleBackColor = false;
            // 
            // Button8
            // 
            Button8.BackColor = Color.LightSteelBlue;
            Button8.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button8.Location = new Point(730, 20);
            Button8.Name = "Button8";
            Button8.Size = new Size(80, 60);
            Button8.TabIndex = 7;
            Button8.Text = "8";
            Button8.UseVisualStyleBackColor = false;
            // 
            // Button9
            // 
            Button9.BackColor = Color.LightSteelBlue;
            Button9.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Button9.Location = new Point(830, 20);
            Button9.Name = "Button9";
            Button9.Size = new Size(80, 60);
            Button9.TabIndex = 8;
            Button9.Text = "9";
            Button9.UseVisualStyleBackColor = false;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.LightSteelBlue;
            ClearButton.Font = new Font("Yu Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ClearButton.Location = new Point(930, 20);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(200, 60);
            ClearButton.TabIndex = 9;
            ClearButton.Text = "Очистити";
            ClearButton.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(1288, 987);
            Controls.Add(panelNumbers);
            Controls.Add(panelControls);
            Controls.Add(panelSudoku);
            Font = new Font("Yu Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sudoku Game";
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarSpeed).EndInit();
            panelNumbers.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSudoku;
        private Panel panelControls;
        private Panel panelNumbers;
        private Button Button1;
        private Button Button2;
        private Button Button3;
        private Button Button4;
        private Button Button5;
        private Button Button6;
        private Button Button7;
        private Button Button8;
        private Button Button9;
        private Button ClearButton;
        private Button CheckSolutionButton;
        private Button SaveButton;
        private Button ReturnButton;
        private Label lblSpeedControl;
        private TrackBar trackBarSpeed;
        private Button NotesButton;
        private Button btnNormalMode;
        private Button btnStepMode;
        private Button btnAutoMode;
        private Button HintButton;
        private Label label1;
    }
}