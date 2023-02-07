
namespace gai
{
    partial class ReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.Back = new System.Windows.Forms.Button();
            this.personsButton = new System.Windows.Forms.Button();
            this.illegalButton = new System.Windows.Forms.Button();
            this.dutyButton = new System.Windows.Forms.Button();
            this.accountingButton = new System.Windows.Forms.Button();
            this.setpathButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(57, 419);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(150, 34);
            this.Back.TabIndex = 0;
            this.Back.Text = "Назад";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // personsButton
            // 
            this.personsButton.Location = new System.Drawing.Point(57, 12);
            this.personsButton.Name = "personsButton";
            this.personsButton.Size = new System.Drawing.Size(150, 37);
            this.personsButton.TabIndex = 1;
            this.personsButton.Text = "ДТП";
            this.personsButton.UseVisualStyleBackColor = true;
            this.personsButton.Click += new System.EventHandler(this.personsButton_Click);
            // 
            // illegalButton
            // 
            this.illegalButton.Location = new System.Drawing.Point(57, 74);
            this.illegalButton.Name = "illegalButton";
            this.illegalButton.Size = new System.Drawing.Size(150, 34);
            this.illegalButton.TabIndex = 2;
            this.illegalButton.Text = "Нарушения";
            this.illegalButton.UseVisualStyleBackColor = true;
            this.illegalButton.Click += new System.EventHandler(this.illegalButton_Click);
            // 
            // dutyButton
            // 
            this.dutyButton.Location = new System.Drawing.Point(57, 129);
            this.dutyButton.Name = "dutyButton";
            this.dutyButton.Size = new System.Drawing.Size(150, 35);
            this.dutyButton.TabIndex = 3;
            this.dutyButton.Text = "Дежурства";
            this.dutyButton.UseVisualStyleBackColor = true;
            this.dutyButton.Click += new System.EventHandler(this.dutyButton_Click);
            // 
            // accountingButton
            // 
            this.accountingButton.Location = new System.Drawing.Point(57, 188);
            this.accountingButton.Name = "accountingButton";
            this.accountingButton.Size = new System.Drawing.Size(150, 35);
            this.accountingButton.TabIndex = 4;
            this.accountingButton.Text = "Учет";
            this.accountingButton.UseVisualStyleBackColor = true;
            this.accountingButton.Click += new System.EventHandler(this.accountingButton_Click);
            // 
            // setpathButton
            // 
            this.setpathButton.Location = new System.Drawing.Point(57, 369);
            this.setpathButton.Name = "setpathButton";
            this.setpathButton.Size = new System.Drawing.Size(150, 34);
            this.setpathButton.TabIndex = 6;
            this.setpathButton.Text = "Выбрать путь";
            this.setpathButton.UseVisualStyleBackColor = true;
            this.setpathButton.Click += new System.EventHandler(this.setpathButton_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(57, 319);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 34);
            this.button5.TabIndex = 7;
            this.button5.Text = "Выбрать интервал";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 494);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.setpathButton);
            this.Controls.Add(this.accountingButton);
            this.Controls.Add(this.dutyButton);
            this.Controls.Add(this.illegalButton);
            this.Controls.Add(this.personsButton);
            this.Controls.Add(this.Back);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчет";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button personsButton;
        private System.Windows.Forms.Button illegalButton;
        private System.Windows.Forms.Button dutyButton;
        private System.Windows.Forms.Button accountingButton;
        private System.Windows.Forms.Button setpathButton;
        private System.Windows.Forms.Button button5;
    }
}