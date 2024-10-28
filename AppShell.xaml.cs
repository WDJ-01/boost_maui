using Boost.Views.Activity;
using Boost.Views.AddEditPages;
using Fitx_Api;

namespace Boost
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ActiveWorkoutPage", typeof(ActiveWorkoutPage));
            Routing.RegisterRoute("AddEditWorkout", typeof(AddEditWorkout));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));

        }
    }
}
