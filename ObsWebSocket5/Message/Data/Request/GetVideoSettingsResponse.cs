namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetVideoSettingsResponse : ResponseData {
        /** Numerator of the fractional FPS value */
        public long fpsNumerator;
        /** Denominator of the fractional FPS value */
        public long fpsDenominator;
        /** Width of the base (canvas) resolution in pixels */
        public long baseWidth;
        /** Height of the base (canvas) resolution in pixels */
        public long baseHeight;
        /** Width of the output resolution in pixels */
        public long outputWidth;
        /** Height of the output resolution in pixels */
        public long outputHeight;
    }
#pragma warning restore CS8618
}
