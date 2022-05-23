using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс секретаря объединиения
    */
    public interface ISecretary<T>
    {
        /**
        * удалить члена объединения
        *
        * @param key - имя члена объединения
        */
        T? remove(String key): 

        /**
        * получить размер объединения
        *
        * @return - получить размер объединения
        */
        ulong size()

        /**
        * добавить члена объединения
        *
        * @param key - имя члена объединения
        * @param value - член объединения
        */
        void put(String key, T value)

        /**
        * проверить наличие члена объединения
        *
        * @param key - имя члена объединения
        */
        Boolean containsKey(String key)

        /**
        * получить члена объединения
        *
        * @param key - имя члена объединения
        */
        T? get(String key)

        /**
        * получить список членов объединения
        */
        List<T> values()

        /**
        * проверить объединение на наличие членов
        *
        * @return true - объединение не пустое
        */
        Boolean isEmpty() 

        /**
        * очистить объединение
        */
        void clear()

        /**
        * получить список имен членов объединения
        * @return список имен
        */
        List<String> keys()
    }
}
