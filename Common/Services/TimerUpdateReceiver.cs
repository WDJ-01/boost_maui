using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common.Services
{
    public class TimerUpdateReceiver : BroadcastReceiver
    {
        public event Action<string> TimerUpdated;

        public override void OnReceive(Context context, Intent intent)
        {
            var timeElapsed = intent.GetStringExtra("timeElapsed");
            TimerUpdated?.Invoke(timeElapsed);
        }
    }
}
