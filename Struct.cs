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
            public int NumberNote { get; set; }
            public string Header { get; set; }
            public string Text { get; set; }
            public TimeSpan Time { get; set; }
            public DateTime DateNote { get; set; }
            public Repite RepiteNote { get; set; }
            public Flags FlagNote { get; set; }
            public Priority PriorityNote { get; set; }
        }

        struct Repite
        {
            public string Name { get; set; }
            public TimeSpan RepetitionPeriod { get; set; }
        }

        struct Flags
        {
            public string Name { get; set; }
            public ImageBrush Image { get; set; }
        }


        struct Priority
        {
            public int Name { get; set; }
            public Brushes Color { get; set; }
        }


    }
}
