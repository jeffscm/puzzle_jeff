using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager : MonoBehaviour {

    public Sprite[] textures;
    public ClickCard prefab;

    public Transform deactivatedHolder, gameHolder;

    List<ClickCard> list = new List<ClickCard>();

    public ClickCard GetCard()
    {
        var temp = list.FirstOrDefault(t => string.IsNullOrEmpty(t.gameObject.name));

        if (temp == null)
        {
            temp = Instantiate(prefab, gameHolder);
            list.Add(temp);
        }
        else
        {
            temp.transform.SetParent(gameHolder);
        }
        temp.ResetThis();
        temp.name = "GAMECARD";
        return temp;
    }

    public void ResetAll()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.name = string.Empty;
            list[i].transform.SetParent(deactivatedHolder);
        }
    }

    public void Randomize(int qtd)
    {

        var idx = new int[qtd];
        for (int i = 0; i < idx.Length; i++) idx[i] = -1;
        int newIdx = -1;

        for (int i = 0; i < idx.Length; i++)
        {
            if (idx[i] != -1) continue;
            newIdx = Random.Range(1, 10);
            idx[i] = newIdx;
            while (true)
            {
                int r = Random.Range(0, idx.Length);
                if (idx[r] == -1)
                {
                    idx[r] = newIdx;
                    newIdx = -1;
                    break;
                }
            }
        }
        newIdx = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (!string.IsNullOrEmpty(list[i].gameObject.name))
            {
                list[i].idxType = idx[newIdx];
                newIdx++;
                if (newIdx >= idx.Length)
                {
                    break;
                }
            }
        }
    }

    public bool HasChildren ()
    {
        return gameHolder.childCount > 0;
    }

    public void ResetThis(ClickCard resetobj)
    {
        resetobj.transform.SetParent(deactivatedHolder);
        resetobj.gameObject.name = string.Empty;
    }
}
