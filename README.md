Multi Region Presenter
============

This demonstrates how to define multiple regions on the screen and dynamically fill the regions with different layouts at runtime.

The region layout, in this example, has three regions: navigation, main, and popup.  The MultiRegionPresenter fills each region with the associated view.  Views are tagged with their region, in the droid project's "Views" folder, like this: [Region(Resource.Id.**YOUR_REGION_NAME**)].

The beauty of this method is it allows you to open ViewModels in the standard way.  MvvmCross takes care of the rest.

Before building, use NuGet to pull in these packages for each project:

RegionEx.Core
 - MvvmCross

RegionEx.Droid
 - MvvmCross 
 - MvvmCross Fragging
 - MvvmCross Visibility Plugin
