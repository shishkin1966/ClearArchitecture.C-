using System;

namespace ClearArchitecture.SL
{
    /*
    * Интерфейс Фабрики поставщиков услуг
    */
    public interface IProviderFactory
    {
        IProvider Create(String name); 
    }
}
