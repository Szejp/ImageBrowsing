using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Helpers
{
    public class TaskDispatcher : MonoBehaviour
    {
        static TaskDispatcher instance;

        static TaskDispatcher Instance
        {
            get
            {
                TryInit();
                return instance;
            }
        }

        Queue<Action> dispatchActions = new Queue<Action>();

        [RuntimeInitializeOnLoadMethod]
        public static void TryInit()
        {
            if (instance == null)
            {
                instance = new GameObject("TaskDispatcher").AddComponent<TaskDispatcher>();
                DontDestroyOnLoad(instance.gameObject);
            }
        }

        public static void Dispatch(Action action)
        {
            Instance.dispatchActions.Enqueue(action);
        }

        void Update()
        {
            if (dispatchActions != null && dispatchActions.Count > 0)
                dispatchActions.Dequeue()?.Invoke();
        }
    }
}