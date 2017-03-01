namespace LionCraft.NetworkCapture.Core.Infrastructure.Constants
{
    public static class GeneralConstants
    {
        public static int MaxBufferSize = 8192;

        public static int DefaultSocketPort = 0;

        public static byte[] InBufferDefaultValue = new byte[] { 1, 0, 0, 0 };

        public static byte[] OutBufferDefaultValue = new byte[] { 1, 0, 0, 0 };
    }
}
