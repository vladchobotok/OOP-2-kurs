using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        Triangle triangle;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "0";
            label4.Text = "0";
            label5.Text = "0";
            label6.Text = "0";
            label7.Text = "0";
            try
            {
                triangle = new Triangle();
                if(Convert.ToDouble(textBox1.Text) <= 0 || Convert.ToDouble(textBox2.Text) <= 0 || Convert.ToDouble(textBox3.Text) <= 0 ||
                   (Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox2.Text) <= Convert.ToDouble(textBox3.Text)) ||
                   (Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox3.Text) <= Convert.ToDouble(textBox2.Text)) ||
                   (Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox2.Text) <= Convert.ToDouble(textBox1.Text)))
                {
                    MessageBox.Show("This triangle is not possible!", "Error");
                }
                else
                {
                    triangle.createTriangle(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text));
                    triangle.calculateAngleBetweenAandB();
                    triangle.calculateAngleBetweenBandC();
                    triangle.calculateAngleBetweenCandA();
                    triangle.calculatePerimeter();
                    MessageBox.Show("Triangle was successfuly created!", "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex, "Error");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "0";
            label4.Text = "0";
            label5.Text = "0";
            label6.Text = "0";
            label7.Text = "0";
            try
            {
                triangle = new EquilateralTriangle();
                if (Convert.ToDouble(textBox4.Text) <= 0)
                {
                    MessageBox.Show("This triangle is not possible!", "Error");
                }
                else
                {
                    triangle.createTriangle(Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox4.Text));
                    triangle.calculateAngleBetweenAandB();
                    triangle.calculateAngleBetweenBandC();
                    triangle.calculateAngleBetweenCandA();
                    triangle.calculatePerimeter();
                    ((EquilateralTriangle)triangle).calculateSquare();
                    MessageBox.Show("Triangle was successfuly created!", "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex, "Error");
            }      
        }
        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = Convert.ToString(triangle.getAngleBetweenAandB());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Text = Convert.ToString(triangle.getAngleBetweenBandC());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label5.Text = Convert.ToString(triangle.getAngleBetweenCandA());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label6.Text = Convert.ToString(triangle.getPerimeter());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(triangle is EquilateralTriangle)
            {
                label7.Text = Convert.ToString(((EquilateralTriangle)triangle).getSquare());
            }
            else
            {
                MessageBox.Show("Triangle class does not contains calculateSquare method!", "Error");
            } 
        }
    }
}
