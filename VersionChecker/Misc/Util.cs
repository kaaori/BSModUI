using System;
using System.Collections;
using System.Linq;
using VersionChecker.Interfaces;
using VersionChecker.Misc.Github;

namespace VersionChecker.Misc {
    public static class Util {

        /// <summary>
        /// A coroutine which returns the json returned by the Github Api about the given plugin as a useable object
        /// </summary>
        /// <param name="interop"></param>
        /// <param name="plugin"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static IEnumerator GetGithubJson(VCInterop interop, IVerCheckPlugin plugin, Action<GithubReleasePage> method) {
            yield return interop.StartCoroutine(interop.GithubInterop(plugin.GithubAuthor, plugin.GithubProjName));
                var page = interop.CoroutineResults.FirstOrDefault(o =>
                    o.author == plugin.GithubAuthor && o.projName == plugin.GithubProjName).Page;
                //Logger.Log($"Pages is Null? {page == null}");
                method.Invoke(page);
        }

    }
}