using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string selectedValue = "";
        double firstNumber;
        double secondNumber;
        double result;
        string selectedOperator;
        public MainWindow()
        {
            InitializeComponent();
            ACbtn.Click += ACbtn_Click;
            NEGbtn.Click += NEGBtn_Click;
            EQbtn.Click += EQbtn_Click;
            PERCbtn.Click += PERCbtn_Click;
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            selectedOperator = (sender as Button).Content.ToString();
            string currentContent = dspl.Content.ToString();

            // If last char already an operator → replace
            if (currentContent.EndsWith("+") || currentContent.EndsWith("-") ||
                currentContent.EndsWith("*") || currentContent.EndsWith("/"))
            {
                dspl.Content = currentContent.Remove(currentContent.Length - 1) + selectedOperator;
                return;
            }

            // If we already have an operator → calculate first
            int operatorIndex = currentContent.LastIndexOfAny(new char[] { '+', '-', '*', '/' });
            if (operatorIndex > 0)
            {
                // Call result logic
                EQbtn_Click(null, null);
                dspl.Content += selectedOperator;
            }
            else
            {
                // Just append operator
                dspl.Content += selectedOperator;
            }
        }

        private void PERCbtn_Click(object sender, RoutedEventArgs e)
        {
            dspl.Content = (double.Parse(dspl.Content.ToString()) / 100).ToString();
        }

        private void EQbtn_Click(object sender, RoutedEventArgs e)
        {
            string currentContent = dspl.Content.ToString();
            int operatorIndex = currentContent.LastIndexOfAny(new char[] { '+', '-', '*', '/' });

            if (operatorIndex > 0) // Means we have something like "12+5"
            {
                firstNumber = double.Parse(currentContent.Substring(0, operatorIndex));
                secondNumber = double.Parse(currentContent.Substring(operatorIndex + 1));
                string op = currentContent[operatorIndex].ToString();

                switch (op)
                {
                    case "+": result = firstNumber + secondNumber; break;
                    case "-": result = firstNumber - secondNumber; break;
                    case "*": result = firstNumber * secondNumber; break;
                    case "/":
                        if (secondNumber != 0)
                            result = firstNumber / secondNumber;
                        else
                        {
                            MessageBox.Show("Cannot divide by zero");
                            return;
                        }
                        break;
                }

                dspl.Content = result.ToString();
            }
        }

        private void NEGBtn_Click(object sender, RoutedEventArgs e)
        {
            dspl.Content = (double.Parse(dspl.Content.ToString()) * -1).ToString();
        }


        private void ACbtn_Click(object sender, RoutedEventArgs e)
        {
            dspl.Content = "0";
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedValue = (sender as Button).Content.ToString();

            string currentContent = dspl.Content.ToString();

            if (currentContent == "0" && selectedValue == ".")
            {
                dspl.Content = "0.";
                return;
            }

            if (currentContent == "0" && selectedValue != ".")
            {
                dspl.Content = selectedValue;
                return;
            }

            if (currentContent.Contains(".") && selectedValue == ".")
            {
                return;
            }

            dspl.Content += selectedValue;

        }


    }

}
