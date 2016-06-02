namespace Game.Core.Events
{
    /// <summary>
    /// Interface implemented by all events 
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Should return a unique name for the event. 
        /// Tipical implementation in concrete event class: 
        ///     public override string GetEventName() { return GetType().Name; } 
        /// </summary>
        /// <returns>A unique name for the event identification. </returns>
        string GetEventName();

        /// <summary>
        /// Used by fire the event 
        /// </summary>
        void Dispatch();

    }

}