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
            var bytes = guid.ToByteArray();
            var ticks = date.Ticks - TicksYear2000;
            var time = BitConverter.GetBytes((uint)(ticks >> 25));
            bytes[0] = time[3];
            bytes[1] = time[2];
            bytes[2] = time[1];
            bytes[3] = time[0];
            return new Guid(bytes);
        }
    }
}
