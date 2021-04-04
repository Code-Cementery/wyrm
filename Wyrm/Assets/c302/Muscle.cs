using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c302
{
    public enum CEMuscleQuadrant { MDL, MVL, MVR, MDR };

    public class Muscle
    {
        //public enum Side { Left, Right };
        public CEMuscleQuadrant Quadrant;

        public string MuscleName;
        public int MuscleId;

        public int CurrentCharge;
        public int NextCharge;
    }
}
