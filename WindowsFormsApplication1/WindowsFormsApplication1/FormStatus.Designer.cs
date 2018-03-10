namespace WindowsFormsApplication1
{
    partial class FormStatus
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
            this.butDelete = new System.Windows.Forms.Button();
            this.butEdit = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(649, 100);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(123, 23);
            this.butDelete.TabIndex = 9;
            this.butDelete.Text = "Удалить";
            this.butDelete.UseVisualStyleBackColor = true;
            // 
            // butEdit
            // 
            this.butEdit.Location = new System.Drawing.Point(649, 42);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(123, 23);
            this.butEdit.TabIndex = 8;
            this.butEdit.Text = "Редактировать";
            this.butEdit.UseVisualStyleBackColor = true;
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(649, 13);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(123, 23);
            this.butAdd.TabIndex = 7;
            this.butAdd.Text = "Добавить";
            this.butAdd.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(13, 12);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(630, 436);
            this.dgv.TabIndex = 6;
            // 
            // FormStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.butDelete);
            this.Controls.Add(this.butEdit);
            this.Controls.Add(this.butAdd);
            this.Controls.Add(this.dgv);
            this.Name = "FormStatus";
            this.Text = "FormStatus";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.DataGridView dgv;
    }
}