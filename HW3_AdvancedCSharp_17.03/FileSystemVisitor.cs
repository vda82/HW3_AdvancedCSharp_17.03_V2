using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HW3_AdvancedCSharp_17._03
{
    public class FileSystemVisitor
    {
        private Func<string, string[]> GetEntriesStrategy;
        public Func<string, bool> Filter { get; }

        public FileSystemVisitor(Func<string, bool> filter, Func<string, string[]> strategy) //for test cases, write array in test method
        {
            this.Filter = filter;
            this.GetEntriesStrategy = strategy;
        }
        
        public FileSystemVisitor(Func<string, bool> filter)
        {
            GetEntriesStrategy = DefaultStrategy;
            this.Filter = filter;
        }

        public event Action SearchStarted;
        public event Action SearchFinished;

        public event EventHandler <string> EntryFound;
        public event EventHandler <string> FilteredEntryFound;


        private string[] DefaultStrategy(string path)
        {
            return Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories);
        }
        public IEnumerable<string> GetAllDirectoriesAndFiles(string path)
        {
            OnSearchStarted();

            string[] entries = GetEntriesStrategy(path);
            int i = 0;
            foreach (var entry in entries)
            {
                //EntryFound?.Invoke(this, entry);
                OnEntryFound(entry);
                if (Filter(entry))
                {
                    //FilteredEntryFound?.Invoke(this, entry);
                    OnFilteredEntryFound(entry);
                    yield return $"{i++}    {entry}";
                }
            }
            OnSearchFinished();
            
        }

        protected virtual void OnEntryFound(string str)
        {
            EntryFound?.Invoke(this, str);
        }

        protected virtual void OnFilteredEntryFound(string str)
        {
            FilteredEntryFound?.Invoke(this, str);
        }
        protected virtual void OnSearchStarted()
        {
            SearchStarted?.Invoke();
        }

        protected virtual void OnSearchFinished()
        {
            SearchFinished?.Invoke();
        }


    }
}



