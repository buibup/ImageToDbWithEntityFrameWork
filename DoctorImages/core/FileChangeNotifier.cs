using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorImages.core
{
    public class FileChangeNotifier
    {
        private StringBuilder m_Sb;
        private bool m_bDirty;
        private System.IO.FileSystemWatcher m_Watcher;
        private bool m_bIsWatching;

        public FileChangeNotifier()
        {
            m_Sb = new StringBuilder();
            m_bDirty = false;
            m_bIsWatching = false;
        }

        void WatchingDirectory(string directoryPath)
        {

        }
    }
}
