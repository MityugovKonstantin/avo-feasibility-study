namespace avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation
{
    partial class FormMaterialEntry
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
            this.label1 = new System.Windows.Forms.Label();
            this.TextMaterialName = new System.Windows.Forms.TextBox();
            this.TextUnit = new System.Windows.Forms.TextBox();
            this.TextCount = new System.Windows.Forms.TextBox();
            this.TextCost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ButtonPost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название материала";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextMaterialName
            // 
            this.TextMaterialName.Location = new System.Drawing.Point(144, 9);
            this.TextMaterialName.Name = "TextMaterialName";
            this.TextMaterialName.Size = new System.Drawing.Size(241, 20);
            this.TextMaterialName.TabIndex = 5;
            // 
            // TextUnit
            // 
            this.TextUnit.Location = new System.Drawing.Point(144, 35);
            this.TextUnit.Name = "TextUnit";
            this.TextUnit.Size = new System.Drawing.Size(241, 20);
            this.TextUnit.TabIndex = 6;
            // 
            // TextCount
            // 
            this.TextCount.Location = new System.Drawing.Point(144, 61);
            this.TextCount.Name = "TextCount";
            this.TextCount.Size = new System.Drawing.Size(241, 20);
            this.TextCount.TabIndex = 7;
            // 
            // TextCost
            // 
            this.TextCost.Location = new System.Drawing.Point(144, 87);
            this.TextCost.Name = "TextCost";
            this.TextCost.Size = new System.Drawing.Size(241, 20);
            this.TextCost.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Единица измерения";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Требуемое кол-во";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Цена за единицу, руб.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonPost
            // 
            this.ButtonPost.Location = new System.Drawing.Point(12, 113);
            this.ButtonPost.Name = "ButtonPost";
            this.ButtonPost.Size = new System.Drawing.Size(373, 28);
            this.ButtonPost.TabIndex = 14;
            this.ButtonPost.Text = "button1";
            this.ButtonPost.UseVisualStyleBackColor = true;
            // 
            // FormMaterialEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 150);
            this.Controls.Add(this.ButtonPost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextCost);
            this.Controls.Add(this.TextCount);
            this.Controls.Add(this.TextUnit);
            this.Controls.Add(this.TextMaterialName);
            this.Controls.Add(this.label1);
            this.Name = "FormMaterialEntry";
            this.Text = "FormMaterialEntry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextMaterialName;
        private System.Windows.Forms.TextBox TextUnit;
        private System.Windows.Forms.TextBox TextCount;
        private System.Windows.Forms.TextBox TextCost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ButtonPost;
    }
}