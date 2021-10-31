using System;
using System.Windows.Forms;
using System.IO;
using Lab1.Table;

namespace Lab1
{
    public partial class form : Form
    {
        TableGrid table = new TableGrid();
        public form()
        {
            InitializeComponent();
            InitializeDataGridView(table.rowCount, table.colCount);
        }

        private void InitializeDataGridView(int rows, int columns)
        {
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.RowHeadersVisible = true;
            dataGridView.ColumnCount = columns;
            for (int i = 0; i < columns; i++)
            {
                string columnName = ColumnIndexConverter.ToChar(i);
                dataGridView.Columns[i].Name = columnName;
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < rows; i++)
            {
                dataGridView.Rows.Add("");
                dataGridView.Rows[i].HeaderCell.Value = (i).ToString();
            }
            dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            table.setTable(columns, rows);
        }
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            int col = dataGridView.SelectedCells[0].ColumnIndex;
            int row = dataGridView.SelectedCells[0].RowIndex;
            string expression = textBox.Text;
            if (expression == "") return;
            table.ChangeCellWithAllPointers(row, col, expression, dataGridView);
            dataGridView[col, row].Value = TableGrid.grid[row][col].value;
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TableFile|*.txt";
            openFileDialog.Title = "Open Table File";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            StreamReader sr = new StreamReader(openFileDialog.FileName);
            table.Clear();
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            int row; int column;
            int.TryParse(sr.ReadLine(), out row);
            int.TryParse(sr.ReadLine(), out column);
            InitializeDataGridView(row, column);
            table.Open(row, column, sr, dataGridView);
            sr.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TableFile|*.txt";
            saveFileDialog.Title = "Save table file";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                table.Save(sw);
                sw.Close();
                fs.Close();
            }
        }

        private void buttonInformation_Click(object sender, EventArgs e)
        {
           MessageBox.Show("Програма виконує обрахунки у таблиці з використанням операцій " +
               "+, -, /, *, ^, max(a, b), min(a, b), dec(a, b), inc(a, b). \n" +
               "Обов'язково на початку виразу має бути знак \'=\'. \n" +
               "Вираз має бути у форматі: \"=max(1, A1) * (2 - B0)\".\n" +
               "Також ви маєте змогу зберігати та читати вашу таблицю. \n" +
               "\nРоботу виконав студент групи К-25 - Чоботок Владислав.", 
               "Information");
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            string name = ColumnIndexConverter.ToChar(table.colCount);
            dataGridView.Columns.Add(name, name);
            table.AddColumn(dataGridView);
        }

        private void buttonDeleteColumn_Click(object sender, EventArgs e)
        {
            if (!table.DeleteColumn(dataGridView))
                return;
            dataGridView.Columns.RemoveAt(table.colCount);
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            if (dataGridView.Columns.Count == 0)
            {
                MessageBox.Show("There are no columns!");
                return;
            }
            dataGridView.Rows.Add(row);
            dataGridView.Rows[table.rowCount].HeaderCell.Value = (table.rowCount).ToString();
            table.AddRow(dataGridView);
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (!table.DeleteRow(dataGridView))
                return;
            dataGridView.Rows.RemoveAt(table.rowCount);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = dataGridView.SelectedCells[0].ColumnIndex;
            int row = dataGridView.SelectedCells[0].RowIndex;
            string expression = TableGrid.grid[row][col].expression;
            string value = TableGrid.grid[row][col].value;
            textBox.Text = expression;
            textBox.Focus();
        }
    }
}
