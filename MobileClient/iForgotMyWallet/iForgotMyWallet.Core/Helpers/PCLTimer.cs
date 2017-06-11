using System;
using System.Threading;
using Amoenus.PclTimer;

namespace iForgotMyWallet.Core
{
    public class PCLTimer
    {

        private CountDownTimer TheTimer;

        private Action _action;

        private int Interval;

        public PCLTimer(Action action, int period)
        {
            _action = action;
            this.Interval = period;

            if (TheTimer != null)
                TheTimer.IntervalPassed -= _timer2_IntervalPassed;

            TheTimer = new CountDownTimer(TimeSpan.FromSeconds(1));
            TheTimer.Interval = TimeSpan.FromSeconds(Interval);

            TheTimer.IntervalPassed += _timer2_IntervalPassed;

            TheTimer.Start();     
        }

        void _timer2_IntervalPassed(object sender, EventArgs e)
        {
            _action.Invoke();
            TheTimer.CurrentTime = TimeSpan.FromSeconds(Interval);
            TheTimer.Start();
        }

        private void PCLTimerCallback(object state)
        {
            _action.Invoke();
        }

        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            return false;
         }

        public void Dispose()
        {
            TheTimer.Stop();
            TheTimer.IntervalPassed -= _timer2_IntervalPassed;
        }
	}
}

