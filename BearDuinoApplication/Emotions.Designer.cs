namespace BearDuino
{
    partial class emotionvisualizer
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
            this.emotionPictureBox = new System.Windows.Forms.PictureBox();
            this.emotionLabel = new System.Windows.Forms.Label();
            this.emotionTypeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.emotionPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // emotionPictureBox
            // 
            this.emotionPictureBox.Location = new System.Drawing.Point(0, -3);
            this.emotionPictureBox.Name = "emotionPictureBox";
            this.emotionPictureBox.Size = new System.Drawing.Size(600, 400);
            this.emotionPictureBox.TabIndex = 0;
            this.emotionPictureBox.TabStop = false;
            // 
            // emotionLabel
            // 
            this.emotionLabel.AutoSize = true;
            this.emotionLabel.BackColor = System.Drawing.Color.Transparent;
            this.emotionLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emotionLabel.Font = new System.Drawing.Font("Cambria", 18F);
            this.emotionLabel.ForeColor = System.Drawing.Color.White;
            this.emotionLabel.Location = new System.Drawing.Point(12, 400);
            this.emotionLabel.MaximumSize = new System.Drawing.Size(560, 200);
            this.emotionLabel.Name = "emotionLabel";
            this.emotionLabel.Size = new System.Drawing.Size(76, 28);
            this.emotionLabel.TabIndex = 1;
            this.emotionLabel.Text = "label1";
            this.emotionLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // emotionTypeLabel
            // 
            this.emotionTypeLabel.AutoSize = true;
            this.emotionTypeLabel.Font = new System.Drawing.Font("Cambria", 20F);
            this.emotionTypeLabel.ForeColor = System.Drawing.Color.White;
            this.emotionTypeLabel.Location = new System.Drawing.Point(450, 365);
            this.emotionTypeLabel.Name = "emotionTypeLabel";
            this.emotionTypeLabel.Size = new System.Drawing.Size(229, 32);
            this.emotionTypeLabel.TabIndex = 2;
            this.emotionTypeLabel.Text = "emotionTypeLabel";
            this.emotionTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // emotionvisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.emotionTypeLabel);
            this.Controls.Add(this.emotionLabel);
            this.Controls.Add(this.emotionPictureBox);
            this.Name = "emotionvisualizer";
            this.Text = "Emotion Visualizer";
            this.Load += new System.EventHandler(this.emotionvisualizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.emotionPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox emotionPictureBox;
        private System.Windows.Forms.Label emotionLabel;
        private System.Windows.Forms.Label emotionTypeLabel;
    }
}