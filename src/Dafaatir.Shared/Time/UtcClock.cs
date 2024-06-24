using System;



namespace Dafaatir.Shared.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow.AddHours(3);
}