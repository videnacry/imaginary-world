public class LevelPattern : Level
{
    // Start is called before the first frame update
    int index = 0;
    public UnityEngine.Sprite cover;

    public enum directions {left, top, right, down, topLeft, topRight, downLeft, downRight};
    public SpriteVector2[] spriteVector2s;
    public System.Collections.Generic.Dictionary<UnityEngine.Sprite, UnityEngine.Vector2> dictionary = new System.Collections.Generic.Dictionary<UnityEngine.Sprite, UnityEngine.Vector2>();

    public UnityEngine.GameObject[] patternsGameObject;
    public UnityEngine.GameObject[] choisesGameObject;
    public LevelPattern.Pattern[] patterns;
    public LevelPattern.Pattern[] choises;


    //Auxialiar classes
    #region
    // Update is called once per frame
    [System.Serializable]
    public struct SpriteVector2
    {
        public UnityEngine.Sprite sprite;
        public UnityEngine.Vector2 vector2;
    }

    [System.Serializable]
    public class Pattern
    {
        public UnityEngine.Sprite[] units;
    }
    #endregion


    void Start()
    {
        foreach (SpriteVector2 spriteDirection in this.spriteVector2s) this.dictionary.Add(spriteDirection.sprite, spriteDirection.vector2);
        this.ChangeSprites();
    }

    public void ChangeSprites()
    {
        for (System.Int16 index = 0; index < this.patternsGameObject.Length; index++) {
            this.patternsGameObject[index].GetComponent<UnityEngine.UI.Image>().sprite = this.patterns[this.index].units[index];
        }
        System.Int16 answerIndex = (System.Int16) UnityEngine.Random.Range(0, this.patternsGameObject.Length);
        int num = 0;
        this.patternsGameObject[answerIndex].GetComponent<UnityEngine.UI.Image>().sprite = this.cover;
        System.Collections.Generic.List<UnityEngine.Sprite> units = new System.Collections.Generic.List<UnityEngine.Sprite>();
        units.AddRange(this.choises[this.index].units);
        UnityEngine.Sprite answer = this.patterns[this.index].units[answerIndex];
        this.patterns[this.index].units[answerIndex] = this.cover;
        units.Remove(answer);
        while (this.choisesGameObject.Length > num) {
            int index = UnityEngine.Random.Range(0,(4-num));
            this.choisesGameObject[num].SetActive(true);
            this.choisesGameObject[num].GetComponent<UnityEngine.UI.Image>().sprite = units[index];
            this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(this.Wrap(answer, units[index], answerIndex));
            units.RemoveAt(index);
            num++;
        }
        num = UnityEngine.Random.Range(0,4);
        this.choisesGameObject[num].GetComponent<UnityEngine.UI.Image>().sprite = answer;
        this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        this.choisesGameObject[num].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(this.Wrap(answer, answer, answerIndex));
    }
    public UnityEngine.Events.UnityAction Wrap(UnityEngine.Sprite pAnswer, UnityEngine.Sprite pSprite, System.Int16 pIndex)
    {
        return () => {
                if (pAnswer == pSprite) UnityEngine.Debug.Log("win");
                this.patterns[this.index].units[pIndex] = pSprite;
                this.patternsGameObject[pIndex].GetComponent<UnityEngine.UI.Image>().sprite = pSprite;
                System.Collections.Generic.List<UnityEngine.Vector2> vectors2 = new System.Collections.Generic.List<UnityEngine.Vector2>();
                foreach (UnityEngine.Sprite sprite in this.patterns[this.index].units) {
                    vectors2.Add(this.dictionary[sprite]);
                }
                foreach (UnityEngine.GameObject choiseGameObject in this.choisesGameObject) choiseGameObject.SetActive(false);
                this.StartCoroutine(LevelPattern.MovePlayer(vectors2, this));
            };
    }
    public static System.Collections.IEnumerator MovePlayer(System.Collections.Generic.List<UnityEngine.Vector2> pVector2, LevelPattern pLevelPattern)
    {
        foreach (UnityEngine.Vector2 vector2 in pVector2) {
            System.Int16 num = 0;
            while (num < 20) {
                pLevelPattern.screenplay.player.transform.Translate(vector2 * 0.3f);
                num++;
                yield return new UnityEngine.WaitForSeconds(0.04f);
            }
            while (num > 0) {
                pLevelPattern.screenplay.player.transform.Translate(-vector2 * 0.3f);
                num--;
                yield return new UnityEngine.WaitForSeconds(0.04f); 
            }
        }
        
        pLevelPattern.index++;
        pLevelPattern.ChangeSprites();
    }
}
