namespace avo_feasibility_study.Forms.AddForms
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextQualityScore = new System.Windows.Forms.TextBox();
            this.TextCoef = new System.Windows.Forms.TextBox();
            this.TextProject = new System.Windows.Forms.TextBox();
            this.TextAnalog = new System.Windows.Forms.TextBox();
            this.ButtonPost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Показатель качества";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Коэффициент весомости";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Оценка проекта";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Оценка аналога";
            // 
            // TextQualityScore
            // 
            this.TextQualityScore.Location = new System.Drawing.Point(160, 6);
            this.TextQualityScore.Name = "TextQualityScore";
            this.TextQualityScore.Size = new System.Drawing.Size(288, 20);
            this.TextQualityScore.TabIndex = 4;
            // 
            // TextCoef
            // 
            this.TextCoef.Location = new System.Drawing.Point(160, 32);
            this.TextCoef.Name = "TextCoef";
            this.TextCoef.Size = new System.Drawing.Size(288, 20);
            this.TextCoef.TabIndex = 5;
            // 
            // TextProject
            // 
            this.TextProject.Location = new System.Drawing.Point(160, 58);
            this.TextProject.Name = "TextProject";
            this.TextProject.Size = new System.Drawing.Size(288, 20);
            this.TextProject.TabIndex = 6;
            // 
            // TextAnalog
            // 
            this.TextAnalog.Location = new System.Drawing.Point(160, 84);
            this.TextAnalog.Name = "TextAnalog";
            this.TextAnalog.Size = new System.Drawing.Size(288, 20);
            this.TextAnalog.TabIndex = 7;
            // 
            // ButtonPost
            // 
            this.ButtonPost.Location = new System.Drawing.Point(280, 110);
            this.ButtonPost.Name = "ButtonPost";
            this.ButtonPost.Size = new System.Drawing.Size(168, 26);
            this.ButtonPost.TabIndex = 8;
            this.ButtonPost.Text = "Добавить";
            this.ButtonPost.UseVisualStyleBackColor = true;
            // 
            // AddFormCompetitiveness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 140);
            this.Controls.Add(this.ButtonPost);
            this.Controls.Add(this.TextAnalog);
            this.Controls.Add(this.TextProject);
            this.Controls.Add(this.TextCoef);
            this.Controls.Add(this.TextQualityScore);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddFormCompetitiveness";
            this.Text = "Добавление записи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextQualityScore;
        private System.Windows.Forms.TextBox TextCoef;
        private System.Windows.Forms.TextBox TextProject;
        private System.Windows.Forms.TextBox TextAnalog;
        private System.Windows.Forms.Button ButtonPost;
    }
}