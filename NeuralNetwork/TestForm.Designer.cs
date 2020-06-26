namespace NeuralNetwork
{
  partial class TestForm
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
      this.components = new System.ComponentModel.Container();
      this.DrawPictureBox = new System.Windows.Forms.PictureBox();
      this.ClearButton = new System.Windows.Forms.Button();
      this.SmallBox = new System.Windows.Forms.PictureBox();
      this.RandomDigit = new System.Windows.Forms.Button();
      this.DigitBox = new System.Windows.Forms.TextBox();
      this.AnswerBox = new System.Windows.Forms.TextBox();
      this.TrainButton = new System.Windows.Forms.Button();
      this.TestButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.TestDataFolderBox = new System.Windows.Forms.TextBox();
      this.BrowseButton = new System.Windows.Forms.Button();
      this.CostLabel = new System.Windows.Forms.Label();
      this.ResultLabel = new System.Windows.Forms.Label();
      this.SaveButton = new System.Windows.Forms.Button();
      this.LoadButton = new System.Windows.Forms.Button();
      this.MainBox = new System.Windows.Forms.GroupBox();
      this.TrainReviewerBox = new System.Windows.Forms.CheckBox();
      this.TrainParticipantsBox = new System.Windows.Forms.CheckBox();
      this.StopFix = new System.Windows.Forms.Timer(this.components);
      this.DemocracyButton = new System.Windows.Forms.RadioButton();
      this.Network1Button = new System.Windows.Forms.RadioButton();
      this.Network2Button = new System.Windows.Forms.RadioButton();
      this.Network3Button = new System.Windows.Forms.RadioButton();
      this.Network4Button = new System.Windows.Forms.RadioButton();
      ((System.ComponentModel.ISupportInitialize)(this.DrawPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SmallBox)).BeginInit();
      this.MainBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // DrawPictureBox
      // 
      this.DrawPictureBox.Location = new System.Drawing.Point(16, 35);
      this.DrawPictureBox.Name = "DrawPictureBox";
      this.DrawPictureBox.Size = new System.Drawing.Size(256, 256);
      this.DrawPictureBox.TabIndex = 0;
      this.DrawPictureBox.TabStop = false;
      this.DrawPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPictureBox_MouseDown);
      this.DrawPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPictureBox_MouseMove);
      this.DrawPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPictureBox_MouseUp);
      // 
      // ClearButton
      // 
      this.ClearButton.Location = new System.Drawing.Point(279, 36);
      this.ClearButton.Name = "ClearButton";
      this.ClearButton.Size = new System.Drawing.Size(75, 23);
      this.ClearButton.TabIndex = 1;
      this.ClearButton.Text = "Clear";
      this.ClearButton.UseVisualStyleBackColor = true;
      this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
      // 
      // SmallBox
      // 
      this.SmallBox.Location = new System.Drawing.Point(279, 94);
      this.SmallBox.Name = "SmallBox";
      this.SmallBox.Size = new System.Drawing.Size(28, 28);
      this.SmallBox.TabIndex = 2;
      this.SmallBox.TabStop = false;
      // 
      // RandomDigit
      // 
      this.RandomDigit.Location = new System.Drawing.Point(278, 65);
      this.RandomDigit.Name = "RandomDigit";
      this.RandomDigit.Size = new System.Drawing.Size(75, 23);
      this.RandomDigit.TabIndex = 3;
      this.RandomDigit.Text = "Random";
      this.RandomDigit.UseVisualStyleBackColor = true;
      this.RandomDigit.Click += new System.EventHandler(this.RandomDigit_Click);
      // 
      // DigitBox
      // 
      this.DigitBox.Location = new System.Drawing.Point(315, 104);
      this.DigitBox.Name = "DigitBox";
      this.DigitBox.Size = new System.Drawing.Size(38, 20);
      this.DigitBox.TabIndex = 4;
      // 
      // AnswerBox
      // 
      this.AnswerBox.Location = new System.Drawing.Point(360, 35);
      this.AnswerBox.Multiline = true;
      this.AnswerBox.Name = "AnswerBox";
      this.AnswerBox.Size = new System.Drawing.Size(100, 157);
      this.AnswerBox.TabIndex = 6;
      // 
      // TrainButton
      // 
      this.TrainButton.Location = new System.Drawing.Point(290, 319);
      this.TrainButton.Name = "TrainButton";
      this.TrainButton.Size = new System.Drawing.Size(75, 23);
      this.TrainButton.TabIndex = 7;
      this.TrainButton.Text = "Train";
      this.TrainButton.UseVisualStyleBackColor = true;
      this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
      // 
      // TestButton
      // 
      this.TestButton.Location = new System.Drawing.Point(280, 226);
      this.TestButton.Name = "TestButton";
      this.TestButton.Size = new System.Drawing.Size(75, 23);
      this.TestButton.TabIndex = 8;
      this.TestButton.Text = "Test";
      this.TestButton.UseVisualStyleBackColor = true;
      this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(89, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Test Data Folder:";
      // 
      // TestDataFolderBox
      // 
      this.TestDataFolderBox.Location = new System.Drawing.Point(111, 13);
      this.TestDataFolderBox.Name = "TestDataFolderBox";
      this.TestDataFolderBox.Size = new System.Drawing.Size(349, 20);
      this.TestDataFolderBox.TabIndex = 10;
      // 
      // BrowseButton
      // 
      this.BrowseButton.Location = new System.Drawing.Point(466, 11);
      this.BrowseButton.Name = "BrowseButton";
      this.BrowseButton.Size = new System.Drawing.Size(75, 23);
      this.BrowseButton.TabIndex = 11;
      this.BrowseButton.Text = "Browse";
      this.BrowseButton.UseVisualStyleBackColor = true;
      this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
      // 
      // CostLabel
      // 
      this.CostLabel.AutoSize = true;
      this.CostLabel.Location = new System.Drawing.Point(278, 252);
      this.CostLabel.Name = "CostLabel";
      this.CostLabel.Size = new System.Drawing.Size(30, 13);
      this.CostLabel.TabIndex = 12;
      this.CostLabel.Text = "cost:";
      // 
      // ResultLabel
      // 
      this.ResultLabel.AutoSize = true;
      this.ResultLabel.Location = new System.Drawing.Point(277, 135);
      this.ResultLabel.Name = "ResultLabel";
      this.ResultLabel.Size = new System.Drawing.Size(37, 13);
      this.ResultLabel.TabIndex = 15;
      this.ResultLabel.Text = "Result";
      // 
      // SaveButton
      // 
      this.SaveButton.Location = new System.Drawing.Point(466, 36);
      this.SaveButton.Name = "SaveButton";
      this.SaveButton.Size = new System.Drawing.Size(75, 23);
      this.SaveButton.TabIndex = 16;
      this.SaveButton.Text = "Save";
      this.SaveButton.UseVisualStyleBackColor = true;
      this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
      // 
      // LoadButton
      // 
      this.LoadButton.Location = new System.Drawing.Point(466, 65);
      this.LoadButton.Name = "LoadButton";
      this.LoadButton.Size = new System.Drawing.Size(75, 23);
      this.LoadButton.TabIndex = 17;
      this.LoadButton.Text = "Load";
      this.LoadButton.UseVisualStyleBackColor = true;
      this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
      // 
      // MainBox
      // 
      this.MainBox.Controls.Add(this.Network4Button);
      this.MainBox.Controls.Add(this.Network3Button);
      this.MainBox.Controls.Add(this.Network2Button);
      this.MainBox.Controls.Add(this.Network1Button);
      this.MainBox.Controls.Add(this.DemocracyButton);
      this.MainBox.Controls.Add(this.TrainReviewerBox);
      this.MainBox.Controls.Add(this.TrainParticipantsBox);
      this.MainBox.Controls.Add(this.label1);
      this.MainBox.Controls.Add(this.LoadButton);
      this.MainBox.Controls.Add(this.DrawPictureBox);
      this.MainBox.Controls.Add(this.SaveButton);
      this.MainBox.Controls.Add(this.ClearButton);
      this.MainBox.Controls.Add(this.ResultLabel);
      this.MainBox.Controls.Add(this.SmallBox);
      this.MainBox.Controls.Add(this.RandomDigit);
      this.MainBox.Controls.Add(this.DigitBox);
      this.MainBox.Controls.Add(this.CostLabel);
      this.MainBox.Controls.Add(this.BrowseButton);
      this.MainBox.Controls.Add(this.AnswerBox);
      this.MainBox.Controls.Add(this.TestDataFolderBox);
      this.MainBox.Controls.Add(this.TestButton);
      this.MainBox.Location = new System.Drawing.Point(12, 12);
      this.MainBox.Name = "MainBox";
      this.MainBox.Size = new System.Drawing.Size(558, 301);
      this.MainBox.TabIndex = 18;
      this.MainBox.TabStop = false;
      this.MainBox.Text = "Main";
      // 
      // TrainReviewerBox
      // 
      this.TrainReviewerBox.AutoSize = true;
      this.TrainReviewerBox.Checked = true;
      this.TrainReviewerBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.TrainReviewerBox.Location = new System.Drawing.Point(400, 251);
      this.TrainReviewerBox.Name = "TrainReviewerBox";
      this.TrainReviewerBox.Size = new System.Drawing.Size(98, 17);
      this.TrainReviewerBox.TabIndex = 19;
      this.TrainReviewerBox.Text = "Train Reviewer";
      this.TrainReviewerBox.UseVisualStyleBackColor = true;
      this.TrainReviewerBox.CheckedChanged += new System.EventHandler(this.TrainReviewerBox_CheckedChanged);
      // 
      // TrainParticipantsBox
      // 
      this.TrainParticipantsBox.AutoSize = true;
      this.TrainParticipantsBox.Checked = true;
      this.TrainParticipantsBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.TrainParticipantsBox.Location = new System.Drawing.Point(400, 226);
      this.TrainParticipantsBox.Name = "TrainParticipantsBox";
      this.TrainParticipantsBox.Size = new System.Drawing.Size(108, 17);
      this.TrainParticipantsBox.TabIndex = 18;
      this.TrainParticipantsBox.Text = "Train Participants";
      this.TrainParticipantsBox.UseVisualStyleBackColor = true;
      this.TrainParticipantsBox.CheckedChanged += new System.EventHandler(this.TrainParticipantsBox_CheckedChanged);
      // 
      // StopFix
      // 
      this.StopFix.Enabled = true;
      this.StopFix.Tick += new System.EventHandler(this.StopFix_Tick);
      // 
      // DemocracyButton
      // 
      this.DemocracyButton.AutoSize = true;
      this.DemocracyButton.Checked = true;
      this.DemocracyButton.Location = new System.Drawing.Point(467, 106);
      this.DemocracyButton.Name = "DemocracyButton";
      this.DemocracyButton.Size = new System.Drawing.Size(79, 17);
      this.DemocracyButton.TabIndex = 20;
      this.DemocracyButton.TabStop = true;
      this.DemocracyButton.Text = "Democracy";
      this.DemocracyButton.UseVisualStyleBackColor = true;
      this.DemocracyButton.CheckedChanged += new System.EventHandler(this.DemocracyButton_CheckedChanged);
      // 
      // Network1Button
      // 
      this.Network1Button.AutoSize = true;
      this.Network1Button.Location = new System.Drawing.Point(466, 129);
      this.Network1Button.Name = "Network1Button";
      this.Network1Button.Size = new System.Drawing.Size(71, 17);
      this.Network1Button.TabIndex = 21;
      this.Network1Button.Text = "Network1";
      this.Network1Button.UseVisualStyleBackColor = true;
      this.Network1Button.CheckedChanged += new System.EventHandler(this.Network1Button_CheckedChanged);
      // 
      // Network2Button
      // 
      this.Network2Button.AutoSize = true;
      this.Network2Button.Location = new System.Drawing.Point(466, 152);
      this.Network2Button.Name = "Network2Button";
      this.Network2Button.Size = new System.Drawing.Size(71, 17);
      this.Network2Button.TabIndex = 22;
      this.Network2Button.Text = "Network2";
      this.Network2Button.UseVisualStyleBackColor = true;
      this.Network2Button.CheckedChanged += new System.EventHandler(this.Network2Button_CheckedChanged);
      // 
      // Network3Button
      // 
      this.Network3Button.AutoSize = true;
      this.Network3Button.Location = new System.Drawing.Point(466, 175);
      this.Network3Button.Name = "Network3Button";
      this.Network3Button.Size = new System.Drawing.Size(71, 17);
      this.Network3Button.TabIndex = 23;
      this.Network3Button.Text = "Network3";
      this.Network3Button.UseVisualStyleBackColor = true;
      this.Network3Button.CheckedChanged += new System.EventHandler(this.Network3Button_CheckedChanged);
      // 
      // Network4Button
      // 
      this.Network4Button.AutoSize = true;
      this.Network4Button.Location = new System.Drawing.Point(466, 198);
      this.Network4Button.Name = "Network4Button";
      this.Network4Button.Size = new System.Drawing.Size(71, 17);
      this.Network4Button.TabIndex = 24;
      this.Network4Button.Text = "Network4";
      this.Network4Button.UseVisualStyleBackColor = true;
      this.Network4Button.CheckedChanged += new System.EventHandler(this.Network4Button_CheckedChanged);
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 347);
      this.Controls.Add(this.MainBox);
      this.Controls.Add(this.TrainButton);
      this.Name = "TestForm";
      this.Text = "TestForm";
      ((System.ComponentModel.ISupportInitialize)(this.DrawPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SmallBox)).EndInit();
      this.MainBox.ResumeLayout(false);
      this.MainBox.PerformLayout();
      this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.PictureBox DrawPictureBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.PictureBox SmallBox;
        private System.Windows.Forms.Button RandomDigit;
        private System.Windows.Forms.TextBox DigitBox;
        private System.Windows.Forms.TextBox AnswerBox;
        private System.Windows.Forms.Button TrainButton;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TestDataFolderBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.GroupBox MainBox;
        private System.Windows.Forms.Timer StopFix;
        private System.Windows.Forms.CheckBox TrainReviewerBox;
        private System.Windows.Forms.CheckBox TrainParticipantsBox;
        private System.Windows.Forms.RadioButton Network4Button;
        private System.Windows.Forms.RadioButton Network3Button;
        private System.Windows.Forms.RadioButton Network2Button;
        private System.Windows.Forms.RadioButton Network1Button;
        private System.Windows.Forms.RadioButton DemocracyButton;
    }
}

