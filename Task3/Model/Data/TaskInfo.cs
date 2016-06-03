using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksTracker.Data
{
    public class TaskInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TrackedTime { get; set; }
        public int TaskBoxID { get; set; }
        public ProjectDescription Project { get; set; }
        public float HourRate { get; set; }
        public int Task_Id { get; set; }
    }
}
