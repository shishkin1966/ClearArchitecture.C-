﻿using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsServiceLocator : IServiceLocator
    {
        private readonly string _name;
        private readonly Secretary<IProvider> _secretary = new();

        public abstract IProviderFactory GetProviderFactory();

        public abstract void Start();

        public string GetName()
        {
            return _name;
        }

        protected AbsServiceLocator(string name) 
        {
            _name = name;
        }

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

            return _secretary.ContainsKey(name);
        }

        public IProvider GetProvider(string name)
        {
            if (string.IsNullOrEmpty(name)) return default;

            if (!ExistsProvider(name) && !RegisterProvider(name)) return default;

            if (_secretary.GetValue(name) != null)
            {
                return _secretary.GetValue(name);
            }
            else
            {
                _secretary.Remove(name);
            }
            return default;
        }

        public bool RegisterProvider(IProvider provider)
        {
            if (provider == null) return false;

            if (_secretary.ContainsKey(provider.GetName()))
            {
                IProvider oldprovider = GetProvider(provider.GetName());
                if (oldprovider != null && oldprovider.CompareTo(provider) != 0)
                {
                    return false;
                }
                UnRegisterProvider(provider.GetName());
            }

            _secretary.Put(provider.GetName(), provider);
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

            if (_secretary.ContainsKey(name))
            {
                IProvider provider = _secretary.GetValue(name);
                if (provider != null && !provider.IsPersistent())
                {
                    provider.Stop();
                    Console.WriteLine("SL:UnRegisterProvider " + provider.GetName());
                    _secretary.Remove(name);
                }
            }
        }

        public List<IProvider> GetProviders()
        {
            return _secretary.Values();
        }

        public bool RegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return true;

            IProvider provider;

            List<string> types = subscriber.GetProviderSubscription();
            // регистрируем subscriber у провайдеров
            foreach (string type in types)
            {
                if (!_secretary.ContainsKey(type))
                {
                    RegisterProvider(type);
                }

                if (_secretary.ContainsKey(type))
                {
                    provider = _secretary.GetValue(type);
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
                if (_secretary.ContainsKey(type))
                {
                    IProvider provider = _secretary.GetValue(type);
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
                if (_secretary.ContainsKey(type))
                {
                    IProvider provider = _secretary.GetValue(type);
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
