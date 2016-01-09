using System.Collections.Generic;
using System;
using System.Windows;
using System.Data;
using System.Linq;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ProjectDescription
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescriptionText { get; set; }
        public ICollection<TaskInfo> ProjectTasks { get; set; }
    }
}
