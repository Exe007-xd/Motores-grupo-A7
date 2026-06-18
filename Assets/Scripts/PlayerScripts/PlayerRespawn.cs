using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] string defaultCheckpointID = "default";

    void Start()
    {
        LoadLastCheckpoint();
    }

    public void LoadLastCheckpoint()
    {
        string id = PlayerPrefs.GetString("lastCheckpoint", defaultCheckpointID);

        if (!PlayerPrefs.HasKey(id + "_x"))
        {
            
            Debug.Log("No checkpoint guardado, usando posición inicial.");
            return;
        }

        Vector3 pos = new Vector3(
            PlayerPrefs.GetFloat(id + "_x"),
            PlayerPrefs.GetFloat(id + "_y"),
            PlayerPrefs.GetFloat(id + "_z")
        );

        Vector3 euler = new Vector3(
            PlayerPrefs.GetFloat(id + "_rx"),
            PlayerPrefs.GetFloat(id + "_ry"),
            PlayerPrefs.GetFloat(id + "_rz")
        );

        transform.position = pos;
        transform.rotation = Quaternion.Euler(euler);
        Debug.Log($"Jugador respawneado en checkpoint: {id}");
    }

    
    public void RespawnPlayer()
    {
        LoadLastCheckpoint();
        
    }

    
    public void RespawnByReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
