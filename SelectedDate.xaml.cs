using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomeWork_7
{
    /// <summary>
    /// Логика взаимодействия для SelectedDate.xaml
    /// Это форма костыль для выбора дат. Лучше варианта не придумал.
    /// </summary>
    public partial class SelectedDate : Window
    {
        public bool ClosedForm { get; set; } = false;
        public SelectedDate()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DataCore.StartSelectDate = this.SelectedCal.SelectedDates[0]; //в дату начала записываем первый элемент массива.
            //в дату окончания записываем последний элемент массива. + добовляем 23 часа 59 минут 59 секунд
            DataCore.EndSelectDate = this.SelectedCal.SelectedDates[SelectedCal.SelectedDates.Count - 1].AddHours(23)
                .AddMinutes(59).AddSeconds(59).AddMilliseconds(999); 
            this.Close(); //закрываем форму.
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClosedForm = true;
            this.Close(); //закрываем форму.
        }
    }
}
