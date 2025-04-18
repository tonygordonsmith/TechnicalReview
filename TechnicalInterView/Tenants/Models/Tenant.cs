using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace TechnicalInterView.Tenants.Models;

public class Tenant
{
    [Description("Tenant Identifier")]
    public string Id { get; set; }
    public string DisplayName {get {return Id;} }
    public Tenant()
    {
        Id = Guid.NewGuid().ToString();
    }
    public Tenant(string id)
    {
        Id = id;
    }
}
