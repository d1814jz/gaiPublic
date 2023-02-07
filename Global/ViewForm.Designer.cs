
namespace gai
{
    partial class ViewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewForm));
            this.Back = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gaidatabaseDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gaidatabaseDataSet = new gai.gaidatabaseDataSet();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bntGridView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gaidatabaseDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaidatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(847, 433);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(128, 33);
            this.Back.TabIndex = 0;
            this.Back.Text = "Назад";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(982, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.btnGridView_Click);
            this.comboBox1.MouseCaptureChanged += new System.EventHandler(this.comboBox1_MouseCaptureChanged);
            // 
            // gaidatabaseDataSetBindingSource
            // 
            this.gaidatabaseDataSetBindingSource.DataSource = this.gaidatabaseDataSet;
            this.gaidatabaseDataSetBindingSource.Position = 0;
            // 
            // gaidatabaseDataSet
            // 
            this.gaidatabaseDataSet.DataSetName = "gaidatabaseDataSet";
            this.gaidatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(980, 388);
            this.dataGridView1.TabIndex = 3;
            // 
            // bntGridView
            // 
            this.bntGridView.Location = new System.Drawing.Point(12, 433);
            this.bntGridView.Name = "bntGridView";
            this.bntGridView.Size = new System.Drawing.Size(125, 31);
            this.bntGridView.TabIndex = 4;
            this.bntGridView.Text = "Отобразить";
            this.bntGridView.UseVisualStyleBackColor = true;
            this.bntGridView.Click += new System.EventHandler(this.btnGridView_Click);
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 478);
            this.Controls.Add(this.bntGridView);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Back);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewForm";
            this.Text = "Просмотр";
            this.Load += new System.EventHandler(this.ViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gaidatabaseDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaidatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource gaidatabaseDataSetBindingSource;
        private gaidatabaseDataSet gaidatabaseDataSet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bntGridView;
    }
}