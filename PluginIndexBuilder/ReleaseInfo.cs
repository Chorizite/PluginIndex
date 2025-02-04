﻿using Octokit;
using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Chorizite.PluginIndexBuilder {
    public class ReleaseInfo {
        private RespositoryInfo respositoryInfo;
        private Release release;
        internal string manifestPath;
        private ReleaseAsset asset;
        internal string manifestVersion;
        internal string manifestName;
        internal string manifestDescription;
        internal string manifestAuthor;
        internal bool HasPackage = false;

        public string Name { get; set; } = "";
        public bool IsBeta { get; private set; }
        public string Version { get; set; } = "";
        public string Changelog { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public DateTime Date { get; private set; }

        public ReleaseInfo(RespositoryInfo respositoryInfo, Release release, ReleaseAsset asset, string manifestPath) {
            this.respositoryInfo = respositoryInfo;
            this.release = release;
            this.manifestPath = manifestPath;
            this.asset = asset;
        }

        internal async Task Build() {
            try {
                var manifest = JsonNode.Parse(File.ReadAllText(manifestPath)).AsObject();

                manifestVersion = manifest["version"]?.ToString() ?? "0.0.0";
                manifestName = manifest["name"]?.ToString() ?? "";
                manifestDescription = manifest["description"]?.ToString() ?? "";
                manifestAuthor = manifest["author"]?.ToString() ?? "";

                Name = release.Name;
                IsBeta = release.Prerelease;
                Version = manifestVersion;
                Changelog = release.Body;
                DownloadUrl = asset.BrowserDownloadUrl;
                Date = (release.PublishedAt ?? release.CreatedAt).UtcDateTime;
            }
            catch (Exception e) {
                Console.WriteLine($"Error building release: {e.Message}");
            }
        }

        private void Log(string v) {
            Console.WriteLine($"[{respositoryInfo.repoPath}@{release.TagName}] {v}");
        }
    }
}