namespace EVEmuTool.Trinity
{
    /// <summary>
    /// Represents a normal 3-dimensional vector
    /// </summary>
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public override string ToString()
        {
            return $"{{{x}, {y}, {z}}}";
        }
    }

}
