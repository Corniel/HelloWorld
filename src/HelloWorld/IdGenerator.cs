using System;

namespace HelloWorld
{
    public static class IdGenerator
    {
        private const long TicksYear2000 = 630822816000000000L;

        /// <summary>Gets a GUID where the first 4 byte are based on the date.</summary>
        /// <remarks>
        /// As long as Dates are between 2000 and 2456-09-05T23:40:07 you should be safe to go.
        /// </remarks>
        public static Guid ByDate() => ByDate(DateTime.UtcNow, Guid.NewGuid());

        /// <summary>Gets a GUID where the first 4 byte are based on the date.</summary>
        /// <remarks>
        /// As long as Dates are between 2000 and 2456-09-05T23:40:07 you should be safe to go.
        /// </remarks>
        public static Guid ByDate(DateTime date, Guid guid)
        {
            var ticks = date.Ticks - TicksYear2000;
            var prefix = BitConverter.GetBytes((uint)(ticks >> 25));
            var bytes = guid.ToByteArray();
            Buffer.BlockCopy(prefix, 0, bytes, 0, 4);
            return new Guid(bytes);
        }
    }
}
