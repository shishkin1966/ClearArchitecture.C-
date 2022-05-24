using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс секретаря объединиения
    */
    public interface ISecretary<T>
    {
        /*
        * удалить члена объединения
        *
        * @param key - имя члена объединения
        */
        public void Remove(String key);

        /*
        * получить размер объединения
        *
        * @return - получить размер объединения
        */
        int Size();

        /*
        * добавить члена объединения
        *
        * @param key - имя члена объединения
        * @param value - член объединения
        */
        void Put(String key, T value);

        /*
        * проверить наличие члена объединения
        *
        * @param key - имя члена объединения
        */
        Boolean ContainsKey(String key);

        /*
        * получить члена объединения
        *
        * @param key - имя члена объединения
        */
        T GetValue(String key);

        /*
        * получить список членов объединения
        */
        List<T> Values();

        /*
        * проверить объединение на наличие членов
        *
        * @return true - объединение не пустое
        */
        Boolean IsEmpty();

        /*
        * очистить объединение
        */
        void Clear();

        /*
        * получить список имен членов объединения
        * @return список имен
        */
        List<String> Keys();
    }
}
