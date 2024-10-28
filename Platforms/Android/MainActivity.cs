using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Graphics;
using Color = Android.Graphics.Color;
using Boost.Platforms.Android.Services;
using Android.Content;
using Boost.Platforms.Android.Common.Classes;

namespace Boost
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private WorkoutTimerReceiver _workoutTimerReceiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _workoutTimerReceiver = new WorkoutTimerReceiver();
            _workoutTimerReceiver.TimerUpdated += timeElapsed => RunOnUiThread(() => UpdateUITimer(timeElapsed));

            // Register BroadcastReceiver to listen for timer updates
            RegisterReceiver(_workoutTimerReceiver, new IntentFilter(WorkoutActions.UpdateTimerAction));

            CreateNotificationChannel();
            // Start service when the user starts a workout
            //StartWorkoutService();

            var uiModeFlags = Resources.Configuration.UiMode & Android.Content.Res.UiMode.NightMask;

            // Check if the system is in dark mode
            if (uiModeFlags == Android.Content.Res.UiMode.NightYes)
            {
                // Set NavigationBar color to black in dark mode
                Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(37, 37, 37));
            }
            else
            {
                // Set NavigationBar color to a different color in light mode
                var lightModeColor = Android.Graphics.Color.Rgb(255, 255, 255); // White or any color you prefer
                Window.SetNavigationBarColor(lightModeColor);
            }

            // Set StatusBar color
            Window.SetStatusBarColor(Android.Graphics.Color.Black);
        }
        private void UpdateUITimer(string timeElapsed)
        {
            // Update your app's UI with the latest timer value
        }

        private void StartWorkoutService()
        {
            var intent = new Intent(this, typeof(WorkoutTimerService));
            intent.SetAction(WorkoutActions.ActionStart);
            StartService(intent);
        }

        protected override void OnDestroy()
        {
            UnregisterReceiver(_workoutTimerReceiver);
            base.OnDestroy();
        }
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelName = "Workout Timer Channel";
                var channelDescription = "Notifications for workout timer";
                var channel = new NotificationChannel("workout_timer", channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }
        public class WorkoutTimerReceiver : BroadcastReceiver
        {
            public event Action<string> TimerUpdated;

            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action == WorkoutActions.UpdateTimerAction)
                {
                    string timeElapsed = intent.GetStringExtra("TimeElapsed");
                    TimerUpdated?.Invoke(timeElapsed);
                }
            }
        }

    }

}
