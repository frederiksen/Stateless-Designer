namespace mrtn.StatelessDesignerEditor
{
    partial class MyEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.richTextBoxCtrl = new mrtn.StatelessDesignerEditor.EditorTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(3, 3);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(194, 94);
      this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox.TabIndex = 0;
      this.pictureBox.TabStop = false;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.AutoScroll = true;
      this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
      this.splitContainer1.Panel1.Controls.Add(this.pictureBox);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.richTextBoxCtrl);
      this.splitContainer1.Size = new System.Drawing.Size(645, 387);
      this.splitContainer1.SplitterDistance = 172;
      this.splitContainer1.TabIndex = 1;
      // 
      // richTextBoxCtrl
      // 
      this.richTextBoxCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBoxCtrl.FilterMouseClickMessages = false;
      this.richTextBoxCtrl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBoxCtrl.Location = new System.Drawing.Point(0, 0);
      this.richTextBoxCtrl.Name = "richTextBoxCtrl";
      this.richTextBoxCtrl.Size = new System.Drawing.Size(645, 211);
      this.richTextBoxCtrl.TabIndex = 0;
      this.richTextBoxCtrl.Text = "";
      this.richTextBoxCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxCtrl_KeyDown);
      this.richTextBoxCtrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBoxCtrl_KeyPress);
      this.richTextBoxCtrl.MouseEnter += new System.EventHandler(this.richTextBoxCtrl_MouseEnter);
      // 
      // MyEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "MyEditor";
      this.Size = new System.Drawing.Size(645, 387);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        private EditorTextBox richTextBoxCtrl;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.SplitContainer splitContainer1;


    }
}
