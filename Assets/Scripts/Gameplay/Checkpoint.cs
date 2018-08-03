using UnityEngine;

namespace Gameplay
{

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint ActiveCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ActiveCheckpoint = this;
    }
}

}