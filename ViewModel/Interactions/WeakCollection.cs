//-----------------------------------------------------------------------
// <brief>
//   Weak collection
// </brief>
//
// <author>Denis Keksel</author>
// <since>Date</since>
//-----------------------------------------------------------------------

namespace ViewModel.Interactions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of weak references to objects.
    /// </summary>
    /// <typeparam name="T">The type of object to hold weak references to.</typeparam>
    internal sealed class WeakCollection<T> where T : class
    {
        /// <summary>
        /// The actual collection of strongly-typed weak references.
        /// </summary>
        private List<WeakReference<T>> list = new List<WeakReference<T>>();

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
	
        /// <summary>
        /// Gets a list of live objects from this collection, causing a purge.
        /// </summary>
        /// <returns></returns>
        public List<T> GetLiveItems()
        {
            var ret = new List<T>(this.list.Count);
           
            var writeIndex = 0;
            for (var readIndex = 0; readIndex != this.list.Count; ++readIndex)
            {
                var weakReference = this.list[readIndex];
                T item;
                if (weakReference.TryGetTarget(out item))
                {
                    ret.Add(item);

                    if (readIndex != writeIndex)
                        this.list[writeIndex] = this.list[readIndex];

                    ++writeIndex;
                }
            }

            this.list.RemoveRange(writeIndex, this.list.Count - writeIndex);

            return ret;
        }

       
        /// <summary>
        /// Adds a weak reference to an object to the collection. Does not cause a purge.
        /// </summary>
        /// <param name="item">The object to add a weak reference to.</param>
        public void Add(T item)
        {
            this.list.Add(new WeakReference<T>(item));
        }

        /// <summary>
        /// Removes a weak reference to an object from the collection. Does not cause a purge.
        /// </summary>
        /// <param name="item">The object to remove a weak reference to.</param>
        /// <returns>True if the object was found and removed; false if the object was not found.</returns>
        public bool Remove(T item)
        {
            for (var i = 0; i != this.list.Count; ++i)
            {
                var weakReference = this.list[i];
                T entry;
                if (weakReference.TryGetTarget(out entry) && entry == item)
                {
                    this.list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

      
    }
}