

**Clean Architecture в проекте на C#**
 
В соответствии с архитектурой Clean Architecture приложение разделено на слои. Каждый слою определен свой провайдер. Архитектура приложения реализует следующую цепочку событий:

 Источник (view или презентер) -> запрос (Request) -> провайдер(объединение провайдеров) -> исполнитель запросов провайдера -> сообщение с результатом -> мессаджер -> получатель результата (презентер или иной объект)

Для  провайдера Observable(слушаемых) объектов цепочка событий представляет собой:

событие (OnChange Observable объекта ) -> провайдер Observable объектов -> Observable объект -> мессаджер сообщений -> получатель сообщений (презентер или иной объект)

Все объекты делятся на генераторы сервиса и пользователей(потребителей) сервисов. Выделены следующие единицы:
- Provider - объект, который предоставляет сервис любым пользователям(объектам) без их учета
- объекты-подписчики - потребители сервисов(ProviderSubscriber), которые регистрируются в объединениях для получения/предоставления сервиса
- малые объединения подписчиков(SmallUnion) - объект группы подписчиков(ProviderSubscriber) для получения/предоставления сервиса
- объединения подписчиков(Union) - объект группы подписчиков(ProviderSubscriber) для получения/предоставления расширенной функциональности

Регистрацию подписчиков и их объединений, а также специалистов, отмену регистрации осуществляет сервис локатор (Service Locator).

Все объекты учитываемые сервис локатором реализуют интерфейсы INamed, IValidated и ISubscriber. 

Provider - объект, предоставляющий какую-либо функциональность любым объектам.
Объединение(SmallUnion или Union) подписчиков (классов, реализующие интерфейс IProviderSubscriber) учитывает объекты подписчики только одного типа. Интерфейс IProviderSubscriber является наследником интерфейса ISubscriber.

По времени жизни провайдеры делятся на:
- постоянные. Время жизни - время жизни приложения. Не могут быть выгружены.
- нормальные. Могут быть загружены и выгружены.
- короткоживущие. Самовыгружающиеся специалисты по какому-либо признаку(например по бездействию или отсутствию подписчиков). По умолчанию.

Пример малого объединения подписчиков  - малое объединение(SmallUnion) презентеров. Объединения, в отличие от малых объединений, поддерживают функцию выбора текущего подписчика.

Как было указано, управлением провайдеров и объединений занимается администратор (Service Locator), реализующий интерфейс IServiceLocator.

Регистрация провайдеров(объединений) проводиться по его имени.

Привязка и отвязка подписчиков (классов с интерфейсом IProviderSubscriber) к провайдерам осуществляется ServiceLocator или самим провайдером. Если провайдер не загружен производится автоматическая создание необходимых провайдеров (объединений). Отписка подписчиков сервис локатором проводится сразу от всех подписанных им провайдеров.

В данном проекте реализованы следующие провайдеры:
- LogProvider - предоставляет услуги по протоколированию работы приложения
- ApplicationProvider - провайдер приложения
- PresenterUnion - Smallunion презентеров
- ObservableUnion - Smallunion Observable объектов
- MessengerUnion - union для доставки сообщений объектам
- ExecutorProvider - провайдер запуска запросов подписчиков
