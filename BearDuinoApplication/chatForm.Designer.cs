namespace BearDuino
{
    partial class ChatForm
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
            this.sendButton = new System.Windows.Forms.Button();
            this.entryBox = new System.Windows.Forms.TextBox();
            this.messageLogBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(494, 381);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(143, 112);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // entryBox
            // 
            this.entryBox.Location = new System.Drawing.Point(22, 393);
            this.entryBox.Multiline = true;
            this.entryBox.Name = "entryBox";
            this.entryBox.Size = new System.Drawing.Size(466, 104);
            this.entryBox.TabIndex = 1;
            this.entryBox.TextChanged += new System.EventHandler(this.sendBox_TextChanged);
            // 
            // messageLogBox
            // 
            this.messageLogBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.messageLogBox.Location = new System.Drawing.Point(22, 16);
            this.messageLogBox.Multiline = true;
            this.messageLogBox.Name = "messageLogBox";
            this.messageLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageLogBox.Size = new System.Drawing.Size(615, 359);
            this.messageLogBox.TabIndex = 2;
            this.messageLogBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ChatForm
            // 
            this.AcceptButton = this.sendButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 517);
            this.Controls.Add(this.messageLogBox);
            this.Controls.Add(this.entryBox);
            this.Controls.Add(this.sendButton);
            this.Name = "ChatForm";
            this.Text = "Chat Form";
            this.Load += new System.EventHandler(this.chatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox entryBox;
        private System.Windows.Forms.TextBox messageLogBox;
    }
}

