using System;

namespace Task3
{
    public class Model
    {
        private int time = 600;
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int Time
        {
            get { return time; }
            set { time = value; }
        }
        public DateTime ProjectStartTime { get; set; }
        public TimeSpan TrackedTime { get; set; }

        public void ResetTime()
        {
            time = 0;
        }
        public string ShowTime()
        {
            return $"{time}:{time / 60}:{time % 60}";
        }
    }
}
