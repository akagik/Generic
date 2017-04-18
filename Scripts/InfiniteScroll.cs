using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
    [SerializeField]
    public RectTransform itemPrototype;

    [SerializeField, Range(0, 30)]
    public int instantateItemCount = 9;

    public Direction direction;

    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

    [System.NonSerialized]
    public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    protected float diffPreFramePosition = 0;

    protected int currentItemNo = 0;

    public enum Direction
    {
        Vertical,
        Horizontal,
    }

    // cache component

    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    private float anchoredPosition
    {
        get
        {
            return direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
        }
    }

    private float _itemScale = -1;
    public float itemScale
    {
        get
        {
            if (itemPrototype != null && _itemScale == -1)
            {
                _itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y : itemPrototype.sizeDelta.x;
            }
            return _itemScale;
        }
    }

    public ScrollRect scrollRect;

    protected override void Awake()
    {
        itemPrototype.gameObject.SetActive(false);
    }

    public void Init(int firstIndex, int total)
    {
        scrollRect.horizontal = direction == Direction.Horizontal;
        scrollRect.vertical = direction == Direction.Vertical;
        scrollRect.content = rectTransform;

        itemPrototype.gameObject.SetActive(false);

        if (direction == Direction.Vertical)
        {
            float width = itemPrototype.sizeDelta.y * total;
            rectTransform.sizeDelta = new Vector2(0, width);
            rectTransform.SetLocalY(firstIndex * itemPrototype.sizeDelta.y);
        }
        else
        {
            float width = itemPrototype.sizeDelta.x * total;
            rectTransform.sizeDelta = new Vector2(width, 0);
            rectTransform.SetLocalX(firstIndex * itemPrototype.sizeDelta.x);
        }

        diffPreFramePosition = -itemScale * firstIndex;
        currentItemNo = firstIndex;

        Debug.LogFormat("{0} {1} {2}", anchoredPosition, diffPreFramePosition, currentItemNo);

        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = GameObject.Instantiate(itemPrototype) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemScale * (i + firstIndex)) : new Vector2(itemScale * (i + firstIndex), 0);
            itemList.AddLast(item);
            item.gameObject.SetActive(true);

            onUpdateItem.Invoke(currentItemNo + i, item.gameObject);
        }
    }

    public void SetCurrentTopIndex(int index)
    {
        rectTransform.SetLocalY(index * itemPrototype.sizeDelta.y);
        diffPreFramePosition = -itemScale * index;
        currentItemNo = index;

        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = itemList.ElementAt(i);

            float pos = itemScale * (i + index);
            item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -pos) : new Vector2(pos, 0);
            onUpdateItem.Invoke(currentItemNo + i, item.gameObject);
        }
    }

    void Update()
    {
        while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
        {
            diffPreFramePosition -= itemScale;

            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);

            var pos = itemScale * (instantateItemCount + currentItemNo);
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

            onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

            currentItemNo++;
        }

        while (anchoredPosition - diffPreFramePosition > 0)
        {
            diffPreFramePosition += itemScale;

            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);

            currentItemNo--;

            var pos = itemScale * currentItemNo;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
            onUpdateItem.Invoke(currentItemNo, item.gameObject);
        }
    }

    [System.Serializable]
    public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}
