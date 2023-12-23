using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id;

    [SerializeField] private static SerializedDictionary<string, GameObject> idDatabase = new SerializedDictionary<string, GameObject>();

    public string ID => _id;

    private void Awake()
    {
        if (idDatabase == null) idDatabase = new SerializedDictionary<string, GameObject>();

        if (idDatabase.ContainsKey(_id)) Generate();
        else idDatabase.Add(_id, gameObject);
    }

    private void OnDestroy()
    {
        if (idDatabase.ContainsKey(_id)) idDatabase.Remove(_id);
    }

    [ContextMenu("GenerateID")]
    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        idDatabase.Add(_id, gameObject);
        Debug.Log(idDatabase.Count);
    }


}
