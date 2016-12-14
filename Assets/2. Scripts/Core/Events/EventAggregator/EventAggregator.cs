using UnityEngine;
using System.Collections.Generic;
using System;

namespace Game.Core
{
    public class EventAggregator
    {

        private static Dictionary<string, List<Action<IGameEvent>>> _eventDictionary = new Dictionary<string, List<Action<IGameEvent>>>();


        public static void AddListener(string eventId, Action<IGameEvent> callback)
        {
            List<Action<IGameEvent>> callbackList;
            if(!_eventDictionary.TryGetValue(eventId, out callbackList))
            {
                callbackList = new List<Action<IGameEvent>>();
                _eventDictionary.Add(eventId, callbackList);
            }
            callbackList.Add(callback);
        }

        public static void AddListener<T>(Action<IGameEvent> callback) where T : IGameEvent
        {
            AddListener(typeof(T).FullName, callback);
        }


        public static void RemoveListener(string eventId, Action<IGameEvent> callback)
        {
            List<Action<IGameEvent>> callbackList;
            if(_eventDictionary.TryGetValue(eventId, out callbackList))
            {
                callbackList.Remove(callback);
            }
        }

        public static void RemoveListener<T>(Action<IGameEvent> callback) where T : IGameEvent
        {
            RemoveListener(typeof(T).FullName, callback);
        }


        public static void Publish(string eventId, IGameEvent ev)
        {
            List<Action<IGameEvent>> callbackList;

            if(_eventDictionary.TryGetValue(eventId, out callbackList))
            {
                foreach(var callback in callbackList)
                {
                    try
                    {
                        if(callback != null)
                            callback(ev);
                    }
                    catch(Exception ex)
                    {
                        Debug.LogError("Err: " + ex.Message);
                    }
                }
            }
        }

        public static void Publish<T>(T ev) where T : IGameEvent
        {
            Publish(typeof(T).FullName, ev);
        }

        public static void Publish<T>() where T:IGameEvent
        {
            Publish(typeof(T).FullName, null);
        }



    }
}