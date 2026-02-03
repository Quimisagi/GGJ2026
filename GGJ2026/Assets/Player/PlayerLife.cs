using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public Camera cameraobj;
    public int life = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        PlayerDie();
    }

    private void DeclineEnemy()
    {
        life--;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") ||
           other.gameObject.CompareTag("Obstacle"))
        {
            DeclineEnemy();
        }
    }

    private void PlayerDie()
    {
        if (life <= 0)
        {
            cameraobj.transform.SetParent(null);
            Destroy(this.gameObject);

            SceneManager.LoadScene("GameOverScene");
            Debug.Log("Ž€‚ñ‚¾");
        }
    }
}
