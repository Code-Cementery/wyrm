
public enum CEMuscleQuadrant { MDL, MVL, MVR, MDR, NONE }

public class MuscleDescriptor
{
    //public enum Side { Left, Right };
    public CEMuscleQuadrant Quadrant;

    public string MuscleName;
    public int MuscleId;

    //public int CurrentCharge;
    //public int NextCharge;

    public override string ToString()
    {
        return MuscleName;
    }
}
