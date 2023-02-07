
namespace gai
{
    partial class DtpFormNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DtpFormNew));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnGridView = new System.Windows.Forms.Button();
            this.btnDrivers = new System.Windows.Forms.Button();
            this.bntAuto = new System.Windows.Forms.Button();
            this.bntDtp = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DateCheckBox = new System.Windows.Forms.CheckBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDtpPersons = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bntDelete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(943, 480);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // btnGridView
            // 
            this.btnGridView.Location = new System.Drawing.Point(994, 369);
            this.btnGridView.Name = "btnGridView";
            this.btnGridView.Size = new System.Drawing.Size(115, 36);
            this.btnGridView.TabIndex = 1;
            this.btnGridView.Text = "Отобразить";
            this.btnGridView.UseVisualStyleBackColor = true;
            this.btnGridView.Click += new System.EventHandler(this.btnGridView_Click);
            // 
            // btnDrivers
            // 
            this.btnDrivers.Location = new System.Drawing.Point(994, 19);
            this.btnDrivers.Name = "btnDrivers";
            this.btnDrivers.Size = new System.Drawing.Size(115, 38);
            this.btnDrivers.TabIndex = 2;
            this.btnDrivers.Text = "Водители";
            this.btnDrivers.UseVisualStyleBackColor = true;
            this.btnDrivers.Click += new System.EventHandler(this.btnDrivers_Click);
            // 
            // bntAuto
            // 
            this.bntAuto.Location = new System.Drawing.Point(994, 90);
            this.bntAuto.Name = "bntAuto";
            this.bntAuto.Size = new System.Drawing.Size(129, 34);
            this.bntAuto.TabIndex = 5;
            this.bntAuto.Text = "Автомобили";
            this.bntAuto.UseVisualStyleBackColor = true;
            this.bntAuto.Click += new System.EventHandler(this.bntAuto_Click);
            // 
            // bntDtp
            // 
            this.bntDtp.Location = new System.Drawing.Point(994, 130);
            this.bntDtp.Name = "bntDtp";
            this.bntDtp.Size = new System.Drawing.Size(129, 34);
            this.bntDtp.TabIndex = 6;
            this.bntDtp.Text = "ДТП";
            this.bntDtp.UseVisualStyleBackColor = true;
            this.bntDtp.Click += new System.EventHandler(this.bntDtp_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(976, 43);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 29);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(958, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Поиск";
            // 
            // DateCheckBox
            // 
            this.DateCheckBox.AutoSize = true;
            this.DateCheckBox.Location = new System.Drawing.Point(1059, 18);
            this.DateCheckBox.Name = "DateCheckBox";
            this.DateCheckBox.Size = new System.Drawing.Size(64, 21);
            this.DateCheckBox.TabIndex = 9;
            this.DateCheckBox.Text = "Дата";
            this.DateCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(994, 450);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(129, 36);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDtpPersons
            // 
            this.btnDtpPersons.Location = new System.Drawing.Point(994, 170);
            this.btnDtpPersons.Name = "btnDtpPersons";
            this.btnDtpPersons.Size = new System.Drawing.Size(129, 45);
            this.btnDtpPersons.TabIndex = 11;
            this.btnDtpPersons.Text = "Участники ДТП";
            this.btnDtpPersons.UseVisualStyleBackColor = true;
            this.btnDtpPersons.Click += new System.EventHandler(this.btnDtpPersons_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(961, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(994, 229);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(129, 45);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Добавить ДТП";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1033, 283);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bntDelete
            // 
            this.bntDelete.Location = new System.Drawing.Point(994, 411);
            this.bntDelete.Name = "bntDelete";
            this.bntDelete.Size = new System.Drawing.Size(129, 33);
            this.bntDelete.TabIndex = 15;
            this.bntDelete.Text = "Удалить";
            this.bntDelete.UseVisualStyleBackColor = true;
            this.bntDelete.Click += new System.EventHandler(this.bntDelete_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(994, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 46);
            this.button1.TabIndex = 16;
            this.button1.Text = "Добавить участника";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(994, 343);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 46);
            this.button3.TabIndex = 17;
            this.button3.Text = "Редактировать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DtpFormNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1150, 519);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.bntDelete);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDtpPersons);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.DateCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bntDtp);
            this.Controls.Add(this.bntAuto);
            this.Controls.Add(this.btnDrivers);
            this.Controls.Add(this.btnGridView);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DtpFormNew";
            this.Text = "DtpFormNew";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnGridView;
        private System.Windows.Forms.Button btnDrivers;
        private System.Windows.Forms.Button bntAuto;
        private System.Windows.Forms.Button bntDtp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox DateCheckBox;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDtpPersons;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bntDelete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}