using Android.App;
using Android.Content;
using Android.OS;
using semana08dam.Droid;
using semana08dam.interfaces;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(BatteryImplementation))]
namespace semana08dam.Droid
{
    internal class BatteryImplementation : IBattery
    {
        public BatteryImplementation()
        {
        }

        public int RemainingChargePercent
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                            var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                            return (int)Math.Floor(level * 100D / scale);
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }

        public semana08dam.interfaces.BatteryStatus batteryStatus
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status == (int)interfaces.BatteryStatus.Charging || status == (int)interfaces.BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;
                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);
                            if (isCharging)
                                return semana08dam.interfaces.BatteryStatus.Charging;

                            switch (status)
                            {
                                case (int)interfaces.BatteryStatus.Charging:
                                    return semana08dam.interfaces.BatteryStatus.Charging;
                                case (int)interfaces.BatteryStatus.Discharging:
                                    return semana08dam.interfaces.BatteryStatus.Discharging;
                                case (int)interfaces.BatteryStatus.Full:
                                    return semana08dam.interfaces.BatteryStatus.Full;
                                case (int)interfaces.BatteryStatus.NotCharging:
                                    return semana08dam.interfaces.BatteryStatus.NotCharging;
                                default:
                                    return semana08dam.interfaces.BatteryStatus.Unknown;
                            }
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status == (int)interfaces.BatteryStatus.Charging || status == (int)interfaces.BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);

                            if (!isCharging)
                                return semana08dam.interfaces.PowerSource.Battery;
                            else if (usbCharge)
                                return semana08dam.interfaces.PowerSource.Usb;
                            else if (acCharge)
                                return semana08dam.interfaces.PowerSource.Ac;
                            else if (wirelessCharge)
                                return semana08dam.interfaces.PowerSource.Wireless;
                            else
                                return semana08dam.interfaces.PowerSource.Other;
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
    }
}