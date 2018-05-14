using System;
using System.Collections.Generic;

namespace BSModUI.Updater.Misc.Github
{

    /*
     *
     *This whole file represents the json structure retrieved when requesting a release page using the json API
     * 
     */

    [Serializable]
    public class Author
    {
        public string Login;
        public int Id;
        public string AvatarUrl;
        public string GravatarId;
        public string Url;
        public string HtmlUrl;
        public string FollowersUrl;
        public string FollowingUrl;
        public string GistsUrl;
        public string StarredUrl;
        public string SubscriptionsUrl;
        public string OrganizationsUrl;
        public string ReposUrl;
        public string EventsUrl;
        public string ReceivedEventsUrl;
        public string Type;
        public bool SiteAdmin;
    }

    [Serializable]
    public class Uploader
    {
        public string Login;
        public int Id;
        public string AvatarUrl;
        public string GravatarId;
        public string Url;
        public string HtmlUrl;
        public string FollowersUrl;
        public string FollowingUrl;
        public string GistsUrl;
        public string StarredUrl;
        public string SubscriptionsUrl;
        public string OrganizationsUrl;
        public string ReposUrl;
        public string EventsUrl;
        public string ReceivedEventsUrl;
        public string Type;
        public bool SiteAdmin;
    }

    [Serializable]
    public class Asset
    {
        public string Url;
        public int Id;
        public string Name;
        public object Label;
        public Uploader Uploader;
        public string ContentType;
        public string State;
        public int Size;
        public int DownloadCount;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public string BrowserDownloadUrl;
    }

    [Serializable]
    public class GithubReleasePage
    {
        public string Url;
        public string AssetsUrl;
        public string UploadUrl;
        public string HtmlUrl;
        public int Id;
        public string TagName;
        public string TargetCommitish;
        public string Name;
        public bool Draft;
        public Author Author;
        public bool Prerelease;
        public DateTime CreatedAt;
        public DateTime PublishedAt;
        public List<Asset> Assets;
        public string TarballUrl;
        public string ZipballUrl;
        public string Body;
    }
}