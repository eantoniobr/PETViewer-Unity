using System;
using System.IO;
using UnityEngine;

public class PETViewer : MonoBehaviour
{
    private const string PetPath = "local_ignored_pets";
    private const string TextureSearchPath = "local_ignored_pets";

    private void Start()
    {
//        Vector3 spacing = new Vector3(3, 0, 0);
//        ViewAllPets(spacing);
        ViewSinglePet("local_ignored_pets/item/ase/item0_34.pet");
        ViewSinglePet("local_ignored_pets/item/ase/item0_01.pet");
    }

    private void ViewAllPets(Vector3 spacing)
    {
        string[] files = Directory.GetFiles(PetPath, "*.pet", SearchOption.AllDirectories);

        Vector3 lastPos = new Vector3(0, 1, 0);
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            try
            {
                Debug.Log("Creating object from file: " + file);
                PangObject pangObject = new PangObject(file, TextureSearchPath);
                GameObject go = pangObject.GameObject;
                go.transform.position = lastPos;
                lastPos += spacing;
            }
            catch (TextureNotFoundException e)
            {
                // files with missing textures will be created but not visible
                Debug.LogWarning(e.Message);
            }
            catch (Exception e)
            {
                // files that can't be parsed won't be created at all
                Debug.LogError(e.ToString());
            }
        }
    }

    private void ViewSinglePet(string filePath)
    {
        PangObject pangObject = new PangObject(filePath, TextureSearchPath);
        pangObject.GameObject.transform.position = Vector3.zero;
    }
}
