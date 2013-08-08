using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bison.Framework.Collections
{
    /// <summary>
    /// A collection that maintains a set of class instances to allow for recycling
    /// instances and minimizing the negative effects of garbage collection events.
    /// </summary>
    /// <remarks>
    /// The array stores the free/invalid items at the beginning of the array.
    /// </remarks>
    /// <typeparam name="T">The type of object to store in the object pool. Only class types can be managed.</typeparam>
    public class ObjectPool<T> where T : class
    {
        #region Members

        /// <summary>
        /// The resize amount if the internal array is out of capacity.
        /// </summary>
        private const int resizeAmount = 20;

        /// <summary>
        /// Indicates whether the internal array can be resized or not.
        /// </summary>
        private bool canResize;

        /// <summary>
        /// The internal array of the managed objects.
        /// </summary>
        private T[] items;

        /// <summary>
        /// The validation predicate.
        /// </summary>
        private readonly Predicate<T> validate;

        /// <summary>
        /// The allocation function of the managed object type.
        /// </summary>
        private readonly Func<T> allocate;

        /// <summary>
        /// Optional initialization function for a new object.
        /// </summary>
        private Action<T> initialize;

        /// <summary>
        /// Optional finalization function for a object.
        /// </summary>
        private Action<T> finalize;

        /// <summary>
        /// The currently free objects.
        /// </summary>
        private int freeCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new object pool instance.
        /// </summary>
        /// <param name="initialSize">The initial size of the object pool.</param>
        /// <param name="resize">Whether the pool can be resized or not.</param>
        /// <param name="validatePred">The managed objects validation predicate.</param>
        /// <param name="allocateFunc">THe managed objects allocation function.</param>
        public ObjectPool(int initialSize, bool resize, Predicate<T> validatePred, Func<T> allocateFunc)
        {
            if (initialSize < 1)
            {
                throw new ArgumentOutOfRangeException("initialSize", "Initial size must be at least 1.");
            }
            if (validatePred == null)
            {
                throw new ArgumentNullException("validateFunc");
            }
            if (allocateFunc == null)
            {
                throw new ArgumentNullException("allocateFunc");
            }

            // init array and counters
            this.items = new T[initialSize];
            this.freeCount = items.Length;
            this.canResize = resize;

            // store the delegates
            this.validate = validatePred;
            this.allocate = allocateFunc;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates and cleans up the pool. This method should be called each frame.
        /// </summary>
        public void Update()
        {
            for (int i = freeCount; i < items.Length; ++i)
            {
                T obj = items[i];

                if (!validate(obj))
                {
                    if (i != freeCount)
                    {
                        items[i] = items[freeCount];
                        items[freeCount] = obj;
                    }

                    if (finalize != null)
                    {
                        finalize(obj);
                    }

                    ++freeCount;
                }
            }
        }

        /// <summary>
        /// Creates a new or recycled instance.
        /// </summary>
        /// <returns>The created instance.</returns>
        public T New()
        {
            // resize to object pool if there is no free object left.
            if (freeCount == 0)
            {
                if (!canResize)
                {
                    return null;
                }

                Debug.WriteLine("Resizing pool from {0} to {1}.", items.Length, items.Length + resizeAmount);

                T[] newItems = new T[items.Length + resizeAmount];
                Array.Copy(
                    items,
                    0,
                    newItems,
                    newItems.Length - items.Length - 1,
                    items.Length);
                items = newItems;

                // update the free counter based on our resize amount
                freeCount += resizeAmount;
            }

            --freeCount;

            // get an object from a free slot
            T obj = items[freeCount];

            // if the slot was not not containing a free item, allocate a new one
            if (obj == null)
            {
                obj = allocate();

                if (obj == null)
                {
                    throw new InvalidOperationException("The object pools allocate operation returns NULL.");
                }

                items[freeCount] = obj;
            }

            if (initialize != null)
            {
                initialize(obj);
            }

            return obj;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the optional initialization function.
        /// </summary>
        public Action<T> Initialize
        {
            get
            {
                return this.initialize;
            }
            set
            {
                this.initialize = value;
            }
        }

        /// <summary>
        /// Gets or sets the optional finalization function.
        /// </summary>
        public Action<T> Finalize
        {
            get
            {
                return this.finalize;
            }
            set
            {
                this.finalize = value;
            }
        }

        /// <summary>
        /// Gets an object at the specified free index.
        /// </summary>
        /// <param name="index">The index of the free part of the internal data structure.</param>
        /// <returns>The free item.</returns>
        public T this[int index]
        {
            get
            {
                index += FreeCount;

                if (index < FreeCount || index >= items.Length)
                {
                    throw new IndexOutOfRangeException("The index must be less or equal to InvalidCount.");
                }

                return items[index];
            }
        }

        /// <summary>
        /// Gets the number of valid items.
        /// </summary>
        public int ValidCount
        {
            get
            {
                return items.Length - freeCount;
            }
        }

        /// <summary>
        /// Gets the number of free or invalid items.
        /// </summary>
        public int FreeCount
        {
            get
            {
                return this.freeCount;
            }
        }

        #endregion
    }
}
