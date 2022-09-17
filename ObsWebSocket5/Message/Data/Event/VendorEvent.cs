namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An event has been emitted from a vendor.
     * 
     * A vendor is a unique name registered by a third-party plugin or script, which allows for custom requests and events to be added to obs-websocket.
     * If a plugin or script implements vendor requests or events, documentation is expected to be provided with them.

     * Event Subscription: Vendors
     */
    public class VendorEvent : EventData {
        /** Name of the vendor emitting the event */
        string vendorName;
        /** Vendor-provided event typedef */
        string eventType;
        /** Vendor-provided event data. {} if event does not provide any data */
        object eventData;
    }
}
