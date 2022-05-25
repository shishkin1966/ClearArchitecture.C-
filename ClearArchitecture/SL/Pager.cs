using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    // Объект, реализующий динамическую постраничную выборку
    public class Pager
    {
        public Pager()
        {
            SetPageSize(5);
        }

        public Pager(int pagesize)
        {
            SetPageSize(pagesize);
        }

        private int currentPageSize = 0; // текущий размер страницы
        public int CurrentPosition = 0; // текущая позиция
        public List<int> PageSize = new(); // массив размеров страниц
        public bool Eof = false; // индикатор достижения конца
        public int NextPageSize // следующий размер страницы
        { 
            get
            {
                for (int i = 0; i < PageSize.Count; i++)
                {
                    if (PageSize[i] > currentPageSize)
                    {
                        currentPageSize = PageSize[i];
                        return PageSize[i];
                    }
                }
                return PageSize[PageSize.Count - 1];
            }
        }

        /**
        * Установить массив размеров страниц
        *
        * @param initialPageSize начальный размер страницы
        */
        public void SetPageSize(int initialPageSize)
        {
            if (initialPageSize > 0)
            {
                PageSize = new();
                PageSize.Add(initialPageSize);
                PageSize.Add(initialPageSize * 2);
                PageSize.Add(initialPageSize * 4);
            }

        }

        /**
        * Инициализация объекта
        */
        public void Init()
        {
            CurrentPosition = 0;
            currentPageSize = 0;
            Eof = false;
        }

        /**
        * Добавить к текущей позиции
        */
        public void Add(int count)
        {
            CurrentPosition += count;
            if (count < currentPageSize)
            {
                Eof = true;
            }
        }
    }
}
