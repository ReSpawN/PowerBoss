using System.Text.Json.Serialization;
using PowerBoss.Domain.Converters;

namespace PowerBoss.Domain.Models;

public class VehicleChargeState
{
    [JsonPropertyName("battery_heater_on")]
    public bool BatteryHeaterOn { get; set; }

    [JsonPropertyName("battery_level")]
    public float? BatteryLevel { get; set; }

    [JsonPropertyName("battery_range")]
    public float? BatteryRange { get; set; }

    [JsonPropertyName("charge_amps")]
    public float? ChargeAmps { get; set; }

    [JsonPropertyName("charge_current_request")]
    public float? ChargeCurrentRequest { get; set; }

    [JsonPropertyName("charge_current_request_max")]
    public float? ChargeCurrentRequestMax { get; set; }

    [JsonPropertyName("charge_enable_request")]
    public bool ChargeEnableRequest { get; set; }

    [JsonPropertyName("charge_energy_added")]
    public float? ChargeEnergyAdded { get; set; }

    [JsonPropertyName("charge_limit_soc")]
    public float? ChargeLimitSoc { get; set; }

    [JsonPropertyName("charge_limit_soc_max")]
    public float? ChargeLimitSocMax { get; set; }

    [JsonPropertyName("charge_limit_soc_min")]
    public float? ChargeLimitSocMin { get; set; }

    [JsonPropertyName("charge_limit_soc_std")]
    public float? ChargeLimitSocStd { get; set; }

    [JsonPropertyName("charge_miles_added_ideal")]
    public float? ChargeMilesAddedIdeal { get; set; }

    [JsonPropertyName("charge_miles_added_rated")]
    public float? ChargeMilesAddedRated { get; set; }

    [JsonPropertyName("charge_port_cold_weather_mode")]
    public bool ChargePortColdWeatherMode { get; set; }

    [JsonPropertyName("charge_port_color")]
    public string? ChargePortColor { get; set; }

    [JsonPropertyName("charge_port_door_open")]
    public bool ChargePortDoorOpen { get; set; }

    [JsonPropertyName("charge_port_latch")]
    public string ChargePortLatch { get; set; }

    [JsonPropertyName("charge_rate")]
    public float? ChargeRate { get; set; }

    [JsonPropertyName("charge_to_max_range")]
    public bool ChargeToMaxRange { get; set; }

    [JsonPropertyName("charger_actual_current")]
    public float? ChargerActualCurrent { get; set; }

    [JsonPropertyName("charger_phases")]
    public float? ChargerPhases { get; set; }

    [JsonPropertyName("charger_pilot_current")]
    public float? ChargerPilotCurrent { get; set; }

    [JsonPropertyName("charger_power")]
    public float? ChargerPower { get; set; }

    [JsonPropertyName("charger_voltage")]
    public float? ChargerVoltage { get; set; }

    [JsonPropertyName("charging_state")]
    public string? ChargingState { get; set; }

    [JsonPropertyName("conn_charge_cable")]
    public string? ConnChargeCable { get; set; }

    [JsonPropertyName("est_battery_range")]
    public float? EstBatteryRange { get; set; }

    [JsonPropertyName("fast_charger_brand")]
    public string? FastChargerBrand { get; set; }

    [JsonPropertyName("fast_charger_present")]
    public bool FastChargerPresent { get; set; }

    [JsonPropertyName("fast_charger_type")]
    public string? FastChargerType { get; set; }

    [JsonPropertyName("ideal_battery_range")]
    public float? IdealBatteryRange { get; set; }

    [JsonPropertyName("managed_charging_active")]
    public bool ManagedChargingActive { get; set; }

    [JsonPropertyName("managed_charging_start_time")]
    public float? ManagedChargingStartTime { get; set; }

    [JsonPropertyName("managed_charging_user_canceled")]
    public bool ManagedChargingUserCanceled { get; set; }

    [JsonPropertyName("max_range_charge_counter")]
    public float? MaxRangeChargeCounter { get; set; }

    [JsonPropertyName("minutes_to_full_charge")]
    public float? MinutesToFullCharge { get; set; }

    [JsonPropertyName("not_enough_power_to_heat")]
    public bool? NotEnoughPowerToHeat { get; set; }

    [JsonPropertyName("off_peak_charging_enabled")]
    public bool OffPeakChargingEnabled { get; set; }

    [JsonPropertyName("off_peak_charging_times")]
    public string OffPeakChargingTimes { get; set; }

    [JsonPropertyName("off_peak_hours_end_time")]
    public float? OffPeakHoursEndTime { get; set; }

    [JsonPropertyName("preconditioning_enabled")]
    public bool PreconditioningEnabled { get; set; }

    [JsonPropertyName("preconditioning_times")]
    public string PreconditioningTimes { get; set; }

    [JsonPropertyName("scheduled_charging_mode")]
    public string ScheduledChargingMode { get; set; }

    [JsonPropertyName("scheduled_charging_pending")]
    public bool ScheduledChargingPending { get; set; }

    [JsonPropertyName("scheduled_charging_start_time")]
    public float? ScheduledChargingStartTime { get; set; }

    [JsonPropertyName("scheduled_charging_start_time_app")]
    public float? ScheduledChargingStartTimeApp { get; set; }

    [JsonPropertyName("scheduled_departure_time")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? ScheduledDepartureTime { get; set; }

    [JsonPropertyName("scheduled_departure_time_minutes")]
    public int? ScheduledDepartureTimeMinutes { get; set; }

    [JsonPropertyName("supercharger_session_trip_planner")]
    public bool SuperchargerSessionTripPlanner { get; set; }

    [JsonPropertyName("time_to_full_charge")]
    public float? TimeToFullCharge { get; set; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? Timestamp { get; set; }

    [JsonPropertyName("trip_charging")]
    public bool TripCharging { get; set; }

    [JsonPropertyName("usable_battery_level")]
    public float? UsableBatteryLevel { get; set; }

    [JsonPropertyName("user_charge_enable_request")]
    public bool? UserChargeEnableRequest { get; set; }
}