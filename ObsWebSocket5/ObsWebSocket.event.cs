using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message;
using ObsWebSocket5.Message.Data.Event;
namespace ObsWebSocket5 {
    public partial class ObsWebSocket {
        public Event.D CreateEventData(Event e) {
            var obj = e.d.eventData as JObject;
            EventData? eventData = null;
            if (obj != null) {
                if (e.d.eventType == "CurrentSceneCollectionChanging")
                    eventData = obj.ToObject<CurrentSceneCollectionChanging>();
                else if (e.d.eventType == "CurrentSceneCollectionChanged")
                    eventData = obj.ToObject<CurrentSceneCollectionChanged>();
                else if (e.d.eventType == "SceneCollectionListChanged")
                    eventData = obj.ToObject<SceneCollectionListChanged>();
                else if (e.d.eventType == "CurrentProfileChanging")
                    eventData = obj.ToObject<CurrentProfileChanging>();
                else if (e.d.eventType == "CurrentProfileChanged")
                    eventData = obj.ToObject<CurrentProfileChanged>();
                else if (e.d.eventType == "ProfileListChanged")
                    eventData = obj.ToObject<ProfileListChanged>();
                else if (e.d.eventType == "SourceFilterListReindexed")
                    eventData = obj.ToObject<SourceFilterListReindexed>();
                else if (e.d.eventType == "SourceFilterCreated")
                    eventData = obj.ToObject<SourceFilterCreated>();
                else if (e.d.eventType == "SourceFilterRemoved")
                    eventData = obj.ToObject<SourceFilterRemoved>();
                else if (e.d.eventType == "SourceFilterNameChanged")
                    eventData = obj.ToObject<SourceFilterNameChanged>();
                else if (e.d.eventType == "SourceFilterEnableStateChanged")
                    eventData = obj.ToObject<SourceFilterEnableStateChanged>();
                else if (e.d.eventType == "ExitStarted")
                    eventData = obj.ToObject<ExitStarted>();
                else if (e.d.eventType == "InputCreated")
                    eventData = obj.ToObject<InputCreated>();
                else if (e.d.eventType == "InputRemoved")
                    eventData = obj.ToObject<InputRemoved>();
                else if (e.d.eventType == "InputNameChanged")
                    eventData = obj.ToObject<InputNameChanged>();
                else if (e.d.eventType == "InputActiveStateChanged")
                    eventData = obj.ToObject<InputActiveStateChanged>();
                else if (e.d.eventType == "InputShowStateChanged")
                    eventData = obj.ToObject<InputShowStateChanged>();
                else if (e.d.eventType == "InputMuteStateChanged")
                    eventData = obj.ToObject<InputMuteStateChanged>();
                else if (e.d.eventType == "InputVolumeChanged")
                    eventData = obj.ToObject<InputVolumeChanged>();
                else if (e.d.eventType == "InputAudioBalanceChanged")
                    eventData = obj.ToObject<InputAudioBalanceChanged>();
                else if (e.d.eventType == "InputAudioSyncOffsetChanged")
                    eventData = obj.ToObject<InputAudioSyncOffsetChanged>();
                else if (e.d.eventType == "InputAudioTracksChanged")
                    eventData = obj.ToObject<InputAudioTracksChanged>();
                else if (e.d.eventType == "InputAudioMonitorTypeChanged")
                    eventData = obj.ToObject<InputAudioMonitorTypeChanged>();
                else if (e.d.eventType == "InputVolumeMeters")
                    eventData = obj.ToObject<InputVolumeMeters>();
                else if (e.d.eventType == "MediaInputPlaybackStarted")
                    eventData = obj.ToObject<MediaInputPlaybackStarted>();
                else if (e.d.eventType == "MediaInputPlaybackEnded")
                    eventData = obj.ToObject<MediaInputPlaybackEnded>();
                else if (e.d.eventType == "MediaInputActionTriggered")
                    eventData = obj.ToObject<MediaInputActionTriggered>();
                else if (e.d.eventType == "StreamStateChanged")
                    eventData = obj.ToObject<StreamStateChanged>();
                else if (e.d.eventType == "RecordStateChanged")
                    eventData = obj.ToObject<RecordStateChanged>();
                else if (e.d.eventType == "ReplayBufferStateChanged")
                    eventData = obj.ToObject<ReplayBufferStateChanged>();
                else if (e.d.eventType == "VirtualcamStateChanged")
                    eventData = obj.ToObject<VirtualcamStateChanged>();
                else if (e.d.eventType == "ReplayBufferSaved")
                    eventData = obj.ToObject<ReplayBufferSaved>();
                else if (e.d.eventType == "SceneItemCreated")
                    eventData = obj.ToObject<SceneItemCreated>();
                else if (e.d.eventType == "SceneItemRemoved")
                    eventData = obj.ToObject<SceneItemRemoved>();
                else if (e.d.eventType == "SceneItemListReindexed")
                    eventData = obj.ToObject<SceneItemListReindexed>();
                else if (e.d.eventType == "SceneItemEnableStateChanged")
                    eventData = obj.ToObject<SceneItemEnableStateChanged>();
                else if (e.d.eventType == "SceneItemLockStateChanged")
                    eventData = obj.ToObject<SceneItemLockStateChanged>();
                else if (e.d.eventType == "SceneItemSelected")
                    eventData = obj.ToObject<SceneItemSelected>();
                else if (e.d.eventType == "SceneItemTransformChanged")
                    eventData = obj.ToObject<SceneItemTransformChanged>();
                else if (e.d.eventType == "SceneCreated")
                    eventData = obj.ToObject<SceneCreated>();
                else if (e.d.eventType == "SceneRemoved")
                    eventData = obj.ToObject<SceneRemoved>();
                else if (e.d.eventType == "SceneNameChanged")
                    eventData = obj.ToObject<SceneNameChanged>();
                else if (e.d.eventType == "CurrentProgramSceneChanged")
                    eventData = obj.ToObject<CurrentProgramSceneChanged>();
                else if (e.d.eventType == "CurrentPreviewSceneChanged")
                    eventData = obj.ToObject<CurrentPreviewSceneChanged>();
                else if (e.d.eventType == "SceneListChanged")
                    eventData = obj.ToObject<SceneListChanged>();
                else if (e.d.eventType == "CurrentSceneTransitionChanged")
                    eventData = obj.ToObject<CurrentSceneTransitionChanged>();
                else if (e.d.eventType == "CurrentSceneTransitionDurationChanged")
                    eventData = obj.ToObject<CurrentSceneTransitionDurationChanged>();
                else if (e.d.eventType == "SceneTransitionStarted")
                    eventData = obj.ToObject<SceneTransitionStarted>();
                else if (e.d.eventType == "SceneTransitionEnded")
                    eventData = obj.ToObject<SceneTransitionEnded>();
                else if (e.d.eventType == "SceneTransitionVideoEnded")
                    eventData = obj.ToObject<SceneTransitionVideoEnded>();
                else if (e.d.eventType == "StudioModeStateChanged")
                    eventData = obj.ToObject<StudioModeStateChanged>();
                else if (e.d.eventType == "VendorEvent")
                    eventData = obj.ToObject<VendorEvent>();
            }
            if (eventData != null)
                e.d.eventData = eventData;
            return e.d;
        }
    }
}
