namespace mysqlgen
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
            this.Generate = new System.Windows.Forms.Button();
            this.SelectedFolder = new System.Windows.Forms.Label();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TypeBtn = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Generate
            // 
            this.Generate.Location = new System.Drawing.Point(10, 87);
            this.Generate.Margin = new System.Windows.Forms.Padding(4);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(100, 28);
            this.Generate.TabIndex = 0;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // SelectedFolder
            // 
            this.SelectedFolder.AutoSize = true;
            this.SelectedFolder.Location = new System.Drawing.Point(7, 54);
            this.SelectedFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SelectedFolder.Name = "SelectedFolder";
            this.SelectedFolder.Size = new System.Drawing.Size(81, 16);
            this.SelectedFolder.TabIndex = 1;
            this.SelectedFolder.Text = "pick a folder";
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(7, 22);
            this.SelectFolderButton.Margin = new System.Windows.Forms.Padding(4);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(191, 28);
            this.SelectFolderButton.TabIndex = 2;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SelectFolderButton);
            this.groupBox1.Controls.Add(this.Generate);
            this.groupBox1.Controls.Add(this.SelectedFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 131);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generate Entities";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TypeBtn);
            this.groupBox2.Location = new System.Drawing.Point(22, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tests";
            // 
            // TypeBtn
            // 
            this.TypeBtn.Location = new System.Drawing.Point(17, 22);
            this.TypeBtn.Name = "TypeBtn";
            this.TypeBtn.Size = new System.Drawing.Size(109, 23);
            this.TypeBtn.TabIndex = 0;
            this.TypeBtn.Text = "Types Test";
            this.TypeBtn.UseVisualStyleBackColor = true;
            this.TypeBtn.Click += new System.EventHandler(this.TypeBtn_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.Enabled = false;
            this.OutputBox.Location = new System.Drawing.Point(22, 276);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.Size = new System.Drawing.Size(568, 100);
            this.OutputBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 388);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Label SelectedFolder;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button TypeBtn;
        private System.Windows.Forms.TextBox OutputBox;
    }
}

