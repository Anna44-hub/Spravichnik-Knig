using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    internal class MainViewModel
    {
        /// <summary> 
        /// ObservableCollection автоматически уведомляет интерфейс об добавлении/удалении элементов
        /// </summary> 
        public ObservableCollection<Book> Books
        {
            get; private set;
        }

        private Book _selectedBook;

        /// <summary> 
        /// Свойство для хранения выбранного в списке элемента 
        /// </summary> 
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;

                // Принудительно заставляем кнопки перепроверить их состояние(CanExecute)
                // Это нужно, чтобы кнопки "Редактировать" и "Удалить" активировались при выборе элемента
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommmand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public MainViewModel()
        {
            Books = new ObservableCollection<Book>();
            Books.Add(new Book("Война и мир", "Толстой", 2012));

            // Инициализируем команды, передавая имена методов 
            // null для AddCommand означает, что кнопка "Добавить" всегда активна
            AddCommand = new RelayCommand(OnAdd, null);
            EditCommmand = new RelayCommand(OnEdit, CanEditOrDelete);
            DeleteCommand = new RelayCommand(OnDelete, CanEditOrDelete);
        }

        // Метод для открытия модального окна 
        private bool OpenDialog(Book book)
        {
            addEdit dialog = new addEdit(book);
            dialog.Owner = Application.Current.MainWindow; // Делаем главное окно родителем


            bool? result = dialog.ShowDialog(); // Показываем окно модально


            if (result != null)
            {
                return (bool)result; // Возвращаем true, если DialogResult был установлен в true
            }
            return false;
        }

        // --- Методы логики (заменяют код из MainWindow.xaml.cs) --- 

        private void OnAdd(object parameter)
        {
            // Создаем новую книгу с пустыми данными
            Book newBook = new Book("", "", 0);

            // Открываем диалог. Если пользователь нажал "Сохранить", добавляем в коллекцию
            if (OpenDialog(newBook))
            {
                Books.Add(newBook);
            }
        }

        private void OnEdit(object parameter)
        {
            if (SelectedBook != null)
            {
                // Передаем выбранного человека в диалог.  
                // Благодаря привязкам, изменения в окне сразу применятся к этому объекту

                OpenDialog(SelectedBook);
            }
        }

        private void OnDelete(object parameter)
        {
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook); // Коллекция сама обновит интерфейс
                SelectedBook = null;         // Сбрасываем выделение 
            }
        }

        // Условие для кнопок: они активны только если выбран элемент в списке
        private bool CanEditOrDelete(object parameter)
        {
            return SelectedBook != null;
        }
    }
}
