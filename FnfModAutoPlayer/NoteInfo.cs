using System;

namespace FnfModAutoPlayer
{
    public class NoteInfo
    {
        public double Time { get; set; }      // 毫秒
        public int Direction { get; set; }    // 0–7
        public double Length { get; set; }    // 毫秒（0 = tap）
    }
}
