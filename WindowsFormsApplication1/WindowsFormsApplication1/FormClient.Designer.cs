namespace WindowsFormsApplication1
{
    partial class FormClient
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.butAdd = new System.Windows.Forms.Button();
            this.butEdit = new System.Windows.Forms.Button();
            this.butDelete = new System.Windows.Forms.Button();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.textAddSurname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textAddName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textAddPatronymic = new System.Windows.Forms.TextBox();
            this.dateAddBirth = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textAddPhone = new System.Windows.Forms.TextBox();
            this.comboAddGender = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.butAddOk = new System.Windows.Forms.Button();
            this.butAddCancel = new System.Windows.Forms.Button();
            this.textAddDiscount = new System.Windows.Forms.TextBox();
            this.groupEdit = new System.Windows.Forms.GroupBox();
            this.textEditDiscount = new System.Windows.Forms.TextBox();
            this.butEditCancel = new System.Windows.Forms.Button();
            this.butEditOk = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboEditGender = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textEditPhone = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dateEditBirth = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.textEditPatronymic = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textEditName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textEditSurname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupAdd.SuspendLayout();
            this.groupEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(13, 13);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(630, 436);
            this.dgv.TabIndex = 0;
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(649, 14);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(123, 23);
            this.butAdd.TabIndex = 1;
            this.butAdd.Text = "Добавить";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // butEdit
            // 
            this.butEdit.Location = new System.Drawing.Point(649, 43);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(123, 23);
            this.butEdit.TabIndex = 2;
            this.butEdit.Text = "Редактировать";
            this.butEdit.UseVisualStyleBackColor = true;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(649, 101);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(123, 23);
            this.butDelete.TabIndex = 3;
            this.butDelete.Text = "Удалить";
            this.butDelete.UseVisualStyleBackColor = true;
            // 
            // groupAdd
            // 
            this.groupAdd.Controls.Add(this.textAddDiscount);
            this.groupAdd.Controls.Add(this.butAddCancel);
            this.groupAdd.Controls.Add(this.butAddOk);
            this.groupAdd.Controls.Add(this.label7);
            this.groupAdd.Controls.Add(this.label6);
            this.groupAdd.Controls.Add(this.comboAddGender);
            this.groupAdd.Controls.Add(this.label5);
            this.groupAdd.Controls.Add(this.textAddPhone);
            this.groupAdd.Controls.Add(this.label4);
            this.groupAdd.Controls.Add(this.dateAddBirth);
            this.groupAdd.Controls.Add(this.label3);
            this.groupAdd.Controls.Add(this.textAddPatronymic);
            this.groupAdd.Controls.Add(this.label2);
            this.groupAdd.Controls.Add(this.textAddName);
            this.groupAdd.Controls.Add(this.label1);
            this.groupAdd.Controls.Add(this.textAddSurname);
            this.groupAdd.Location = new System.Drawing.Point(149, 13);
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.Size = new System.Drawing.Size(391, 257);
            this.groupAdd.TabIndex = 4;
            this.groupAdd.TabStop = false;
            this.groupAdd.Text = "Добавление клиента";
            this.groupAdd.Visible = false;
            // 
            // textAddSurname
            // 
            this.textAddSurname.Location = new System.Drawing.Point(6, 42);
            this.textAddSurname.Name = "textAddSurname";
            this.textAddSurname.Size = new System.Drawing.Size(135, 20);
            this.textAddSurname.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Имя:";
            // 
            // textAddName
            // 
            this.textAddName.Location = new System.Drawing.Point(162, 42);
            this.textAddName.Name = "textAddName";
            this.textAddName.Size = new System.Drawing.Size(135, 20);
            this.textAddName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество:";
            // 
            // textAddPatronymic
            // 
            this.textAddPatronymic.Location = new System.Drawing.Point(6, 90);
            this.textAddPatronymic.Name = "textAddPatronymic";
            this.textAddPatronymic.Size = new System.Drawing.Size(135, 20);
            this.textAddPatronymic.TabIndex = 4;
            // 
            // dateAddBirth
            // 
            this.dateAddBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateAddBirth.Location = new System.Drawing.Point(6, 140);
            this.dateAddBirth.Name = "dateAddBirth";
            this.dateAddBirth.Size = new System.Drawing.Size(135, 20);
            this.dateAddBirth.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Дата рождения:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(162, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Номер телефона:";
            // 
            // textAddPhone
            // 
            this.textAddPhone.Location = new System.Drawing.Point(162, 140);
            this.textAddPhone.Name = "textAddPhone";
            this.textAddPhone.Size = new System.Drawing.Size(135, 20);
            this.textAddPhone.TabIndex = 8;
            // 
            // comboAddGender
            // 
            this.comboAddGender.FormattingEnabled = true;
            this.comboAddGender.Items.AddRange(new object[] {
            "муж",
            "жен"});
            this.comboAddGender.Location = new System.Drawing.Point(317, 140);
            this.comboAddGender.Name = "comboAddGender";
            this.comboAddGender.Size = new System.Drawing.Size(63, 21);
            this.comboAddGender.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(314, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Пол:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Скидка:";
            // 
            // butAddOk
            // 
            this.butAddOk.Location = new System.Drawing.Point(222, 218);
            this.butAddOk.Name = "butAddOk";
            this.butAddOk.Size = new System.Drawing.Size(75, 23);
            this.butAddOk.TabIndex = 13;
            this.butAddOk.Text = "OK";
            this.butAddOk.UseVisualStyleBackColor = true;
            // 
            // butAddCancel
            // 
            this.butAddCancel.Location = new System.Drawing.Point(305, 218);
            this.butAddCancel.Name = "butAddCancel";
            this.butAddCancel.Size = new System.Drawing.Size(75, 23);
            this.butAddCancel.TabIndex = 14;
            this.butAddCancel.Text = "Отмена";
            this.butAddCancel.UseVisualStyleBackColor = true;
            this.butAddCancel.Click += new System.EventHandler(this.butAddCancel_Click);
            // 
            // textAddDiscount
            // 
            this.textAddDiscount.Location = new System.Drawing.Point(7, 198);
            this.textAddDiscount.Name = "textAddDiscount";
            this.textAddDiscount.Size = new System.Drawing.Size(134, 20);
            this.textAddDiscount.TabIndex = 15;
            // 
            // groupEdit
            // 
            this.groupEdit.Controls.Add(this.textEditDiscount);
            this.groupEdit.Controls.Add(this.butEditCancel);
            this.groupEdit.Controls.Add(this.butEditOk);
            this.groupEdit.Controls.Add(this.label8);
            this.groupEdit.Controls.Add(this.label9);
            this.groupEdit.Controls.Add(this.comboEditGender);
            this.groupEdit.Controls.Add(this.label10);
            this.groupEdit.Controls.Add(this.textEditPhone);
            this.groupEdit.Controls.Add(this.label11);
            this.groupEdit.Controls.Add(this.dateEditBirth);
            this.groupEdit.Controls.Add(this.label12);
            this.groupEdit.Controls.Add(this.textEditPatronymic);
            this.groupEdit.Controls.Add(this.label13);
            this.groupEdit.Controls.Add(this.textEditName);
            this.groupEdit.Controls.Add(this.label14);
            this.groupEdit.Controls.Add(this.textEditSurname);
            this.groupEdit.Location = new System.Drawing.Point(147, 14);
            this.groupEdit.Name = "groupEdit";
            this.groupEdit.Size = new System.Drawing.Size(391, 257);
            this.groupEdit.TabIndex = 5;
            this.groupEdit.TabStop = false;
            this.groupEdit.Text = "Редактирование клиента";
            this.groupEdit.Visible = false;
            // 
            // textEditDiscount
            // 
            this.textEditDiscount.Location = new System.Drawing.Point(7, 198);
            this.textEditDiscount.Name = "textEditDiscount";
            this.textEditDiscount.Size = new System.Drawing.Size(134, 20);
            this.textEditDiscount.TabIndex = 15;
            // 
            // butEditCancel
            // 
            this.butEditCancel.Location = new System.Drawing.Point(305, 218);
            this.butEditCancel.Name = "butEditCancel";
            this.butEditCancel.Size = new System.Drawing.Size(75, 23);
            this.butEditCancel.TabIndex = 14;
            this.butEditCancel.Text = "Отмена";
            this.butEditCancel.UseVisualStyleBackColor = true;
            this.butEditCancel.Click += new System.EventHandler(this.butEditCancel_Click);
            // 
            // butEditOk
            // 
            this.butEditOk.Location = new System.Drawing.Point(222, 218);
            this.butEditOk.Name = "butEditOk";
            this.butEditOk.Size = new System.Drawing.Size(75, 23);
            this.butEditOk.TabIndex = 13;
            this.butEditOk.Text = "OK";
            this.butEditOk.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Скидка:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(314, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Пол:";
            // 
            // comboEditGender
            // 
            this.comboEditGender.FormattingEnabled = true;
            this.comboEditGender.Items.AddRange(new object[] {
            "муж",
            "жен"});
            this.comboEditGender.Location = new System.Drawing.Point(317, 140);
            this.comboEditGender.Name = "comboEditGender";
            this.comboEditGender.Size = new System.Drawing.Size(63, 21);
            this.comboEditGender.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(162, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Номер телефона:";
            // 
            // textEditPhone
            // 
            this.textEditPhone.Location = new System.Drawing.Point(162, 140);
            this.textEditPhone.Name = "textEditPhone";
            this.textEditPhone.Size = new System.Drawing.Size(135, 20);
            this.textEditPhone.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Дата рождения:";
            // 
            // dateEditBirth
            // 
            this.dateEditBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEditBirth.Location = new System.Drawing.Point(6, 140);
            this.dateEditBirth.Name = "dateEditBirth";
            this.dateEditBirth.Size = new System.Drawing.Size(135, 20);
            this.dateEditBirth.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Отчество:";
            // 
            // textEditPatronymic
            // 
            this.textEditPatronymic.Location = new System.Drawing.Point(6, 90);
            this.textEditPatronymic.Name = "textEditPatronymic";
            this.textEditPatronymic.Size = new System.Drawing.Size(135, 20);
            this.textEditPatronymic.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(162, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Имя:";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(162, 42);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(135, 20);
            this.textEditName.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Фамилия:";
            // 
            // textEditSurname
            // 
            this.textEditSurname.Location = new System.Drawing.Point(6, 42);
            this.textEditSurname.Name = "textEditSurname";
            this.textEditSurname.Size = new System.Drawing.Size(135, 20);
            this.textEditSurname.TabIndex = 0;
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.groupEdit);
            this.Controls.Add(this.groupAdd);
            this.Controls.Add(this.butDelete);
            this.Controls.Add(this.butEdit);
            this.Controls.Add(this.butAdd);
            this.Controls.Add(this.dgv);
            this.Name = "FormClient";
            this.Text = "FormClient";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupAdd.ResumeLayout(false);
            this.groupAdd.PerformLayout();
            this.groupEdit.ResumeLayout(false);
            this.groupEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.GroupBox groupAdd;
        private System.Windows.Forms.TextBox textAddDiscount;
        private System.Windows.Forms.Button butAddCancel;
        private System.Windows.Forms.Button butAddOk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboAddGender;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textAddPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateAddBirth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textAddPatronymic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textAddName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textAddSurname;
        private System.Windows.Forms.GroupBox groupEdit;
        private System.Windows.Forms.TextBox textEditDiscount;
        private System.Windows.Forms.Button butEditCancel;
        private System.Windows.Forms.Button butEditOk;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboEditGender;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textEditPhone;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateEditBirth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textEditPatronymic;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textEditName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textEditSurname;
    }
}