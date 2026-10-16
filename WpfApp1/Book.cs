using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace WpfApp1
{
    public class Book : INotifyPropertyChanged
    {
        string _title;
        string _author;
        int _year;

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get => _author; set { _author = value; OnPropertyChanged(); } }

        /// <summary>
        /// Год выпуска книги
        /// </summary>
        public int Year { get => _year; set { _year = value; OnPropertyChanged(); } }

        /// <summary> 
        /// Событие, изменяющее значение объекта 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary> 
        /// Обработка изменения свойств 
        /// </summary> 
        /// <param name="propertyName">Название параметра, которое будем менять(берет по правилу CallerMemberName - забирает название свойства вызываемого</param> 
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }
}

