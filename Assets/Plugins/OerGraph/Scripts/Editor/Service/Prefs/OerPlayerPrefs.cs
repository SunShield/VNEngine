using UnityEngine;

namespace OerGraph.Editor.Service.Prefs
{
    public static class OerPlayerPrefs
    {
        private const string RecentlyCreatedGraphPathKey = "RecentlyCreatedGraphPath";

        public static string GetRecentlyCreatedGraphName() => PlayerPrefs.GetString(RecentlyCreatedGraphPathKey, "");
        public static void SetRecentlyCreatedGraphName(string str) => PlayerPrefs.SetString(RecentlyCreatedGraphPathKey, str);
        public static void ClearRecentlyCreatedGraphName() => PlayerPrefs.SetString(RecentlyCreatedGraphPathKey, "");
    }
}