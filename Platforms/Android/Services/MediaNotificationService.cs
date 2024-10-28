using Android.App;
using Android.Content;
using Android.Graphics;
using AndroidX.Core.App;
using Boost.Common.Interfaces;
using Boost.Platforms.Android.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Dependency(typeof(MediaNotificationService))]
namespace Boost.Platforms.Android.Services
{
    public class MediaNotificationService : IMediaNotificationService
    {
        const int NotificationId = 1001;
        const string ChannelId = "media_playback_channel";
        private NotificationManager _notificationManager;

        public MediaNotificationService()
        {
            //_notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            CreateNotificationChannel();
        }

        private void CreateNotificationChannel()
        {
            //if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            //{
            //    var channel = new NotificationChannel(ChannelId, "Media Playback", NotificationImportance.Low)
            //    {
            //        Description = "Media playback controls"
            //    };
            //    _notificationManager.CreateNotificationChannel(channel);
            //}
        }

        public void ShowMediaNotification(string songTitle, string artist, string albumArtPath)
        {
            //var playIntent = new Intent(Application.Context, typeof(MediaPlaybackService)); // Define a custom MediaPlaybackService to handle actions
            //playIntent.SetAction("PLAY");

            //var playPendingIntent = PendingIntent.GetService(Application.Context, 0, playIntent, PendingIntentFlags.UpdateCurrent);

            //var builder = new NotificationCompat.Builder(Application.Context, ChannelId)
            //    .SetSmallIcon(Resource.Drawable.ic_music_note)
            //    .SetContentTitle(songTitle)
            //    .SetContentText(artist)
            //    .SetLargeIcon(BitmapFactory.DecodeFile(albumArtPath))
            //    .AddAction(Resource.Drawable.ic_play, "Play", playPendingIntent)
            //    .SetStyle(new AndroidX.Media.App.NotificationCompat.MediaStyle().SetShowActionsInCompactView(0));

            //_notificationManager.Notify(NotificationId, builder.Build());
        }

        public void UpdateMediaNotification(string songTitle, string artist, bool isPlaying)
        {
            // Update notification (similar to ShowMediaNotification but with modified actions)
        }

        public void HideMediaNotification()
        {
            _notificationManager.Cancel(NotificationId);
        }
    }
}
