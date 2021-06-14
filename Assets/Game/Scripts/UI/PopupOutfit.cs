using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopupOutfit : UICanvas
{
    public static int m_SelectedCharacter;

    public TextMeshProUGUI txt_TotalGold;
    public TextMeshProUGUI txt_CharName;
    public Image img_Char;
    public Button btn_Equip;
    public Button btn_Equipped;
    public Button btn_BuyByGold;
    public Button btn_BuyByAds;
    public Button btn_AdsGold;

    public TextMeshProUGUI txt_BuyByGold;
    public TextMeshProUGUI txt_AdsNumber;

    public GameObject g_Warning;

    // [Header("Rarity buttons")]
    // public Button btn_Rare;
    // public Button btn_Epic;
    // public Button btn_Legend;

    // public UICharacterOutfit m_UICharacterOutfit;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Equip, OnEquip);
        GUIManager.Instance.AddClickEvent(btn_BuyByGold, OnBuyByGold);
        GUIManager.Instance.AddClickEvent(btn_BuyByAds, OnBuyByAds);
        // GUIManager.Instance.AddClickEvent(btn_BuyByAds, OnByBuyAdsLogic);
        GUIManager.Instance.AddClickEvent(btn_AdsGold, OnAdsGold);


        // SetChar(ProfileManager.GetSelectedCharacter());
    }

    // public void OpenTut()
    // {
    //     PopupCaller.OpenTutorialPopup(false);
    // }

    public override void OnEnable()
    {
        base.OnEnable();
        // g_Warning.SetActive(false);
        m_SelectedCharacter = ProfileManager.GetSelectedCharacter();
        txt_TotalGold.text = ProfileManager.GetGold();

        SetChar(m_SelectedCharacter);
        // MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(m_SelectedCharacter);

        // CheckTutorial();

        Event_LOAD_CHAR_OUTFIT(m_SelectedCharacter);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnGoldEffect();
        }
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager1<int>.AddListener(GameEvent.LOAD_CHAR_OUTFIT, Event_LOAD_CHAR_OUTFIT);
        EventManager.AddListener(GameEvent.ADS_CHARACTER_LOGIC, OnByBuyAdsLogic);
        EventManager.AddListener(GameEvent.ADS_GOLD_1_LOGIC, OnAdsGoldLogic);
        EventManager.AddListener(GameEvent.UPDATE_GOLD, Event_UPDATE_GOLD);
        EventManager.AddListener(GameEvent.ADS_GOLD_1_ANIM, OnAdsGoldAnim);
    }

    public override void StopListenToEvents()
    {
        base.StartListenToEvents();
        EventManager1<int>.RemoveListener(GameEvent.LOAD_CHAR_OUTFIT, Event_LOAD_CHAR_OUTFIT);
        EventManager.RemoveListener(GameEvent.ADS_CHARACTER_LOGIC, OnByBuyAdsLogic);
        EventManager.RemoveListener(GameEvent.ADS_GOLD_1_LOGIC, OnAdsGoldLogic);
        EventManager.RemoveListener(GameEvent.UPDATE_GOLD, Event_UPDATE_GOLD);
        EventManager.RemoveListener(GameEvent.ADS_GOLD_1_ANIM, OnAdsGoldAnim);
    }

    public void Event_LOAD_CHAR_OUTFIT(int _id)
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);
        txt_CharName.text = config.m_Name;
        m_SelectedCharacter = _id;
        img_Char.sprite = SpriteManager.Instance.m_CharCards[_id - 1];

        SetClaimBtnLogic(_id);
    }

    public void Event_UPDATE_GOLD()
    {
        txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void SetChar(int _id)
    {
        // g_Warning.SetActive(false);

        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);

        txt_CharName.text = config.m_Name;
        m_SelectedCharacter = _id;

        img_Char.sprite = SpriteManager.Instance.m_CharCards[_id - 1];

        SetClaimBtnLogic(_id);

        // if (checkowned)
        // {
        //     btn_Equipped.gameObject.SetActive(checkowned);
        //     btn_BuyByAds.gameObject.SetActive(!checkowned);
        //     btn_BuyByGold.gameObject.SetActive(!checkowned);
        //     btn_Equip.gameObject.SetActive(!checkowned);
        // }
        // else
        // {

        // }
    }

    public void OnEquip()
    {
        ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
        ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
        EventManager1<int>.CallEvent(GameEvent.LOAD_CHAR_OUTFIT, m_SelectedCharacter);
        EventManager.CallEvent(GameEvent.UPDATE_OUTFIT);

        GameObject go = PrefabManager.Instance.SpawnChar(m_SelectedCharacter - 1);
        Character charr = InGameObjectsManager.Instance.m_Char;
        CharacterType type = charr.m_CharacterType;
        InGameObjectsManager.Instance.m_Char = go.GetComponent<Character>();
        CameraController.Instance.m_CMFreeLook.Follow = InGameObjectsManager.Instance.m_Char.tf_Owner;
        // EventManager.CallEvent(GameEvent.LOAD_CHAR);
        // // StartCoroutine(RemoveChar(charr));
        EventManager1<CharacterType>.CallEvent(GameEvent.CHAR_DESTROY, type);
    }

    public void OnBuyByGold() //Remember to Update UICharacterCard when buy succeed
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (ProfileManager.IsEnoughGold(config.m_Price))
        // if (ProfileManager.MyProfile.IsEnoughGold(config.m_Price))
        {
            // if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
            // {
            //     TutorialManager.Instance.PassTutorial(TutorialType.SHOP_BUYBYGOLD);
            //     PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_UnClickBuyByGoldUI(GetComponent<RectTransform>());
            //     // PopupCaller.GetTutorialPopup().OnClose();
            // }

            // ProfileManager.ConsumeGold(config.m_Price);
            // ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            // ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            // SetOwned(m_SelectedCharacter);
            // EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
            // EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

            // txt_TotalGold.text = ProfileManager.GetGold();
            // GameManager.Instance.GetPanelInGame().txt_TotalGold.text = ProfileManager.GetGold();

            // SoundManager.Instance.PlaySoundBuySuccess();

            // AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);

            ProfileManager.ConsumeGold(config.m_Price);
            txt_TotalGold.text = ProfileManager.GetGold();
            PlaySceneManager.Instance.txt_TotalGold.text = ProfileManager.GetGold();
            OnEquip();
            Helper.DebugLog("Gold: " + ProfileManager.GetGold());
        }
        else
        {
            // StartCoroutine(IEWarning());
            Helper.DebugLog("Not enough golddddddddd");
            Helper.DebugLog("Gold: " + ProfileManager.GetGold());
        }
    }

    public void OnBuyByAds() //Remember to Update UICharacterCard when buy succeed
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.CHARACTER);
        // OnByBuyAdsLogic();
    }

    public void OnByBuyAdsLogic()
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (data == null)
        {
            ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            data = new CharacterProfileData();
            data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
            // data.ClaimByAds(1);
        }
        // else
        // {
        //     data.ClaimByAds(1);
        // }

        data.ClaimByAds(1);

        if (ProfileManager.IsOwned(m_SelectedCharacter))
        {
            // ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            // EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

            OnEquip();

            // SoundManager.Instance.PlaySoundBuySuccess();

            // AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);
        }

        EventManager.CallEvent(GameEvent.UPDATE_OUTFIT);
        Event_LOAD_CHAR_OUTFIT(m_SelectedCharacter);

        // SetOwned(m_SelectedCharacter);
        // EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
    }

    // public bool CheckTutorial()
    // {
    //     if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // IEnumerator IEWarning()
    // {
    //     g_Warning.SetActive(true);
    //     yield return Yielders.Get(2f);
    //     g_Warning.SetActive(false);
    // }

    // public void OnBuyByAds() //Remember to Update UICharacterCard when buy succeed
    // {
    //     AdsManager.Instance.WatchRewardVideo(RewardType.CHARACTER);
    //     // OnByBuyAdsLogic();
    // }

    // public void OnByBuyAdsLogic()
    // {
    //     CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
    //     CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

    //     if (data == null)
    //     {
    //         ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
    //         data = new CharacterProfileData();
    //         data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
    //         data.ClaimByAds(1);
    //     }
    //     else
    //     {
    //         data.ClaimByAds(1);
    //     }

    //     if (ProfileManager.IsOwned(m_SelectedCharacter))
    //     {
    //         ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
    //         EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

    //         SoundManager.Instance.PlaySoundBuySuccess();

    //         AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);
    //     }

    //     SetOwned(m_SelectedCharacter);
    //     EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
    // }

    public void OnAdsGold()
    {
        // InGameObjectsManager.Instance.RemoveEffectFlyer();
        AdsManager.Instance.WatchRewardVideo(RewardType.GOLD_1);
        // OnAdsGoldLogic();
        // OnAdsGoldAnim();
    }

    public void OnAdsGoldLogic()
    {
        ProfileManager.AddGold(250);
        EventManager.CallEvent(GameEvent.UPDATE_GOLD);
        // AnalysticsManager.LogGetShopGold1();
    }

    public void OnAdsGoldAnim()
    {
        SpawnGoldEffect();
    }

    // public override void OnClose()
    // {
    //     base.OnClose();
    //     MiniCharacterStudio.Instance.DestroyChar();
    //     InGameObjectsManager.Instance.RemoveEffectFlyer();
    // }

    public void SetClaimBtnLogic(int _id)
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        bool _checkowned = ProfileManager.IsOwned(_id);
        bool _adsCheck = config.CheckAds();

        bool equipped = (_id == ProfileManager.GetSelectedCharacter());

        if (equipped)
        {
            btn_Equipped.gameObject.SetActive(equipped);
            btn_BuyByAds.gameObject.SetActive(!equipped);
            btn_BuyByGold.gameObject.SetActive(!equipped);
            btn_Equip.gameObject.SetActive(!equipped);
            return;
        }

        if (_checkowned)
        {
            btn_Equipped.gameObject.SetActive(!_checkowned);
            btn_BuyByAds.gameObject.SetActive(!_checkowned);
            btn_BuyByGold.gameObject.SetActive(!_checkowned);
            btn_Equip.gameObject.SetActive(_checkowned);
            // return;
        }
        else
        {
            if (config.GetRatity() == (int)OutfitRarity.LEGEND)
            {
                btn_Equipped.gameObject.SetActive(false);
                // btn_BuyByAds.gameObject.SetActive(false);
                // btn_BuyByGold.gameObject.SetActive(false);
                btn_Equip.gameObject.SetActive(false);
                return;
            }

            if (_adsCheck)
            {
                btn_Equipped.gameObject.SetActive(!_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(!_adsCheck);

                if (data != null)
                {
                    txt_AdsNumber.text = data.m_AdsNumber.ToString() + "/" + config.m_AdsNumber.ToString();
                }
                else
                {
                    txt_AdsNumber.text = "0" + "/" + config.m_AdsNumber.ToString();
                }
            }
            else
            {
                btn_Equipped.gameObject.SetActive(_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(_adsCheck);

                txt_BuyByGold.text = config.m_Price.ToString();
            }
        }
    }

    public void SpawnGoldEffect()
    {
        InGameObjectsManager.Instance.DespawnGoldEffectPool();

        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnGoldEffect(ConfigKeys.m_GoldEffect1, btn_AdsGold.transform.position);
            g_EffectGold.transform.SetParent(this.transform);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);
            g_EffectGold.transform.position = btn_AdsGold.transform.position;

            InGameObjectsManager.Instance.g_GoldEffects.Add(g_EffectGold);

            g_EffectGold.transform.DOMove(txt_TotalGold.gameObject.transform.position, 0.7f).SetDelay(0.1f + i * 0.1f).OnComplete(
                () =>
                {
                    PrefabManager.Instance.DespawnPool(g_EffectGold);
                }
            );
        }
    }

    public override void OnClose()
    {
        InGameObjectsManager.Instance.DespawnGoldEffectPool();
        base.OnClose();
    }
}
