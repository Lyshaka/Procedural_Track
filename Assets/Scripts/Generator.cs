using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	List<Vector2Int> trackList = new List<Vector2Int>();
	LineRenderer lineRenderer;

	void GenerateTrack()
	{
		int index = trackList.Count - 1;
		List<Vector2Int> validTracks = new List<Vector2Int>();

		if (IsValid(trackList[index].x + 1, trackList[index].y)) validTracks.Add(new Vector2Int(trackList[index].x + 1, trackList[index].y));
		if (IsValid(trackList[index].x - 1, trackList[index].y)) validTracks.Add(new Vector2Int(trackList[index].x - 1, trackList[index].y));
		if (IsValid(trackList[index].x, trackList[index].y + 1)) validTracks.Add(new Vector2Int(trackList[index].x, trackList[index].y + 1));
		if (IsValid(trackList[index].x, trackList[index].y - 1)) validTracks.Add(new Vector2Int(trackList[index].x, trackList[index].y - 1));

		trackList.Add(validTracks[Random.Range(0, validTracks.Count)]);

		Debug.Log("Index : " + index + " ; Value : " + trackList[index]);
	}

	bool IsValid(int x, int y)
	{
		for (int i = 0; i < trackList.Count; i++)
		{
			if (trackList[i].x == x && trackList[i].y == y)
				return false;
		}
		return true;
	}

	void PrintTrack()
	{
		string result = "";
		for (int i = 0; i < trackList.Count; i++)
		{
			result += "{" + i + "}[" + trackList[i].x + ";" + trackList[i].y + "]\n";
		}
		Debug.Log("Result :\n" + result);
	}

	void ShowTrack()
	{
		lineRenderer.positionCount = trackList.Count;
		if (trackList.Count == 0)
			return;
		for (int i = 0; i < trackList.Count; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(trackList[i].x, 0, trackList[i].y));
		}
	}

	private void Start()
	{
		trackList.Add(Vector2Int.zero);
		lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(TimeGen(0.1f));
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			for (int i = 0; i < 50; i++)
			{
				GenerateTrack();
			}
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			PrintTrack();
			ShowTrack();
		}
	}

	IEnumerator TimeGen(float time)
	{
		GenerateTrack();
		ShowTrack();
        yield return new WaitForSeconds(time);
		StartCoroutine(TimeGen(time));
	}
}
