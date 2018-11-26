using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Runtime Card List")]
public class RunTimeCardList : RuntimeList<Card>
{
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Card card in Items)
        {
            sb.AppendLine(card.ToString());
        }
        return sb.ToString();
    }


    public float GetPoints()
	{
		float points = 0f;
		foreach (Card card in this.Items)
		{
			points += card.Value;
		}
		return points;
	}
}