using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] string checkpointID = ""; 
    [SerializeField] bool savePosition = true;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 pos = savePosition ? transform.position : Vector3.zero;
        Quaternion rot = savePosition ? transform.rotation : Quaternion.identity;

        
        string id = string.IsNullOrEmpty(checkpointID) ? "default" : checkpointID;
        PlayerPrefs.SetFloat(id + "_x", pos.x);
        PlayerPrefs.SetFloat(id + "_y", pos.y);
        PlayerPrefs.SetFloat(id + "_z", pos.z);
        PlayerPrefs.SetFloat(id + "_rx", rot.eulerAngles.x);
        PlayerPrefs.SetFloat(id + "_ry", rot.eulerAngles.y);
        PlayerPrefs.SetFloat(id + "_rz", rot.eulerAngles.z);
        PlayerPrefs.SetString("lastCheckpoint", id);
        PlayerPrefs.Save();

        
        Debug.Log($"Checkpoint guardado: {id}");
    }
}
