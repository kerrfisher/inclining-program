using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIncliningProgram
{
    public class TestData
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double MinRollMean { get; set; }
        public double MinPitchMean { get; set; }
        public double MinRollStdDev { get; set; }
        public double MinPitchStdDev { get; set; }
        public double AngRollMean { get; set; }
        public double AngPitchMean { get; set; }
        public double AngRollStdDev { get; set; }
        public double AngPitchStdDev { get; set; }
    }
}
