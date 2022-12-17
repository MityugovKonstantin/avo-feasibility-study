using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace avo_feasibility_study
{
    public class TableService
    {
        private TableLayoutPanel[] _tables;

        public TableService(TableLayoutPanel[] tables)
        {
            _tables = tables;
        }

        public void AddEvents()
        {
            for (int i = 0; i < _tables.Length; i++)
            {
                var currentTable = _tables[i];
                AddConnectionInTable(currentTable);
                if (i != _tables.Length - 2)
                {
                    TableConnect(currentTable);
                }
            }
        }

        public void ConnectFirstDate_ValueChange(object sender, EventArgs e)
        {
            var dateObject = sender as DateTimePicker;
            var table = dateObject.Parent as TableLayoutPanel;
            var nextDate = table.GetControlFromPosition(1, 1) as DateTimePicker;
            nextDate.Value = dateObject.Value;
        }

        private void AddConnectionInTable(TableLayoutPanel table)
        {
            var tableEndIndex = table.RowCount;

            for (int row = 0; row < tableEndIndex; row++)
            {
                var beginDate = table.GetControlFromPosition(1, row) as DateTimePicker;
                beginDate.ValueChanged += BeginDate_ValueChanged;

                var counter = table.GetControlFromPosition(0, row) as NumericUpDown;
                counter.ValueChanged += Counter_ValueChanged;
            }

            for (int row = 0; row < tableEndIndex - 2; row++)
            {
                var endDate = table.GetControlFromPosition(2, row) as DateTimePicker;
                endDate.ValueChanged += EndDate_ValueChanged;
            }

            var penultimateEndDate = table.GetControlFromPosition(2, tableEndIndex - 2) as DateTimePicker;
            var lastEndDate = table.GetControlFromPosition(2, tableEndIndex - 1) as DateTimePicker;
            penultimateEndDate.ValueChanged += TableConnectEvent;
            lastEndDate.ValueChanged += TableConnectEvent;
        }

        private void TableConnect(TableLayoutPanel currentTable)
        {
            var penultimateEndDate = currentTable.GetControlFromPosition(2, currentTable.RowCount - 2) as DateTimePicker;
            var lastEndDate = currentTable.GetControlFromPosition(2, currentTable.RowCount - 1) as DateTimePicker;
            penultimateEndDate.ValueChanged += TableConnectEvent;
            lastEndDate.ValueChanged += TableConnectEvent;
        }

        private void TableConnectEvent(object sender, EventArgs e)
        {
            var currentTableFirstEndDate = sender as DateTimePicker;
            var currentTable = currentTableFirstEndDate.Parent as TableLayoutPanel;
            var row = currentTable.GetPositionFromControl(currentTableFirstEndDate).Row;
            var nextTable = null as TableLayoutPanel;
            
            for (int i = 0; i < _tables.Length - 1; i++)
            {
                if (currentTable == _tables[i])
                {
                    nextTable = _tables[i + 1];
                    break;
                }
            }
            if (nextTable != null)
            {
                var nextTableFirstBeginDate = nextTable.GetControlFromPosition(1, 0) as DateTimePicker;
                var nextTableSecondBeginDate = nextTable.GetControlFromPosition(1, 1) as DateTimePicker;

                DateTimePicker currentTableOtherEndDate = null;

                if (row % 2 == 0)
                    currentTableOtherEndDate = currentTable.GetControlFromPosition(2, row + 1) as DateTimePicker;
                else
                    currentTableOtherEndDate = currentTable.GetControlFromPosition(2, row - 1) as DateTimePicker;

                if (currentTableFirstEndDate.Value >= currentTableOtherEndDate.Value)
                {
                    nextTableFirstBeginDate.Value = currentTableFirstEndDate.Value.AddDays(1);
                    nextTableSecondBeginDate.Value = currentTableFirstEndDate.Value.AddDays(1);
                }
                else
                {
                    nextTableFirstBeginDate.Value = nextTableSecondBeginDate.Value.AddDays(1);
                    nextTableSecondBeginDate.Value = nextTableSecondBeginDate.Value.AddDays(1);
                }
            }
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            var currentEndDate = sender as DateTimePicker;
            var table = currentEndDate.Parent as TableLayoutPanel;
            var currentEndDateValue = currentEndDate.Value;
            var row = table.GetPositionFromControl(currentEndDate).Row;

            if (row % 2 == 0)
            {
                var nextEndDate = table.GetControlFromPosition(2, row + 1) as DateTimePicker;
                var nextEndDateValue = nextEndDate.Value;
                if (nextEndDateValue >= currentEndDateValue)
                {
                    var nextBeginDate1 = table.GetControlFromPosition(1, row + 2) as DateTimePicker;
                    var nextBeginDate2 = table.GetControlFromPosition(1, row + 3) as DateTimePicker;
                    nextBeginDate1.Value = nextEndDateValue.AddDays(1);
                    nextBeginDate2.Value = nextEndDateValue.AddDays(1);
                }
                else
                {
                    var nextBeginDate1 = table.GetControlFromPosition(1, row + 2) as DateTimePicker;
                    var nextBeginDate2 = table.GetControlFromPosition(1, row + 3) as DateTimePicker;
                    nextBeginDate1.Value = currentEndDateValue.AddDays(1);
                    nextBeginDate2.Value = currentEndDateValue.AddDays(1);
                }
            }
            else
            {
                var previousEndDate = table.GetControlFromPosition(2, row - 1) as DateTimePicker;
                var previousEndDateValue = previousEndDate.Value;
                if (previousEndDateValue >= currentEndDateValue)
                {
                    var nextBeginDate1 = table.GetControlFromPosition(1, row + 1) as DateTimePicker;
                    var nextBeginDate2 = table.GetControlFromPosition(1, row + 2) as DateTimePicker;
                    nextBeginDate1.Value = previousEndDateValue.AddDays(1);
                    nextBeginDate2.Value = previousEndDateValue.AddDays(1);
                }
                else
                {
                    var nextBeginDate1 = table.GetControlFromPosition(1, row + 1) as DateTimePicker;
                    var nextBeginDate2 = table.GetControlFromPosition(1, row + 2) as DateTimePicker;
                    nextBeginDate1.Value = currentEndDateValue.AddDays(1);
                    nextBeginDate2.Value = currentEndDateValue.AddDays(1);
                }
            }
        }

        private void BeginDate_ValueChanged(object sender, EventArgs e)
        {
            var beginDate = sender as DateTimePicker;
            var table = beginDate.Parent as TableLayoutPanel;
            var row = table.GetPositionFromControl(beginDate).Row;
            var counter = table.GetControlFromPosition(0, row) as NumericUpDown;
            var endDate = table.GetControlFromPosition(2, row) as DateTimePicker;
            var counterValue = (int)counter.Value;
            if (counterValue == 0)
            {
                beginDate.Hide();
                endDate.Hide();
                endDate.Value = beginDate.Value;
            }
            else
            {
                beginDate.Show();
                endDate.Show();
                endDate.Value = beginDate.Value.AddDays(counterValue - 1);
            }
        }

        private void Counter_ValueChanged(object sender, EventArgs e)
        {
            var counter = sender as NumericUpDown;
            var table = counter.Parent as TableLayoutPanel;
            var row = table.GetPositionFromControl(counter).Row;
            var beginDate = table.GetControlFromPosition(1, row) as DateTimePicker;
            var beginDateValue = beginDate.Value;
            var endDate = table.GetControlFromPosition(2, row) as DateTimePicker;
            if (counter.Value == 0)
            {
                beginDate.Hide();
                endDate.Hide();
            }
            else
            {
                beginDate.Show();
                endDate.Show();
                endDate.Value = beginDateValue.AddDays((int)counter.Value - 1);
            }

        }
    }
}
