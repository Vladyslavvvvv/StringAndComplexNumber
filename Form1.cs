using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringAndComplexNumber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool IsValidComplexNumber(string input)
        {
            // Divide the line by the "+" sign
            string[] parts = input.Split('+');

            // If after splitting, the string consists of two parts
            if (parts.Length == 2)
            {
                // Checking the first part for numbers
                if (parts[0].Any(char.IsDigit))
                {
                    // Checking the second part for numbers and the "i" symbol at the end
                    if (parts[1].TrimEnd().Any(char.IsDigit) && parts[1].TrimEnd().EndsWith("i"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string firstInput = textBoxFirst.Text;
            string secondInput = textBoxSecond.Text;

            // Checking the correctness of entering the first number
            if (!IsValidComplexNumber(firstInput))
            {
                MessageBox.Show("Incorrect input format for first number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Checking the correctness of entering the second number
            if (!IsValidComplexNumber(secondInput))
            {
                MessageBox.Show("Incorrect input format for second number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create complex numbers based on the entered lines
            ComplexNumber firstComplex = new ComplexNumber(firstInput);
            ComplexNumber secondComplex = new ComplexNumber(secondInput);

            // Check for equality
            bool isEqual = firstComplex.Equals(secondComplex);
            labelEquals.Text = "Equals? " + isEqual.ToString();

            // Adding numbers
            ComplexNumber sum = firstComplex.Add(secondComplex);
            labelAddition.Text = "Addition: " + sum.ToString();

            // Multiplication of numbers
            ComplexNumber product = firstComplex.Multiply(secondComplex);
            labelMultiplication.Text = "Multiplication: " + product.ToString();
        }
    }

    // Base class
    public abstract class tring
    {
        protected string value;
        protected byte length;

        // Constructor without parameters
        public tring()
        {
            value = "";
            length = 0;
        }

        // Constructor that accepts a string as parameters
        public tring(string str)
        {
            value = str;
            length = (byte)str.Length;
        }

        // Constructor that accepts a character as a parameter
        public tring(char ch)
        {
            value = ch.ToString();
            length = 1;
        }

        // Method to get the length of the string
        public byte Length()
        {
            return length;
        }

        // Abstract method to clear the string
        public abstract void Clear();

        // Overriding the ToString method to correctly display the string value
        public override string ToString()
        {
            return value;
        }
    }

    // Derived class 
    public class ComplexNumber : tring
    {
        public ComplexNumber() : base()
        {
        }

        public ComplexNumber(string str) : base(str)
        {
        }

        // Override the Clear method to clear the string
        public override void Clear()
        {
            value = "";
            length = 0;
        }

        // Check for equality
        public bool Equals(ComplexNumber other)
        {
            return this.value.Equals(other.value);
        }

        // Adding numbers
        public ComplexNumber Add(ComplexNumber other)
        {
            double realPart1 = ExtractRealPart(this.value);
            double imagPart1 = ExtractImaginaryPart(this.value);
            double realPart2 = ExtractRealPart(other.value);
            double imagPart2 = ExtractImaginaryPart(other.value);

            double resultReal = realPart1 + realPart2;
            double resultImag = imagPart1 + imagPart2;

            return new ComplexNumber(resultReal.ToString() + "+" + resultImag.ToString() + "i");
        }

        // Multiplication of numbers
        public ComplexNumber Multiply(ComplexNumber other)
        {
            double realPart1 = ExtractRealPart(this.value);
            double imagPart1 = ExtractImaginaryPart(this.value);
            double realPart2 = ExtractRealPart(other.value);
            double imagPart2 = ExtractImaginaryPart(other.value);

            double resultReal = (realPart1 * realPart2) - (imagPart1 * imagPart2);
            double resultImag = (realPart1 * imagPart2) + (imagPart1 * realPart2);

            return new ComplexNumber(resultReal.ToString() + "+" + resultImag.ToString() + "i");
        }

        // Extracting the real part of a complex number
        private double ExtractRealPart(string complexNumber)
        {
            string[] parts = complexNumber.Split('+');
            return double.Parse(parts[0]);
        }

        // Extracting the imaginary part of a complex number
        private double ExtractImaginaryPart(string complexNumber)
        {
            string[] parts = complexNumber.Split('+');
            string imagPart = parts[1].Substring(0, parts[1].Length - 1);
            return double.Parse(imagPart);
        }

        // Overriding the ToString method to correctly display the value of a complex number
        public override string ToString()
        {
            return value;
        }
    }
}