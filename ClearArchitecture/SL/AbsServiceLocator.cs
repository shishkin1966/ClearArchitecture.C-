using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsServiceLocator : IServiceLocator
    {
        private readonly Secretary<IProvider> secretary = new();

        public abstract IProviderFactory GetProviderFactory();

        public abstract void Start();

        public void Stop()
        {
            foreach (IProvider provider in GetProviders())
            {
                UnRegisterProvider(provider.GetName());
                provider.Stop();
            }
        }

        public abstract string GetName();

        public bool ExistsProvider(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return secretary.ContainsKey(name);
        }

        public IProvider GetProvider(string name)
        {
            if (string.IsNullOrEmpty(name)) return default;

            if (!ExistsProvider(name)) {
                if (!RegisterProvider(name)) return default;
            }
            if (secretary.GetValue(name) != null)
            {
                return secretary.GetValue(name);
            }
            else
            {
                secretary.Remove(name);
            }
            return default;
        }

        public bool RegisterProvider(IProvider provider)
        {
            if (provider == null) return false;

            if (secretary.ContainsKey(provider.GetName()))
            {
                IProvider oldprovider = GetProvider(provider.GetName());
                if (oldprovider != null && oldprovider.CompareTo(provider) != 0)
                {
                    return false;
                }
                if (!UnRegisterProvider(provider.GetName()))
                {
                    return false;
                }
            }

            secretary.Put(provider.GetName(), provider);
            provider.OnRegister();
            return true;
        }

        public bool RegisterProvider(string name)
        {
            IProvider provider = GetProviderFactory().Create(name);
            if (provider == null)
            {
                return false;
            }
            else
            {
                return RegisterProvider(provider);
            }
        }

        public bool UnRegisterProvider(string name)
        {
            if (string.IsNullOrEmpty(name)) return true;

            if (secretary.ContainsKey(name))
            {
                IProvider provider = secretary.GetValue(name);
                if (provider != null)
                {
                    // нельзя отменить регистрацию у объединения с подписчиками
                    if (!provider.IsPersistent())
                    {
                        if (provider is ISmallUnion<IProviderSubscriber> p)
                        {
                            if (p.HasSubscribers()) return false;
                        }
                        provider.OnUnRegister();
                        secretary.Remove(name);
                    }
                }
                else
                {
                    secretary.Remove(name);
                }
            }
            return true;
        }

        public List<IProvider> GetProviders()
        {
            return secretary.Values();
        }

        public bool RegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return true;

            IProvider provider;

            List<string> types = subscriber.GetProviderSubscription();
            // регистрируем subscriber у провайдеров
            foreach (string type in types)
            {
                if (!secretary.ContainsKey(type))
                {
                    RegisterProvider(type);
                }

                if (secretary.ContainsKey(type))
                {
                    provider = secretary.GetValue(type);
                    if (provider is ISmallUnion<IProviderSubscriber> p)
                    {
                        p.RegisterSubscriber(subscriber);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public bool UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return true;

            List<string> types = subscriber.GetProviderSubscription();
            List<IProvider> stopProviders = new();
            foreach (string type in types)
            {
                if (secretary.ContainsKey(type))
                {
                    IProvider provider = secretary.GetValue(type);
                    if (provider is ISmallUnion <IProviderSubscriber> p)
                    {
                        p.UnRegisterSubscriber(subscriber);
                        if (!p.IsPersistent() && !p.HasSubscribers())
                        {
                            stopProviders.Add(provider);
                        }
                    }
                }
            }
            // удаляем провайдеры без подписчиков
            foreach (IProvider provider in stopProviders)
            {
                provider.Stop();
                UnRegisterProvider(provider.GetName());
            }
            return true;
        }

        public bool SetCurrentSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return true;

            List<string> types = subscriber.GetProviderSubscription();
            foreach (string type in types)
            {
                if (secretary.ContainsKey(type))
                {
                    IProvider provider = secretary.GetValue(type);
                    if (provider is IUnion<IProviderSubscriber> p)
                    {
                        p.SetCurrentSubscriber(subscriber);
                    }
                }
            }
            return true;

        }
    }
}
