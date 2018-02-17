namespace Palindromes
{
    public struct Range
    {
        public int Index;
        public int Length;

        public Range(int index, int length)
        {
            Index = index;
            Length = length;
        }

        public override string ToString()
        {
            return $"Index: {Index} Length: {Length}";
        }
    }
}