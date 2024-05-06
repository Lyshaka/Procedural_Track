using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Generator2 : MonoBehaviour
{
	[SerializeField] private Vector3Int[] tracksDeltas;
	[SerializeField] private List<Track> trackList = new List<Track>();
	LineRenderer lineRenderer;

	Vector3Int GetRandomValidSpot()
	{
		List<Vector3Int> validTracks = new List<Vector3Int>();
		
		for (int i = 0; i < tracksDeltas.Length; i++)
		{
			for (int j = 0; j < trackList.Count; j++)
			{
				if (trackList[trackList.Count - 1].end + tracksDeltas[i] != trackList[j].end)
				{
					validTracks.Add(tracksDeltas[i]);
				}
			}
		}
		
		return validTracks[UnityEngine.Random.Range(0, validTracks.Count)];
	}

	[Serializable]
	public struct Track
	{
		public Vector3Int start;
		public Vector3Int end;

		public Track(Vector3Int s, Vector3Int e)
		{
			start = s;
			end = e;
		}
	}

	void AddTrack(Vector3Int pos)
	{
		Track track = new Track(trackList[trackList.Count - 1].end, trackList[trackList.Count - 1].end + pos);
		trackList.Add(track);
	}

	void ShowTrack()
	{
		lineRenderer.positionCount = trackList.Count;
		if (trackList.Count == 0)
			return;
		lineRenderer.SetPosition(0, new Vector3(trackList[0].start.x, trackList[0].start.y, trackList[0].start.z));
		for (int i = 0; i < trackList.Count; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(trackList[i].end.x, trackList[i].end.y, trackList[i].end.z));
		}
	}

	IEnumerator GenerateTrack()
	{
		AddTrack(GetRandomValidSpot());
		ShowTrack();
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(GenerateTrack());
	}

	// Start is called before the first frame update
	void Start()
	{
		trackList.Add(new Track());
		lineRenderer = GetComponent<LineRenderer>();
		StartCoroutine(GenerateTrack());
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
