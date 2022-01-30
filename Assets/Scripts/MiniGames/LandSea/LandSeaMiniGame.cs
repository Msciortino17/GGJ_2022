using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandSeaMiniGame : MiniGameManager
{
    public RoundTimer RoundTimerRef;
    public Text VictoryText;
    public bool GameOver;
    public int Winner;
    private float victoryTimer;

    public Text InstructionsText;
    public GameObject TitleCard;
    public GameObject CanvasRef;

    void Start()
    {
        TitleCard.SetActive(true);
        CanvasRef.SetActive(false);
        GameManager.Instance.HUDMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (RoundTimerRef.RoundOver && !GameOver)
        {
            if (Physics.Raycast(new Vector3(0, 5f, 0f), Vector3.down, out RaycastHit Hit, 10f))
            {
                GameObject MyGameObject = Hit.collider.gameObject;
                PlayerMovementLand PML = MyGameObject.GetComponent<PlayerMovementLand>();
                PlayerMovementSea PMS = MyGameObject.GetComponent<PlayerMovementSea>();

                if (PML != null)
                {
                    GameOver = true;
                    Winner = 1;
                    VictoryText.text = "The Land has conquered the Sea!";
                }
                else if (PMS != null)
                {
                    GameOver = true;
                    Winner = 2;
                    VictoryText.text = "The Sea has conquered the Land!";
                }
            }
        }
        if (GameOver)
        {
            victoryTimer += Time.deltaTime;

            if (victoryTimer >= 3f)
            {
                RoundOver(Winner);
            }
        }
        if (TitleCard.gameObject.activeInHierarchy && (Input.anyKey || GameManager.Instance.RoundTimer > 3f))
        {
            TitleCard.SetActive(false);
            CanvasRef.SetActive(true);
            GameManager.Instance.HUDMenu.gameObject.SetActive(true);
            GameManager.Instance.PlayBeepBoop();
        }

        if (GameManager.Instance.RoundTimer > 7f)
        {
            InstructionsText.gameObject.SetActive(false);
        }
    }
}