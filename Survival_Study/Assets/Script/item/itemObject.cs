using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    public string GetinteractPrompt();
    public void OnInteract();
}

public class itemObject : MonoBehaviour, Iinteractable
{
    public itemData data;

    public string GetinteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
