namespace Base.Game.Manager
{
    using Base.Game.Data;
    using UnityEngine;

    public class FileManager : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            if (PlayerPrefs.HasKey("PlayerData"))
            {
                _player = new Player(JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData")));
            }
            else
            {
                PlayerData playerData = new PlayerData();
                playerData.coin = 0;
                PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(playerData));
                _player = new Player(playerData);
            }
        }
    }
}
