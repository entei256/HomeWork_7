using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HomeWork_7
{
    namespace Struct
    {
        
        /// <summary>
        /// Структура для заметок.
        /// </summary>
        struct Note : INotifyPropertyChanged
        {
            private string _Header;  //Название/заголовок
            private string _Text;  //Текст заметки
            private TimeSpan? _Duration;  //Длительность
            private DateTime? _DateNote;  //Дата начала
            private Repite _RepiteNote; //Дата повторения
            private Flags _FlagNote;  //Группа
            private Priority _PriorityNote;  //Приоритет

            //public int NumberNote { get; private set; }  //Номер заметки
            public string Header 
            { 
                get { return this._Header; } 
                set { this._Header = value; OnPropertyChanged("Header"); } 
            }  //Название/заголовок
            public string Text 
            { 
                get { return this._Text; } 
                set { this._Text = value; OnPropertyChanged("Text"); } 
            }  //Текст заметки
            public TimeSpan? Duration 
            { 
                get { return this._Duration; } 
                set { this._Duration = value; OnPropertyChanged("Duration"); } 
            }  //Длительность
            public DateTime? DateNote 
            { 
                get { return this._DateNote; } 
                set { this._DateNote = value; OnPropertyChanged("Date"); } 
            }  //Дата начала
            public Repite RepiteNote 
            { 
                get { return this._RepiteNote; } 
                set { this._RepiteNote = value; OnPropertyChanged("Repite"); } 
            }  //Дата повторения
            public Flags FlagNote 
            { 
                get { return this._FlagNote; } 
                set { this._FlagNote = value; OnPropertyChanged("Flag"); } 
            }  //Группа
            public Priority PriorityNote 
            { 
                get { return this._PriorityNote; } 
                set { this._PriorityNote = value; OnPropertyChanged("Priority"); } 
            }  //Приоритет

            public event PropertyChangedEventHandler PropertyChanged;  //Событие для паттерна MVVM

            //Основной конструктор.
            public Note(string header, string text, TimeSpan duration, DateTime StartDate , 
                ERepite repite = ERepite.Никогда, EFlag Group = EFlag.Избранные, EPriority priority = EPriority.Низкий)
            {
                //this.NumberNote = Number;
                this._Header = header;
                this._Text = text;
                this._Duration = duration;
                this._DateNote = StartDate;
                this._RepiteNote = new Repite(repite);
                this._FlagNote = new Flags(Group);
                this._PriorityNote = new Priority(priority);
                PropertyChanged = null;

                OnPropertyChanged("Note");
            }

            
            
            
            //Честно скопированный из инета код. Надо разобраться как он работает.
            public void OnPropertyChanged([CallerMemberName] string prop = "")  
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        //структура для признака повтора заметки.
        struct Repite
        {
            private ERepite _Name; //переменная для проперти.
            public TimeSpan RepetitionPeriod { private set; get; }  //Будет менять только если меняется название.
            public ERepite Name 
            {
                get {return this._Name;}
                //Пришлось написать сеттер отдельно, т.к. нужно что бы значение обновлялось.
                set 
                {
                    this._Name = value;
                    switch (value)
                    {
                        case ERepite.Никогда:
                            this.RepetitionPeriod = TimeSpan.Zero;
                            break;
                        case ERepite.Ежедневно:
                            this.RepetitionPeriod = TimeSpan.FromDays(1);
                            break;
                        case ERepite.Ежеквартально:
                            this.RepetitionPeriod = TimeSpan.FromDays(90); //для простоты беру что в квартале 90 дней.
                            break;
                        case ERepite.Ежемесячно:
                            this.RepetitionPeriod = TimeSpan.FromDays(30); //для простоты беру что в месяце 90 дней.
                            break;
                        case ERepite.Ежегодно:
                            this.RepetitionPeriod = TimeSpan.FromDays(365);
                            break;
                        default:
                            this.RepetitionPeriod = TimeSpan.Zero;
                            new ArgumentOutOfRangeException();
                            break;
                    }
                }
            }

            //В зависимости от имени выставляем период для повторения
            public Repite(ERepite eRepite)
            {
                this.RepetitionPeriod = TimeSpan.Zero;
                this._Name = eRepite;
                /*switch (eRepite)
                {
                    case ERepite.Никогда:
                        this.RepetitionPeriod = TimeSpan.Zero;
                        break;
                    case ERepite.Ежедневно:
                        this.RepetitionPeriod = TimeSpan.FromDays(1);
                        break;
                    case ERepite.Ежеквартально:
                        this.RepetitionPeriod = TimeSpan.FromDays(90); //для простоты беру что в квартале 90 дней.
                        break;
                    case ERepite.Ежемесячно:
                        this.RepetitionPeriod  = TimeSpan.FromDays(30); //для простоты беру что в месяце 90 дней.
                        break;
                    case ERepite.Ежегодно:
                        this.RepetitionPeriod = TimeSpan.FromDays(365);
                        break;
                    default:
                        this.RepetitionPeriod = TimeSpan.Zero;
                        new ArgumentOutOfRangeException();
                        break;
                }*/

            }
        }

        //Структура групп.
        struct Flags
        {
            private EFlag _Name;
            public EFlag Name 
            {
                get { return _Name; }
                //При изменении названия группы, меняем картинку.
                set 
                { 
                    this._Name = value;
                    switch (value)
                    {
                        case EFlag.Работа:
                            Image = BitmapFrame.Create(new Uri("./Resourse/PNG/job_flag.png", UriKind.Relative));
                            break;
                        case EFlag.Личные:
                            Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Home_flag.png", UriKind.Relative));
                            break;
                        case EFlag.Избранные:
                            Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Favorite_flag.png", UriKind.Relative));
                            break;
                        default:
                            new ArgumentOutOfRangeException();
                            Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Cros.png", UriKind.Relative));
                            break;
                    }
                } 
            }
            public ImageSource Image { get; set; }
            //Основной конструктор.
            public Flags(EFlag eFlag)
            {
                Image = BitmapFrame.Create(new Uri(@"./Resourse/PNG/Cros.png", UriKind.Relative)) ;
                this._Name = eFlag;
            }
        }

        //Структура приоритетов.
        struct Priority
        {
            private EPriority _Name;
            public EPriority Name 
            {
                get { return this._Name; }
                set 
                {
                    this.Name = value;

                    switch (value)
                    {
                        case EPriority.Высокий:
                            this.Color = Brushes.OrangeRed;
                            this.Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Hight_Priority.png", UriKind.Relative));
                            break;
                        case EPriority.Низкий:
                            this.Color = Brushes.Aqua;
                            this.Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Hight_Priority.png", UriKind.Relative));
                            break;
                        case EPriority.Средний:
                            this.Color = Brushes.Orange;
                            this.Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Hight_Priority.png", UriKind.Relative));
                            break;
                        default:
                            new ArgumentOutOfRangeException();
                            this.Color = Brushes.White;
                            this.Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Cros.png", UriKind.Relative));
                            break;
                    }
                }
            }
            public Brush Color {get; private set;}
            public ImageSource Image {get; private set;}

            public Priority(EPriority ePriority)
            {
                this.Color = Brushes.White;
                this.Image = BitmapFrame.Create(new Uri("./Resourse/PNG/Cros.png",UriKind.Relative));
                this._Name = ePriority;
            }

        }

        //Список Групп.
        enum EFlag
        {
            Избранные = 0,
            Работа, 
            Личные
        }
        //Список приоритетов.
        enum EPriority
        {
            Низкий = 0,
            Средний,
            Высокий
        }
        //Список повторений.
        enum ERepite
        {
            Никогда = 0,
            Ежедневно,
            Ежемесячно,
            Ежеквартально,
            Ежегодно
        }
    }
}
