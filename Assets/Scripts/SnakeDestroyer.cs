using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeDestroyer : MonoBehaviour
{
    public IEnumerator DestroyIO(List<GameObject> gameObjects)
    {

        foreach (GameObject gmb in Enumerable.Reverse(gameObjects))
        {
            if (gmb != null)
            {
                var soundPlayer = gmb.GetComponent<AudioSource>();

                var animator = gmb.AddComponent<SnakeBodyAnimatorHolder>();
                if (gameObjects[0] == gmb)
                {

                    animator.animateHead();
                }
                else animator.animate();
                soundPlayer.Play();
                yield return new WaitForSeconds(0.06f);

                Destroy(gmb);
            }
            if (gmb == gameObjects[0])
            {
                GameObject grid = GameObject.FindGameObjectWithTag("GridManager");
                grid.GetComponent<Grid>().WinText();
            }
                
        }
    }
}
