using HomeWork_7.Struct;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        private CommandForm _importFile;
        private CommandForm _openFile;
        private CommandForm _saveFile;
        private CommandForm _saveNote;
        private CommandForm _contextSelected;

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
        //Сохранить в файл. Комманда.
        public CommandForm SaveFile
        {
            get
            {
                return _saveFile ?? (_saveFile = new CommandForm(obj => { saveFile(); })); //Возврращаем либо null либо метод для выполнения
            }
        }
        //Открыть в файл. Комманда.
        public CommandForm OpenFile
        {
            get
            {
                return _openFile ?? (_openFile = new CommandForm(obj => { openFile(); })); //Возврращаем либо null либо метод для выполнения
            }
        }
        //Сохранить в файл. Комманда.
        public CommandForm ImportFile
        {
            get
            {
                return _importFile ?? (_importFile = new CommandForm(obj => { importFile(); })); //Возврращаем либо null либо метод для выполнения
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

        //Открытть файл
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();        
            openFileDialog.Filter = "\"Файлы pзаметок (*.ndb)|*.ndb;\"";             //Делаем фильтр по файлу
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;       //Указываем деректоию прогаммы.

            
            if (openFileDialog.ShowDialog().GetValueOrDefault(false)) //Повека что указан файл который надо пррочитать.
            {
                Notes.Clear();                    //Т.к. откытие файла то надо очистить сначала.
                LoadFromFile(openFileDialog.FileName);
            }

        }

        //Сохранить в файл
        private void saveFile()
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "\"Файлы pзаметок (*.ndb)|*.ndb;\"";           //Делаем фильтр по файлу
            SaveFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;          //Указываем деректоию прогаммы.
            if (SaveFile.ShowDialog().GetValueOrDefault(false)) //Повека что указан файл для сохранения.
                SavedToFile(Notes, SaveFile.FileName);
        }

        //Импорт из файла
        private void importFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "\"Файлы pзаметок (*.ndb)|*.ndb;\"";             //Делаем фильтр по файлу
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;       //Указываем деректоию прогаммы.

            if (openFileDialog.ShowDialog().GetValueOrDefault(false)) //Повека что указан файл который надо пррочитать.
            {
                LoadFromFile(openFileDialog.FileName);               //А можно напямую сделать.
            }
        }

        //Импорт из файла по дате
        private void importFileToDate()
        {

        }




        //Честно скопированный из интернета код.
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }







        /// <summary>
        /// Сохраняем в файл список заметок.
        /// </summary>
        /// <param name="notes">Список заметок.</param>
        /// <param name="FileName">Полный путь до файла.</param>
        private void SavedToFile(ObservableCollection<SNote> notes, string FileName)
        {
            StringBuilder sb = new StringBuilder();     //Билдер стоки.
            byte[] buff;         //Буфе записи
            if(File.Exists(FileName))             //Удаляем файл если он существует. Подазумиваем именно пеезапись файла, так что старый не нужен.
                File.Delete(FileName);

            using (FileStream fs = File.Open(FileName,FileMode.Create))         //Создаем поток к указанному файлу с его созданием
            {
                using(var writer = new StreamWriter(fs))                  //Поток записи
                {
                    //Походимся по всем заметкам и пиобрразуем их в стоку
                    foreach (var note in notes)             
                    {
                        sb.Clear(); //Очищаем билдер перед формированием новой строки

                        //Делаем сохрранение в той же последовательности что и конструктор. 
                        sb.Append(note.Header + ";");
                        sb.Append(note.Text + ";");
                        sb.Append(note.Duration + ";");
                        sb.Append(note.DateNote + ";");
                        sb.Append(note.RepiteNote.Name + ";");
                        sb.Append(note.FlagNote.Name + ";");
                        sb.Append(note.PriorityNote.Name + ";");

                        buff = System.Text.Encoding.Default.GetBytes(sb.ToString()+"\n");           //Стоку переводим в массив байт.
                        fs.Write(buff, 0, buff.Length);                      //Записываем массив байт в файл.
                    }
                }
            }
        }

        /*private ObservableCollection<SNote> ImportFromFile(string FileName, DateTime startFilter, DateTime stopFilter, bool FilterOn )
        {

            return notes;
        }*/

        private void LoadFromFile(string FileName)
        {
            //var resoult = notes;
            string[] NoteLines = File.ReadAllLines(FileName);
            for(int index = 0;index < NoteLines.Length;index++ )
            {
                string[] noteString = NoteLines[index].Split(';');
                //Проверка на глупость.Если менее 4 пааметров то выдаем сообщение и пропускаем.
                if (noteString.Length < 7) 
                {
                    MessageBox.Show("Найдена запись не соответствующая заметке.");
                    continue;
                }
                Notes.Add(new SNote(noteString[0], noteString[1],TimeSpan.Parse(noteString[2]),DateTime.Parse(noteString[3]),
                    (ERepite)Enum.Parse(typeof(ERepite), noteString[4], true),                     //Очень странный на мой взгляд парсинг Enum.
                    (EFlag)Enum.Parse(typeof(EFlag), noteString[5], true) ,
                    (EPriority)Enum.Parse(typeof(EPriority), noteString[6], true)));
            }
            //return resoult;
        }
    }
}
