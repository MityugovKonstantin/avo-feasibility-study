namespace avo_feasibility_study
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabCompetitiveness = new System.Windows.Forms.TabPage();
            this.TabPlanning = new System.Windows.Forms.TabPage();
            this.TabExpenses = new System.Windows.Forms.TabPage();
            this.TabOperatingCosts = new System.Windows.Forms.TabPage();
            this.TabEconomicEfficiency = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabCompetitiveness);
            this.tabControl1.Controls.Add(this.TabPlanning);
            this.tabControl1.Controls.Add(this.TabExpenses);
            this.tabControl1.Controls.Add(this.TabOperatingCosts);
            this.tabControl1.Controls.Add(this.TabEconomicEfficiency);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(896, 545);
            this.tabControl1.TabIndex = 0;
            // 
            // TabCompetitiveness
            // 
            this.TabCompetitiveness.Location = new System.Drawing.Point(4, 22);
            this.TabCompetitiveness.Name = "TabCompetitiveness";
            this.TabCompetitiveness.Padding = new System.Windows.Forms.Padding(3);
            this.TabCompetitiveness.Size = new System.Drawing.Size(888, 519);
            this.TabCompetitiveness.TabIndex = 0;
            this.TabCompetitiveness.Text = "Оценка конкурентоспособности проекта в сравнении с аналогом";
            this.TabCompetitiveness.UseVisualStyleBackColor = true;
            // 
            // TabPlanning
            // 
            this.TabPlanning.Location = new System.Drawing.Point(4, 22);
            this.TabPlanning.Name = "TabPlanning";
            this.TabPlanning.Padding = new System.Windows.Forms.Padding(3);
            this.TabPlanning.Size = new System.Drawing.Size(888, 519);
            this.TabPlanning.TabIndex = 1;
            this.TabPlanning.Text = "Планирование комплекса работ по разработке проекта и оценка";
            this.TabPlanning.UseVisualStyleBackColor = true;
            // 
            // TabExpenses
            // 
            this.TabExpenses.Location = new System.Drawing.Point(4, 22);
            this.TabExpenses.Name = "TabExpenses";
            this.TabExpenses.Size = new System.Drawing.Size(888, 519);
            this.TabExpenses.TabIndex = 2;
            this.TabExpenses.Text = "Расчет затрат на разработку проекта";
            this.TabExpenses.UseVisualStyleBackColor = true;
            // 
            // TabOperatingCosts
            // 
            this.TabOperatingCosts.Location = new System.Drawing.Point(4, 22);
            this.TabOperatingCosts.Name = "TabOperatingCosts";
            this.TabOperatingCosts.Size = new System.Drawing.Size(888, 519);
            this.TabOperatingCosts.TabIndex = 3;
            this.TabOperatingCosts.Text = "Расчет эксплуатационных затрат";
            this.TabOperatingCosts.UseVisualStyleBackColor = true;
            // 
            // TabEconomicEfficiency
            // 
            this.TabEconomicEfficiency.Location = new System.Drawing.Point(4, 22);
            this.TabEconomicEfficiency.Name = "TabEconomicEfficiency";
            this.TabEconomicEfficiency.Size = new System.Drawing.Size(888, 519);
            this.TabEconomicEfficiency.TabIndex = 4;
            this.TabEconomicEfficiency.Text = "Расчет показателей экономической эффективности";
            this.TabEconomicEfficiency.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 569);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabCompetitiveness;
        private System.Windows.Forms.TabPage TabPlanning;
        private System.Windows.Forms.TabPage TabExpenses;
        private System.Windows.Forms.TabPage TabOperatingCosts;
        private System.Windows.Forms.TabPage TabEconomicEfficiency;
    }
}

