using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int inititalSize = 1;
    public Text score;
    public GameObject Game_Over_UI;

    private void Start()
    {
        ResetState();

    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up; 
        }
       else if (Input.GetKeyDown(KeyCode.S)) 
        { 
            _direction = Vector2.down; 
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }

       score.text = "Points: " + _segments.Count;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    private void Grow()
    {
        Transform segment =  Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.inititalSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            Game_Over_UI.SetActive(true);
            Time.timeScale = 0;
            ResetState();
        }
    }

    #region Menu_Options
    public void Reset()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    #endregion

}
