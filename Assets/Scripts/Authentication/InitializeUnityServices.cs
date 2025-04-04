using System;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class InitializeUnityServices : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
