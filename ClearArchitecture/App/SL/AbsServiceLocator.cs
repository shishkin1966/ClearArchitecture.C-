using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsServiceLocator : IServiceLocator
    {
        private readonly Secretary<IProvider> secretary = new();

        public abstract IProviderFactory GetProviderFactory();

        public abstract void Start();

        public abstract string GetName();

        public void Stop()
        {
            foreach (IProvider provider in GetProviders())
            {
                if (!provider.IsPersistent()) 
                {
                    UnRegisterProvider(provider.GetName());
                }
            }
        }

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
                UnRegisterProvider(provider.GetName());
            }

            secretary.Put(provider.GetName(), provider);
            Console.WriteLine("SL:RegisterProvider " + provider.GetName());
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

        public void UnRegisterProvider(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            if (secretary.ContainsKey(name))
            {
                IProvider provider = secretary.GetValue(name);
                if (provider != null)
                {
                    if (!provider.IsPersistent())
                    {
                        provider.Stop();
                        Console.WriteLine("SL:UnRegisterProvider " + provider.GetName());
                        secretary.Remove(name);
                    }
                }
            }
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
                    if (provider is ISmallUnion p)
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

        public void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return;

            List<string> types = subscriber.GetProviderSubscription();
            List<IProvider> stopProviders = new();
            foreach (string type in types)
            {
                if (secretary.ContainsKey(type))
                {
                    IProvider provider = secretary.GetValue(type);
                    if (provider is ISmallUnion  p)
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
                UnRegisterProvider(provider.GetName());
            }
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
                    if (provider is IUnion p)
                    {
                        p.SetCurrentSubscriber(subscriber);
                    }
                }
            }
            return true;
        }
    }
}
