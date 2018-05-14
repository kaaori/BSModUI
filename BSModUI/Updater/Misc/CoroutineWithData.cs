using System.Collections;
using UnityEngine;

namespace BSModUI.Updater.Misc
{
    /*
     *
     *This object is used to execute a coroutine while preserving data from the method for use within another
     * 
     */
    public class CoroutineWithData
    {
        /// <summary>
        /// coroutine handling the operations, this needs to be yielded to use result
        /// </summary>
        public Coroutine Coroutine { get; }

        /// <summary>
        /// the result obtained from coroutine
        /// </summary>
        public object Result;

        /// <summary>
        ///  the target coroutine which contains data to be preserved
        /// </summary>
        private IEnumerator _target;

        public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
        {
            this._target = target;
            this.Coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while (_target.MoveNext())
            {
                Result = _target.Current;
                yield return Result;
            }
        }
    }
}