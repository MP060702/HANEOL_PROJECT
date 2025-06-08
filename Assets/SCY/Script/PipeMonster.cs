using UnityEngine;
using System.Collections;

public class PipeMonster : MonoBehaviour
{
    public PipeMonsterHead Head;

    void Start()
    {
        InvokeRepeating("CallEveryThreeSeconds", 5f, 5f);
    }

    void CallEveryThreeSeconds()
    {
        if (Head != null)
        {
            Head.gameObject.SetActive(true);
            Head.transform.localPosition = new Vector3(0, 0.5f, 0);
            StopAllCoroutines();
            StartCoroutine(HeadMoveRoutine());
        }
    }

    System.Collections.IEnumerator HeadMoveRoutine()
    {
        // 올라가기
        float duration = 0.75f;
        float elapsed = 0f;
        Vector3 start = new Vector3(0, 0.5f, 0);
        Vector3 end = new Vector3(0, 1f, 0);
        while (elapsed < duration)
        {
            Head.transform.localPosition = Vector3.Lerp(start, end, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return null;
        Head.transform.localPosition = end;

        // 잠깐 대기
        yield return new WaitForSeconds(0.5f);

        // 내려가기
        elapsed = 0f;
        while (elapsed < duration)
        {
            Head.transform.localPosition = Vector3.Lerp(end, start, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return null;
        Head.transform.localPosition = start;

        // 비활성화
        Head.gameObject.SetActive(false);
    }
} 