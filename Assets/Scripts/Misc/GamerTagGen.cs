using TMPro;
using TMPro;
using UnityEngine;

public class GamerTagGen : MonoBehaviour
{
    public TextMeshProUGUI gamertagText;

    private static string[] FirstPart = { "killer", "mani24", "Hateful", "XX", "xXx", "Epic", "T-T", "loser", "Crazy_", "Wise", "" , "BEEZ", "TUIF"};
    private static string[] SecondPart = { "G0d", "Ninjaa", "Champ", "Hero", "Legend", "G_Ratta", "Gamer", "Cyndrom", "bmason", "ID-Sketch", "Leighton", "Maulol", "MaxCool", "Xl-Pepple", "AdenoidSmile", "GIFTED", "IDKWHATTOPUTLOL", "LoL", "Pyhco" };

    private void Start()
    {
        GenerateGamertag();
    }

    public void GenerateGamertag()
    {
        string adjective = FirstPart[Random.Range(0, FirstPart.Length)];
        string noun = SecondPart[Random.Range(0, SecondPart.Length)];
        string number = Random.Range(0, 100).ToString().PadLeft(2, '0');

        string gamertag = adjective + "_" + noun + "_" + number;
        gamertagText.text = gamertag;
    }
}
