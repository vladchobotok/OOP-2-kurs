using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(textBox1.Text);
                double b = Convert.ToDouble(textBox2.Text);
                double angle = Convert.ToDouble(textBox3.Text);
                if (Convert.ToDouble(textBox1.Text) <= 0 || Convert.ToDouble(textBox2.Text) <= 0 || Convert.ToDouble(textBox3.Text) <= 0)
                {
                    MessageBox.Show("This triangle is not possible!", "Error");
                }
                else
                {
                    if (angle == 90)
                    {
                        Triangle triangle = new RectangularTriangle();
                        label1.Text = triangle.calculatePerimeter(a, b, angle);
                        label2.Text = triangle.calculateSquare(a, b, angle);
                    }
                    else if (a == b)
                    {
                        Triangle triangle = new IsoscelesTriangle();
                        label1.Text = triangle.calculatePerimeter(a, b, angle);
                        label2.Text = triangle.calculateSquare(a, b, angle);
                    }
                    else
                    {
                        Triangle triangle = new OtherTriangle();
                        label1.Text = triangle.calculatePerimeter(a, b, angle);
                        label2.Text = triangle.calculateSquare(a, b, angle);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex, "Error");
            }
        }
    }
}