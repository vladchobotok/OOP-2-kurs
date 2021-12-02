using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        Figure figure;
        int figureIndex;
        RadioButton selectedRb;
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            selectedRb = radioButton1;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            if (rb.Checked)
            {
                selectedRb = rb;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                figureIndex = 1;
            }  
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            if (rb.Checked)
            {
                selectedRb = rb;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                figureIndex = 2;
            }
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            if (rb.Checked)
            {
                selectedRb = rb;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = true;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = true;
                figureIndex = 3;
            }     
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            if (rb.Checked)
            {
                selectedRb = rb;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = true;
                radioButton5.Checked = false;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                figureIndex = 4;
            }   
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            if (rb.Checked)
            {
                selectedRb = rb;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = true;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = true;
                figureIndex = 5;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                    switch (figureIndex)
                    {
                        case 1:
                            if (Convert.ToDouble(textBox1.Text) <= 0 || Convert.ToDouble(textBox2.Text) <= 0 || Convert.ToDouble(textBox3.Text) <= 0 ||
                               (Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox2.Text) <= Convert.ToDouble(textBox3.Text)) ||
                               (Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox3.Text) <= Convert.ToDouble(textBox2.Text)) ||
                               (Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox2.Text) <= Convert.ToDouble(textBox1.Text)))
                            {
                                MessageBox.Show("This triangle is not possible!", "Error");
                            }
                            figure = new Triangle(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text));
                            break;
                        case 2:
                            if (Convert.ToDouble(textBox1.Text) <= 0)
                            {
                                MessageBox.Show("This circle is not possible!", "Error");
                            }
                            figure = new Circle(Convert.ToDouble(textBox1.Text));
                            break;
                        case 3:
                            if (Convert.ToDouble(textBox1.Text) <= 0 || Convert.ToDouble(textBox2.Text) <= 0)
                            {
                                MessageBox.Show("This rectangle is not possible!", "Error");
                            }
                            figure = new Rectangle(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
                            break;
                        case 4:
                            if (Convert.ToDouble(textBox1.Text) <= 0)
                            {
                                MessageBox.Show("This square is not possible!", "Error");
                            }
                            figure = new Square(Convert.ToDouble(textBox1.Text));
                            break;
                        case 5:
                            if (Convert.ToDouble(textBox1.Text) <= 0 || Convert.ToDouble(textBox2.Text) <= 0)
                            {
                                MessageBox.Show("This rhombus is not possible!", "Error");
                            }
                            figure = new Rhombus(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
                            break;
                    }
                    MessageBox.Show("Figure was successfuly created!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(figure.calculatePerimeter());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(figure.calculateSquare());
        }
    }
}
