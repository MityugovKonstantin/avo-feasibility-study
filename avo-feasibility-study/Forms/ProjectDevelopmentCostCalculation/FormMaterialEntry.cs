using avo_feasibility_study.Models;
using System;
using System.Windows.Forms;

namespace avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation
{
    public partial class FormMaterialEntry : Form
    {
        public event EventHandler<MaterialEntry> AddMaterial;
        public event EventHandler<ChangeMaterialEntry> ChangeMaterial;

        private int _row;

        public FormMaterialEntry()
        {
            InitializeComponent();

            ButtonPost.Text = "Добавить";
            this.Text = "Добавление записи";

            ButtonPost.Click += AddButton_Click;
        }

        public FormMaterialEntry(ChangeMaterialEntry materialEntry)
        {
            InitializeComponent();

            ButtonPost.Text = "Изменить";
            this.Text = "Изменение записи";
            TextMaterialName.Text = materialEntry.MaterialName;
            TextUnit.Text = materialEntry.Unit;
            TextCount.Text = materialEntry.Count.ToString();
            TextCost.Text = materialEntry.Cost.ToString();
            _row = materialEntry.Row;

            ButtonPost.Click += ChangeButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                var entry = CollectParams();

                AddMaterial?.Invoke(sender, entry);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var entry = CollectParams();

                ChangeMaterialEntry changeEntry = new ChangeMaterialEntry()
                {
                    MaterialName = entry.MaterialName,
                    Unit = entry.Unit,
                    Count = entry.Count,
                    Cost = entry.Cost,
                    Sum = entry.Sum,
                    Row = _row
                };

                ChangeMaterial?.Invoke(sender, changeEntry);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private MaterialEntry CollectParams()
        {
            var name = TextMaterialName.Text;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Поле названия материала не может быть пустым!");

            var unit = TextUnit.Text;
            if (string.IsNullOrEmpty(unit))
                throw new ArgumentException("Поле единицы измерения не может быть пустым!");

            bool isCountParse = float.TryParse(TextCount.Text, out float count);
            if (!isCountParse)
                throw new ArgumentException("Требуемое кол-во было введено неверно!");
            if (count < 0)
                throw new ArgumentException("Требуемое кол-во не может быть меньше 0!");

            bool isCostParse = float.TryParse(TextCost.Text, out float cost);
            if (!isCostParse)
                throw new ArgumentException("Цена за единицу была введено неверно!");
            if (cost < 1)
                throw new ArgumentException("Цена за единицу не может быть меньше 1!");

            MaterialEntry entry = new MaterialEntry()
            {
                MaterialName = name,
                Unit = unit,
                Count = count,
                Cost = cost,
                Sum = count * cost
            };

            return entry;
        }
    }
}
