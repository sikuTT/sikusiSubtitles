namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The audio balance value of an input has changed.

     * Event Subscription: Inputs
     */
    public class InputAudioBalanceChanged : EventData {
        /** Name of the affected input */
        public string inputName;
        /** New audio balance value of the input */
        public long inputAudioBalance;
    }
#pragma warning restore CS8618
}
