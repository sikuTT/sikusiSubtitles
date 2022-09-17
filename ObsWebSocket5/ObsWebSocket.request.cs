using ObsWebSocket5.Message.Data.InputSettings;
using ObsWebSocket5.Message.Data.Request;
namespace ObsWebSocket5 {
    public partial class ObsWebSocket {
        async public Task<GetPersistentDataResponse> GetPersistentDataAsync(string realm, string slotName) {
            var requestData = new GetPersistentDataRequest() { realm = realm, slotName = slotName };
            var responseData = await SendRequestAsync<GetPersistentDataResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetPersistentDataResponse> SetPersistentDataAsync(string realm, string slotName, object slotValue) {
            var requestData = new SetPersistentDataRequest() { realm = realm, slotName = slotName, slotValue = slotValue };
            var responseData = await SendRequestAsync<SetPersistentDataResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneCollectionListResponse> GetSceneCollectionListAsync() {
            var requestData = new GetSceneCollectionListRequest() {  };
            var responseData = await SendRequestAsync<GetSceneCollectionListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentSceneCollectionResponse> SetCurrentSceneCollectionAsync(string sceneCollectionName) {
            var requestData = new SetCurrentSceneCollectionRequest() { sceneCollectionName = sceneCollectionName };
            var responseData = await SendRequestAsync<SetCurrentSceneCollectionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CreateSceneCollectionResponse> CreateSceneCollectionAsync(string sceneCollectionName) {
            var requestData = new CreateSceneCollectionRequest() { sceneCollectionName = sceneCollectionName };
            var responseData = await SendRequestAsync<CreateSceneCollectionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetProfileListResponse> GetProfileListAsync() {
            var requestData = new GetProfileListRequest() {  };
            var responseData = await SendRequestAsync<GetProfileListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentProfileResponse> SetCurrentProfileAsync(string profileName) {
            var requestData = new SetCurrentProfileRequest() { profileName = profileName };
            var responseData = await SendRequestAsync<SetCurrentProfileResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CreateProfileResponse> CreateProfileAsync(string profileName) {
            var requestData = new CreateProfileRequest() { profileName = profileName };
            var responseData = await SendRequestAsync<CreateProfileResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<RemoveProfileResponse> RemoveProfileAsync(string profileName) {
            var requestData = new RemoveProfileRequest() { profileName = profileName };
            var responseData = await SendRequestAsync<RemoveProfileResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetProfileParameterResponse> GetProfileParameterAsync(string parameterCategory, string parameterName) {
            var requestData = new GetProfileParameterRequest() { parameterCategory = parameterCategory, parameterName = parameterName };
            var responseData = await SendRequestAsync<GetProfileParameterResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetProfileParameterResponse> SetProfileParameterAsync(string parameterCategory, string parameterName, string parameterValue) {
            var requestData = new SetProfileParameterRequest() { parameterCategory = parameterCategory, parameterName = parameterName, parameterValue = parameterValue };
            var responseData = await SendRequestAsync<SetProfileParameterResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetVideoSettingsResponse> GetVideoSettingsAsync() {
            var requestData = new GetVideoSettingsRequest() {  };
            var responseData = await SendRequestAsync<GetVideoSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetVideoSettingsResponse> SetVideoSettingsAsync(long fpsNumerator, long fpsDenominator, long baseWidth, long baseHeight, long outputWidth, long outputHeight) {
            var requestData = new SetVideoSettingsRequest() { fpsNumerator = fpsNumerator, fpsDenominator = fpsDenominator, baseWidth = baseWidth, baseHeight = baseHeight, outputWidth = outputWidth, outputHeight = outputHeight };
            var responseData = await SendRequestAsync<SetVideoSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetStreamServiceSettingsResponse> GetStreamServiceSettingsAsync() {
            var requestData = new GetStreamServiceSettingsRequest() {  };
            var responseData = await SendRequestAsync<GetStreamServiceSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetStreamServiceSettingsResponse> SetStreamServiceSettingsAsync(string streamServiceType, object streamServiceSettings) {
            var requestData = new SetStreamServiceSettingsRequest() { streamServiceType = streamServiceType, streamServiceSettings = streamServiceSettings };
            var responseData = await SendRequestAsync<SetStreamServiceSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetRecordDirectoryResponse> GetRecordDirectoryAsync() {
            var requestData = new GetRecordDirectoryRequest() {  };
            var responseData = await SendRequestAsync<GetRecordDirectoryResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSourceFilterListResponse> GetSourceFilterListAsync(string sourceName) {
            var requestData = new GetSourceFilterListRequest() { sourceName = sourceName };
            var responseData = await SendRequestAsync<GetSourceFilterListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSourceFilterDefaultSettingsResponse> GetSourceFilterDefaultSettingsAsync(string filterKind) {
            var requestData = new GetSourceFilterDefaultSettingsRequest() { filterKind = filterKind };
            var responseData = await SendRequestAsync<GetSourceFilterDefaultSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CreateSourceFilterResponse> CreateSourceFilterAsync(string sourceName, string filterName, string filterKind, object filterSettings) {
            var requestData = new CreateSourceFilterRequest() { sourceName = sourceName, filterName = filterName, filterKind = filterKind, filterSettings = filterSettings };
            var responseData = await SendRequestAsync<CreateSourceFilterResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<RemoveSourceFilterResponse> RemoveSourceFilterAsync(string sourceName, string filterName) {
            var requestData = new RemoveSourceFilterRequest() { sourceName = sourceName, filterName = filterName };
            var responseData = await SendRequestAsync<RemoveSourceFilterResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSourceFilterNameResponse> SetSourceFilterNameAsync(string sourceName, string filterName, string newFilterName) {
            var requestData = new SetSourceFilterNameRequest() { sourceName = sourceName, filterName = filterName, newFilterName = newFilterName };
            var responseData = await SendRequestAsync<SetSourceFilterNameResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSourceFilterResponse> GetSourceFilterAsync(string sourceName, string filterName) {
            var requestData = new GetSourceFilterRequest() { sourceName = sourceName, filterName = filterName };
            var responseData = await SendRequestAsync<GetSourceFilterResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSourceFilterIndexResponse> SetSourceFilterIndexAsync(string sourceName, string filterName, long filterIndex) {
            var requestData = new SetSourceFilterIndexRequest() { sourceName = sourceName, filterName = filterName, filterIndex = filterIndex };
            var responseData = await SendRequestAsync<SetSourceFilterIndexResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSourceFilterSettingsResponse> SetSourceFilterSettingsAsync(string sourceName, string filterName, object filterSettings, bool overlay) {
            var requestData = new SetSourceFilterSettingsRequest() { sourceName = sourceName, filterName = filterName, filterSettings = filterSettings, overlay = overlay };
            var responseData = await SendRequestAsync<SetSourceFilterSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSourceFilterEnabledResponse> SetSourceFilterEnabledAsync(string sourceName, string filterName, bool filterEnabled) {
            var requestData = new SetSourceFilterEnabledRequest() { sourceName = sourceName, filterName = filterName, filterEnabled = filterEnabled };
            var responseData = await SendRequestAsync<SetSourceFilterEnabledResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetVersionResponse> GetVersionAsync() {
            var requestData = new GetVersionRequest() {  };
            var responseData = await SendRequestAsync<GetVersionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetStatsResponse> GetStatsAsync() {
            var requestData = new GetStatsRequest() {  };
            var responseData = await SendRequestAsync<GetStatsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<BroadcastCustomEventResponse> BroadcastCustomEventAsync(object eventData) {
            var requestData = new BroadcastCustomEventRequest() { eventData = eventData };
            var responseData = await SendRequestAsync<BroadcastCustomEventResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CallVendorRequestResponse> CallVendorRequestAsync(string vendorName, string requestType, object requestData) {
            var _requestData = new CallVendorRequestRequest() { vendorName = vendorName, requestType = requestType, requestData = requestData };
            var responseData = await SendRequestAsync<CallVendorRequestResponse>(_requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetHotkeyListResponse> GetHotkeyListAsync() {
            var requestData = new GetHotkeyListRequest() {  };
            var responseData = await SendRequestAsync<GetHotkeyListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<TriggerHotkeyByNameResponse> TriggerHotkeyByNameAsync(string hotkeyName) {
            var requestData = new TriggerHotkeyByNameRequest() { hotkeyName = hotkeyName };
            var responseData = await SendRequestAsync<TriggerHotkeyByNameResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<TriggerHotkeyByKeySequenceResponse> TriggerHotkeyByKeySequenceAsync(string keyId, object keyModifiers) {
            var requestData = new TriggerHotkeyByKeySequenceRequest() { keyId = keyId, keyModifiers = keyModifiers };
            var responseData = await SendRequestAsync<TriggerHotkeyByKeySequenceResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SleepResponse> SleepAsync(long sleepMillis, long sleepFrames) {
            var requestData = new SleepRequest() { sleepMillis = sleepMillis, sleepFrames = sleepFrames };
            var responseData = await SendRequestAsync<SleepResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputListResponse> GetInputListAsync(string inputKind) {
            var requestData = new GetInputListRequest() { inputKind = inputKind };
            var responseData = await SendRequestAsync<GetInputListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputKindListResponse> GetInputKindListAsync(bool unversioned) {
            var requestData = new GetInputKindListRequest() { unversioned = unversioned };
            var responseData = await SendRequestAsync<GetInputKindListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSpecialInputsResponse> GetSpecialInputsAsync() {
            var requestData = new GetSpecialInputsRequest() {  };
            var responseData = await SendRequestAsync<GetSpecialInputsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CreateInputResponse> CreateInputAsync(string sceneName, string inputName, string inputKind, object inputSettings, bool sceneItemEnabled) {
            var requestData = new CreateInputRequest() { sceneName = sceneName, inputName = inputName, inputKind = inputKind, inputSettings = inputSettings, sceneItemEnabled = sceneItemEnabled };
            var responseData = await SendRequestAsync<CreateInputResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<RemoveInputResponse> RemoveInputAsync(string inputName) {
            var requestData = new RemoveInputRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<RemoveInputResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputNameResponse> SetInputNameAsync(string inputName, string newInputName) {
            var requestData = new SetInputNameRequest() { inputName = inputName, newInputName = newInputName };
            var responseData = await SendRequestAsync<SetInputNameResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputDefaultSettingsResponse> GetInputDefaultSettingsAsync(string inputKind) {
            var requestData = new GetInputDefaultSettingsRequest() { inputKind = inputKind };
            var responseData = await SendRequestAsync<GetInputDefaultSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputSettingsResponse> GetInputSettingsAsync(string inputName) {
            var requestData = new GetInputSettingsRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputSettingsResponse> SetInputSettingsAsync(string inputName, InputSettings inputSettings, bool overlay = true) {
            var requestData = new SetInputSettingsRequest() { inputName = inputName, inputSettings = inputSettings, overlay = overlay };
            var responseData = await SendRequestAsync<SetInputSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputMuteResponse> GetInputMuteAsync(string inputName) {
            var requestData = new GetInputMuteRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputMuteResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputMuteResponse> SetInputMuteAsync(string inputName, bool inputMuted) {
            var requestData = new SetInputMuteRequest() { inputName = inputName, inputMuted = inputMuted };
            var responseData = await SendRequestAsync<SetInputMuteResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleInputMuteResponse> ToggleInputMuteAsync(string inputName) {
            var requestData = new ToggleInputMuteRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<ToggleInputMuteResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputVolumeResponse> GetInputVolumeAsync(string inputName) {
            var requestData = new GetInputVolumeRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputVolumeResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputVolumeResponse> SetInputVolumeAsync(string inputName, long inputVolumeMul, long inputVolumeDb) {
            var requestData = new SetInputVolumeRequest() { inputName = inputName, inputVolumeMul = inputVolumeMul, inputVolumeDb = inputVolumeDb };
            var responseData = await SendRequestAsync<SetInputVolumeResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputAudioBalanceResponse> GetInputAudioBalanceAsync(string inputName) {
            var requestData = new GetInputAudioBalanceRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputAudioBalanceResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputAudioBalanceResponse> SetInputAudioBalanceAsync(string inputName, long inputAudioBalance) {
            var requestData = new SetInputAudioBalanceRequest() { inputName = inputName, inputAudioBalance = inputAudioBalance };
            var responseData = await SendRequestAsync<SetInputAudioBalanceResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputAudioSyncOffsetResponse> GetInputAudioSyncOffsetAsync(string inputName) {
            var requestData = new GetInputAudioSyncOffsetRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputAudioSyncOffsetResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputAudioSyncOffsetResponse> SetInputAudioSyncOffsetAsync(string inputName, long inputAudioSyncOffset) {
            var requestData = new SetInputAudioSyncOffsetRequest() { inputName = inputName, inputAudioSyncOffset = inputAudioSyncOffset };
            var responseData = await SendRequestAsync<SetInputAudioSyncOffsetResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputAudioMonitorTypeResponse> GetInputAudioMonitorTypeAsync(string inputName) {
            var requestData = new GetInputAudioMonitorTypeRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputAudioMonitorTypeResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputAudioMonitorTypeResponse> SetInputAudioMonitorTypeAsync(string inputName, string monitorType) {
            var requestData = new SetInputAudioMonitorTypeRequest() { inputName = inputName, monitorType = monitorType };
            var responseData = await SendRequestAsync<SetInputAudioMonitorTypeResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputAudioTracksResponse> GetInputAudioTracksAsync(string inputName) {
            var requestData = new GetInputAudioTracksRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputAudioTracksResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetInputAudioTracksResponse> SetInputAudioTracksAsync(string inputName, object inputAudioTracks) {
            var requestData = new SetInputAudioTracksRequest() { inputName = inputName, inputAudioTracks = inputAudioTracks };
            var responseData = await SendRequestAsync<SetInputAudioTracksResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetInputPropertiesListPropertyItemsResponse> GetInputPropertiesListPropertyItemsAsync(string inputName, string propertyName) {
            var requestData = new GetInputPropertiesListPropertyItemsRequest() { inputName = inputName, propertyName = propertyName };
            var responseData = await SendRequestAsync<GetInputPropertiesListPropertyItemsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<PressInputPropertiesButtonResponse> PressInputPropertiesButtonAsync(string inputName, string propertyName) {
            var requestData = new PressInputPropertiesButtonRequest() { inputName = inputName, propertyName = propertyName };
            var responseData = await SendRequestAsync<PressInputPropertiesButtonResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetMediaInputStatusResponse> GetMediaInputStatusAsync(string inputName) {
            var requestData = new GetMediaInputStatusRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetMediaInputStatusResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetMediaInputCursorResponse> SetMediaInputCursorAsync(string inputName, long mediaCursor) {
            var requestData = new SetMediaInputCursorRequest() { inputName = inputName, mediaCursor = mediaCursor };
            var responseData = await SendRequestAsync<SetMediaInputCursorResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<OffsetMediaInputCursorResponse> OffsetMediaInputCursorAsync(string inputName, long mediaCursorOffset) {
            var requestData = new OffsetMediaInputCursorRequest() { inputName = inputName, mediaCursorOffset = mediaCursorOffset };
            var responseData = await SendRequestAsync<OffsetMediaInputCursorResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<TriggerMediaInputActionResponse> TriggerMediaInputActionAsync(string inputName, string mediaAction) {
            var requestData = new TriggerMediaInputActionRequest() { inputName = inputName, mediaAction = mediaAction };
            var responseData = await SendRequestAsync<TriggerMediaInputActionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetVirtualCamStatusResponse> GetVirtualCamStatusAsync() {
            var requestData = new GetVirtualCamStatusRequest() {  };
            var responseData = await SendRequestAsync<GetVirtualCamStatusResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleVirtualCamResponse> ToggleVirtualCamAsync() {
            var requestData = new ToggleVirtualCamRequest() {  };
            var responseData = await SendRequestAsync<ToggleVirtualCamResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StartVirtualCamResponse> StartVirtualCamAsync() {
            var requestData = new StartVirtualCamRequest() {  };
            var responseData = await SendRequestAsync<StartVirtualCamResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StopVirtualCamResponse> StopVirtualCamAsync() {
            var requestData = new StopVirtualCamRequest() {  };
            var responseData = await SendRequestAsync<StopVirtualCamResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetReplayBufferStatusResponse> GetReplayBufferStatusAsync() {
            var requestData = new GetReplayBufferStatusRequest() {  };
            var responseData = await SendRequestAsync<GetReplayBufferStatusResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleReplayBufferResponse> ToggleReplayBufferAsync() {
            var requestData = new ToggleReplayBufferRequest() {  };
            var responseData = await SendRequestAsync<ToggleReplayBufferResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StartReplayBufferResponse> StartReplayBufferAsync() {
            var requestData = new StartReplayBufferRequest() {  };
            var responseData = await SendRequestAsync<StartReplayBufferResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StopReplayBufferResponse> StopReplayBufferAsync() {
            var requestData = new StopReplayBufferRequest() {  };
            var responseData = await SendRequestAsync<StopReplayBufferResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SaveReplayBufferResponse> SaveReplayBufferAsync() {
            var requestData = new SaveReplayBufferRequest() {  };
            var responseData = await SendRequestAsync<SaveReplayBufferResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetLastReplayBufferReplayResponse> GetLastReplayBufferReplayAsync() {
            var requestData = new GetLastReplayBufferReplayRequest() {  };
            var responseData = await SendRequestAsync<GetLastReplayBufferReplayResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetOutputListResponse> GetOutputListAsync() {
            var requestData = new GetOutputListRequest() {  };
            var responseData = await SendRequestAsync<GetOutputListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetOutputStatusResponse> GetOutputStatusAsync(string outputName) {
            var requestData = new GetOutputStatusRequest() { outputName = outputName };
            var responseData = await SendRequestAsync<GetOutputStatusResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleOutputResponse> ToggleOutputAsync(string outputName) {
            var requestData = new ToggleOutputRequest() { outputName = outputName };
            var responseData = await SendRequestAsync<ToggleOutputResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StartOutputResponse> StartOutputAsync(string outputName) {
            var requestData = new StartOutputRequest() { outputName = outputName };
            var responseData = await SendRequestAsync<StartOutputResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StopOutputResponse> StopOutputAsync(string outputName) {
            var requestData = new StopOutputRequest() { outputName = outputName };
            var responseData = await SendRequestAsync<StopOutputResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetOutputSettingsResponse> GetOutputSettingsAsync(string outputName) {
            var requestData = new GetOutputSettingsRequest() { outputName = outputName };
            var responseData = await SendRequestAsync<GetOutputSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetOutputSettingsResponse> SetOutputSettingsAsync(string outputName, object outputSettings) {
            var requestData = new SetOutputSettingsRequest() { outputName = outputName, outputSettings = outputSettings };
            var responseData = await SendRequestAsync<SetOutputSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetRecordStatusResponse> GetRecordStatusAsync() {
            var requestData = new GetRecordStatusRequest() {  };
            var responseData = await SendRequestAsync<GetRecordStatusResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleRecordResponse> ToggleRecordAsync() {
            var requestData = new ToggleRecordRequest() {  };
            var responseData = await SendRequestAsync<ToggleRecordResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StartRecordResponse> StartRecordAsync() {
            var requestData = new StartRecordRequest() {  };
            var responseData = await SendRequestAsync<StartRecordResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StopRecordResponse> StopRecordAsync() {
            var requestData = new StopRecordRequest() {  };
            var responseData = await SendRequestAsync<StopRecordResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleRecordPauseResponse> ToggleRecordPauseAsync() {
            var requestData = new ToggleRecordPauseRequest() {  };
            var responseData = await SendRequestAsync<ToggleRecordPauseResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<PauseRecordResponse> PauseRecordAsync() {
            var requestData = new PauseRecordRequest() {  };
            var responseData = await SendRequestAsync<PauseRecordResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ResumeRecordResponse> ResumeRecordAsync() {
            var requestData = new ResumeRecordRequest() {  };
            var responseData = await SendRequestAsync<ResumeRecordResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemListResponse> GetSceneItemListAsync(string sceneName) {
            var requestData = new GetSceneItemListRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<GetSceneItemListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetGroupSceneItemListResponse> GetGroupSceneItemListAsync(string sceneName) {
            var requestData = new GetGroupSceneItemListRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<GetGroupSceneItemListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemIdResponse> GetSceneItemIdAsync(string sceneName, string sourceName, long searchOffset) {
            var requestData = new GetSceneItemIdRequest() { sceneName = sceneName, sourceName = sourceName, searchOffset = searchOffset };
            var responseData = await SendRequestAsync<GetSceneItemIdResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CreateSceneItemResponse> CreateSceneItemAsync(string sceneName, string sourceName, bool sceneItemEnabled) {
            var requestData = new CreateSceneItemRequest() { sceneName = sceneName, sourceName = sourceName, sceneItemEnabled = sceneItemEnabled };
            var responseData = await SendRequestAsync<CreateSceneItemResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<RemoveSceneItemResponse> RemoveSceneItemAsync(string sceneName, long sceneItemId) {
            var requestData = new RemoveSceneItemRequest() { sceneName = sceneName, sceneItemId = sceneItemId };
            var responseData = await SendRequestAsync<RemoveSceneItemResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<DuplicateSceneItemResponse> DuplicateSceneItemAsync(string sceneName, long sceneItemId, string destinationSceneName) {
            var requestData = new DuplicateSceneItemRequest() { sceneName = sceneName, sceneItemId = sceneItemId, destinationSceneName = destinationSceneName };
            var responseData = await SendRequestAsync<DuplicateSceneItemResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemTransformResponse> GetSceneItemTransformAsync(string sceneName, long sceneItemId) {
            var requestData = new GetSceneItemTransformRequest() { sceneName = sceneName, sceneItemId = sceneItemId };
            var responseData = await SendRequestAsync<GetSceneItemTransformResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneItemTransformResponse> SetSceneItemTransformAsync(string sceneName, long sceneItemId, object sceneItemTransform) {
            var requestData = new SetSceneItemTransformRequest() { sceneName = sceneName, sceneItemId = sceneItemId, sceneItemTransform = sceneItemTransform };
            var responseData = await SendRequestAsync<SetSceneItemTransformResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemEnabledResponse> GetSceneItemEnabledAsync(string sceneName, long sceneItemId) {
            var requestData = new GetSceneItemEnabledRequest() { sceneName = sceneName, sceneItemId = sceneItemId };
            var responseData = await SendRequestAsync<GetSceneItemEnabledResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneItemEnabledResponse> SetSceneItemEnabledAsync(string sceneName, long sceneItemId, bool sceneItemEnabled) {
            var requestData = new SetSceneItemEnabledRequest() { sceneName = sceneName, sceneItemId = sceneItemId, sceneItemEnabled = sceneItemEnabled };
            var responseData = await SendRequestAsync<SetSceneItemEnabledResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemLockedResponse> GetSceneItemLockedAsync(string sceneName, long sceneItemId) {
            var requestData = new GetSceneItemLockedRequest() { sceneName = sceneName, sceneItemId = sceneItemId };
            var responseData = await SendRequestAsync<GetSceneItemLockedResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneItemLockedResponse> SetSceneItemLockedAsync(string sceneName, long sceneItemId, bool sceneItemLocked) {
            var requestData = new SetSceneItemLockedRequest() { sceneName = sceneName, sceneItemId = sceneItemId, sceneItemLocked = sceneItemLocked };
            var responseData = await SendRequestAsync<SetSceneItemLockedResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemIndexResponse> GetSceneItemIndexAsync(string sceneName, long sceneItemId) {
            var requestData = new GetSceneItemIndexRequest() { sceneName = sceneName, sceneItemId = sceneItemId };
            var responseData = await SendRequestAsync<GetSceneItemIndexResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneItemIndexResponse> SetSceneItemIndexAsync(string sceneName, long sceneItemId, long sceneItemIndex) {
            var requestData = new SetSceneItemIndexRequest() { sceneName = sceneName, sceneItemId = sceneItemId, sceneItemIndex = sceneItemIndex };
            var responseData = await SendRequestAsync<SetSceneItemIndexResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneItemBlendModeResponse> GetSceneItemBlendModeAsync(string sceneName, long sceneItemId) {
            var requestData = new GetSceneItemBlendModeRequest() { sceneName = sceneName, sceneItemId = sceneItemId };
            var responseData = await SendRequestAsync<GetSceneItemBlendModeResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneItemBlendModeResponse> SetSceneItemBlendModeAsync(string sceneName, long sceneItemId, string sceneItemBlendMode) {
            var requestData = new SetSceneItemBlendModeRequest() { sceneName = sceneName, sceneItemId = sceneItemId, sceneItemBlendMode = sceneItemBlendMode };
            var responseData = await SendRequestAsync<SetSceneItemBlendModeResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneListResponse> GetSceneListAsync() {
            var requestData = new GetSceneListRequest() {  };
            var responseData = await SendRequestAsync<GetSceneListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetGroupListResponse> GetGroupListAsync() {
            var requestData = new GetGroupListRequest() {  };
            var responseData = await SendRequestAsync<GetGroupListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetCurrentProgramSceneResponse> GetCurrentProgramSceneAsync() {
            var requestData = new GetCurrentProgramSceneRequest() {  };
            var responseData = await SendRequestAsync<GetCurrentProgramSceneResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentProgramSceneResponse> SetCurrentProgramSceneAsync(string sceneName) {
            var requestData = new SetCurrentProgramSceneRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<SetCurrentProgramSceneResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetCurrentPreviewSceneResponse> GetCurrentPreviewSceneAsync() {
            var requestData = new GetCurrentPreviewSceneRequest() {  };
            var responseData = await SendRequestAsync<GetCurrentPreviewSceneResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentPreviewSceneResponse> SetCurrentPreviewSceneAsync(string sceneName) {
            var requestData = new SetCurrentPreviewSceneRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<SetCurrentPreviewSceneResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<CreateSceneResponse> CreateSceneAsync(string sceneName) {
            var requestData = new CreateSceneRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<CreateSceneResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<RemoveSceneResponse> RemoveSceneAsync(string sceneName) {
            var requestData = new RemoveSceneRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<RemoveSceneResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneNameResponse> SetSceneNameAsync(string sceneName, string newSceneName) {
            var requestData = new SetSceneNameRequest() { sceneName = sceneName, newSceneName = newSceneName };
            var responseData = await SendRequestAsync<SetSceneNameResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneSceneTransitionOverrideResponse> GetSceneSceneTransitionOverrideAsync(string sceneName) {
            var requestData = new GetSceneSceneTransitionOverrideRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<GetSceneSceneTransitionOverrideResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetSceneSceneTransitionOverrideResponse> SetSceneSceneTransitionOverrideAsync(string sceneName, string transitionName, long transitionDuration) {
            var requestData = new SetSceneSceneTransitionOverrideRequest() { sceneName = sceneName, transitionName = transitionName, transitionDuration = transitionDuration };
            var responseData = await SendRequestAsync<SetSceneSceneTransitionOverrideResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSourceActiveResponse> GetSourceActiveAsync(string sourceName) {
            var requestData = new GetSourceActiveRequest() { sourceName = sourceName };
            var responseData = await SendRequestAsync<GetSourceActiveResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSourceScreenshotResponse> GetSourceScreenshotAsync(string sourceName, string imageFormat, long imageWidth, long imageHeight, long imageCompressionQuality) {
            var requestData = new GetSourceScreenshotRequest() { sourceName = sourceName, imageFormat = imageFormat, imageWidth = imageWidth, imageHeight = imageHeight, imageCompressionQuality = imageCompressionQuality };
            var responseData = await SendRequestAsync<GetSourceScreenshotResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SaveSourceScreenshotResponse> SaveSourceScreenshotAsync(string sourceName, string imageFormat, string imageFilePath, long imageWidth, long imageHeight, long imageCompressionQuality) {
            var requestData = new SaveSourceScreenshotRequest() { sourceName = sourceName, imageFormat = imageFormat, imageFilePath = imageFilePath, imageWidth = imageWidth, imageHeight = imageHeight, imageCompressionQuality = imageCompressionQuality };
            var responseData = await SendRequestAsync<SaveSourceScreenshotResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetStreamStatusResponse> GetStreamStatusAsync() {
            var requestData = new GetStreamStatusRequest() {  };
            var responseData = await SendRequestAsync<GetStreamStatusResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<ToggleStreamResponse> ToggleStreamAsync() {
            var requestData = new ToggleStreamRequest() {  };
            var responseData = await SendRequestAsync<ToggleStreamResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StartStreamResponse> StartStreamAsync() {
            var requestData = new StartStreamRequest() {  };
            var responseData = await SendRequestAsync<StartStreamResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<StopStreamResponse> StopStreamAsync() {
            var requestData = new StopStreamRequest() {  };
            var responseData = await SendRequestAsync<StopStreamResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SendStreamCaptionResponse> SendStreamCaptionAsync(string captionText) {
            var requestData = new SendStreamCaptionRequest() { captionText = captionText };
            var responseData = await SendRequestAsync<SendStreamCaptionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetTransitionKindListResponse> GetTransitionKindListAsync() {
            var requestData = new GetTransitionKindListRequest() {  };
            var responseData = await SendRequestAsync<GetTransitionKindListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetSceneTransitionListResponse> GetSceneTransitionListAsync() {
            var requestData = new GetSceneTransitionListRequest() {  };
            var responseData = await SendRequestAsync<GetSceneTransitionListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetCurrentSceneTransitionResponse> GetCurrentSceneTransitionAsync() {
            var requestData = new GetCurrentSceneTransitionRequest() {  };
            var responseData = await SendRequestAsync<GetCurrentSceneTransitionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentSceneTransitionResponse> SetCurrentSceneTransitionAsync(string transitionName) {
            var requestData = new SetCurrentSceneTransitionRequest() { transitionName = transitionName };
            var responseData = await SendRequestAsync<SetCurrentSceneTransitionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentSceneTransitionDurationResponse> SetCurrentSceneTransitionDurationAsync(long transitionDuration) {
            var requestData = new SetCurrentSceneTransitionDurationRequest() { transitionDuration = transitionDuration };
            var responseData = await SendRequestAsync<SetCurrentSceneTransitionDurationResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetCurrentSceneTransitionSettingsResponse> SetCurrentSceneTransitionSettingsAsync(object transitionSettings, bool overlay) {
            var requestData = new SetCurrentSceneTransitionSettingsRequest() { transitionSettings = transitionSettings, overlay = overlay };
            var responseData = await SendRequestAsync<SetCurrentSceneTransitionSettingsResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetCurrentSceneTransitionCursorResponse> GetCurrentSceneTransitionCursorAsync() {
            var requestData = new GetCurrentSceneTransitionCursorRequest() {  };
            var responseData = await SendRequestAsync<GetCurrentSceneTransitionCursorResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<TriggerStudioModeTransitionResponse> TriggerStudioModeTransitionAsync() {
            var requestData = new TriggerStudioModeTransitionRequest() {  };
            var responseData = await SendRequestAsync<TriggerStudioModeTransitionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetTBarPositionResponse> SetTBarPositionAsync(long position, bool release) {
            var requestData = new SetTBarPositionRequest() { position = position, release = release };
            var responseData = await SendRequestAsync<SetTBarPositionResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetStudioModeEnabledResponse> GetStudioModeEnabledAsync() {
            var requestData = new GetStudioModeEnabledRequest() {  };
            var responseData = await SendRequestAsync<GetStudioModeEnabledResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<SetStudioModeEnabledResponse> SetStudioModeEnabledAsync(bool studioModeEnabled) {
            var requestData = new SetStudioModeEnabledRequest() { studioModeEnabled = studioModeEnabled };
            var responseData = await SendRequestAsync<SetStudioModeEnabledResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<OpenInputPropertiesDialogResponse> OpenInputPropertiesDialogAsync(string inputName) {
            var requestData = new OpenInputPropertiesDialogRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<OpenInputPropertiesDialogResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<OpenInputFiltersDialogResponse> OpenInputFiltersDialogAsync(string inputName) {
            var requestData = new OpenInputFiltersDialogRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<OpenInputFiltersDialogResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<OpenInputInteractDialogResponse> OpenInputInteractDialogAsync(string inputName) {
            var requestData = new OpenInputInteractDialogRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<OpenInputInteractDialogResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<GetMonitorListResponse> GetMonitorListAsync() {
            var requestData = new GetMonitorListRequest() {  };
            var responseData = await SendRequestAsync<GetMonitorListResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<OpenVideoMixProjectorResponse> OpenVideoMixProjectorAsync(string videoMixType, long monitorIndex, string projectorGeometry) {
            var requestData = new OpenVideoMixProjectorRequest() { videoMixType = videoMixType, monitorIndex = monitorIndex, projectorGeometry = projectorGeometry };
            var responseData = await SendRequestAsync<OpenVideoMixProjectorResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
        async public Task<OpenSourceProjectorResponse> OpenSourceProjectorAsync(string sourceName, long monitorIndex, string projectorGeometry) {
            var requestData = new OpenSourceProjectorRequest() { sourceName = sourceName, monitorIndex = monitorIndex, projectorGeometry = projectorGeometry };
            var responseData = await SendRequestAsync<OpenSourceProjectorResponse>(requestData);
            return GetResponseOrThrow(responseData);
        }
    }
}
