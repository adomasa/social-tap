namespace social_tap
{
    partial class Form1
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
            this.reviewTitleLabel = new System.Windows.Forms.Label();
            this.barNameLabel = new System.Windows.Forms.Label();
            this.beverageAmountLabel = new System.Windows.Forms.Label();
            this.doYouRecommendLabel = new System.Windows.Forms.Label();
            this.commentLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.yesRadioButton = new System.Windows.Forms.RadioButton();
            this.noRadioButton = new System.Windows.Forms.RadioButton();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.barNameTextBox = new System.Windows.Forms.TextBox();
            this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.UploadPhotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pixelPercentageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // reviewTitleLabel
            // 
            this.reviewTitleLabel.AutoSize = true;
            this.reviewTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reviewTitleLabel.Location = new System.Drawing.Point(12, 43);
            this.reviewTitleLabel.Name = "reviewTitleLabel";
            this.reviewTitleLabel.Size = new System.Drawing.Size(493, 64);
            this.reviewTitleLabel.TabIndex = 0;
            this.reviewTitleLabel.Text = "Baro reitingavimas";
            // 
            // barNameLabel
            // 
            this.barNameLabel.AutoSize = true;
            this.barNameLabel.Location = new System.Drawing.Point(19, 125);
            this.barNameLabel.Name = "barNameLabel";
            this.barNameLabel.Size = new System.Drawing.Size(135, 20);
            this.barNameLabel.TabIndex = 1;
            this.barNameLabel.Text = "Baro pavadinimas";
            // 
            // beverageAmountLabel
            // 
            this.beverageAmountLabel.AutoSize = true;
            this.beverageAmountLabel.Location = new System.Drawing.Point(28, 207);
            this.beverageAmountLabel.Name = "beverageAmountLabel";
            this.beverageAmountLabel.Size = new System.Drawing.Size(83, 20);
            this.beverageAmountLabel.TabIndex = 2;
            this.beverageAmountLabel.Text = "Kiek įpylė?";
            // 
            // doYouRecommendLabel
            // 
            this.doYouRecommendLabel.AutoSize = true;
            this.doYouRecommendLabel.Location = new System.Drawing.Point(28, 480);
            this.doYouRecommendLabel.Name = "doYouRecommendLabel";
            this.doYouRecommendLabel.Size = new System.Drawing.Size(133, 20);
            this.doYouRecommendLabel.TabIndex = 3;
            this.doYouRecommendLabel.Text = "Ar rekomenduoji?";
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(28, 319);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(95, 20);
            this.commentLabel.TabIndex = 4;
            this.commentLabel.Text = "Komentaras";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(32, 619);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(247, 70);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Pateikti atsiliepimą";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.SubmitClicked);
            // 
            // yesRadioButton
            // 
            this.yesRadioButton.AutoSize = true;
            this.yesRadioButton.Location = new System.Drawing.Point(32, 522);
            this.yesRadioButton.Name = "yesRadioButton";
            this.yesRadioButton.Size = new System.Drawing.Size(64, 24);
            this.yesRadioButton.TabIndex = 6;
            this.yesRadioButton.TabStop = true;
            this.yesRadioButton.Text = "Taip";
            this.yesRadioButton.UseVisualStyleBackColor = true;
            // 
            // noRadioButton
            // 
            this.noRadioButton.AutoSize = true;
            this.noRadioButton.Location = new System.Drawing.Point(32, 552);
            this.noRadioButton.Name = "noRadioButton";
            this.noRadioButton.Size = new System.Drawing.Size(54, 24);
            this.noRadioButton.TabIndex = 7;
            this.noRadioButton.TabStop = true;
            this.noRadioButton.Text = "Ne";
            this.noRadioButton.UseVisualStyleBackColor = true;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(23, 247);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(191, 69);
            this.trackBar.TabIndex = 8;
            // 
            // barNameTextBox
            // 
            this.barNameTextBox.Location = new System.Drawing.Point(23, 161);
            this.barNameTextBox.Name = "barNameTextBox";
            this.barNameTextBox.Size = new System.Drawing.Size(191, 26);
            this.barNameTextBox.TabIndex = 9;
            // 
            // commentRichTextBox
            // 
            this.commentRichTextBox.Location = new System.Drawing.Point(32, 359);
            this.commentRichTextBox.Name = "commentRichTextBox";
            this.commentRichTextBox.Size = new System.Drawing.Size(261, 96);
            this.commentRichTextBox.TabIndex = 10;
            this.commentRichTextBox.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UploadPhotoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1320, 33);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // UploadPhotoToolStripMenuItem
            // 
            this.UploadPhotoToolStripMenuItem.Name = "UploadPhotoToolStripMenuItem";
            this.UploadPhotoToolStripMenuItem.Size = new System.Drawing.Size(146, 29);
            this.UploadPhotoToolStripMenuItem.Text = "Įkelti nuotrauką";
            this.UploadPhotoToolStripMenuItem.Click += new System.EventHandler(this.UploadPhotoToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(489, 146);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(743, 543);
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            // 
            // pixelPercentageLabel
            // 
            this.pixelPercentageLabel.AutoSize = true;
            this.pixelPercentageLabel.Location = new System.Drawing.Point(1167, 708);
            this.pixelPercentageLabel.Name = "pixelPercentageLabel";
            this.pixelPercentageLabel.Size = new System.Drawing.Size(65, 20);
            this.pixelPercentageLabel.TabIndex = 13;
            this.pixelPercentageLabel.Text = "Pikseliai";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 865);
            this.Controls.Add(this.pixelPercentageLabel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.commentRichTextBox);
            this.Controls.Add(this.barNameTextBox);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.noRadioButton);
            this.Controls.Add(this.yesRadioButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.commentLabel);
            this.Controls.Add(this.doYouRecommendLabel);
            this.Controls.Add(this.beverageAmountLabel);
            this.Controls.Add(this.barNameLabel);
            this.Controls.Add(this.reviewTitleLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Social tap";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reviewTitleLabel;
        private System.Windows.Forms.Label barNameLabel;
        private System.Windows.Forms.Label beverageAmountLabel;
        private System.Windows.Forms.Label doYouRecommendLabel;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.RadioButton yesRadioButton;
        private System.Windows.Forms.RadioButton noRadioButton;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.TextBox barNameTextBox;
        private System.Windows.Forms.RichTextBox commentRichTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UploadPhotoToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label pixelPercentageLabel;
    }
}

