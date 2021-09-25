using CalculatorWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CalculatorWpfApp.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {

        private string _screenVal;
        private List<string> _availableOperations = new List<string> { "+", "-", "/", "*" };
        private DataTable _dataTable = new DataTable();
        private bool _isLastSignAnOperation;

        public MainViewModel()
        {
            ScreenVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation, CanAddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult, CanGetResult);
        }

        private bool CanGetResult(object obj) => !_isLastSignAnOperation;

        private bool CanAddOperation(object obj) => !_isLastSignAnOperation;

        private void GetResult(object obj)
        {
            var result = Math.Round(Convert.ToDouble(_dataTable.Compute(ScreenVal.Replace(",", "."), "")), 2);

            ScreenVal = result.ToString();
        }

        private void ClearScreen(object obj)
        {
            ScreenVal = "0";

            _isLastSignAnOperation = false;
        }

        private void AddOperation(object obj)
        {
            // MessageBox.Show(obj as string);

            var operation = obj as string;

            ScreenVal += operation;

            _isLastSignAnOperation = true;
        }

        private void AddNumber(object obj)
        {
            // MessageBox.Show(obj as string);

            var number = obj as string;

            if (ScreenVal == "0" && number != ",")
                ScreenVal = string.Empty;
            else if (number == "," && _availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
                number = "0,";

            ScreenVal += number;

            _isLastSignAnOperation = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ScreenVal
        {
            get { return _screenVal; }
            set
            {
                _screenVal = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddNumberCommand { get; }

        public RelayCommand AddOperationCommand { get; }

        public RelayCommand ClearScreenCommand { get; }

        public RelayCommand GetResultCommand { get; }

    }
}
