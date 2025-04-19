The Razor component InputSelectorTenant can be improved by implementing an extenstion of InputSelector as InputSelector<Tenant>

# SignalR 
I have implemented a Registry-Dispatcher-Observer pattern, where there is a registry for Camera components, where each instantiated component registers with the Registry-Dispatcher.
The Registry-Dispatcher connects to the SignalR Hub, so that there is only one SignalR client per Asp.Net connection, for the lifetime of the connection that persists over multiple possible selections for Camera Tenant.
This reduces the load on SignalR handling.
The Registry-Dispatcher sends an update to only the affected component, using Camera Identifier as the key for routing. 
This allows for a lightweight and high-performance update of the UI 

# Filtering the Camera Table
I have provided a Camera Filter Context Object, which is injected for all Camera Components. The filter object derived from CameraModel class that provides a method PassFilter. Each property of the filter class is nullable and is null unless a filter is set.