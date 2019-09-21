using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pathology", menuName = "Pathology")]
public class Pathology : ScriptableObject {
    public uint id;
    public string namePathology;
    public float timeOfLife;
    public enum DifficultyState {very_easy, easy, medium, hard, very_hard };
    public DifficultyState difficulty;
    public List<Actuator> actuators;
    public Sprite sprite;
}
