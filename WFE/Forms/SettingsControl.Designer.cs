namespace WFE.Forms
{
    partial class SettingsControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridReplacements = new System.Windows.Forms.DataGridView();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtReplaceBy = new System.Windows.Forms.TextBox();
            this.chkIsRegEx = new System.Windows.Forms.CheckBox();
            this.btnAddReplacement = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReplacements)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.58823F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.Controls.Add(this.gridReplacements, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtFind, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtReplaceBy, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkIsRegEx, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnAddReplacement, 5, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(581, 373);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridReplacements
            // 
            this.gridReplacements.AllowUserToAddRows = false;
            this.gridReplacements.AllowUserToDeleteRows = false;
            this.gridReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.gridReplacements, 6);
            this.gridReplacements.Location = new System.Drawing.Point(3, 129);
            this.gridReplacements.Name = "gridReplacements";
            this.gridReplacements.ReadOnly = true;
            this.gridReplacements.Size = new System.Drawing.Size(575, 241);
            this.gridReplacements.TabIndex = 0;
            // 
            // txtFind
            // 
            this.txtFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFind.Location = new System.Drawing.Point(64, 74);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(140, 20);
            this.txtFind.TabIndex = 1;
            // 
            // txtReplaceBy
            // 
            this.txtReplaceBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplaceBy.Location = new System.Drawing.Point(290, 74);
            this.txtReplaceBy.Name = "txtReplaceBy";
            this.txtReplaceBy.Size = new System.Drawing.Size(139, 20);
            this.txtReplaceBy.TabIndex = 2;
            // 
            // chkIsRegEx
            // 
            this.chkIsRegEx.AutoSize = true;
            this.chkIsRegEx.Checked = true;
            this.chkIsRegEx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsRegEx.Location = new System.Drawing.Point(435, 74);
            this.chkIsRegEx.Name = "chkIsRegEx";
            this.chkIsRegEx.Size = new System.Drawing.Size(69, 17);
            this.chkIsRegEx.TabIndex = 5;
            this.chkIsRegEx.Text = "Is RegEx";
            this.chkIsRegEx.UseVisualStyleBackColor = true;
            // 
            // btnAddReplacement
            // 
            this.btnAddReplacement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddReplacement.Location = new System.Drawing.Point(520, 74);
            this.btnAddReplacement.Name = "btnAddReplacement";
            this.btnAddReplacement.Size = new System.Drawing.Size(57, 22);
            this.btnAddReplacement.TabIndex = 4;
            this.btnAddReplacement.Text = "&+";
            this.btnAddReplacement.UseVisualStyleBackColor = false;
            this.btnAddReplacement.Click += new System.EventHandler(this.btnAddReplacement_Click);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(587, 379);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReplacements)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView gridReplacements;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.TextBox txtReplaceBy;
        private System.Windows.Forms.Button btnAddReplacement;
        private System.Windows.Forms.CheckBox chkIsRegEx;
    }
}
