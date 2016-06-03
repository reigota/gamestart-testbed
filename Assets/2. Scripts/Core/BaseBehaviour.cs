using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

using Game.Core.Events;

namespace Game.Core
{
    public abstract class BaseBehaviour : MonoBehaviour
    {
        #region Private elements

        /// <summary>
        /// Dicionario contendo todos os possíveis eventos que a classe concreta disponibiliza
        /// </summary>
        private Dictionary<string, IEvent> _myEvents = new Dictionary<string, IEvent>();

        #endregion

        #region Protected methods

        /// <summary>
        /// Usado pela classe concreta para registrar um evento
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        /// <param name="evt">A instância do evento em si</param>
        protected void RegistryEvent(string eventName, IEvent evt)
        {
            _myEvents.Add(eventName, evt);
        }
        
        /// <summary>
        /// Usado pela classe concreta para disparar o evento
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        protected void Dispatch(string eventName)
        {
            IEvent thisEvent = null;

            if(_myEvents.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Dispatch();
            }
            else
            {
                Debug.LogErrorFormat("Event '{0}' does not exist", eventName);
            }
        }

        /// <summary>
        /// Usado pela classe concreta para disparar o evento que tenha um parametro.
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        protected void Dispatch<T>(string eventName, T arg)
        {
            IEvent thisEvent = null;

            if(_myEvents.TryGetValue(eventName, out thisEvent))
            {
                ((BaseEvent<T>)thisEvent).Dispatch(arg);
            }
            else
            {
                Debug.LogErrorFormat("Event '{0}' does not exist", eventName);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Usado pelos clientes que desejam escutar a ocorrencia do evento
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        /// <param name="listener">O notificado</param>
        public void Subscribe(string eventName, UnityAction listener)
        {
            IEvent thisEvent = null;
            if(_myEvents.TryGetValue(eventName, out thisEvent))
            {
                ((BaseEvent)thisEvent).AddListener(listener);
            }
            else
            {
                Debug.LogErrorFormat("Event '{0}' does not exist", eventName);
            }
        }

        /// <summary>
        /// Usado pelos clientes que desejam escutar a ocorrencia do evento
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        /// <param name="listener">O notificado</param>
        public void Subscribe<T>(string eventName, UnityAction<T> listener)
        {
            IEvent thisEvent = null;
            if(_myEvents.TryGetValue(eventName, out thisEvent))
            {
                ((BaseEvent<T>)thisEvent).AddListener(listener);
            }
            else
            {
                Debug.LogErrorFormat("Event '{0}' does not exist", eventName);
            }
        }

        /// <summary>
        /// Usado pelos clientes que desejam parar de escutar a ocorrencia do evento
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        /// <param name="listener">O notificado</param>
        public void Unsubscribe(string eventName, UnityAction listener)
        {
            IEvent thisEvent = null;
            if(_myEvents.TryGetValue(eventName, out thisEvent))
            {
                ((BaseEvent)thisEvent).RemoveListener(listener);
            }
            else
            {
                Debug.LogErrorFormat("Event '{0}' does not exist", eventName);
            }
        }

        /// <summary>
        /// Usado pelos clientes que desejam parar de escutar a ocorrencia do evento
        /// </summary>
        /// <param name="eventName">Nome do evento</param>
        /// <param name="listener">O notificado</param>
        public void Unsubscribe<T>(string eventName, UnityAction<T> listener)
        {
            IEvent thisEvent = null;
            if(_myEvents.TryGetValue(eventName, out thisEvent))
            {
                ((BaseEvent<T>)thisEvent).RemoveListener(listener);
            }
            else
            {
                Debug.LogErrorFormat("Event '{0}' does not exist", eventName);
            }
        }

        #endregion
    }

}