using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

enum Lane
{
    top = 1,
    mid = 0,
    bot = -1,
}

public class Movement : MonoBehaviour
{
    private Lane position = Lane.mid;

    void Update()
    {
        InputManager();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManager()
    {
        if ((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (position == Lane.mid)
            {
                position = Lane.top;
            }
            else if (position == Lane.bot)
            {
                position = Lane.mid;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            if (position == Lane.top)
            {
                position = Lane.mid;
            }
            else if (position == Lane.mid)
            {
                position = Lane.bot;
            }
        }
    }

    void Move()
    {
        float y = this.position == Lane.top ? 1f : this.position == Lane.mid ? 0f : -1f;
        float x = this.transform.position.x;

        Vector3 targetPosition = new Vector3(-6, y, this.transform.position.z);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        float lerpSpeed = 1f; // Ubah kecepatan sesuai keinginan
        float timeToReachTarget = distanceToTarget / lerpSpeed;

        // Mendapatkan semua obstacle yang ada pada scene
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        bool isCollidingWithObstacle = false;
        foreach (GameObject obstacle in obstacles)
        {
            // Mendapatkan lebar obstacle
            float obstacleWidth = obstacle.GetComponent<Renderer>().bounds.size.x;

            // Jika player menabrak obstacle, tandai bahwa player menabrak obstacle dan keluar dari loop
            if (Mathf.Abs(obstacle.transform.position.x - x) < obstacleWidth / 2)
            {
                isCollidingWithObstacle = true;
                break;
            }
        }

        // Menggerakkan player ke posisi baru
        Vector3 newPosition = isCollidingWithObstacle ? targetPosition : new Vector3(x, y, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, 0.1f);

        // Menggerakkan player ke posisi baru secara perlahan
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / timeToReachTarget);

    }

}