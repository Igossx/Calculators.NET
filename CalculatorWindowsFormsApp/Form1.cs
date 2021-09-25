using System;
using System.Windows.Forms;

namespace CalculatorWindowsFormsApp
{
    public partial class Form1 : Form
    {
        private string _firstValue;
        private string _secondValue;
        private Operation _currentOperation = Operation.None;
        private bool _isTheResultOnTheScreen;

        public enum Operation
        {
            None,
            Addition,
            Subtraction,
            Division,
            Multiplication
        }

        public Form1()
        {
            InitializeComponent();
            tbScreen.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnBtnNumberClick(object sender, EventArgs e)
        {
            var clickedValue = (sender as Button).Text;

            if (tbScreen.Text == "0" && clickedValue != ",")
                tbScreen.Text = string.Empty;

            tbScreen.Text += clickedValue;

            if (_currentOperation != Operation.None)
                _secondValue += clickedValue;

            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                tbScreen.Text = string.Empty;

                if (clickedValue == ",")
                    clickedValue = "0,";
            }

            SetResultBtnState(true);

            if (_currentOperation != Operation.None)
                _secondValue += clickedValue;
            else
                SetOperationBtnState(true);

        }

        private void OnBtnOperationClick(object sender, EventArgs e)
        {
            _firstValue = tbScreen.Text;

            var operation = (sender as Button).Text;

            tbScreen.Text += $" {operation} ";

            if (_isTheResultOnTheScreen)
                _isTheResultOnTheScreen = false;

            // tutaj poprawiony switch
            switch (operation)
            {
                case "+":
                    _currentOperation = Operation.Addition;
                    break;
                case "-":
                    _currentOperation = Operation.Subtraction;
                    break;
                case "/":
                    _currentOperation = Operation.Division;
                    break;
                case "*":
                    _currentOperation = Operation.Multiplication;
                    break;
                default:
                    _currentOperation = Operation.None;
                    break;
            }

            SetOperationBtnState(false);
            SetResultBtnState(false);
        }

        private void OnBtnResultClick(object sender, EventArgs e)
        {
            if (_currentOperation == Operation.None)
                return;

            var firstNumber = double.Parse(_firstValue);
            var secondNumber = double.Parse(_secondValue);

            var result = Calculate(firstNumber, secondNumber);

            tbScreen.Text = result.ToString();

            _secondValue = string.Empty;
            _currentOperation = Operation.None;

            _isTheResultOnTheScreen = true;

            SetOperationBtnState(true);
            SetResultBtnState(true);
        }

        private void OnBtnClearClick(object sender, EventArgs e)
        {
            tbScreen.Text = "0";
            _firstValue = string.Empty;
            _secondValue = string.Empty;
            _currentOperation = Operation.None;
        }

        private double Calculate(double firstNumber, double secondNumber)
        {
            switch (_currentOperation)
            {
                case Operation.None:
                    return firstNumber;
                case Operation.Addition:
                    return firstNumber + secondNumber;
                case Operation.Subtraction:
                    return firstNumber - secondNumber;
                case Operation.Division:
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("Nie można dzielić przez 0!");
                        return 0;
                    }
                    return firstNumber / secondNumber;
                case Operation.Multiplication:
                    return firstNumber * secondNumber;
            }

            return 0;
        }

        private void SetOperationBtnState(bool value)
        {
            btnAddition.Enabled = value;
            btnMultiplication.Enabled = value;
            btnDivision.Enabled = value;
            btnSubtraction.Enabled = value;
        }

        private void SetResultBtnState(bool value)
        {
            btnResult.Enabled = value;
        }
    }
}
