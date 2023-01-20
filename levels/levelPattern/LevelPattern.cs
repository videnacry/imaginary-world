public class LevelPattern : Level
{
    // Start is called before the first frame update
    int index = 0;
    public UnityEngine.Sprite cover;
    public enum directions {left, top, right, down, topLeft, topRight, downLeft, downRight};
    public UnityEngine.Events.UnityAction[] actions = {Left, Top, Right, Down, TopLeft, TopRight, DownLeft, DownRight};
    [System.Serializable]
    public struct SpriteDirection
    {
        public UnityEngine.Sprite sprite;
        public LevelPattern.directions direction;
    }
    public SpriteDirection[] spriteDirections;
    public System.Collections.Generic.Dictionary<UnityEngine.Sprite, LevelPattern.directions> dictionary = new System.Collections.Generic.Dictionary<UnityEngine.Sprite, LevelPattern.directions>();

    [UnityEngine.SerializeField]
    public UnityEngine.GameObject[] patternsGameObject;
    public UnityEngine.GameObject[] choisesGameObject;
    public LevelPattern.Pattern[] patterns;
    public LevelPattern.Pattern[] choises;


    //Auxialiar classes
    #region
    // Update is called once per frame
    [System.Serializable]
    public class Pattern
    {
        public UnityEngine.Sprite[] units;
    }
    #endregion


    void Start()
    {
        foreach (SpriteDirection spriteDirection in this.spriteDirections) this.dictionary.Add(spriteDirection.sprite, spriteDirection.direction);
        this.ChangeSprites();
    }

    public void ChangeSprites()
    {
        for (System.Int16 index = 0; index < this.patternsGameObject.Length; index++) {
            this.patternsGameObject[index].GetComponent<UnityEngine.UI.Image>().sprite = this.patterns[this.index].units[index];
        }
        int num = UnityEngine.Random.Range(0, this.patternsGameObject.Length);
        this.patternsGameObject[num].GetComponent<UnityEngine.UI.Image>().sprite = this.cover;
        System.Collections.Generic.List<UnityEngine.Sprite> units = new System.Collections.Generic.List<UnityEngine.Sprite>();
        units.AddRange(this.choises[this.index].units);
        UnityEngine.Sprite unitAnswer = this.patterns[this.index].units[num];
        foreach (UnityEngine.Sprite unit in units) {
            if (unit == this.patterns[this.index].units[num]) {
                unitAnswer = unit;
            }
        }
        units.Remove(unitAnswer);
        num = 0;
        while (this.choisesGameObject.Length > num) {
            int index = UnityEngine.Random.Range(0,(4-num));
            this.choisesGameObject[num].GetComponent<UnityEngine.UI.Image>().sprite = units[index];
            this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(this.Wrap(units[index]));
            units.RemoveAt(index);
            num++;
        }
        num = UnityEngine.Random.Range(0,4);
        this.choisesGameObject[num].GetComponent<UnityEngine.UI.Image>().sprite = unitAnswer;
        this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(this.Wrap(unitAnswer));
    }
    public UnityEngine.Events.UnityAction Wrap(UnityEngine.Sprite pSprite)
    {
        return () => {
                this.actions[(int)this.dictionary[pSprite]]();
                this.index++;
                this.ChangeSprites();
            };
    }


    public static void Left()
    {
        UnityEngine.Debug.Log("Left");
    }
    public static void Top()
    {
        UnityEngine.Debug.Log("Top");
    }
    public static void Right()
    {
        UnityEngine.Debug.Log("Right");
    }
    public static void Down()
    {
        UnityEngine.Debug.Log("Down");
    }
    public static void TopLeft()
    {
        UnityEngine.Debug.Log("TopLeft");
    }
    public static void DownLeft()
    {
        UnityEngine.Debug.Log("DownLeft");
    }
    public static void TopRight()
    {
        UnityEngine.Debug.Log("TopRight");
    }
    public static void DownRight()
    {
        UnityEngine.Debug.Log("DownRight");
    }
}
