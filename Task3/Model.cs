namespace Task3
{
    public class Model
    {
        private int time = 0;
        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public void ResetTime()
        {
            time = 0;
        }
        public string ShowTime()
        {
            return $"00:0{time / 60}:{time % 60}";
        }
    }
}
