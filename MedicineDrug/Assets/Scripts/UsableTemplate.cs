using UnityEngine;

[CreateAssetMenu(fileName = "UsableTemplate", menuName = "ScriptableObjects/UsableTemplate", order = 1)]

public class UsableTemplate : ScriptableObject
{
   [SerializeField] public float interactTime;
   [SerializeField] public UsableTemplate toolNeeded;
   [SerializeField] public bool droppableOnFloor;
}
