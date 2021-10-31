using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using Sprache.Calc;

namespace Lab1.Table
{
    public class TableGrid
    {
        private const int defaultCol = 30;
        private const int defaultRow = 20;
        public int colCount;
        public int rowCount;
        public static List<List<Cell>> grid = new List<List<Cell>>();
        public Dictionary<string, string> dictionary = new Dictionary<string, string>();
        XtensibleCalculator calc = new XtensibleCalculator().RegisterFunction("max", (a, b) => Math.Max(a, b))
                                                            .RegisterFunction("min", (a, b) => Math.Min(a, b))
                                                            .RegisterFunction("inc", (a, b) => a + b)
                                                            .RegisterFunction("dec", (a, b) => a - b);
        public TableGrid()
        {
            setTable(defaultCol, defaultRow);
        }

        public void setTable(int col, int row)
        {
            Clear();
            colCount = col;
            rowCount = row;
            for (int i = 0; i < rowCount; i++)
            {
                List<Cell> newRow = new List<Cell>();
                for (int j = 0; j < colCount; j++)
                {
                    newRow.Add(new Cell(i, j));
                    dictionary.Add(newRow.Last().getName(), "");
                }
                grid.Add(newRow);
            }
        }
        public void Clear()
        {
            foreach (List<Cell> list in grid)
            {
                list.Clear();
            }
            grid.Clear();
            dictionary.Clear();
            rowCount = 0;
            colCount = 0;
        }
        public void ChangeCellWithAllPointers(int row, int col, string expression, DataGridView dataGridView)
        {
            grid[row][col].DeletePointersAndReferences();
            grid[row][col].expression = expression;
            grid[row][col].new_referencesFromThis.Clear();

            if (expression != "")
            {
                if (expression[0] != '=')
                {
                    grid[row][col].value = expression;
                    dictionary[FullName(row, col)] = expression;
                    foreach (Cell cell in grid[row][col].pointersToThis)
                    {
                        RefreshCellAndPointers(cell, dataGridView);
                    }
                    return;
                }
            }

            string new_expression = ConvertReferences(row, col, expression);
            if (new_expression != "")
            {
                new_expression = new_expression.Remove(0, 1);
            }

            if (!grid[row][col].CheckLoop(grid[row][col].new_referencesFromThis))
            {
                MessageBox.Show("There is a loop! Change the expression.");
                grid[row][col].expression = "";
                grid[row][col].value = "0";
                dataGridView[col, row].Value = "0";
                return;
            }

            grid[row][col].AddPointersAndReferences();
            string val = Calculate(new_expression);
            if (val == "Error" || val == "Division by zero error")
            {
                MessageBox.Show("Error in cell " + FullName(row, col));
                grid[row][col].expression = "";
                grid[row][col].value = "0";
                dataGridView[col, row].Value = "0";
                return;
            }
            grid[row][col].value = val;
            dictionary[FullName(row, col)] = val;
            foreach (Cell cell in grid[row][col].pointersToThis)
                RefreshCellAndPointers(cell, dataGridView);
        }

        private string FullName(int row, int col)
        {
            Cell cell = new Cell(row, col);
            return cell.getName();
        }

        public bool RefreshCellAndPointers(Cell cell, DataGridView dataGridView)
        {
            cell.new_referencesFromThis.Clear();
            string new_expression = ConvertReferences(cell.row, cell.column, cell.expression);
            new_expression = new_expression.Remove(0, 1);
            string Value = Calculate(new_expression);

            if (Value == "Error" || Value == "Division by zero error")
            {
                MessageBox.Show("Error in cell " + cell.getName());
                cell.expression = "";
                cell.value = "0";
                dataGridView[cell.column, cell.row].Value = "0";
                return false;
            }
            grid[cell.row][cell.column].value = Value;
            dictionary[FullName(cell.row, cell.column)] = Value;
            dataGridView[cell.column, cell.row].Value = Value;

            foreach (Cell point in cell.pointersToThis)
            {
                if (!RefreshCellAndPointers(point, dataGridView))
                    return false;
            }
            return true;
        }

        public string ConvertReferences(int row, int col, string expr)
        {
            Regex regex = new Regex(@"[A-Z]+[0-9]+", RegexOptions.IgnoreCase);
            Index nums;

            foreach (Match match in regex.Matches(expr)) 
            {
                if (dictionary.ContainsKey(match.Value))
                {
                    nums = ColumnIndexConverter.FromChar(match.Value);
                    grid[row][col].new_referencesFromThis.Add(grid[nums.row][nums.column]);
                }
            }

            MatchEvaluator evaluator = new MatchEvaluator(referenceToValue);
            string new_expression = regex.Replace(expr, evaluator);

            new_expression = new_expression.Replace(" ", "");

            for(int i = 1; i < new_expression.Length; i++)
            {
                if(new_expression[i] == '+' && char.IsNumber(new_expression[i - 1]) == false && new_expression[i - 1] != ')')
                {
                    new_expression = new_expression.Remove(i, 1);
                    MessageBox.Show(new_expression);
                }
            }

            return new_expression;
        }

        public string referenceToValue(Match m)
        {
            if (dictionary.ContainsKey(m.Value))
                if (dictionary[m.Value] == "")
                    return "0";
                else
                    return dictionary[m.Value];
            return m.Value;
        }
        public string Calculate(string expression)
        {
            if (expression == "")
            {
                return "";
            }
            try
            {
                var res = calc.ParseExpression(expression).Compile();
                string func = res().ToString();
               if (func == "∞")
                {
                    return "Division by zero error";
                }
                
                return func;
            }
            catch
            {
                return "Error";
            }
        }
        public void AddRow(DataGridView dataGridView)
        {
            List<Cell> newRow = new List<Cell>();

            for (int j = 0; j < colCount; j++)
            {
                newRow.Add(new Cell(rowCount, j));
                dictionary.Add(newRow.Last().getName(), "");
            }
            grid.Add(newRow);
            RefreshReferences();
            foreach (List<Cell> list in grid)
            {
                foreach (Cell cell in list)
                {
                    if (cell.referencesFromThis != null)
                    {
                        foreach (Cell cell_ref in cell.referencesFromThis)
                        {
                            if (cell_ref.row == rowCount)
                            {
                                if (!cell_ref.pointersToThis.Contains(cell))
                                    cell_ref.pointersToThis.Add(cell);
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < colCount; j++)
            {
                ChangeCellWithAllPointers(rowCount, j, "", dataGridView);
            }
            rowCount++;
        }

        public void AddColumn(DataGridView dataGridView)
        {
            colCount++;
            for (int i = 0; i < rowCount; i++)
            {
                grid[i].Add(new Cell(i, colCount - 1));
                dictionary.Add(grid[i].Last().getName(), "");
            }
            RefreshReferences();
            foreach (List<Cell> list in grid)
            {
                foreach (Cell cell in list)
                {
                    if (cell.referencesFromThis != null)
                    {
                        foreach (Cell cell_ref in cell.referencesFromThis)
                        {
                            if (cell_ref.column == colCount - 1)
                            {
                                if (!cell_ref.pointersToThis.Contains(cell))
                                    cell_ref.pointersToThis.Add(cell);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                ChangeCellWithAllPointers(i, colCount - 1, "", dataGridView);
            }

        }

        public void RefreshReferences()
        {
            foreach (List<Cell> list in grid)
            {
                foreach (Cell cell in list)
                {
                    if (cell.referencesFromThis != null)
                        cell.referencesFromThis.Clear();
                    if (cell.new_referencesFromThis != null)
                        cell.new_referencesFromThis.Clear();
                    if (cell.expression == "")
                        continue;
                    string new_expession = cell.expression;
                    if (cell.expression[0] == '=')
                    {
                        new_expession = ConvertReferences(cell.row, cell.column, cell.expression);
                        cell.referencesFromThis.AddRange(cell.new_referencesFromThis);
                    }
                }
            }
        }
        public bool DeleteRow(DataGridView dataGridView)
        {
            List<Cell> lastRow = new List<Cell>(); 
            List<string> notEmptyCells = new List<string>();
            if (rowCount == 0)
                return false;
            int curCount = rowCount - 1;
            for (int i = 0; i < colCount; i++)
            {
                string name = FullName(curCount, i);
                if (dictionary[name] != "0" && dictionary[name] != "" && dictionary[name] != " ")
                    notEmptyCells.Add(name);
                if (grid[curCount][i].pointersToThis.Count != 0)
                    lastRow.AddRange(grid[curCount][i].pointersToThis);
            }

            if (lastRow.Count != 0 || notEmptyCells.Count != 0)
            {
                string errorMessage = "";
                if (notEmptyCells.Count != 0)
                {
                    errorMessage = "There are not empty cells: ";
                    errorMessage += string.Join(";", notEmptyCells.ToArray());
                    errorMessage += ' ';
                }
                if (lastRow.Count != 0)
                {
                    errorMessage += "There are cells that point to cells from current row : ";
                    foreach (Cell cell in lastRow)
                    {
                        errorMessage += string.Join(";", cell.getName());
                        errorMessage += " ";
                    }

                }
                errorMessage += "Are you sure you want to delete this row?";
                DialogResult res = MessageBox.Show(errorMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                    return false;
            }
            for (int i = 0; i < colCount; i++)
            {
                string name = FullName(curCount, i);
                dictionary.Remove(name);
            }
            foreach (Cell cell in lastRow)
                RefreshCellAndPointers(cell, dataGridView);
            grid.RemoveAt(curCount);
            rowCount--;
            return true;
        }

        public bool DeleteColumn(DataGridView dataGridView)
        {
            List<Cell> lastCol = new List<Cell>();
            List<string> notEmptyCells = new List<string>();
            if (colCount == 0)
                return false;
            int curCount = colCount - 1;
            for (int i = 0; i < rowCount; i++)
            {
                string name = FullName(i, curCount);
                if (dictionary[name] != "0" && dictionary[name] != "" && dictionary[name] != " ")
                    notEmptyCells.Add(name);
                if (grid[i][curCount].pointersToThis.Count != 0)
                    lastCol.AddRange(grid[i][curCount].pointersToThis);
            }

            if (lastCol.Count != 0 || notEmptyCells.Count != 0)
            {
                string errorMessage = "";
                if (notEmptyCells.Count != 0)
                {
                    errorMessage = "There are not empty cells: ";
                    errorMessage += string.Join(";", notEmptyCells.ToArray());
                }
                if (lastCol.Count != 0)
                {
                    errorMessage += "There are cells that point to cells from current column :";
                    foreach (Cell cell in lastCol)
                        errorMessage += string.Join(";", cell.getName());
                }
                errorMessage += " Are you sure you want to delete this column?";
                DialogResult res = MessageBox.Show(errorMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                    return false;
            }
            for (int i = 0; i < rowCount; i++)
            {
                string name = FullName(i, curCount);
                dictionary.Remove(name);
            }
            foreach (Cell cell in lastCol)
                RefreshCellAndPointers(cell, dataGridView);
            for (int i = 0; i < rowCount; i++)
            {
                grid[i].RemoveAt(curCount);
            }
            colCount--;
            return true;
        }
        public void Open(int r, int c, StreamReader sr, DataGridView dataGridView)
        {
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    string index = sr.ReadLine();
                    string expression = sr.ReadLine();
                    string value = sr.ReadLine();

                    if (expression != "")
                        dictionary[index] = value;
                    else
                        dictionary[index] = "";

                    int refCount = Convert.ToInt32(sr.ReadLine());
                    List<Cell> newRef = new List<Cell>();
                    string refer;

                    for (int k = 0; k < refCount; k++)
                    {
                        refer = sr.ReadLine();
                        if (ColumnIndexConverter.FromChar(refer).row < rowCount
                            && ColumnIndexConverter.FromChar(refer).column < colCount)
                            newRef.Add(grid[ColumnIndexConverter.FromChar(refer).row][ColumnIndexConverter.FromChar(refer).column]);
                    }

                    int pointCount = Convert.ToInt32(sr.ReadLine());
                    List<Cell> newPoint = new List<Cell>();
                    string point;

                    for (int k = 0; k < pointCount; k++)
                    {
                        point = sr.ReadLine();
                        newPoint.Add(grid[ColumnIndexConverter.FromChar(point).row][ColumnIndexConverter.FromChar(point).column]);
                    }
                    grid[i][j].setCell(expression, value, newRef, newPoint);

                    int curCol = grid[i][j].column;
                    int curRow = grid[i][j].row;
                    dataGridView[curCol, curRow].Value = dictionary[index];
                }
            }
        }
        public void Save(StreamWriter sw)
        {
            sw.WriteLine(rowCount);
            sw.WriteLine(colCount);
            foreach (List<Cell> list in grid)
            {
                foreach (Cell cell in list)
                {
                    sw.WriteLine(cell.getName());
                    sw.WriteLine(cell.expression);
                    sw.WriteLine(cell.value);

                    if (cell.referencesFromThis == null)
                        sw.WriteLine("0");
                    else
                    {
                        sw.WriteLine(cell.referencesFromThis.Count);
                        foreach (Cell refCell in cell.referencesFromThis)
                            sw.WriteLine(refCell.getName());
                    }
                    if (cell.pointersToThis == null)
                        sw.WriteLine("0");
                    else
                    {
                        sw.WriteLine(cell.pointersToThis.Count);
                        foreach (Cell pointCell in cell.pointersToThis)
                            sw.WriteLine(pointCell.getName());
                    }
                }
            }
        }
    }
}
