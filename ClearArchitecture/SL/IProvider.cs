using System;
using System.Collections.Generic;
using System.Text;

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
        virtual Boolean isPersistent();

        /*
         * Событие - отключить регистрацию
         */
        virtual void onUnRegister();

        /*
        * Событие - регистрация
        */
        virtual void onRegister();

        /*
        * Остановитить работу провайдера
        */
        virtual void stop();
    }
}
