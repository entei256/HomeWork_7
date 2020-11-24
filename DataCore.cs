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
        private CommandForm _importFileToDate;

        static public DateTime StartSelectDate { get; set; }
        static public DateTime EndSelectDate { get; set; }
        public ObservableCollection<SNote> Notes { get; set; } = new ObservableCollection<SNote>();  //Коллекция заметок
        public event PropertyChangedEventHandler PropertyChanged;  //Проперти событий для MVVM
        private SNote _SelectedNote;  //Будем записывать выделенный элемент
        private int _SelectedNoteIndex;  //Будем записывать выделенный элемент


        //Метод определения выделенной заметки.
        public int SelectedNoteIndex
        {
            get { return _SelectedNoteIndex; }
            set
            {
                _SelectedNoteIndex = value;
                OnPropertyChanged("SelectedNote");
            }
        }
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
        //Сохранение заметки. Комманда.
        public CommandForm SaveNote
        {
            get
            {
                return _saveNote ?? (_saveNote = new CommandForm(obj => { saveNote(); })); //Возврращаем либо null либо метод для выполнения
            }
        }
        //Импорт по датам.Комманда.
        public CommandForm ImportFromDate
        {
            get
            {
                return _importFileToDate ?? (_importFileToDate = new CommandForm(obj => { importFileToDate(); })); //Возврращаем либо null либо метод для выполнения
            }
        }




        //Метод добовления заметки.
        private void addNote()
        {
            if (Notes == null)
                Notes = new ObservableCollection<SNote>();
            Notes.Add(new SNote("Введите название заметки", "Описание заметки", TimeSpan.FromMinutes(30), DateTime.Now)); ///костыль т.к. видать не очень хорошо продумал модель данных. Да и чеез стуктуы станно это делать.
        }

        //Удаление заметки. 
        private void removeNote()
        {
            if (Notes.Count <= 0)  //если заметок нет, то просто возвращаемся.
                return;

            Notes.RemoveAt(SelectedNoteIndex);  //Удаляем по индексу
            SelectedNote = new SNote();
        }

        //Открытть файл
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();        
            openFileDialog.Filter = "\"Файлы pзаметок (*.ndb)|*.ndb;\"";             //Делаем фильтр по файлу
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;       //Указываем деректоию прогаммы.

            
            if (openFileDialog.ShowDialog().GetValueOrDefault(false)) //Повека что указан файл который надо прочитать.
            {
                if (Notes.Count > 0)
                    Notes.Clear();                   //Т.к. откытие файла то надо очистить сначала.
                LoadFromFile(openFileDialog.FileName);   //Добовляем в колллекцию список поллученный из файла.
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

            if (openFileDialog.ShowDialog().GetValueOrDefault(false)) //Повека что указан файл который надо прочитать.
            {
                LoadFromFile(openFileDialog.FileName);       //Сделано через откытие файла. Что бы не громаздить метот импорта.       
            }

        }

        
        //Импорт из файла по дате
        private void importFileToDate()
        {
            SelectedDate sd = new SelectedDate();
            sd.ShowDialog();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "\"Файлы pзаметок (*.ndb)|*.ndb;\"";             //Делаем фильтр по файлу
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;       //Указываем деректоию прогаммы.


            if (openFileDialog.ShowDialog().GetValueOrDefault(false)) //Повека что указан файл который надо прочитать.
            {
                LoadFromFileFromDate(openFileDialog.FileName,StartSelectDate,EndSelectDate);
            }
        }

        //TODO:
        //Сохранения текущей заметки.
        private void saveNote()
        {
            for (int iter = 0; iter < Notes.Count;iter++)
            {
                if(Notes[iter].ID == SelectedNote.ID)
                {
                   // Notes[iter].Duration = SelectedNote.Duration;

                }
            }
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

        /// <summary>
        /// Импорт из файла с фильтром по дате.
        /// </summary>
        /// <param name="FileName">Указать путь к файлу.</param>
        /// <param name="startDate">Дата начала фильтра</param>
        /// <param name="endDate">Дата оканчания фильтра.</param>
        private void LoadFromFileFromDate(string FileName,DateTime startDate, DateTime endDate)
        {
            string[] NoteLines = File.ReadAllLines(FileName);  //Получаем все строки из файла
            for (int index = 0; index < NoteLines.Length; index++)
            {
                string[] noteString = NoteLines[index].Split(';');
                //Проверка на глупость.Если менее 4 пааметров то выдаем сообщение и пропускаем.
                if (noteString.Length < 7)
                {
                    MessageBox.Show("Найдена запись не соответствующая заметке.");
                    continue;
                }
                // проверяем что заметкка Входит в диапозон дат. 
                if (DateTime.Parse(noteString[3]) > startDate && DateTime.Parse(noteString[3]) < endDate)  
                {
                    //Если попадаетв диапозон то добовляем заметку.
                    Notes.Add(new SNote(noteString[0], noteString[1], TimeSpan.Parse(noteString[2]), DateTime.Parse(noteString[3]),
                          (ERepite)Enum.Parse(typeof(ERepite), noteString[4], true),                     //Очень странный на мой взгляд парсинг Enum.
                          (EFlag)Enum.Parse(typeof(EFlag), noteString[5], true),
                          (EPriority)Enum.Parse(typeof(EPriority), noteString[6], true)));
                }
            }
        }

        /// <summary>
        /// Парсинг файла и добовлление к коллекции.
        /// </summary>
        /// <param name="FileName">Указать путь к файлу.</param>
        private void LoadFromFile(string FileName)
        {
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
                //Добовляем заметку.
                Notes.Add(new SNote(noteString[0], noteString[1],TimeSpan.Parse(noteString[2]),DateTime.Parse(noteString[3]),
                    (ERepite)Enum.Parse(typeof(ERepite), noteString[4], true),                     //Очень странный на мой взгляд парсинг Enum.
                    (EFlag)Enum.Parse(typeof(EFlag), noteString[5], true) ,
                    (EPriority)Enum.Parse(typeof(EPriority), noteString[6], true)));
            }
        }
    }
}
