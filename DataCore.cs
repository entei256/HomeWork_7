using HomeWork_7.Struct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using SNote = HomeWork_7.Struct.Note;

namespace HomeWork_7
{
    /// <summary>
    /// Логика аналог. ViewModel
    /// </summary>
    class DataCore : INotifyPropertyChanged
    {
        private CommandForm _addNote;
        private CommandForm _removeNote;
        private CommandForm _filterDate;
        private CommandForm _openFile;
        private CommandForm _saveFile;
        private CommandForm _saveNote;


        public event PropertyChangedEventHandler PropertyChanged;  //Проперти событий для MVVM

        private SNote _SelectedNote;  //Будем записывать выделенный элемент

        public ObservableCollection<SNote> Notes { get; set; }  //Коллекция заметок

        //Метод определения выделенной заметки.
        public SNote SelectedNote
        {
            get { return _SelectedNote; }
            set
            {
                _SelectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }

        //Добавить заметку. Команда.
        public CommandForm AddNote
        {
            get
            {
                return _addNote ?? (_addNote = new CommandForm(obj => { addNote(); })); //Возврращаем либо null либо метод для выполнения
            }
        }
        //Удалить заметку. Команда.
        public CommandForm RemoveNote
        {
            get
            {
                return _removeNote ?? (_removeNote = new CommandForm(obj => { removeNote(); })); //Возврращаем либо null либо метод для выполнения
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



        //Метод добовления заметки.
        private void addNote()
        {
            Notes.Add(new SNote("Введите название заметки", "Описание заметки", TimeSpan.FromMinutes(30), DateTime.Now)); ///костыль т.к. видать не очень хорошо продумал модель данных. Да и чеез стуктуы станно это делать.
        }

        //Удаление заметки. 
        private void removeNote()
        {
            //Я не знаю какого не работает Notes.Remove(SelectedNote); так то костылим.
            foreach (var note in Notes)
            {
                if (note.ID == SelectedNote.ID)
                {
                    Notes.Remove(note);
                    break;
                }
            }
            SelectedNote = new SNote();
        }

        private void openFile()
        {

        }







        //Честно скопированный из интернета код.
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
