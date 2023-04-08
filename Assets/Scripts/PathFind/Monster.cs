using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject pointObject;
    [SerializeField] Transform player;
    Path monsterPath = new Path();
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator StartPath()
    //{
    //    List<Vector2> firstPath = new List<Vector2>();
    //    yield return StartCoroutine(monsterPath.Search(transform.position, player.position, MapGrid.OpenMap, (path) => firstPath = path));
    //}

    void DrawPath(List<Vector2> path)
    {
        foreach (Vector2 point in path)
        {
            Instantiate(pointObject, point, Quaternion.identity);
        }
    }
}
