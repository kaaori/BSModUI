using System.Collections;
using UnityEngine;

namespace VersionChecker.Misc {
    /*
     *
     *This object is used to execute a coroutine while preserving data from the method for use within another
     * 
     */
    public class CoroutineWithData {
        /// <summary>
        /// coroutine handling the operations, this needs to be yielded to use result
        /// </summary>
        public Coroutine coroutine { get; private set; }
        /// <summary>
        /// the result obtained from coroutine
        /// </summary>
        public object result;
        /// <summary>
        ///  the target coroutine which contains data to be preserved
        /// </summary>
        private IEnumerator target;
        
        public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
            this.target = target;
            this.coroutine = owner.StartCoroutine(Run());
        }
 
        private IEnumerator Run() {
            while(target.MoveNext()) {
                result = target.Current;
                yield return result;
            }
        }
    }
}