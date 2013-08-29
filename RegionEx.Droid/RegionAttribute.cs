using System;
using System.Linq;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Views;

namespace RegionEx.Droid
{
    /// <summary>
    /// Attribute for tagging views with the region in which they should displayed when shown with IMultiRegionPresenter.
    /// </summary>
    public class RegionAttribute : Attribute
    {
        public RegionAttribute(int id)
        {
            Id = id;
        }

        /// <summary>
        /// The Android resource ID of the region.
        /// </summary>
        public int Id { get; private set; }      
    }
}