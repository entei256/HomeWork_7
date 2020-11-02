using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace HomeWork_7
{
    namespace Struct
    {
        struct Note
        {
            public string Header { get; set; }
            public string Text { get; set; }
            public TimeSpan Time { get; set; }
            public DateTime NoteDate { get; set; }
            public int RepiteNote_id { get; set; }
            public int Flag { get; set; }

        }

        struct Repite
        {
            static public int Id { get; set; }
            public string Name { get; set; }
            public TimeSpan RepetitionPeriod { get; set; }
        }

        struct FlagNote
        {
            static public int id { get; set; }
            public string Name { get; set; }
            public ImageBrush Image { get; set; }
        }


        struct Priority
        {
            static public int id { get; set; }
            public int Name { get; set; }
            public Brushes Color { get; set; }
        }


    }
}
