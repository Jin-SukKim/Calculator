using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    //Purpose: Creating Calculator by using c#
    //Input: clicking buttons or keyboard input
    //output: calculated values
    //Author: Jin-Suk Kim (Jason)
    //Date: Jan 24th 2019
    public partial class Form1 : Form
    {
        private double value;
        private double memory;
        private string operators = "\0";
        private bool opFlag = false;
        private bool memFlag = false;
        private bool power = false;

        public Form1()
        {
            InitializeComponent();
            mc.Enabled = false;
            mr.Enabled = false;
            KeyPreview = true;  
        }

        //Calculator power
        private void power_Click(object sender, EventArgs e)
        {
            if(power)
            {
                power = false;
                result.Text = "";
                label.Text = "";
            } else
            {
                power = true;
                result.Text = "0";
            }
        }

        //number button click event handler
        private void button_Click(object sender, EventArgs e)
        {
            if(!power)
            {
                return;
            }
            Button btn = (Button)sender;
            if (result.Text == "0" || opFlag == true || memFlag == true)
            {
                result.Text = btn.Text;
                opFlag = false;
                memFlag = false;
            }
            else
                result.Text = result.Text + btn.Text;
        }

        //put dot to create decimal number
        private void dot_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            if (result.Text.Contains("."))
                return;
            else
                result.Text += ".";
        }

        //converting negative and positive values
        private void PlusMinus_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            double v = Double.Parse(result.Text);
            result.Text = (-v).ToString();
        }

        //equal (=) operation to calculate fomula
        private void calculate(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            double v = Double.Parse(result.Text);
            result.Text = label.Text + v;
            if (operators == "%")
            {
                result.Text = (value % v).ToString();
            }
            else
            {
                result.Text = new DataTable().Compute(result.Text, null).ToString();
                double x = Double.Parse(result.Text);
            }

            label.Text = "";

        
        }

        //Mathmetic equation(+,-,/,*) event handler
        private void operator_Click(object sender, EventArgs e) {
            if (!power)
            {
                return;
            }
            Button btn = (Button)sender;
            value = Double.Parse(result.Text);
            label.Text += result.Text + btn.Text;
            operators = btn.Text;
            opFlag = true;
        }

        //find remainder calculation
        private void remainder_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            label.Text += result.Text + "%";
        }

        //Squar root calculation event
        private void sqrt_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            label.Text = "";
            label.Text += "√(" + result.Text + ") ";
            result.Text = Math.Sqrt(Double.Parse(result.Text)).ToString();
        }
        
        //x squar calculation event x^2
        private void sqr_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            label.Text = "";
            label.Text += "sqr(" + result.Text + ") ";
            result.Text = (Double.Parse(result.Text) * Double.Parse(result.Text)).ToString();
        }
        
        //1/x calculation event
        private void recip_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            label.Text = "";
            label.Text += "1 / (" + result.Text + ") ";
            result.Text = (1 / Double.Parse(result.Text)).ToString();
        }

        // current value sets to 0
        private void CE_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            result.Text = "0";
        }

        // delete all
        private void C_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            result.Text = "0";
            label.Text = "";
            value = 0;
            operators = "\0";
            opFlag = false;
        }

        // delete last digit
        private void delete_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            result.Text = result.Text.Remove(result.Text.Length - 1);
            if (result.Text.Length == 0)
                result.Text = "0";
        }

        // Memory Save
        private void MS_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            memory = Double.Parse(result.Text);
            mc.Enabled = true;
            mr.Enabled = true;
            memFlag = true;
        }

        // Memory Read
        private void mr_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            result.Text = memory.ToString();
            memFlag = true;
        }

        // Memory Clear
        private void mc_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            result.Text = "0";
            memory = 0;
            mr.Enabled = false;
            mc.Enabled = false;
        }

        // M+
        private void MPlus_Click(object sender, EventArgs e)
        {
            if (!power)
            {
                return;
            }
            if(!mr.Enabled)
            {
                memory = Double.Parse(result.Text);
                mc.Enabled = true;
                mr.Enabled = true;
                memFlag = true;
            }
            else
                memory += Double.Parse(result.Text);
        }

        //keyboard key press down event
        private void keyPress_Event(object sender, KeyPressEventArgs e)
        {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    calculate(sender, e);
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    delete_Click(sender, e);
                }
                if (e.KeyChar == (char)Keys.Add)
                {
                    operator_Click(sender, e);
                }
                if (e.KeyChar == (char)Keys.Subtract)
                {
                    operator_Click(sender, e);
                }
                if (e.KeyChar == (char)Keys.Multiply)
                {
                    operator_Click(sender, e);
                }
                if (e.KeyChar == (char)Keys.Divide)
                {
                    operator_Click(sender, e);
                }
                else
                {
                result.Text = result.Text + e.KeyChar.ToString();
                label.Text = result.Text;
                }      
        }

        
    }
}
