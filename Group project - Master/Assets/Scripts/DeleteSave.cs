using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    public void deleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
