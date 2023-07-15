using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Information", order = 1, fileName = "New Wave Information")]
public class WaveInfo : ScriptableObject
{

    public int numberOfArchers;
    public int numberOfTownies;
    public int numberOfHeros;

}
