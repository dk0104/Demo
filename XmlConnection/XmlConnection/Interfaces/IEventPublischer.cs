namespace XmlConnection.Interfaces
{
    public interface IEventPublischer
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEventIdentification;
    }
}