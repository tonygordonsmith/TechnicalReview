using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR.Client;
using TechnicalInterView.Cameras.Models;

namespace TechnicalInterView.Services;

public class ComponentRegistryService
{
    private readonly HubConnection _hubConnection;
    private bool _isInitialized = false;

    private readonly ConcurrentDictionary<string, Action<CameraModel>> _cameraCallbacks = new();

    public ComponentRegistryService(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized)
            return;

        _hubConnection.On<string, string>("ReceiveStatusUpdate", (cameraId, newStatus) =>
        {
            if (_cameraCallbacks.TryGetValue(cameraId, out var callback))
            {
                var updatedCamera = new CameraModel
                {
                    CameraId = cameraId,
                    Status = newStatus
                };

                callback.Invoke(updatedCamera);
            }
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        _isInitialized = true;
    }

    public void RegisterCallback(string cameraId, Action<CameraModel> callback)
    {
        if (string.IsNullOrEmpty(cameraId) || callback == null)
            return;
        _cameraCallbacks[cameraId] = callback;
    }

    public void UnregisterCallback(string cameraId)
    {
        if (string.IsNullOrEmpty(cameraId))
            return;
        _cameraCallbacks.TryRemove(cameraId, out _);
    }
}

