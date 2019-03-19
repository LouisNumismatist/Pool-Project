namespace MonoGamePool1
{
    partial class HighScores
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
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.BallsPottedLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.UsernameASC = new System.Windows.Forms.Button();
            this.BallsPottedASC = new System.Windows.Forms.Button();
            this.TimeASC = new System.Windows.Forms.Button();
            this.UsernameBox = new System.Windows.Forms.ListBox();
            this.BallsPottedBox = new System.Windows.Forms.ListBox();
            this.TimeBox = new System.Windows.Forms.ListBox();
            this.TimeDESC = new System.Windows.Forms.Button();
            this.BallsPottedDESC = new System.Windows.Forms.Button();
            this.UsernameDESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(10, 11);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "Username";
            this.UsernameLabel.Click += new System.EventHandler(this.Label1_Click);
            // 
            // BallsPottedLabel
            // 
            this.BallsPottedLabel.AutoSize = true;
            this.BallsPottedLabel.Location = new System.Drawing.Point(154, 11);
            this.BallsPottedLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BallsPottedLabel.Name = "BallsPottedLabel";
            this.BallsPottedLabel.Size = new System.Drawing.Size(63, 13);
            this.BallsPottedLabel.TabIndex = 1;
            this.BallsPottedLabel.Text = "Balls Potted";
            this.BallsPottedLabel.Click += new System.EventHandler(this.Label2_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(302, 11);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(64, 13);
            this.TimeLabel.TabIndex = 2;
            this.TimeLabel.Text = "Time Taken";
            // 
            // UsernameASC
            // 
            this.UsernameASC.Location = new System.Drawing.Point(69, 7);
            this.UsernameASC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UsernameASC.Name = "UsernameASC";
            this.UsernameASC.Size = new System.Drawing.Size(33, 20);
            this.UsernameASC.TabIndex = 3;
            this.UsernameASC.Text = "asc";
            this.UsernameASC.UseVisualStyleBackColor = true;
            this.UsernameASC.Click += new System.EventHandler(this.UsernameASC_Click);
            // 
            // BallsPottedASC
            // 
            this.BallsPottedASC.Location = new System.Drawing.Point(220, 7);
            this.BallsPottedASC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BallsPottedASC.Name = "BallsPottedASC";
            this.BallsPottedASC.Size = new System.Drawing.Size(33, 20);
            this.BallsPottedASC.TabIndex = 5;
            this.BallsPottedASC.Text = "asc";
            this.BallsPottedASC.UseVisualStyleBackColor = true;
            this.BallsPottedASC.Click += new System.EventHandler(this.BallsPottedASC_Click);
            // 
            // TimeASC
            // 
            this.TimeASC.Location = new System.Drawing.Point(368, 7);
            this.TimeASC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TimeASC.Name = "TimeASC";
            this.TimeASC.Size = new System.Drawing.Size(33, 20);
            this.TimeASC.TabIndex = 6;
            this.TimeASC.Text = "asc";
            this.TimeASC.UseVisualStyleBackColor = true;
            this.TimeASC.Click += new System.EventHandler(this.TimeASC_Click);
            // 
            // UsernameBox
            // 
            this.UsernameBox.FormattingEnabled = true;
            this.UsernameBox.Location = new System.Drawing.Point(12, 37);
            this.UsernameBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(134, 394);
            this.UsernameBox.TabIndex = 9;
            this.UsernameBox.SelectedIndexChanged += new System.EventHandler(this.UsernameBox_SelectedIndexChanged);
            // 
            // BallsPottedBox
            // 
            this.BallsPottedBox.FormattingEnabled = true;
            this.BallsPottedBox.Location = new System.Drawing.Point(156, 37);
            this.BallsPottedBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BallsPottedBox.Name = "BallsPottedBox";
            this.BallsPottedBox.Size = new System.Drawing.Size(134, 394);
            this.BallsPottedBox.TabIndex = 10;
            this.BallsPottedBox.SelectedIndexChanged += new System.EventHandler(this.BallsPottedBox_SelectedIndexChanged);
            // 
            // TimeBox
            // 
            this.TimeBox.FormattingEnabled = true;
            this.TimeBox.Location = new System.Drawing.Point(304, 37);
            this.TimeBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(134, 394);
            this.TimeBox.TabIndex = 11;
            this.TimeBox.SelectedIndexChanged += new System.EventHandler(this.TimeBox_SelectedIndexChanged);
            // 
            // TimeDESC
            // 
            this.TimeDESC.Location = new System.Drawing.Point(406, 7);
            this.TimeDESC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TimeDESC.Name = "TimeDESC";
            this.TimeDESC.Size = new System.Drawing.Size(40, 20);
            this.TimeDESC.TabIndex = 8;
            this.TimeDESC.Text = "desc";
            this.TimeDESC.UseVisualStyleBackColor = true;
            this.TimeDESC.Click += new System.EventHandler(this.TimeDESC_Click);
            // 
            // BallsPottedDESC
            // 
            this.BallsPottedDESC.Location = new System.Drawing.Point(258, 7);
            this.BallsPottedDESC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BallsPottedDESC.Name = "BallsPottedDESC";
            this.BallsPottedDESC.Size = new System.Drawing.Size(40, 20);
            this.BallsPottedDESC.TabIndex = 7;
            this.BallsPottedDESC.Text = "desc";
            this.BallsPottedDESC.UseVisualStyleBackColor = true;
            this.BallsPottedDESC.Click += new System.EventHandler(this.BallsPottedDESC_Click);
            // 
            // UsernameDESC
            // 
            this.UsernameDESC.Location = new System.Drawing.Point(106, 7);
            this.UsernameDESC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UsernameDESC.Name = "UsernameDESC";
            this.UsernameDESC.Size = new System.Drawing.Size(40, 20);
            this.UsernameDESC.TabIndex = 4;
            this.UsernameDESC.Text = "desc";
            this.UsernameDESC.UseVisualStyleBackColor = true;
            this.UsernameDESC.Click += new System.EventHandler(this.UsernameDESC_Click);
            // 
            // HighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 449);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.BallsPottedBox);
            this.Controls.Add(this.UsernameBox);
            this.Controls.Add(this.TimeDESC);
            this.Controls.Add(this.BallsPottedDESC);
            this.Controls.Add(this.TimeASC);
            this.Controls.Add(this.BallsPottedASC);
            this.Controls.Add(this.UsernameDESC);
            this.Controls.Add(this.UsernameASC);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.BallsPottedLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "HighScores";
            this.Text = "HighScores";
            this.Load += new System.EventHandler(this.HighScores_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label BallsPottedLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button UsernameASC;
        private System.Windows.Forms.Button BallsPottedASC;
        private System.Windows.Forms.Button TimeASC;
        private System.Windows.Forms.ListBox UsernameBox;
        private System.Windows.Forms.ListBox BallsPottedBox;
        private System.Windows.Forms.ListBox TimeBox;
        private System.Windows.Forms.Button TimeDESC;
        private System.Windows.Forms.Button BallsPottedDESC;
        private System.Windows.Forms.Button UsernameDESC;
    }
}