using System;
using System.Collections.Generic;

namespace VersionChecker.Misc.Github {
    
    /*
     *
     *This whole file represents the json structure retrieved when requesting a release page using the json API
     * 
     */
    
    [Serializable]
    public class Author {
        public string login;
        public int id;
        public string avatar_url;
        public string gravatar_id;
        public string url;
        public string html_url;
        public string followers_url;
        public string following_url;
        public string gists_url;
        public string starred_url;
        public string subscriptions_url;
        public string organizations_url;
        public string repos_url;
        public string events_url;
        public string received_events_url;
        public string type;
        public bool site_admin;
    }

    [Serializable]
    public class Uploader {
        public string login;
        public int id;
        public string avatar_url;
        public string gravatar_id;
        public string url;
        public string html_url;
        public string followers_url;
        public string following_url;
        public string gists_url;
        public string starred_url;
        public string subscriptions_url;
        public string organizations_url;
        public string repos_url;
        public string events_url;
        public string received_events_url;
        public string type;
        public bool site_admin;
    }

    [Serializable]
    public class Asset {
        public string url;
        public int id;
        public string name;
        public object label;
        public Uploader uploader;
        public string content_type;
        public string state;
        public int size;
        public int download_count;
        public DateTime created_at;
        public DateTime updated_at;
        public string browser_download_url;
    }

    [Serializable]
    public class GithubReleasePage {
        public string url;
        public string assets_url;
        public string upload_url;
        public string html_url;
        public int id;
        public string tag_name;
        public string target_commitish;
        public string name;
        public bool draft;
        public Author author;
        public bool prerelease;
        public DateTime created_at;
        public DateTime published_at;
        public List<Asset> assets;
        public string tarball_url;
        public string zipball_url;
        public string body;
    }
}