using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс провайдера - объекта предоставлющий сервис
    */
    public interface IProvider : INamed, IValidated, IComparable<IProvider>
    {
        /**
         * Получить тип провайдера
         *
         * @return true - не будет удаляться администратором
         */
        Boolean isPersistent();

        /**
         * Событие - отключить регистрацию
         */
        void onUnRegister();

        /**
        * Событие - регистрация
        */
        void onRegister();

        /**
        * Остановитить работу провайдера
        */
        void stop();
    }
}
