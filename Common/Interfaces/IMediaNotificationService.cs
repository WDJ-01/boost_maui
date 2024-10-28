using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common.Interfaces
{
    public interface IMediaNotificationService
    {
        void ShowMediaNotification(string songTitle, string artist, string albumArtPath);
        void UpdateMediaNotification(string songTitle, string artist, bool isPlaying);
        void HideMediaNotification();
    }
}
