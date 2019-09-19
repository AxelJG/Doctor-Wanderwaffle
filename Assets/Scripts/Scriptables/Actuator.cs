using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="New Actuator", menuName="Medical Actuator")]
public class Actuator : ScriptableObject
{
   public uint id;
   public new string name;
   public string utility;
   public Sprite sprite;
public enum loadingTimeOptions { fast, medium, slow, verySlow };
   public loadingTimeOptions loadingTime;
}
