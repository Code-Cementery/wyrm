namespace UnityEngine.EstUtils
{
    public class SeededRandom
    {
        uint m;
        uint a;
        uint c;
        uint state;

        public uint Seed;

        public SeededRandom(uint seed = 0x8000000 - 1)
        {
            // LCG using TURBO PASCAL constants
            this.m = 0x80000000; // 2**31;
            this.a = 0x8088405;
            this.c = 1;

            this.Seed = seed;
            this.state = seed;
        }

        public void Reset()
        {
            this.state = this.Seed;
        }

        public int nextInt()
        {
            this.state = (this.a * this.state + this.c) % this.m;

            return (int) this.state;
        }

        public float uniform() {
            // returns in range [0,1]
            return this.nextInt() / ((float) this.m - 1);
        }

            //SRNG.prototype.nextRange = function(start, end) {
            //    // returns in range [start, end): including start, excluding end
            //    // can't modulu nextInt because of weak randomness in lower bits
            //    var rangeSize = end - start;
            //    var randomUnder1 = this.nextInt() / this.m;
            //    return start + Math.floor(randomUnder1 * rangeSize);
            //}
            //SRNG.prototype.choice = function(array) {
            //    return array[this.nextRange(0, array.length)];
            //}
    }
}