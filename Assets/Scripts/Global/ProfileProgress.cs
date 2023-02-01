using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[Serializable]
public class ProfileProgress
{
    public static string profileGameData;

    private int _wins;
    private int _losses;
    public int Wins
    {
        get => _wins;
        set => _wins = value;
    }

    public int Losses
    {
        get => _losses;
        set => _losses = value;
    }

    public void SaveLocalProfile()
    {
        File.WriteAllBytes(profileGameData, ToBytes());
    }

    public static ProfileProgress LoadLocalProfile()
    {
        if (File.Exists(profileGameData))
        {
            return FromBytes(File.ReadAllBytes(profileGameData));
        }

        return new ProfileProgress();
    }

    public byte[] ToBytes()
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, this);
        return ms.ToArray();
    }

    public static ProfileProgress FromBytes(byte[] arrBytes)
    {
        if (arrBytes == null)
        {
            return new ProfileProgress();
        }

        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        ProfileProgress profile = (ProfileProgress)binForm.Deserialize(memStream);
        return profile;
    }
}
