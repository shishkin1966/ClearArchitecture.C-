using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IObservableSubscriber : ILifecycle, IProviderSubscriber
    {
        /**
        * Получить список имен слушаемых объектов
        *
        * @return список имен слушаемых объектов
        */
        List<string> GetObservable();

        /**
        * Событие - объект изменен
        *
        * @param name имя слушаемого объекта
        * @param obj значение слушаемого объекта
        */
        void OnChangeObservable(string name, object obj);

    }
}
