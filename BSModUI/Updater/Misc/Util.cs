﻿using System;
using System.Collections;
using System.Linq;
using BSModUI.Updater.Interfaces;
using BSModUI.Updater.Misc.Github;

namespace BSModUI.Updater.Misc
{
    public static class Util
    {

        /// <summary>
        /// A coroutine which returns the json returned by the Github Api about the given plugin as a useable object
        /// </summary>
        /// <param name="interop"></param>
        /// <param name="plugin"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static IEnumerator GetGithubJson(VcInterop interop, IGithubInfoPlugin plugin, Action<GithubReleasePage> method)
        {
            yield return interop.StartCoroutine(interop.GithubInterop(plugin.Author, plugin.GithubProjName));
            var page = interop.CoroutineResults.FirstOrDefault(o =>
                o.Author == plugin.Author && o.ProjName == plugin.GithubProjName).Page;
            //Logger.Log($"Pages is Null? {page == null}");
            method.Invoke(page);
        }

    }
}