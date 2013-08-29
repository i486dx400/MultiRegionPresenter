namespace RegionEx.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            // Service initialization would go here if there were services.

            //CreatableTypes()
            //    .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();
				
            RegisterAppStart<ViewModels.Main1ViewModel>();
        }
    }
}