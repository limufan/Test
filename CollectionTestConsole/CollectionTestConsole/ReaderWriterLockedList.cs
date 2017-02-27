using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionTestConsole
{
    public class ReaderWriterLockedList<T>
    {
        public ReaderWriterLockedList(T[] list)
        {
            this._lock = new ReaderWriterLock();
            this._list = new List<T>();
            this._list.AddRange(list);
        }

        ReaderWriterLock _lock;
        List<T> _list;

        public void Add(T t)
        {
            if (this.Contains(t))
            {
                return;
            }

            this._lock.AcquireWriterLock(10000);
            try
            {
                this._list.Add(t);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public void Remove(T t)
        {
            this._lock.AcquireWriterLock(10000);
            try
            {
                this._list.Remove(t);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public List<T> Where(Func<T, bool> predicate)
        {
            this._lock.AcquireReaderLock(10000);
            try
            {
                return this._list.Where(predicate).ToList();
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public bool Any(Func<T, bool> predicate)
        {
            this._lock.AcquireReaderLock(10000);
            try
            {
                return this._list.Any(predicate);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public List<T> ToList()
        {
            this._lock.AcquireReaderLock(10000);
            try
            {
                return this._list.ToList();
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public bool Contains(T t)
        {
            this._lock.AcquireReaderLock(10000);
            try
            {
                return this._list.Contains(t);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public List<T> Clear()
        {
            this._lock.AcquireWriterLock(10000);
            try
            {
                List<T> list = this._list.ToList();
                this._list.Clear();

                return list;
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public int Count
        {
            get
            {
                return this._list.Count;
            }
        }
    }
}
