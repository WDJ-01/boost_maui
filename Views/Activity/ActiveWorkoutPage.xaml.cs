using Android.Content;
using Android.Text;
using Boost.Common.Services;
using Boost.Platforms.Android.Common.Classes;
using Boost.Platforms.Android.Services;
using Microsoft.Maui.Dispatching;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Boost.Views.Activity;


public partial class ActiveWorkoutPage : ContentPage
{
    private Timer _timer;
    private TimeSpan _timeElapsed;
    bool IsPaused = false;
    private TimerUpdateReceiver _timerUpdateReceiver;
    public ActiveWorkoutPage()
    {
        InitializeComponent();
        //_timer = new Timer(1000); // Set interval to 1 second
        //_timer.Elapsed += OnTimerElapsed;
        _timerUpdateReceiver = new TimerUpdateReceiver();
        _timerUpdateReceiver.TimerUpdated += (timeElapsed) =>
        {
            // Update the UI element showing the timer
            Device.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = timeElapsed;
            });
        };
        Android.App.Application.Context.RegisterReceiver(_timerUpdateReceiver, new IntentFilter(WorkoutActions.UpdateTimerAction));
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (_timerUpdateReceiver != null)
        {
            Android.App.Application.Context.UnregisterReceiver(_timerUpdateReceiver);
            _timerUpdateReceiver = null;
        }
    }
    //private void OnStartButtonClicked(object sender, EventArgs e)
    //{
    //    if (IsPaused)
    //    {
    //        _timeElapsed = _timeElapsed;
    //        _timer.Start();
    //        StopButton.IsVisible = false;
    //    }
    //    else
    //    {
    //        _timeElapsed = TimeSpan.Zero;
    //        _timer.Start();
    //        StopButton.IsVisible = false;
    //    }
    //}

    //private void OnPauseButtonClicked(object sender, EventArgs e)
    //{
    //    _timer.Stop();
    //    IsPaused = true;
    //    StartButton.Text = "Resume";
    //    StopButton.IsVisible = true;
    //}
    //private void OnStopButtonClicked(object sender, EventArgs e)
    //{
    //    _timer.Stop();
    //}

    //private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    //{
    //    _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
    //    MainThread.BeginInvokeOnMainThread(() =>
    //    {
    //        TimerLabel.Text = _timeElapsed.ToString(@"hh\:mm\:ss");
    //    });
    //}
    private void StartTimer(object sender, EventArgs e)
    {
        if(IsPaused)
        {
            var intent = new Intent(Android.App.Application.Context, typeof(WorkoutTimerService));
            intent.SetAction(WorkoutActions.ActionPause);
            Android.App.Application.Context.StartService(intent);
        }
        else
        {
            IsPaused = false;
            var intent = new Intent(Android.App.Application.Context, typeof(WorkoutTimerService));
            intent.SetAction(WorkoutActions.ActionStart);
            Android.App.Application.Context.StartService(intent);
        }
    }

    private void PauseTimer(object sender, EventArgs e)
    {
        IsPaused = true;
        StartButton.Text = "Resume";
        var intent = new Intent(Android.App.Application.Context, typeof(WorkoutTimerService));
        intent.SetAction(WorkoutActions.ActionPause);
        Android.App.Application.Context.StartService(intent);
    }

    private void ResumeTimer(object sender, EventArgs e)
    {
        var intent = new Intent(Android.App.Application.Context, typeof(WorkoutTimerService));
        intent.SetAction(WorkoutActions.ActionResume);
        Android.App.Application.Context.StartService(intent);
    }

    private void StopTimer(object sender, EventArgs e)
    {
        var intent = new Intent(Android.App.Application.Context, typeof(WorkoutTimerService));
        intent.SetAction(WorkoutActions.ActionStop);
        Android.App.Application.Context.StartService(intent);
    }
}
