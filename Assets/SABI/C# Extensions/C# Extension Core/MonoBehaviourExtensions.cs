﻿using System;
using System.Collections;
using UnityEngine;

namespace SABI
{
    public static class MonoBehaviourExtensions
    {
        #region DelayedExecution
        public static MonoBehaviour DelayedExecution(
            this MonoBehaviour monoBehaviour,
            float delay,
            Action callback
        )
        {
            monoBehaviour.StartCoroutine(Execute(delay, callback));
            return monoBehaviour;
        }

        private static IEnumerator Execute(float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }

        public static MonoBehaviour DelayedExecution(
            this MonoBehaviour monoBehaviour,
            float delay,
            Action callback,
            out Coroutine coroutine
        )
        {
            coroutine = monoBehaviour.StartCoroutine(Execute(delay, callback));
            return monoBehaviour;
        }
        #endregion

        #region Delayed Execution Frame
        public static MonoBehaviour DelayedExecutionUntilNextFrame(
            this MonoBehaviour monoBehaviour,
            Action callback
        )
        {
            monoBehaviour.StartCoroutine(ExecuteAfterFrame(callback));
            return monoBehaviour;
        }

        private static IEnumerator ExecuteAfterFrame(Action callback)
        {
            yield return null;
            callback?.Invoke();
        }
        #endregion

        #region Delayed Execution Until Condition True
        public static MonoBehaviour DelayedExecutionUntil(
            this MonoBehaviour monoBehaviour,
            Func<bool> condition,
            Action callback,
            bool expectedResult = true
        )
        {
            if (condition != null)
                monoBehaviour.StartCoroutine(WaitForCondition(condition, callback, expectedResult));
            return monoBehaviour;
        }

        private static IEnumerator WaitForCondition(
            Func<bool> condition,
            Action callback,
            bool expectedResult
        )
        {
            yield return new WaitUntil(() => condition() == expectedResult);
            callback?.Invoke();
        }
        #endregion

        #region Repeated Execution
        public static MonoBehaviour RepeatExecutionWhile(
            this MonoBehaviour monoBehaviour,
            Func<bool> condition,
            float interval,
            Action callback,
            bool expectedResult = true
        )
        {
            if (condition != null)
                monoBehaviour.StartCoroutine(
                    RepeatWhileCoroutine(condition, interval, callback, expectedResult)
                );
            return monoBehaviour;
        }

        private static IEnumerator RepeatWhileCoroutine(
            Func<bool> condition,
            float interval,
            Action callback,
            bool expectedResult
        )
        {
            while (condition() == expectedResult)
            {
                yield return new WaitForSeconds(interval);
                callback?.Invoke();
            }
        }
        #endregion

        public static T GetOrAddComponent<T>(this MonoBehaviour behaviour)
            where T : Component
        {
            T component = behaviour.GetComponent<T>();
            return component != null ? component : behaviour.gameObject.AddComponent<T>();
        }

        public static void AddComponentIfMissing<T>(this MonoBehaviour behaviour)
            where T : Component
        {
            if (behaviour.GetComponent<T>() == null)
                behaviour.gameObject.AddComponent<T>();
        }
    }
}
