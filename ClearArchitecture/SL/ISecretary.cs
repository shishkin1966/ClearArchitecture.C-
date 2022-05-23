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
        virtual void remove(String key)

        /*
        * получить размер объединения
        *
        * @return - получить размер объединения
        */
        virtual int size()

        /*
        * добавить члена объединения
        *
        * @param key - имя члена объединения
        * @param value - член объединения
        */
        virtual void put(String key, T value)

        /*
        * проверить наличие члена объединения
        *
        * @param key - имя члена объединения
        */
        virtual Boolean containsKey(String key)

        /*
        * получить члена объединения
        *
        * @param key - имя члена объединения
        */
        virtual T? getValue(String key)

        /*
        * получить список членов объединения
        */
        virtual List<T> values()

        /*
        * проверить объединение на наличие членов
        *
        * @return true - объединение не пустое
        */
        virtual Boolean isEmpty()

        /*
        * очистить объединение
        */
        virtual void clear()

        /*
        * получить список имен членов объединения
        * @return список имен
        */
        virtual List<String> keys()
    }
}
