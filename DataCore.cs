using HomeWork_7.Struct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SNote = HomeWork_7.Struct.Note;

namespace HomeWork_7
{
    class DataCore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;  //Проперти событий для MVVM

        private SNote _SelectedNote;  //Будем записывать выделенный элемент(Точнее помещать ссылку на него)
        public ObservableCollection<SNote> Notes { get; set; }

        //Метод определения выделенного элемента.
        public SNote SelectedNote
        {
            get { return _SelectedNote; }
            set
            {
                _SelectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }



        /// <summary>
        /// Тестовые данные.
        /// </summary>
        public DataCore()
        {
            Notes = new ObservableCollection<SNote>
            {
                new SNote("Первая заметка","Какой-то текст 1",TimeSpan.FromMinutes(30),DateTime.Now),
                new SNote("Вторая заметка","Какой-то текст 2",TimeSpan.FromMinutes(30),DateTime.Now,Struct.ERepite.Ежеквартально),
                new SNote("Третья заметка","Какой-то текст 3",TimeSpan.FromMinutes(30),DateTime.Now,Struct.ERepite.Никогда,EFlag.Личные, EPriority.Высокий),
                new SNote("Четвертая заметка","Какой-то текст 4",TimeSpan.FromMinutes(30),DateTime.Now,Struct.ERepite.Ежегодно,EFlag.Работа, EPriority.Средний),
                new SNote("Пятая заметка","Какой-то текст 5",TimeSpan.FromMinutes(30),DateTime.Now,Struct.ERepite.Ежедневно,EFlag.Личные, EPriority.Низкий),
                new SNote("Шестая заметка","Какой-то текст 6",TimeSpan.FromMinutes(30),DateTime.Now,Struct.ERepite.Ежемесячно,EFlag.Избранные, EPriority.Высокий),
                new SNote("Седьмая заметка","Какой-то текст 7",TimeSpan.FromMinutes(30),DateTime.Now,Struct.ERepite.Никогда,EFlag.Личные, EPriority.Средний),
                new SNote("Восьмая заметка","Какой-то текст 8",TimeSpan.FromMinutes(30),DateTime.Now),
                new SNote("Девятая заметка","Какой-то текст 9",TimeSpan.FromMinutes(30),DateTime.Now),
                new SNote("Десятая заметка","Какой-то текст 10",TimeSpan.FromMinutes(30),DateTime.Now)
            };
        }





        //Честно скопированный из интернета код.
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
