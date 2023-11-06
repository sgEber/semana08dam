using System;
using System.Collections.Generic;
using System.Text;


namespace semana08dam.interfaces
{
    public enum BatteryStatus
    {
        Charging,
        Discharging,
        Full,
        NotCharging,
        Unknown
    }

    public enum PowerSource
    {
        Battery,
        Ac,
        Usb,
        Wireless,
        Other
    }

    public interface IBattery
    {
        int RemainingChargePercent { get; }
        semana08dam.interfaces.BatteryStatus Status { get; }
        PowerSource PowerSource { get; }
    }
}
