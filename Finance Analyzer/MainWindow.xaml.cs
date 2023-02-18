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

namespace Finance_Analyzer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "Analyse_Button":
                    Analyse();
                    break;
                case "Exit_Button":
                    CloseApplication();
                    break;
                case "AddIncome_Button":
                    AddIncome();
                    break;
                case "AddExpenses_Button":
                    AddExpens();
                    break;
                case "DeleteExpens_Button":
                    RemoveExpense();
                    break;
                case "DeleteIncome_Button":
                    RemoveIncome();
                    break;
            }
        }


        //Вспомогательные методы
        //__________________________________________________________________________________________________________________________

        private bool CheckIsNumberString(string Text)
        {
            if (double.TryParse(Text, out var number)) return true;
            return false;
        }


        //__________________________________________________________________________________________________________________________





        //Методы - функционал кнопок
        //__________________________________________________________________________________________________________________________
        private void CloseApplication()
        {
            Close();
        }
        private void AddIncome()
        {
            //Income_TextBox.Text;
            if(CheckIsNumberString(Income_TextBox.Text) && DateIncome_DatePicker.Text != "")
                AllIncomes_ListView.Items.Add(Income_TextBox.Text + " — " + DateIncome_DatePicker.Text);
            else
            MessageBox.Show("Не корректные данные", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void AddExpens()
        {
            if (CheckIsNumberString(Expenses_TextBox.Text) && DateExpens_DatePicker.Text != "")
                AllExpenses_ListView.Items.Add(Expenses_TextBox.Text + " — " + DateExpens_DatePicker.Text);
            else
                MessageBox.Show("Не корректные данные", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void RemoveIncome()
        {
            AllIncomes_ListView.Items.Remove(AllIncomes_ListView.SelectedItem);
        }
        private void RemoveExpense()
        {
            AllExpenses_ListView.Items.Remove(AllExpenses_ListView.SelectedItem);
        }

        private void Analyse()
        {
            double fullIncomes = 0;
            foreach (string text in AllIncomes_ListView.Items)
            {
                string[] ArrayString = text.Split('—').ToArray();
                fullIncomes += double.Parse(ArrayString[0].Trim());
            }

            double fullExpenses = 0;
            foreach (string text in AllExpenses_ListView.Items)
            {
                string[] ArrayString = text.Split('—').ToArray();
                fullExpenses += double.Parse(ArrayString[0].Trim());
            }
            string Balance = "";
            if(fullIncomes-fullExpenses == 0)
            {
                Balance = "Нулевое";
            }
            else if(fullIncomes - fullExpenses < 0)
            {
                Balance = "Отрицательное";
            }
            else
            {
                Balance = "Положительное";
            }

            ResultWindow result = new ResultWindow();
            result.AllIncome_TextBlock.Text = fullIncomes.ToString();
            result.AllExpense_TextBlock.Text = fullExpenses.ToString();
            result.Balance_TextBlock.Text = Balance;
            result.Balance_In_Numbers_TextBlock.Text = (fullIncomes - fullExpenses).ToString();
            result.Show();
        }


        //__________________________________________________________________________________________________________________________
    }
}
