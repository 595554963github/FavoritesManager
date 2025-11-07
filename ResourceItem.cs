using System;
using System.ComponentModel;

namespace FavoritesManager
{
    public class ResourceItem : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = "Free";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastAccessed { get; set; } = DateTime.MinValue;
        public bool IsDefault { get; set; } = false;

        public string CategoryDisplay
        {
            get { return string.IsNullOrEmpty(Category) ? "未分类" : Category; }
        }

        public string TypeDisplay
        {
            get { return string.Equals(Type, "Free", StringComparison.OrdinalIgnoreCase) ? "免费" : "付费"; }
        }

        public string CategoryColor
        {
            get { return GetCategoryColor(Category); }
        }

        public string TypeColor
        {
            get { return string.Equals(Type, "Free", StringComparison.OrdinalIgnoreCase) ? "#4CAF50" : "#F44336"; }
        }

        private static string GetCategoryColor(string category)
        {
            if (string.IsNullOrEmpty(category)) return "#9E9E9E";

            var colors = new System.Collections.Generic.Dictionary<string, string>
            {
                {"代码托管平台", "#2196F3"},
                {"设计资源", "#E91E63"},
                {"学习平台", "#9C27B0"},
                {"素材网站", "#4CAF50"},
                {"Cosplay", "#FF4081"},
                {"云服务", "#FF9800"},
                {"论坛社区", "#795548"},
                {"游戏下载", "#607D8B"},
                {"动漫", "#FF0000"},
                {"漫画", "#FF6B35"},
                {"其他", "#9E9E9E"}
            };

            return colors.ContainsKey(category) ? colors[category] : "#9E9E9E";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}