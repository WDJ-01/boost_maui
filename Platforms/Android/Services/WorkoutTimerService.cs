using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Boost.Platforms.Android.Common.Classes;
using System;
using System.Timers;
using static Boost.MainActivity;
using Timer = System.Timers.Timer;

namespace Boost.Platforms.Android.Services
{

    [Service]
    public class WorkoutTimerService : Service
    {
        private System.Timers.Timer _timer;
        private TimeSpan _timeElapsed;
        private bool _isPaused;

        public override IBinder OnBind(Intent intent) => null;
        private PendingIntent GetPendingIntent(string action)
        {
            var intent = new Intent(this, typeof(WorkoutTimerService));
            intent.SetAction(action);
            return PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            switch (intent?.Action)
            {
                case WorkoutActions.ActionStart:
                    StartTimer();
                    break;
                case WorkoutActions.ActionPause:
                    PauseTimer();
                    break;
                case WorkoutActions.ActionResume:
                    ResumeTimer();
                    break;
                case WorkoutActions.ActionStop:
                    StopTimer();
                    StopForeground(true);
                    StopSelf();
                    break;
            }

            // Update notification after handling actions
            StartForeground(1, CreateNotification(_timeElapsed.ToString(@"mm\:ss")));
            return StartCommandResult.Sticky;
        }



        private void StartTimer()
        {
            _timeElapsed = TimeSpan.Zero;
            _isPaused = false;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (s, e) =>
            {
                if (!_isPaused)
                {
                    _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
                    UpdateNotification(_timeElapsed.ToString(@"mm\:ss"));
                    SendBroadcast(_timeElapsed.ToString(@"mm\:ss"));
                }
            };
            _timer.Start();
        }

        private void PauseTimer()
        {
            _isPaused = true;
        }

        private void ResumeTimer()
        {
            _isPaused = false;
        }

        private void StopTimer()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void UpdateNotification(string timeElapsed)
        {
            var notification = CreateNotification(timeElapsed); // Pass the timeElapsed to CreateNotification
            NotificationManagerCompat.From(this).Notify(1, notification);
        }

        private void SendBroadcast(string timeElapsed)
        {
            var intent = new Intent(WorkoutActions.UpdateTimerAction);
            intent.PutExtra("timeElapsed", timeElapsed);
            SendBroadcast(intent);
        }

        private Notification CreateNotification(string timeElapsed)
        {
            var startPendingIntent = GetPendingIntent(WorkoutActions.ActionStart);
            var pausePendingIntent = GetPendingIntent(WorkoutActions.ActionPause);
            var resumePendingIntent = GetPendingIntent(WorkoutActions.ActionResume);
            var stopPendingIntent = GetPendingIntent(WorkoutActions.ActionStop);

            var builder = new Notification.Builder(this, "workout_timer")
                .SetContentTitle("Workout Timer")
                .SetContentText($"Elapsed Time: {timeElapsed}")  // Update the notification content text
                .SetSmallIcon(Resource.Drawable.abc_btn_check_material)
                .SetStyle(new Notification.MediaStyle())
                .AddAction(new Notification.Action(Resource.Drawable.start_gray, "Start", startPendingIntent))
                .AddAction(new Notification.Action(Resource.Drawable.abc_btn_check_material, "Pause", pausePendingIntent))
                .AddAction(new Notification.Action(Resource.Drawable.abc_btn_check_material, "Resume", resumePendingIntent))
                .AddAction(new Notification.Action(Resource.Drawable.abc_btn_check_material, "Stop", stopPendingIntent))
                .SetOngoing(true);

            return builder.Build();
        }



        private Notification.Action BuildAction(string action, string title)
        {
            var intent = new Intent(this, typeof(WorkoutTimerReceiver));
            intent.SetAction(action);
            var pendingIntent = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);
            return new Notification.Action.Builder(Resource.Drawable.abc_btn_check_material, title, pendingIntent).Build();
        }
    }
}
