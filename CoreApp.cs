using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HomeWork_7
{
    /// <summary>
    /// Здесь будет вся логика событий из формы.
    /// </summary>
    class CoreApp
    {
        static void LoadData(ref List<HomeWork_7.Struct.Note> Notes) 
        {
            if (Notes == null)
                Notes = new List<Struct.Note>();
            var path = new OpenFileDialog();
            string[] Lines = File.ReadAllLines(path.FileName);
            foreach (var l in Lines)
            {

            }
            
        }
        static void SaveData() { }
        static void ImportData() { }
        static void FilterDate() { }
        static void AddNote() { }
        static void DeleteNote() { }

    }
}
