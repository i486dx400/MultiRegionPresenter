using System;
using System.Linq;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace RegionEx.Droid
{

    /// <summary>
    /// Extension methods that allow views to access an associated RegionAttribute.
    /// </summary>
    static public class RegionAttributeExtentionMethods
    {
        /// <summary>
        /// Returns true iff the view has a region attribute.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static bool HasRegionAttribute(this MvxFragment view)
        {
            var attributes = view
                .GetType()
                .GetCustomAttributes(typeof(RegionAttribute), true);
            return attributes.Count() > 0;
        }

        /// <summary>
        /// Gets the Android resource ID from the RecionAttribute associated with the view.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static int GetRegionId(this MvxFragment view)
        {
            var attributes = view
                .GetType()
                .GetCustomAttributes(typeof(RegionAttribute), true);
            if (attributes.Count() == 0)
            {
                throw new InvalidOperationException("The IMvxView has no region attribute.");
            }
            else
            {
                return ((RegionAttribute)attributes.First()).Id;
            }
        }
    }

}