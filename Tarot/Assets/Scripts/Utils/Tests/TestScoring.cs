using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScoring : MonoBehaviour {

    public ScoringData scoringData;

	void Start () {

        scoringData.PrintSummary();
    }

}
