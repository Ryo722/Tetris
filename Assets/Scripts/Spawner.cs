using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Mino[] Minos;

    // テトリミノをランダムに1つ取得
    Mino GetRandomMino()
    {
        int i = Random.Range(0, Minos.Length);

        if (Minos[i])
        {
            return Minos[i];
        }
        else
        {
            return null;
        }
    }

    public Mino SpawnMino()
    {
        Mino mino = Instantiate(GetRandomMino(), transform.position, Quaternion.identity);

        if (mino)
        {
            return mino;
        }
        else
        {
            return null;
        }
    }
}
