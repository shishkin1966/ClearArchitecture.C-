using System;
using System.Collections.Generic;
using System.Text;

namespace ClearArchitecture.SL
{
    /**
    * Интерфейс провайдера - объекта предоставлющий сервис
    */
    public interface IProvider : INamed, IValidated, IComparable
    {
        /**
         * Получить тип провайдера
         *
         * @return true - не будет удаляться администратором
         */
        fun isPersistent(): Boolean

        /**
         * Событие - отключить регистрацию
         */
        fun onUnRegister()

        /**
        * Событие - регистрация
        */
        fun onRegister()

        /**
        * Остановитить работу провайдера
        */
        fun stop()
    }
}
