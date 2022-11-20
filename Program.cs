using System;
using System.Collections.Generic;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Depository depository = new Depository();
            depository.Work();
        }
    }

    class Depository
    {
        private List<Book> _books;

        public Depository()
        {
            _books = new List<Book>();

            LoadBaseBooks();
        }

        public void Work()
        {
            bool isWorking = true;

            while (isWorking)
            {
                const string ShowBooksCommand = "1";
                const string FindByWhirterCommand = "2";
                const string FindByYearCommand = "3";
                const string FindByCategoryCommand = "4";
                const string AddBookCommand = "5";
                const string RemoveBookCommand = "6";
                const string ExitCommand = "7";
               
                Console.WriteLine($"Библиотечное хранилище\n");

                Console.WriteLine($"{ShowBooksCommand}. Показать все книги");
                Console.WriteLine($"{FindByWhirterCommand}. Фильтр по автору");
                Console.WriteLine($"{FindByYearCommand}. Фильтр по году издания");
                Console.WriteLine($"{FindByCategoryCommand}. Фильтр по категории");
                Console.WriteLine($"{AddBookCommand}. Добавить книгу");
                Console.WriteLine($"{RemoveBookCommand}. Удалить книгу");
                Console.WriteLine($"{ExitCommand}. Выход");

                Console.WriteLine($"Введите номер команды:");
                string caseNumber = Console.ReadLine();

                switch (caseNumber)
                {
                    case ShowBooksCommand:
                        ShowAllBooks();
                        break;

                    case FindByWhirterCommand:
                        ShowBooksByName();
                        break;

                    case FindByYearCommand:
                        ShowBooksByYears();
                        break;

                    case FindByCategoryCommand:
                        ShowBookByCategory();
                        break;

                    case AddBookCommand:
                        AddBook();
                        break;

                    case RemoveBookCommand:
                        RemoveBook();
                        break;

                    case ExitCommand:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine($"Ошибка ввода команды");
                        break;
                }

                Console.WriteLine($"\npress any to continue  ...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddBook()
        {
            Book book = CreateNewBook();

            _books.Add(book);

            Console.WriteLine($"Книга добавлена");
        }

        private void RemoveBook()
        {
            Console.WriteLine($"Введите порядковый номер книги для удаления:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int bookIndex) == false)
            {
                Console.WriteLine($"Ошибка ввода порядкового номера");
                return;
            }

            --bookIndex;

            if (bookIndex >= 0 && bookIndex < _books.Count)
            {
                _books.RemoveAt(bookIndex);

                Console.WriteLine($"Книга удалена");
            }
        }

        private void ShowAllBooks()
        {
            if (_books.Count == 0)
            {
                Console.WriteLine($"Пустое книжное хранилище");
                return;
            }

            for (int i = 0; i < _books.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _books[i].ShowInfo();
            }
        }

        private void ShowBooksByName()
        {
            bool isFind = false;

            Console.Clear();
            Console.WriteLine($"Поиск по автору");

            Console.WriteLine($"Введите буквы имени и/или фамилии автора:");
            string name = Console.ReadLine().ToLower();

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Writer.ToLower().Contains(name))
                {
                    Console.Write($"{i + 1}. ");
                    _books[i].ShowInfo();

                    isFind = true;
                }
            }

            if (isFind == false)
            {
                Console.WriteLine($"Автор {name} не найден.");
            }
        }

        private void ShowBooksByYears()
        {
            bool isFind = false;

            Console.Clear();
            Console.WriteLine($"Поиск по году издания");

            Console.WriteLine($"Введите год издания книги:");
            string yearByText = Console.ReadLine();

            if (int.TryParse(yearByText, out int year) == false)
            {
                Console.WriteLine($"Ошибка ввода года издания книги");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].ImprintDate == year)
                {
                    Console.Write($"{i + 1}. ");
                    _books[i].ShowInfo();

                    isFind = true;
                }
            }

            if (isFind == false)
            {
                Console.WriteLine($"книг за {year} год не найдено.");
            }
        }

        private void ShowBookByCategory()
        {
            bool isFind = false;

            Console.Clear();
            Console.WriteLine($"Поиск по категории");

            Console.WriteLine($"Выберите категорию книги:");
            int index = (int)GetCategory();

            for (int i = 1; i < _books.Count; i++)
            {
                if (_books[i].Category == (Category)index)
                {
                    Console.Write($"{i + 1}. ");
                    _books[i].ShowInfo();

                    isFind = true;
                }
            }

            if (isFind == false)
            {
                Console.WriteLine($"книги в категории {(Category)index} не найдены.");
            }
        }

        private Book CreateNewBook()
        {
            Console.Clear();

            Console.WriteLine($"Введите автора книги: ");
            string writer = Console.ReadLine();

            Console.WriteLine($"Введите название книги: ");
            string title = Console.ReadLine();

            Console.WriteLine($"Введите год издания книги:");
            int imprintDate = GetYear();

            Console.WriteLine($"Введите название издательства");
            string publisher = Console.ReadLine();

            Console.WriteLine($"Укажите категорию");
            Category category = GetCategory();

            return new Book(writer, title, publisher, imprintDate, category);
        }

        private int GetYear()
        {
            int currentYear = DateTime.Now.Year;

            if (int.TryParse(Console.ReadLine(), out int number) == false)
            {
                number = currentYear;
                Console.WriteLine($"Ошибка ввода, присвоен {currentYear} год");
            }

            return number;
        }

        private Category GetCategory()
        {
            bool isContinue = true;
            int categoryNumber = 0;

            while (isContinue)
            {
                for (int i = 1; i < Enum.GetNames(typeof(Category)).Length; i++)
                {
                    Console.WriteLine($"{i}. {(Category)i}");
                }

                if (int.TryParse(Console.ReadLine(), out categoryNumber))
                {
                    for (int i = 0; i < Enum.GetNames(typeof(Category)).Length; i++)
                    {
                        if (i == categoryNumber)
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                Console.Clear();
            }

            return (Category)categoryNumber;
        }

        private void LoadBaseBooks()
        {
            _books.Add(new Book("Джон Толкин", "Хоббит, или Туда и обратно", "рукопись", 1937, Category.Fantesy));
            _books.Add(new Book("Джон Толкин", "Братство кольца", "рукопись", 1954, Category.Fantesy));
            _books.Add(new Book("Джон Толкин", "Две крепости", "рукопись", 1954, Category.Fantastic));
            _books.Add(new Book("Джон Толкин", "Возвращение короля", "рукопись", 1955, Category.Fantastic));
            _books.Add(new Book("Фрэнк Герберт", "Дюна", "рукопись", 1965, Category.Fantastic));
            _books.Add(new Book("Джордж Мартин", "Игра Престолов", "рукопись", 1996, Category.Historical));
        }
    }

    class Book
    {
        private string _title;
        private string _publisher;

        public Book(string writer, string title, string publisher, int imprintDate, Category category)
        {
            _title = title;
            _publisher = publisher;

            Writer = writer;
            ImprintDate = imprintDate;
            Category = category;
        }

        public string Writer { get; private set; }
        public int ImprintDate { get; private set; }
        public Category Category { get; private set; }


        public void ShowInfo()
        {
            Console.WriteLine($"{Writer}, {_title}, год издания {ImprintDate}, категория {Category}, издательство {_publisher}");
        }
    }

    enum Category
    {
        Fantastic = 1,
        Fantesy,
        Historical,
        Other
    }
}
