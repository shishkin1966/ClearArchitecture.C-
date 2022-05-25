using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс провайдера - объекта предоставлющий сервис
    */
    public interface IProvider : INamed, IValidated, IComparable<IProvider>
    {
        /*
         * Получить тип провайдера
         *
         * @return true - не будет удаляться администратором
         */
        bool IsPersistent();

        /*
         * Событие - отключить регистрацию
         */
        void OnUnRegister();

        /*
        * Событие - регистрация
        */
        void OnRegister();

        /*
        * Остановитить работу провайдера
        */
        void Stop();
    }
}
