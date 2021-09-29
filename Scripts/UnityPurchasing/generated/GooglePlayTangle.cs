// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("yOR4KQGJxQyyK0nEPhyP56lrXzUDgI6BsQOAi4MDgICBBTh7SwjcP73SXB3hzJmOCecV7mdnWqqC4Jt3KAX/kQIdw3wQsunVCQ0ET4ibT47uFKZT8ANDSiliOu//yHHdhTcS7gpQ48cC+onkTVt4spBbBJhSLauToABQzClg4j+mKcfHnr1eil4vLx1HH8NEPaVM6uIw9fLCUyKzmJ9pgsDQpHfICuOoJYrLpM7ym4rIOvSnsQOAo7GMh4irB8kHdoyAgICEgYI3yHz7ayLtC64JXUzAE2JXOuBVdyAA9BPBn+SU+0O1NUo/Nm9X8YaPN4SsLMC60TDajdTY00ZosM4pLawJIis5t1G5KCLtg1LojhuzJy4DeT4t8BKZxd5ypIOCgIGA");
        private static int[] order = new int[] { 2,1,8,7,6,9,11,13,11,11,10,13,12,13,14 };
        private static int key = 129;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
