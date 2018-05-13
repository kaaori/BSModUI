using System;
using System.Collections;
using System.Collections.Generic;
using VersionChecker.Interfaces;
using VersionChecker.Misc.Github;
using UnityEngine;
using UnityEngine.Networking;

namespace VersionChecker.Misc {
    /// <summary>
    /// An object used to call the local MonoBehaviour methods, such as StartCoroutine
    /// </summary>
    public class VCInterop : MonoBehaviour {

        public List<GithubCoroutine> CoroutineResults = new List<GithubCoroutine>();

        /// <summary>
        /// A struct to store the results of each coroutine
        /// </summary>
        public struct GithubCoroutine {
            public string author { get; private set; }
            public string projName { get; private set; }
            public GithubReleasePage Page { get; private set; }

            public GithubCoroutine(string author, string projName, GithubReleasePage page) {
                this.author = author;
                this.projName = projName;
                this.Page = page;
            }
        }
        
        //only want to create this object once really
        void Start() {
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Used as a middle-man to access the data returned by the _GetGithubJson method
        /// </summary>
        /// <param name="author"></param>
        /// <param name="projName"></param>
        /// <returns></returns>
        internal IEnumerator GithubInterop(string author, string projName) {
            var x = new CoroutineWithData(this, _GetGithubJson(author, projName));
            yield return x.coroutine;
            var page = x.result as GithubReleasePage;
            CoroutineResults.Add(new GithubCoroutine(author, projName, page));
        }

        /// <summary>
        /// returns either a GithubReleasePage if it exists, or null if it doesn't
        /// </summary>
        /// <param name="author"></param>
        /// <param name="projName"></param>
        /// <returns></returns>
        IEnumerator _GetGithubJson(string author, string projName) {
            //creates the GET request
            using (var git = new WWW($"https://api.github.com/repos/{author}/{projName}/releases/latest")) {
                yield return git; //gets the GET response
                // Show results as text
                var sanitisedJson = git.text.Replace("\\t", ""); //removes the \\t characters so it can be parsed
                GithubReleasePage page;
                try {
                    page = JsonUtility.FromJson<GithubReleasePage>(sanitisedJson); //try parsing it
                }
                catch (Exception) {
                    page = null; //if it can't be parse, it probably wasn't a real repo
                }

                yield return page;
            }
        }
    }
}