using System.Text.Json.Serialization;
using PowerBoss.Domain.Converters;

namespace PowerBoss.Domain.Models;

public class VehicleState
{
    [JsonPropertyName("api_version")]
    public int ApiVersion { get; set; }

    [JsonPropertyName("autopark_state_v2")]
    public string? AutoparkStateV2 { get; set; }

    [JsonPropertyName("autopark_style")]
    public string? AutoparkStyle { get; set; }

    [JsonPropertyName("calendar_supported")]
    public bool CalendarSupported { get; set; }

    [JsonPropertyName("car_version")]
    public string? CarVersion { get; set; }

    [JsonPropertyName("center_display_state")]
    public CenterDisplayStateEnum CenterDisplayState { get; set; }

    [JsonPropertyName("dashcam_clip_save_available")]
    public bool DashcamClipSaveAvailable { get; set; }

    [JsonPropertyName("dashcam_state")]
    public string? DashcamState { get; set; }

    [JsonPropertyName("df")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool DriverFrontDoor { get; set; }

    [JsonPropertyName("dr")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool DriverRearDoor { get; set; }

    [JsonPropertyName("fd_window")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool DriverFrontWindow { get; set; }

    [JsonPropertyName("feature_bitmask")]
    public string? FeatureBitmask { get; set; }

    [JsonPropertyName("fp_window")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool PassengerFrontWindow { get; set; }

    [JsonPropertyName("ft")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool FrontTrunkLatch { get; set; }

    [JsonPropertyName("is_user_present")]
    public bool IsUserPresent { get; set; }

    [JsonPropertyName("last_autopark_error")]
    public string? LastAutoparkError { get; set; }

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("media_info")]
    public MediaInfo MediaInfo { get; set; }

    [JsonPropertyName("media_state")]
    public MediaState MediaState { get; set; }

    [JsonPropertyName("notifications_supported")]
    public bool NotificationsSupported { get; set; }

    [JsonPropertyName("odometer")]
    public double Odometer { get; set; }

    [JsonPropertyName("parsed_calendar_supported")]
    public bool ParsedCalendarSupported { get; set; }

    [JsonPropertyName("pf")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool PassengerFrontDoor { get; set; }

    [JsonPropertyName("pr")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool PassengerRearDoor { get; set; }

    [JsonPropertyName("rd_window")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool DriverRearWindow { get; set; }

    [JsonPropertyName("remote_start")]
    public bool RemoteStart { get; set; }

    [JsonPropertyName("remote_start_enabled")]
    public bool RemoteStartEnabled { get; set; }

    [JsonPropertyName("remote_start_supported")]
    public bool RemoteStartSupported { get; set; }

    [JsonPropertyName("rp_window")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool PassengerRearWindow { get; set; }

    [JsonPropertyName("rt")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool RearTrunkLatch { get; set; }

    [JsonPropertyName("santa_mode")]
    [JsonConverter(typeof(IntToBooleanJsonConverter))]
    public bool SantaModeEnabled { get; set; }

    [JsonPropertyName("sentry_mode")]
    public bool SentryModeEnabled { get; set; }

    [JsonPropertyName("sentry_mode_available")]
    public bool SentryModeAvailable { get; set; }

    [JsonPropertyName("service_mode")]
    public bool ServiceMode { get; set; }

    [JsonPropertyName("service_mode_plus")]
    public bool ServiceModePlus { get; set; }

    [JsonPropertyName("smart_summon_available")]
    public bool SmartSummonAvailable { get; set; }

    [JsonPropertyName("software_update")]
    public SoftwareUpdate SoftwareUpdate { get; set; }

    [JsonPropertyName("speed_limit_mode")]
    public SpeedLimitMode SpeedLimitMode { get; set; }

    [JsonPropertyName("summon_standby_mode_enabled")]
    public bool SummonStandbyModeEnabled { get; set; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? Timestamp { get; set; }

    [JsonPropertyName("tpms_hard_warning_fl")]
    public bool TpmsHardWarningFl { get; set; }

    [JsonPropertyName("tpms_hard_warning_fr")]
    public bool TpmsHardWarningFr { get; set; }

    [JsonPropertyName("tpms_hard_warning_rl")]
    public bool TpmsHardWarningRl { get; set; }

    [JsonPropertyName("tpms_hard_warning_rr")]
    public bool TpmsHardWarningRr { get; set; }

    [JsonPropertyName("tpms_last_seen_pressure_time_fl")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? TpmsLastSeenPressureTimeFl { get; set; }

    [JsonPropertyName("tpms_last_seen_pressure_time_fr")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? TpmsLastSeenPressureTimeFr { get; set; }

    [JsonPropertyName("tpms_last_seen_pressure_time_rl")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? TpmsLastSeenPressureTimeRl { get; set; }

    [JsonPropertyName("tpms_last_seen_pressure_time_rr")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? TpmsLastSeenPressureTimeRr { get; set; }

    [JsonPropertyName("tpms_pressure_fl")]
    public float TpmsPressureFl { get; set; }

    [JsonPropertyName("tpms_pressure_fr")]
    public float TpmsPressureFr { get; set; }

    [JsonPropertyName("tpms_pressure_rl")]
    public float TpmsPressureRl { get; set; }

    [JsonPropertyName("tpms_pressure_rr")]
    public float TpmsPressureRr { get; set; }

    [JsonPropertyName("tpms_rcp_front_value")]
    public float TpmsRcpFrontValue { get; set; }

    [JsonPropertyName("tpms_rcp_rear_value")]
    public float TpmsRcpRearValue { get; set; }

    [JsonPropertyName("tpms_soft_warning_fl")]
    public bool TpmsSoftWarningFl { get; set; }

    [JsonPropertyName("tpms_soft_warning_fr")]
    public bool TpmsSoftWarningFr { get; set; }

    [JsonPropertyName("tpms_soft_warning_rl")]
    public bool TpmsSoftWarningRl { get; set; }

    [JsonPropertyName("tpms_soft_warning_rr")]
    public bool TpmsSoftWarningRr { get; set; }

    [JsonPropertyName("valet_mode")]
    public bool ValetMode { get; set; }

    [JsonPropertyName("valet_pin_needed")]
    public bool ValetPinNeeded { get; set; }

    [JsonPropertyName("vehicle_name")]
    public string? VehicleName { get; set; }

    [JsonPropertyName("vehicle_self_test_progress")]
    public int VehicleSelfTestProgress { get; set; }

    [JsonPropertyName("vehicle_self_test_requested")]
    public bool VehicleSelfTestRequested { get; set; }

    [JsonPropertyName("webcam_available")]
    public bool WebcamAvailable { get; set; }
}

public enum CenterDisplayStateEnum
{
    Off = 0,
    OnStandbyOrCampMOde = 2,
    OnChargingScreen = 3,
    On = 4,
    OnBigChargingScreen = 5,
    OnReadyToUnlock = 6,
    SentryMode = 7,
    DogMode = 8,
    Media = 9,
}
