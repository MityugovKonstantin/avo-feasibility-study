namespace avo_feasibility_study.Forms.Competitiveness
{
    partial class FormCompetitiveness
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
            this.TextQualityScore = new System.Windows.Forms.TextBox();
            this.TextCoef = new System.Windows.Forms.TextBox();
            this.TextProject = new System.Windows.Forms.TextBox();
            this.TextAnalog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ButtonPost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextQualityScore
            // 
            this.TextQualityScore.Location = new System.Drawing.Point(190, 9);
            this.TextQualityScore.Name = "TextQualityScore";
            this.TextQualityScore.Size = new System.Drawing.Size(188, 20);
            this.TextQualityScore.TabIndex = 0;
            // 
            // TextCoef
            // 
            this.TextCoef.Location = new System.Drawing.Point(190, 35);
            this.TextCoef.Name = "TextCoef";
            this.TextCoef.Size = new System.Drawing.Size(188, 20);
            this.TextCoef.TabIndex = 1;
            // 
            // TextProject
            // 
            this.TextProject.Location = new System.Drawing.Point(190, 61);
            this.TextProject.Name = "TextProject";
            this.TextProject.Size = new System.Drawing.Size(188, 20);
            this.TextProject.TabIndex = 2;
            // 
            // TextAnalog
            // 
            this.TextAnalog.Location = new System.Drawing.Point(190, 87);
            this.TextAnalog.Name = "TextAnalog";
            this.TextAnalog.Size = new System.Drawing.Size(188, 20);
            this.TextAnalog.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Название показателя качества";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Коэффициент весомости";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Экспертная оценка проекта";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(32, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Экспертная оценка аналога";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonPost
            // 
            this.ButtonPost.Location = new System.Drawing.Point(15, 114);
            this.ButtonPost.Name = "ButtonPost";
            this.ButtonPost.Size = new System.Drawing.Size(362, 32);
            this.ButtonPost.TabIndex = 8;
            this.ButtonPost.UseVisualStyleBackColor = true;
            // 
            // FormCompetitiveness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 156);
            this.Controls.Add(this.ButtonPost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextAnalog);
            this.Controls.Add(this.TextProject);
            this.Controls.Add(this.TextCoef);
            this.Controls.Add(this.TextQualityScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormCompetitiveness";
            this.Text = "TableCompetitiveness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextQualityScore;
        private System.Windows.Forms.TextBox TextCoef;
        private System.Windows.Forms.TextBox TextProject;
        private System.Windows.Forms.TextBox TextAnalog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ButtonPost;
    }
}