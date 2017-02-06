using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TinyStack.Modules.iSqlHelper
{
    public delegate void FileSystemEventChange(String path);
    public delegate void FileSystemEventDeleted(FileSystemEventArgs e);
    public delegate void FileSystemEventRenamed(RenamedEventArgs e);
    public class MyFileSystemWatcher
    {
        #region 属性
        /// <summary>
        /// 文件监视器
        /// </summary>
        private readonly FileSystemWatcher m_fileSystemWatcher = new FileSystemWatcher();
        /// <summary>
        /// 文件Change事件字典列表
        /// </summary>
        private readonly Dictionary<string, DateTime> m_pendingEvents = new Dictionary<string, DateTime>();
        /// <summary>
        /// 计时器
        /// </summary>
        private readonly Timer m_timer;
        /// <summary>
        /// 是否启用计时器
        /// </summary>
        private bool m_timerStarted = false;
        /// <summary>
        /// 文件改变事件
        /// </summary>
        public event FileSystemEventChange Change;
        /// <summary>
        /// 文件删除事件
        /// </summary>
        public event FileSystemEventDeleted Deleted;
        /// <summary>
        /// 文件重命名事件
        /// </summary>
        public event FileSystemEventRenamed Renamed;

        #region 确定目录中监视哪些文件
        /// <summary>
        /// 确定目录中监视哪些文件
        /// </summary>
        private string _Filter;
        /// <summary>
        /// 确定目录中监视哪些文件
        /// </summary>
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;
                m_fileSystemWatcher.Filter = _Filter;
            }
        }
        #endregion

        #region 获取或设置要监视的更改类型
        /// <summary>
        /// 获取或设置要监视的更改类型
        /// </summary>
        private NotifyFilters _NotifyFilter;
        /// <summary>
        /// 获取或设置要监视的更改类型
        /// </summary>
        public NotifyFilters NotifyFilter
        {
            get { return _NotifyFilter; }
            set
            {
                _NotifyFilter = value;
                m_fileSystemWatcher.NotifyFilter = _NotifyFilter;
            }
        }
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dirPath">要监视的目录路径</param>
        public MyFileSystemWatcher(string dirPath)
        {
            //需要监视目录的路径
            m_fileSystemWatcher.Path = dirPath;
            //是否监视子目录
            m_fileSystemWatcher.IncludeSubdirectories = false;
            //m_fileSystemWatcher.Created += new FileSystemEventHandler(OnChange);
            m_fileSystemWatcher.Changed += new FileSystemEventHandler(OnChange);
            m_fileSystemWatcher.Deleted += new FileSystemEventHandler(m_fileSystemWatcher_Deleted);
            m_fileSystemWatcher.Renamed += new RenamedEventHandler(m_fileSystemWatcher_Renamed);

            m_timer = new Timer(OnTimeout, null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region 当目录内文件发生变化时被触发。
        /// <summary>
        /// 当目录内文件发生变化时被触发。
        /// </summary>
        /// <param name="sender">事件触发源。</param>
        /// <param name="e">事件信息。</param>
        private void OnChange(object sender, FileSystemEventArgs e)
        {
            // Don't want other threads messing with the pending events right now
            lock (m_pendingEvents)
            {
                // Save a timestamp for the most recent event for this path
                m_pendingEvents[e.FullPath] = DateTime.Now;

                // Start a timer if not already started
                if (!m_timerStarted)
                {
                    m_timer.Change(100, 100);
                    m_timerStarted = true;
                }
            }
        }
        #endregion

        #region 当目录内文件被删除时被触发。
        /// <summary>
        /// 当目录内文件被删除时被触发。
        /// </summary>
        /// <param name="sender">事件触发源。</param>
        /// <param name="e">事件信息。</param>
        void m_fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (Deleted != null)
            {
                Deleted(e);
            }
        }
        #endregion

        #region 当目录内文件重命名时被触发。
        /// <summary>
        /// 当目录内文件重命名时被触发。
        /// </summary>
        /// <param name="sender">事件触发源。</param>
        /// <param name="e">事件信息。</param>
        void m_fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (Renamed != null)
            {
                Renamed(e);
            }
        }
        #endregion

        #region 启动监视器
        /// <summary>
        /// 启动监视器
        /// </summary>
        public void Start()
        {
            m_fileSystemWatcher.EnableRaisingEvents = true;
        }
        #endregion

        #region 计时器事件
        /// <summary>
        /// 计时器事件
        /// </summary>
        /// <param name="state"></param>
        private void OnTimeout(object state)
        {
            List<string> paths;

            // Don't want other threads messing with the pending events right now
            lock (m_pendingEvents)
            {
                // Get a list of all paths that should have events thrown
                paths = FindReadyPaths(m_pendingEvents);

                // Remove paths that are going to be used now
                paths.ForEach(delegate(string path)
                {
                    m_pendingEvents.Remove(path);
                });

                // Stop the timer if there are no more events pending
                if (m_pendingEvents.Count == 0)
                {
                    m_timer.Change(Timeout.Infinite, Timeout.Infinite);
                    m_timerStarted = false;
                }
            }

            // Fire an event for each path that has changed
            paths.ForEach(delegate(string path)
            {
                FireEvent(path);
            });
        }
        #endregion

        #region 获得发生Change事件的文件路径列表
        /// <summary>
        /// 获得发生Change事件的文件路径列表
        /// </summary>
        /// <param name="events">文件Change事件字典列表</param>
        /// <returns>发生Change事件的文件路径列表</returns>
        private List<string> FindReadyPaths(Dictionary<string, DateTime> events)
        {
            List<string> results = new List<string>();
            DateTime now = DateTime.Now;

            foreach (KeyValuePair<string, DateTime> entry in events)
            {
                // If the path has not received a new event in the last 75ms
                // an event for the path should be fired
                double diff = now.Subtract(entry.Value).TotalMilliseconds;
                if (diff >= 75)
                {
                    results.Add(entry.Key);
                }
            }

            return results;
        }
        #endregion

        #region 执行事件
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="path"></param>
        private void FireEvent(string path)
        {
            FileSystemEventChange evt = Change;
            if (evt != null)
            {
                evt(path);
            }
        }
        #endregion
        
    }
}
